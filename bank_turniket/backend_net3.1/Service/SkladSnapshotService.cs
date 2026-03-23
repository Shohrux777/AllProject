using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiAll.Model.tegirmon;
using ApiAll.Contex;


public class SkladSnapshotService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;

    public SkladSnapshotService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Har 2 soatda ishlaydi (boshlanishda ham darhol bir marta ishlaydi)
        _timer = new Timer(SaveSnapshot, null, TimeSpan.Zero, TimeSpan.FromHours(4));
        return Task.CompletedTask;
    }

    private async void SaveSnapshot(object state)
{
    try
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var now = DateTime.Now;
            var today = now.Date;

            // Shu kunga oid eski snapshotlarni o‘chirish
            var oldSnapshots = context.Set<TegirmonSkladDailyBalance>()
                .Where(x => x.Date.Date == today);

            context.Set<TegirmonSkladDailyBalance>().RemoveRange(oldSnapshots);

            // Barcha skladdagi tovarlarni olish
            var skladTovarList = await context.Set<TegirmonSkladTovar>()
                .Include(x => x.Sklad)
                .Include(x => x.Product)
                .ToListAsync();

            foreach (var item in skladTovarList)
            {
                var snapshot = new TegirmonSkladDailyBalance
                {
                    Date = now,
                    TegirmonSkladId = item.TegirmonSkladId,
                    TegirmonProductId = item.TegirmonProductId,
                    qty = item.qty
                };

                context.Set<TegirmonSkladDailyBalance>().Add(snapshot);
            }

            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Sklad snapshot yangilandi: {now}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Snapshot yozishda xatolik: {ex.Message}");
    }
}

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}