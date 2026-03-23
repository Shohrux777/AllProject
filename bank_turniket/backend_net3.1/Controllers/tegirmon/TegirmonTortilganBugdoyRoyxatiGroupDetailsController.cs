using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonTortilganBugdoyRoyxatiGroupDetailsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonTortilganBugdoyRoyxatiGroupDetailsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiGroupDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonTortilganBugdoyRoyxatiGroupDetail>>> GetTegirmonTortilganBugdoyRoyxatiGroupDetail()
        {
            return await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.ToListAsync();
        }



        // GET: api/TegirmonTortilganBugdoyRoyxatiGroupDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroupDetail>> GetTegirmonTortilganBugdoyRoyxatiGroupDetail(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroupDetail = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.FindAsync(id);

            if (tegirmonTortilganBugdoyRoyxatiGroupDetail == null)
            {
                return NotFound();
            }

            return tegirmonTortilganBugdoyRoyxatiGroupDetail;
        }


        [HttpGet("getPaginationByGroupId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByGroupId([FromQuery] int page, [FromQuery] int size,[FromQuery] long group_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroupDetail> categoryList = 
                await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p =>p.royxati)
                .ThenInclude(p =>p.product)
                .Include(p =>p.invoice)
                .ThenInclude(p =>p.item_list).ThenInclude(p =>p.product)
                .Where(p => p.active_status == true && p.TegirmonTortilganBugdoyRoyxatiGroupid == group_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroupDetail>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Where(p => p.active_status == true && p.TegirmonTortilganBugdoyRoyxatiGroupid == group_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByInvoiceId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByInvoiceId([FromQuery] int page, [FromQuery] int size,[FromQuery] long invoice_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroupDetail> categoryList = 
                await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Where(p => p.active_status == true && p.TegirmonInvoiceid == invoice_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroupDetail>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Where(p => p.active_status == true && p.TegirmonInvoiceid == invoice_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonTortilganBugdoyRoyxatiGroupDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonTortilganBugdoyRoyxatiGroupDetail(long id, TegirmonTortilganBugdoyRoyxatiGroupDetail tegirmonTortilganBugdoyRoyxatiGroupDetail)
        {
            if (id != tegirmonTortilganBugdoyRoyxatiGroupDetail.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonTortilganBugdoyRoyxatiGroupDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonTortilganBugdoyRoyxatiGroupDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TegirmonTortilganBugdoyRoyxatiGroupDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroupDetail>> PostTegirmonTortilganBugdoyRoyxatiGroupDetail(TegirmonTortilganBugdoyRoyxatiGroupDetail tegirmonTortilganBugdoyRoyxatiGroupDetail)
        {
            _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.Update(tegirmonTortilganBugdoyRoyxatiGroupDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonTortilganBugdoyRoyxatiGroupDetail", new { id = tegirmonTortilganBugdoyRoyxatiGroupDetail.id }, tegirmonTortilganBugdoyRoyxatiGroupDetail);
        }

        // DELETE: api/TegirmonTortilganBugdoyRoyxatiGroupDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroupDetail>> DeleteTegirmonTortilganBugdoyRoyxatiGroupDetail(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroupDetail = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.FindAsync(id);
            if (tegirmonTortilganBugdoyRoyxatiGroupDetail == null)
            {
                return NotFound();
            }

            _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.Remove(tegirmonTortilganBugdoyRoyxatiGroupDetail);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiGroupDetail;
        }

        private bool TegirmonTortilganBugdoyRoyxatiGroupDetailExists(long id)
        {
            return _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.Any(e => e.id == id);
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
