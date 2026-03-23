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
using ApiAll.Contex;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAll.Services
{
    public class PostgresNotificationService1 : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PostgresNotificationService1> _logger;
        private readonly string _connString;

        private readonly ConcurrentQueue<CheckinoutModel1s> _queue = new ConcurrentQueue<CheckinoutModel1s>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        public PostgresNotificationService1(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ILogger<PostgresNotificationService1> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
            _connString = _configuration.GetConnectionString("DefaultConnection");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🟢 Postgres listener ishga tushdi.");

            _ = Task.Run(() => ProcessQueueAsync(stoppingToken), stoppingToken);

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
                            var data = JsonSerializer.Deserialize<CheckinoutModel1s>(e.Payload);
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
                        _logger.LogInformation("📡 LISTEN buyruq yuborildi.");
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
                        if (data.checktype == "C")
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                            var startTime = DateTime.Now.AddHours(-24);

                            var record = await db.SkudMyCheckinout
                                .Where(c => c.userid == data.userid &&
                                            c.sana >= startTime.Date &&
                                            c.code != data.code)
                                .ToListAsync(stoppingToken);

                            var lastRecord = record
                                .Where(c => c.sana.Add(c.checktime) >= startTime)
                                .OrderByDescending(c => c.sana)
                                .ThenByDescending(c => c.checktime)
                                .FirstOrDefault();

                            if (lastRecord != null)
                            {
                                if (lastRecord.checktype == "K")
                                {
                                    var entryTime = lastRecord.sana + lastRecord.checktime;
                                    var exitTime = data.sana + data.checktime;
                                    var duration = exitTime - entryTime;
                                    var kDate = lastRecord.sana + lastRecord.checktime;
                                    var workTime = duration;
                                    
                                    // using var scope2 = _serviceProvider.CreateScope();
                                    // var db2 = scope2.ServiceProvider.GetRequiredService<ApplicationContext>();

                                    // K_date = kirgan vaqtning sanasi
                                    var existingRecord = await db.TegirmonUserIshlaganVaqt
                                        .FirstOrDefaultAsync(x => x.userid == data.userid && x.K_date.Date == kDate.Date);

                                    if (existingRecord != null)
                                    {
                                        // Avval yozilgan bo‘lsa, ish vaqtini qo‘shamiz
                                        existingRecord.work_time += workTime;
                                        existingRecord.note = "Yangilandi"; // ixtiyoriy
                                    }
                                    else
                                    {
                                        // Yangi yozuv yaratamiz
                                        var newWorkTimeRecord = new TegirmonUserIshlaganVaqt
                                        {
                                            userid = data.userid,
                                            work_time = workTime,
                                            K_date = kDate,
                                            created_date = exitTime,
                                            note = "Yangi qo‘shildi"
                                        };
                                        db.TegirmonUserIshlaganVaqt.Add(newWorkTimeRecord);
                                    }

                                    var user = await db.SkudMyUserinfo
                                    .Include(u => u.skudOylik) // ForeignKey orqali oylikni chaqirish
                                    .FirstOrDefaultAsync(u => u.userid == data.userid);
                                    if (user?.skudOylik == null || user.skudOylik.value <= 0)
                                    {
                                        _logger.LogWarning("❌ Xodim (ID: {UserId}) uchun oylik ma’lumoti topilmadi yoki nolga teng.", data.userid);
                                    }
                                    else{
                                        if(user.without_gr_id == 1){
                                            var hourlyRate = user.skudOylik.value;
                                            var workedHours = duration.TotalHours;
                                            var workedSum = hourlyRate * workedHours;

                                            var newSalary = new TegirmonUserIshlaganPuli
                                            {
                                                userid = data.userid,
                                                salary_id = user.res_badgenumber, // agar shu id bilan ulangan bo‘lsa
                                                work_time = workTime,
                                                sum = workedSum,
                                                num = 1,
                                                K_date = kDate,
                                                created_date = exitTime,
                                                note = "Yangi qo‘shildi"
                                            };
                                            db.TegirmonUserIshlaganPuli.Add(newSalary);
                                            _logger.LogInformation("💰 Xodim (ID: {UserId}) uchun {Sum} so‘m ishlagan pul saqlandi.", user.ism, workedSum);

                                            var userOylik = await db.TegirmonUserOylik
                                                .FirstOrDefaultAsync(x => x.userid == data.userid);

                                            if (userOylik != null)
                                            {
                                                // Avval yozilgan bo‘lsa, ishlagan pulini
                                                userOylik.sum += workedSum;
                                            }
                                            else
                                            {
                                                // Yangi yozuv yaratamiz
                                                var newUserSumma = new TegirmonUserOylik
                                                {
                                                    userid = data.userid,
                                                    sum = workedSum,
                                                };
                                                db.TegirmonUserOylik.Add(newUserSumma);
                                            }
                                        }
                                    }

                                    await db.SaveChangesAsync();
                                    _logger.LogInformation("📝 Ishlagan vaqt bazaga saqlandi: {0}", workTime);
                                    _logger.LogInformation("⏱️ Xodim (ID: {UserId}) ishlagan vaqt: {Hours:F2} soat.", data.userid, duration.TotalHours);
                                    _logger.LogInformation("⏱️ Xodim (ID: {UserId}) ishlagan vaqt: {Duration}.", data.userid, duration.ToString(@"hh\:mm\:ss"));
                                }
                                else if (lastRecord.checktype == "C")
                                {
                                    _logger.LogInformation("⚠️ Xodim (ID: {UserId}) allaqachon chiqib ketgan. Oxirgi yozuv ham 'C' turida.", data.userid);
                                }
                            }
                            else
                            {
                                _logger.LogInformation("❌ 24 soatda hech qanday 'K' (kirish) topilmadi.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Navbatdagi elementni qayta ishlashda xatolik: {Message}", ex.Message);
                    }
                }
            }
        }
    }

    public class CheckinoutModel1s
    {
        public long code { get; set; }
        public long userid { get; set; }
        public DateTime sana { get; set; }
        public TimeSpan checktime { get; set; }
        public string checktype { get; set; }
        public string door_name { get; set; }
        public bool status { get; set; }
    }
}