using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using ApiAll.Model.tegirmon;
using ApiAll.Model.skud;
using ApiAll.Contex;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAll.Services
{
    public class PostgresNotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PostgresNotificationService> _logger;
        private readonly string _connString;

        private readonly ConcurrentQueue<CheckinoutModel> _queue = new ConcurrentQueue<CheckinoutModel>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        private readonly ConcurrentDictionary<long, BufferItem> _buffer = new ConcurrentDictionary<long, BufferItem>();

        public PostgresNotificationService(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ILogger<PostgresNotificationService> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
            _connString = _configuration.GetConnectionString("DefaultConnection");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(() => ProcessQueueAsync(stoppingToken), stoppingToken);
            _ = Task.Run(() => ProcessBufferAsync(stoppingToken), stoppingToken);

            _logger.LogInformation("🟢 Postgres listener ishga tushdi.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await using var conn = new NpgsqlConnection(_connString);
                    await conn.OpenAsync(stoppingToken);

                    conn.Notification += (sender, e) =>
                    {
                        try
                        {
                            var data = JsonSerializer.Deserialize<CheckinoutModel>(e.Payload);
                            _queue.Enqueue(data);
                            _signal.Release();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "❌ NOTIFY parsing xatosi: {Message}", ex.Message);
                        }
                    };

                    await using (var cmd = new NpgsqlCommand("LISTEN new_checkinout;", conn))
                    {
                        await cmd.ExecuteNonQueryAsync(stoppingToken);
                        _logger.LogInformation("📱 LISTEN buyruq yuborildi.");
                    }

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            await conn.WaitAsync(stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "⚠️ WaitAsync xatosi: {Message}", ex.Message);
                            break;
                        }
                    }

                    _logger.LogWarning("🔄 Postgres ulanishi tugadi, qayta ulaniladi.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Tinglash xatosi: {Message}", ex.Message);
                }

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }

            _logger.LogInformation("🛑 Listener xizmat to‘xtatildi.");
        }

        private async Task ProcessQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _signal.WaitAsync(stoppingToken);

                if (_queue.TryDequeue(out var data))
                {
                    try
                    {
                        if (data.checktype == "C" && data.door_name != "user")
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                            var dataDateTime = data.sana.Add(data.checktime);
                            var startTime = dataDateTime.AddHours(-24);

                            var query = await db.SkudMyCheckinout
                                .Where(c => c.userid == data.userid && c.sana >= startTime.Date && c.code != data.code)
                                .ToListAsync(stoppingToken); // SQLdan barcha kerakli ma'lumotlarni olamiz

                            var lastRecord = query
                                .Where(c => c.sana.Add(c.checktime) >= startTime &&
                                            c.sana.Add(c.checktime) <= dataDateTime)
                                .OrderByDescending(c => c.sana)
                                .ThenByDescending(c => c.checktime)
                                .FirstOrDefault();

                            if (lastRecord == null || lastRecord.checktype == "C")
                            {
                                _buffer[data.userid] = new BufferItem { Data = data, InsertedAt = DateTime.Now };
                                _logger.LogInformation("⏳ Chiqish buferga qo‘shildi: {0}", data.userid);
                            }
                            else if (lastRecord.checktype == "K")
                            {
                                await HisoblaAsync(data, lastRecord, db);
                                _logger.LogInformation("⏳ Hisoblashga utib ketayabdi");
                            }
                        }
                        else if (data.checktype == "K")
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Navbatdagi elementni qayta ishlashda xatolik: {Message}", ex.Message);
                    }
                }
            }
        }

        private async Task ProcessBufferAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    foreach (var key in _buffer.Keys.ToList())
                    {
                        var item = _buffer[key];

                        if (DateTime.Now - item.InsertedAt >= TimeSpan.FromMinutes(8))
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                            var dataDateTime = item.Data.sana.Add(item.Data.checktime);
                            var startTime = dataDateTime.AddHours(-24);
                            
                            var query = await db.SkudMyCheckinout
                                .Where(c => c.userid == item.Data.userid && c.sana >= startTime.Date && c.code != item.Data.code)
                                .ToListAsync(CancellationToken.None); // SQLdan barcha kerakli ma'lumotlarni olamiz

                            var lastRecord = query
                                .Where(c => c.sana.Add(c.checktime) >= startTime &&
                                            c.sana.Add(c.checktime) <= dataDateTime)
                                .OrderByDescending(c => c.sana)
                                .ThenByDescending(c => c.checktime)
                                .FirstOrDefault();

                            if (lastRecord != null && lastRecord.checktype == "K")
                            {
                                _logger.LogInformation("⏳ Buferdan qayta hisoblashga yuborildi", lastRecord.userid);
                                await HisoblaAsync(item.Data, lastRecord, db);
                            }

                            _buffer.TryRemove(key, out _);
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(30), token);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Buferni qayta ishlashda xatolik: {Message}", ex.Message);
                }
            }
        }

        

        private async Task HisoblaAsync(CheckinoutModel chiqish, SkudMyCheckinout kirish, ApplicationContext db)
        {
            var entryTime = kirish.sana + kirish.checktime;
            var exitTime = chiqish.sana + chiqish.checktime;
            var duration = exitTime - entryTime;
            var workTime = duration;
            var kDate = kirish.sana + kirish.checktime;
            
            var existingRecord = await db.TegirmonUserIshlaganVaqt
                .FirstOrDefaultAsync(x => x.userid == chiqish.userid && x.K_date.Date == kDate.Date);

            if (existingRecord != null)
            {
                existingRecord.work_time += workTime;
                existingRecord.note = "Yangilandi";
            }
            else
            {
                db.TegirmonUserIshlaganVaqt.Add(new TegirmonUserIshlaganVaqt
                {
                    userid = chiqish.userid,
                    work_time = workTime,
                    K_date = kDate,
                    created_date = exitTime,
                    note = "Yangi qo‘shildi"
                });
            }

            var user = await db.SkudMyUserinfo
                .Include(u => u.skudOylik)
                .FirstOrDefaultAsync(u => u.userid == chiqish.userid);

            if (user?.skudOylik != null && user.skudOylik.value > 0 && user.without_gr_id == 1)
            {
                var hourlyRate = user.skudOylik.value;
                var workedSum = hourlyRate * duration.TotalHours;

                var roundedWorkedSum = RoundToNearest500(workedSum);

                db.TegirmonUserIshlaganPuli.Add(new TegirmonUserIshlaganPuli
                {
                    userid = chiqish.userid,
                    salary_id = user.res_badgenumber,
                    work_time = workTime,
                    sum = roundedWorkedSum,
                    num = 1,
                    K_date = kDate,
                    created_date = exitTime,
                    note = "Face ishlagan pulini hisobladi"
                });
                _logger.LogInformation("💰 Xodim (ID: {UserId}) uchun {Sum} so‘m ishlagan pul saqlandi.", user.ism, workedSum);
                var userOylik = await db.TegirmonUserOylik.FirstOrDefaultAsync(x => x.userid == chiqish.userid);

                if (userOylik != null)
                {
                    userOylik.sum += roundedWorkedSum;
                }
                else
                {
                    db.TegirmonUserOylik.Add(new TegirmonUserOylik
                    {
                        userid = chiqish.userid,
                        sum = roundedWorkedSum
                    });
                }
            }
            else if (user.without_gr_id > 1)
            {
                var hourlyRate = 0;
                var workedSum = hourlyRate * duration.TotalHours;
                var roundedWorkedSum = RoundToNearest500(workedSum);

                db.TegirmonUserIshlaganPuli.Add(new TegirmonUserIshlaganPuli
                {
                    userid = chiqish.userid,
                    salary_id = user.res_badgenumber,
                    work_time = workTime,
                    sum = roundedWorkedSum,
                    num = 1,
                    K_date = kDate,
                    created_date = exitTime,
                    note = "Face ishlagan pulini hisobladi"
                });
                _logger.LogInformation("💰 Xodim (ID: {UserId}) uchun {Sum} so‘m ishlagan pul saqlandi.", user.ism, workedSum);
                var userOylik = await db.TegirmonUserOylik.FirstOrDefaultAsync(x => x.userid == chiqish.userid);

                if (userOylik != null)
                {
                    userOylik.sum += roundedWorkedSum;
                }
                else
                {
                    db.TegirmonUserOylik.Add(new TegirmonUserOylik
                    {
                        userid = chiqish.userid,
                        sum = roundedWorkedSum
                    });
                }
            }
            

            await db.SaveChangesAsync();
        }
        private double RoundToNearest500(double value)
            {
                var remainder = value % 1000;

                if (remainder >= 500)
                {
                    return value - remainder + 1000;
                }
                else
                {
                    return value - remainder;
                }
            }
    }

    

    public class BufferItem
    {
        public CheckinoutModel Data { get; set; }
        public DateTime InsertedAt { get; set; }
    }

    public class CheckinoutModel
    {
        public long code { get; set; }
        public long userid { get; set; }
        public DateTime sana { get; set; }
        public TimeSpan checktime { get; set; }
        public string checktype { get; set; }
        public string door_name { get; set; }
        public bool? status { get; set; }
    }
}
