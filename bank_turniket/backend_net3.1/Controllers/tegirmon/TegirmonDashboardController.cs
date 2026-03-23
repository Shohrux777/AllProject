using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonDashboardController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonDashboardController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asosiy ko'rsatkichlar uchun placeholder.
        /// Keyinchalik real hisob-kitoblar bilan to'ldiriladi.
        /// </summary>
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? begin_date, [FromQuery] DateTime? end_date)
        {
            var start = begin_date?.Date ?? DateTime.UtcNow.Date;
            var end = (end_date?.Date ?? DateTime.UtcNow.Date).AddDays(1);

            var result = new
            {
                start_date = start,
                end_date = end,
                total_sales = 0d,
                total_income = 0d,
                total_expense = 0d,
                total_clients = 0,
                updated_at = DateTime.UtcNow
            };

            return Ok(result);
        }

        /// <summary>
        /// Karta/statistikalar uchun placeholder ro'yxati.
        /// </summary>
        [HttpGet("cards")]
        public async Task<IActionResult> GetCards([FromQuery] DateTime? begin_date, [FromQuery] DateTime? end_date)
        {
            var cards = new List<object>();
            return Ok(cards);
        }

        /// <summary>
        /// Grafik (line/bar) uchun placeholder ma'lumot.
        /// </summary>
        [HttpGet("chart")]
        public async Task<IActionResult> GetChart([FromQuery] DateTime? begin_date, [FromQuery] DateTime? end_date, [FromQuery] long? sklad_id, [FromQuery] long? kassa_id)
        {
            var chart = new
            {
                labels = new List<string>(),
                series = new List<double>()
            };
            return Ok(chart);
        }

        /// <summary>
        /// Jadval uchun placeholder ma'lumot.
        /// </summary>
        [HttpGet("table")]
        public async Task<IActionResult> GetTable([FromQuery] DateTime? begin_date, [FromQuery] DateTime? end_date, [FromQuery] int page = 0, [FromQuery] int size = 20)
        {
            var table = new
            {
                items_list = new List<object>(),
                items_count = 0,
                current_page = page,
                current_item_count = 0
            };
            return Ok(table);
        }

        /// <summary>
        /// Bugun qancha bug'doy olib kelindi, qanchasi mahsulotga almashtirildi, 
        /// qanchasi pulga sotib olindi, qanchasi zaxiradga quyildi statistikasi.
        /// </summary>
        [HttpGet("getTodayBugdoyStatistics")]
        public async Task<IActionResult> GetTodayBugdoyStatistics([FromQuery] DateTime? begin_date, [FromQuery] DateTime? end_date)
        {
            try
            {
                // Agar sana berilmagan bo'lsa, bugungi kunni olamiz
                var startDate = begin_date?.Date ?? DateTime.UtcNow.Date;
                var endDateValue = (end_date?.Date ?? DateTime.UtcNow.Date).AddDays(1);
            // INVOICE TURLARI
            string INVOICE_BUGDOY_SOTISH_PULGA = "INVOICE_BUGDOY_SOTISH_PULGA";
            string INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH";
            string INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH";

            // 1. Bugun qancha bug'doy olib kelindi (TegirmonTortilganBugdoyRoyxatiGroupDetail orqali TegirmonTortilganBugdoyRoyxati dan qty yig'indisi)
            double totalBugdoyReceived = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p => p.bugdoyRoyxatiGroup)
                .Include(p => p.royxati)
                .Where(p => p.active_status == true
                    && p.bugdoyRoyxatiGroup != null
                    && p.bugdoyRoyxatiGroup.active_status == true
                    && p.bugdoyRoyxatiGroup.date_time >= startDate
                    && p.bugdoyRoyxatiGroup.date_time < endDateValue
                    && p.royxati != null)
                .SumAsync(p => p.royxati.qty);

            // 2. Qanchasi mahsulotga almashtirildi (TegirmonTortilganBugdoyRoyxatiGroupDetail orqali invoice -> item_list -> qty / real_sum yig'indisi)
            var groupDetailsWithInvoices = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p => p.bugdoyRoyxatiGroup)
                .Include(p => p.invoice)
                    .ThenInclude(inv => inv.item_list)
                .Where(p => p.active_status == true
                    && p.bugdoyRoyxatiGroup != null
                    && p.bugdoyRoyxatiGroup.active_status == true
                    && p.bugdoyRoyxatiGroup.date_time >= startDate
                    && p.bugdoyRoyxatiGroup.date_time < endDateValue
                    && p.invoice != null
                    && p.invoice.active_status == true
                    && p.invoice.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH
                    && p.invoice.item_list != null)
                .ToListAsync();

            double totalChangedToProduct = 0;
            foreach (var detail in groupDetailsWithInvoices)
            {
                if (detail.invoice.item_list != null)
                {
                    foreach (var item in detail.invoice.item_list)
                    {
                        if (item.real_sum > 0)
                        {
                            double bugdoyQty = item.qty;
                            // Agar enough_qty > 0 bo'lsa, qty dan ayirish kerak
                            if (item.enough_qty != null && item.enough_qty > 0)
                            {
                                bugdoyQty = item.qty - item.enough_qty.Value;
                            }
                            // real_sum ga bo'lib bug'doy miqdorini olamiz
                            totalChangedToProduct += bugdoyQty / item.real_sum;
                        }
                    }
                }
            }

            // 3. Qanchasi pulga sotib olindi (TegirmonTortilganBugdoyRoyxatiGroupDetail orqali TegirmonTortilganBugdoyRoyxati ichidagi invoice larni note maydoni yig'indisi)
            var groupDetailsSoldForMoney = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p => p.bugdoyRoyxatiGroup)
                .Include(p => p.royxati)
                .Include(p => p.invoice)
                .Where(p => p.active_status == true
                    && p.bugdoyRoyxatiGroup != null
                    && p.bugdoyRoyxatiGroup.active_status == true
                    && p.bugdoyRoyxatiGroup.date_time >= startDate
                    && p.bugdoyRoyxatiGroup.date_time < endDateValue
                    && p.invoice != null
                    && p.invoice.active_status == true
                    && p.invoice.note != null
                    && p.invoice.note != "")
                .ToListAsync();

            double totalSoldForMoney = 0; // kg miqdori
            double totalSoldForMoneySumm = 0; // so'm miqdori
            foreach (var detail in groupDetailsSoldForMoney)
            {
                if (detail.invoice != null)
                {
                    // kg miqdori (note maydoni)
                    if (detail.invoice.note != null && detail.invoice.note != "")
                    {
                        // Vergulni nuqtaga o'zgartirish (masalan, "18,95" -> "18.95")
                        string noteStr = detail.invoice.note.Replace(",", ".");
                        // Bo'sh joylarni olib tashlash
                        noteStr = noteStr.Trim();
                        
                        // System.Globalization.CultureInfo.InvariantCulture bilan parse qilish
                        if (double.TryParse(noteStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double noteValue))
                        {
                            totalSoldForMoney += noteValue;
                        }
                    }
                    
                    // so'm miqdori (summ maydoni)
                    totalSoldForMoneySumm += detail.invoice.summ;
                }
            }

            // 4. Qanchasi zaxiradga quyildi (TegirmonTortilganBugdoyRoyxatiGroupDetail orqali name maydonidagi invoice id lardan qty_real yig'indisi)
            var groupDetailsToZaxira = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p => p.bugdoyRoyxatiGroup)
                .Include(p => p.royxati)
                .Where(p => p.active_status == true
                    && p.bugdoyRoyxatiGroup != null
                    && p.bugdoyRoyxatiGroup.active_status == true
                    && p.bugdoyRoyxatiGroup.date_time >= startDate
                    && p.bugdoyRoyxatiGroup.date_time < endDateValue
                    && p.name != null
                    && p.name != "")
                .ToListAsync();

            double totalToZaxira = 0;
            var invoiceIds = new List<long>();
            
            // name maydonidagi invoice id larni ajratamiz
            foreach (var detail in groupDetailsToZaxira)
            {
                if (detail.name != null && detail.name != "")
                {
                    // name maydoni vergul bilan ajratilgan invoice id larni o'z ichiga oladi
                    string[] ids = detail.name.Split(',');
                    foreach (string idStr in ids)
                    {
                        if (long.TryParse(idStr.Trim(), out long invoiceId))
                        {
                            invoiceIds.Add(invoiceId);
                        }
                    }
                }
            }

            // Barcha invoice larni bir marta olamiz
            if (invoiceIds.Count > 0)
            {
                var invoices = await _context.TegirmonInvoice
                    .Where(inv => inv.active_status == true
                        && inv.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH
                        && invoiceIds.Contains(inv.id))
                    .ToListAsync();

                foreach (var invoice in invoices)
                {
                    totalToZaxira += invoice.qty_real;
                }
            }

                var result = new
                {
                    begin_date = startDate,
                    end_date = end_date?.Date ?? DateTime.UtcNow.Date,
                    total_bugdoy_received = totalBugdoyReceived,
                    total_changed_to_product = totalChangedToProduct,
                    total_sold_for_money = totalSoldForMoney,
                    total_sold_for_money_summ = totalSoldForMoneySumm,
                    total_to_zaxira = totalToZaxira,
                    updated_at = DateTime.UtcNow
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Xatolik yuz berdi", message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}

