using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiAll.Contex;
using ApiAll.Model.skud;

public class AvtoRasxodService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AvtoRasxodService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            // Agar soat 12:00 bo'lsa
            if (now.Hour == 12 && now.Minute == 0)
            {
                await ProcessAvtoRasxod();
                // 1 daqiqa kutamiz, shunda 12:00 da faqat bir marta ishlaydi
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            // Har 30 soniyada vaqtni tekshiradi
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task ProcessAvtoRasxod()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var todayDay = DateTime.Now.Day;

            Console.WriteLine($"[AvtoRasxodService] Ishga tushdi - {DateTime.Now}");

            // Bugungi kun bilan mos avto rasxodlarni olamiz
            var avtoList = db.SkudUserAvtoRasxod
                .Where(x => x.day == todayDay)
                .ToList();

            foreach (var item in avtoList)
            {
                var rasxod = new SkudUserRasxod
                {
                    userid = item.userid,
                    user_name = item.user_name,
                    count = item.sum,
                    note = item.note,
                    num_1 = 1,
                    reg_date = DateTime.Now
                };

                db.SkudUserRasxod.Add(rasxod);
            }

            await db.SaveChangesAsync();
        }
    }
}