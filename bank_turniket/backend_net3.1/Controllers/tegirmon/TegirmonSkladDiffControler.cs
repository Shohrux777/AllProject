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
    public class TegirmonSkladDiffControler : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonSkladDiffControler(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonSkladDiffControler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonSkladDiff>>> GetTegirmonSkladDiff()
        {
            return await _context.TegirmonSkladDiff.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladDiff> categoryList = await _context.TegirmonSkladDiff
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladDiff>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSkladDiff.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonSkladDiffControler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonSkladDiff>> GetTegirmonSkladDiff(long id)
        {
            var tegirmonSkladDiff = await _context.TegirmonSkladDiff.FindAsync(id);

            if (tegirmonSkladDiff == null)
            {
                return NotFound();
            }

            return tegirmonSkladDiff;
        }

        // PUT: api/TegirmonSkladDiffControler/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonSkladDiff(long id, TegirmonSkladDiff tegirmonSkladDiff)
        {
            if (id != tegirmonSkladDiff.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonSkladDiff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonSkladDiffExists(id))
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

        // POST: api/TegirmonSkladDiffControler
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonSkladDiff>> PostTegirmonSkladDiff(TegirmonSkladDiff tegirmonSkladDiff)
        {
            _context.TegirmonSkladDiff.Update(tegirmonSkladDiff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonSkladDiff", new { id = tegirmonSkladDiff.id }, tegirmonSkladDiff);
        }

        [HttpPost("add-diff-list")]
        public async Task<IActionResult> AddDiffList([FromBody] List<TegirmonSkladDiff> model)
        {
            if (model == null || model.Count == 0)
                return BadRequest("List bo‘sh bo‘lmasligi kerak");

            // Navigation propertylar null bo‘lishi kerak
            model.ForEach(x =>
            {
                x.Sklad = null;
                x.Product = null;
                x.create_date = DateTime.Now;
            });

            await _context.TegirmonSkladDiff.AddRangeAsync(model);
            await _context.SaveChangesAsync();
            
            return Ok(new
            {
                success = true,
                count = model.Count,
                message = $"{model.Count} ta diff muvaffaqiyatli qo‘shildi"
            });
        }

        /// <summary>
        /// Frontenddagi sklad sanash (CNV) jarayonini bitta API da bajarish:
        /// 1) diff larni saqlaydi
        /// 2) tegirmon_invoice va tegirmon_invoice_item yaratadi
        /// 3) skladdagi qoldiqni yangilaydi va tarixga yozadi
        /// Hammasi bitta transaction ichida.
        /// </summary>
        [HttpPost("add-diff-and-prixod")]
        public async Task<IActionResult> AddDiffAndPrixod([FromBody] List<TegirmonSkladDiff> model)
        {
            if (model == null || model.Count == 0)
                return BadRequest("List bo‘sh bo‘lmasligi kerak");

            // Faqat haqiqiy farqi borlarini olamiz
            var plusDiffItems = model.Where(x => x.diff > 0).ToList();   // kirim
            var minusDiffItems = model.Where(x => x.diff < 0).ToList();  // chiqim

            // if (plusDiffItems.Count == 0 && minusDiffItems.Count == 0)
            // {
            //     return BadRequest("Farqi bo‘lgan mahsulot topilmadi (diff = 0).");
            // }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Diff larni saqlash
                foreach (var x in model)
                {
                    x.Sklad = null;
                    x.Product = null;
                    x.create_date = DateTime.Now;
                }

                await _context.TegirmonSkladDiff.AddRangeAsync(model);
                await _context.SaveChangesAsync();

                TegirmonInvoice invoiceIn = null;
                TegirmonInvoice invoiceOut = null;

                // 2️⃣ Kirim invoice (diff > 0) - prixod_status = 1
                if (plusDiffItems.Count > 0)
                {
                    var first = plusDiffItems.First();

                    // Itemlar va umumiy summa (diff * product_price) hisoblash
                    var plusItems = plusDiffItems.Select(d =>
                    {
                        var qty = d.diff; // > 0
                        var price = d.product_price ?? 0.0; // nullable emas bo'lishi uchun
                        var lineSum = price * qty;

                        return new TegirmonInvoiceItem
                        {
                            active_status = true,
                            TegirmonProductid = d.TegirmonProductId,
                            qty = qty,
                            real_qty = qty,
                            sum = lineSum,
                            real_sum = lineSum,
                            all_sum = lineSum,
                            product_price = price,
                            buy_dollor = 0,
                            buy_sum = 0,
                            for_money = false,
                            created_date_time = DateTime.Now,
                            updated_date_time = DateTime.Now
                        };
                    }).ToList();

                    var plusTotalSum = plusItems.Sum(i => i.sum);

                    invoiceIn = new TegirmonInvoice
                    {
                        active_status = true,
                        note = "Sklad sanalganda (kirim)",
                        user_name = first.user_name,
                        auth_user_name = first.user_name,
                        summ = plusTotalSum,   // umumiy qiymat
                        pol_all_sum = 0,
                        prixod_status = 1, // kirim
                        TegirmonSkladid = first.TegirmonSkladId,
                        TegirmonAuthid = first.auth_id,
                        status_inv_type_name = "INVOICE_BUGDOY_PRIXOD_QILISH",
                        // diff bilan bog'lash uchun diff id ni yozib qo'yamiz
                        sklad_diff_id = first.id,
                        created_date_time = DateTime.Now,
                        updated_date_time = DateTime.Now,
                        item_list = plusItems
                    };

                    _context.TegirmonInvoice.Add(invoiceIn);
                    await _context.SaveChangesAsync();
                }

                // 3️⃣ Chiqim invoice (diff < 0) - prixod_status = 0, qty = |diff|
                if (minusDiffItems.Count > 0)
                {
                    var first = minusDiffItems.First();

                    // Itemlar va umumiy summa (|diff| * product_price) hisoblash
                    var minusItems = minusDiffItems.Select(d =>
                    {
                        var qty = Math.Abs(d.diff); // har doim musbat
                        var price = d.product_price ?? 0.0; // nullable emas bo'lishi uchun
                        var lineSum = price * qty;

                        return new TegirmonInvoiceItem
                        {
                            active_status = true,
                            TegirmonProductid = d.TegirmonProductId,
                            qty = qty,
                            real_qty = qty,
                            sum = lineSum,
                            real_sum = lineSum,
                            all_sum = lineSum,
                            product_price = price,
                            buy_dollor = 0,
                            buy_sum = 0,
                            for_money = false,
                            created_date_time = DateTime.Now,
                            updated_date_time = DateTime.Now
                        };
                    }).ToList();

                    var minusTotalSum = minusItems.Sum(i => i.sum);

                    invoiceOut = new TegirmonInvoice
                    {
                        active_status = true,
                        note = "Sklad sanalganda (chiqim)",
                        user_name = first.user_name,
                        auth_user_name = first.user_name,
                        summ = minusTotalSum,   // umumiy qiymat
                        pol_all_sum = 0,
                        prixod_status = 2, // chiqim (bu endpoint uchun shartli qiymat)
                        TegirmonSkladid = first.TegirmonSkladId,
                        TegirmonAuthid = first.auth_id,
                        status_inv_type_name = "INVOICE_BUGDOY_PRIXOD_QILISH",
                        // chiqim invoice ham o'z diff paketi bilan bog'lanadi
                        sklad_diff_id = first.id,
                        created_date_time = DateTime.Now,
                        updated_date_time = DateTime.Now,
                        item_list = minusItems
                    };

                    _context.TegirmonInvoice.Add(invoiceOut);
                    await _context.SaveChangesAsync();
                }

                // 4️⃣ Skladdagi qoldiqni yangilash va tarixga yozish (kirim)
                if (invoiceIn != null && invoiceIn.item_list != null && invoiceIn.item_list.Count > 0)
                {
                    foreach (var item in invoiceIn.item_list)
                    {
                        var skladTovarList = await _context.TegirmonSkladTovar
                            .Where(p => p.TegirmonProductId == item.TegirmonProductid
                                     && p.TegirmonSkladId == invoiceIn.TegirmonSkladid)
                            .ToListAsync();

                        if (skladTovarList != null && skladTovarList.Count > 0)
                        {
                            skladTovarList.First().qty = skladTovarList.First().qty + item.qty;
                            skladTovarList.First().real_qty = skladTovarList.First().real_qty + item.qty;

                            _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                        }
                        else
                        {
                            var skladTovar = new TegirmonSkladTovar
                            {
                                active_status = true,
                                qty = item.qty,
                                real_qty = item.qty,
                                TegirmonProductId = item.TegirmonProductid,
                                TegirmonSkladId = invoiceIn.TegirmonSkladid ?? 0
                            };

                            _context.TegirmonSkladTovar.Update(skladTovar);
                        }

                        // Yangi qty_log tablega yozamiz (ehtimoliy qo'shilish uchun)
                        var qtyLog = new TegirmonSkladQtyLog
                        {
                            active_status = true,
                            qty = item.qty,
                            type = 1, // 1 = kirim (sklad diff)
                            TegirmonProductId = item.TegirmonProductid,
                            TegirmonSkladId = invoiceIn.TegirmonSkladid ?? 0,
                            note = "Sklad diff kirim (Invoice ID: " + invoiceIn.id + ") - ehtimoliy qo'shilish",
                            text = "Sklad_diff_kirim_ehtimoliy",
                            auth_user_creator_id = invoiceIn.TegirmonAuthid ?? 0,
                            userId = invoiceIn.id,
                            user_name = invoiceIn.user_name,
                            operation_id = invoiceIn.id,
                            operation_type = "Invoice",
                            create_date = DateTime.Now
                        };

                        _context.TegirmonSkladQtyLog.Add(qtyLog);

                        var history = new TegirmonSkladHistory
                        {
                            active_status = true,
                            qty = item.qty,
                            type = 1, // kirim
                            TegirmonProductId = item.TegirmonProductid,
                            TegirmonSkladId = invoiceIn.TegirmonSkladid ?? 0,
                            note = invoiceIn.note + " <<Sklad diff>>",
                            text = "Sklad_diff_kirim",
                            user_name = invoiceIn.user_name,
                            auth_user_creator_id = invoiceIn.TegirmonAuthid ?? 0,
                            // keyinchalik diffni orqaga qaytarish uchun invoice id ni saqlab qo'yamiz
                            userId = invoiceIn.id,
                            create_date = DateTime.Now
                        };

                        _context.TegirmonSkladHistory.Update(history);
                    }

                    invoiceIn.inv_accepted_status = true;
                    _context.TegirmonInvoice.Update(invoiceIn);
                    await _context.SaveChangesAsync();
                }

                // 5️⃣ Skladdagi qoldiqni yangilash va tarixga yozish (chiqim)
                if (invoiceOut != null && invoiceOut.item_list != null && invoiceOut.item_list.Count > 0)
                {
                    foreach (var item in invoiceOut.item_list)
                    {
                        var skladTovarList = await _context.TegirmonSkladTovar
                            .Where(p => p.TegirmonProductId == item.TegirmonProductid
                                     && p.TegirmonSkladId == invoiceOut.TegirmonSkladid)
                            .ToListAsync();

                        if (skladTovarList != null && skladTovarList.Count > 0)
                        {
                            skladTovarList.First().qty = skladTovarList.First().qty - item.qty;
                            skladTovarList.First().real_qty = skladTovarList.First().real_qty - item.qty;

                            _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                        }
                        else
                        {
                            var skladTovar = new TegirmonSkladTovar
                            {
                                active_status = true,
                                qty = -1 * item.qty,
                                real_qty = -1 * item.qty,
                                TegirmonProductId = item.TegirmonProductid,
                                TegirmonSkladId = invoiceOut.TegirmonSkladid ?? 0
                            };

                            _context.TegirmonSkladTovar.Update(skladTovar);
                        }

                        var history = new TegirmonSkladHistory
                        {
                            active_status = true,
                            qty = item.qty,
                            type = 2, // chiqim
                            TegirmonProductId = item.TegirmonProductid,
                            TegirmonSkladId = invoiceOut.TegirmonSkladid ?? 0,
                            note = invoiceOut.note + " <<Sklad diff>>",
                            text = "Sklad_diff_chiqim",
                            user_name = invoiceOut.user_name,
                            auth_user_creator_id = invoiceOut.TegirmonAuthid ?? 0,
                            // keyinchalik diffni orqaga qaytarish uchun invoice id ni saqlab qo'yamiz
                            userId = invoiceOut.id,
                            create_date = DateTime.Now
                        };

                        _context.TegirmonSkladHistory.Update(history);
                    }

                    invoiceOut.inv_accepted_status = true;
                    _context.TegirmonInvoice.Update(invoiceOut);

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    diffCount = model.Count,
                    plusDiffCount = plusDiffItems.Count,
                    minusDiffCount = minusDiffItems.Count,
                    invoiceInId = invoiceIn?.id,
                    invoiceOutId = invoiceOut?.id
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Eski difflarni va ular asosida yaratilgan invoice + history ni bekor qilib,
        /// yangi diff ro'yxati bilan qayta hisoblaydi.
        /// Frontendda bir kunga diff yozilgan bo'lsa, edit qilish uchun ishlatiladi.
        /// </summary>
        [HttpPost("update-diff-and-prixod")]
        public async Task<IActionResult> UpdateDiffAndPrixod([FromBody] List<TegirmonSkladDiff> model)
        {
            if (model == null || model.Count == 0)
                return BadRequest("List bo‘sh bo‘lmasligi kerak");

            // Kun va sklad bo'yicha eski difflarni topamiz
            var first = model.First();
            var date = first.create_date.Date;
            var skladId = first.TegirmonSkladId;

            var oldDiffs = await _context.TegirmonSkladDiff
                .Where(x => x.create_date.Date == date && x.TegirmonSkladId == skladId)
                .ToListAsync();

            if (oldDiffs.Any())
            {
                var oldDiffIds = oldDiffs.Select(d => d.id).ToList();

                // Eski difflar asosida yaratilgan invoice'larni topamiz
                var oldInvoices = await _context.TegirmonInvoice
                    .Where(inv =>
                        inv.status_inv_type_name == "INVOICE_BUGDOY_PRIXOD_QILISH" &&
                        inv.TegirmonSkladid == skladId &&
                        inv.sklad_diff_id.HasValue &&
                        oldDiffIds.Contains(inv.sklad_diff_id.Value))
                    .Include(inv => inv.item_list)
                    .ToListAsync();

                foreach (var inv in oldInvoices)
                {
                    // 1️⃣ Sklad qoldig'ini orqaga qaytaramiz
                    if (inv.item_list != null && inv.item_list.Count > 0)
                    {
                        foreach (var item in inv.item_list)
                        {
                            var skladTovarList = await _context.TegirmonSkladTovar
                                .Where(p => p.TegirmonProductId == item.TegirmonProductid
                                         && p.TegirmonSkladId == inv.TegirmonSkladid)
                                .ToListAsync();

                            if (skladTovarList != null && skladTovarList.Count > 0)
                            {
                                // Agar avval kirim bo'lgan bo'lsa (1) - endi ayiramiz
                                // Agar avval chiqim bo'lgan bo'lsa (2) - endi qo'shamiz
                                if (inv.prixod_status == 1)
                                {
                                    skladTovarList.First().qty -= item.qty;
                                    skladTovarList.First().real_qty -= item.qty;
                                }
                                else if (inv.prixod_status == 2)
                                {
                                    skladTovarList.First().qty += item.qty;
                                    skladTovarList.First().real_qty += item.qty;

                                    // Yangi qty_log tablega yozamiz (ehtimoliy qo'shilish uchun)
                                    var qtyLog = new TegirmonSkladQtyLog
                                    {
                                        active_status = true,
                                        qty = item.qty,
                                        type = 1, // 1 = kirim (update diff - eski chiqimni qaytarish)
                                        TegirmonProductId = item.TegirmonProductid,
                                        TegirmonSkladId = inv.TegirmonSkladid ?? 0,
                                        note = "Update diff - eski chiqimni qaytarish (Invoice ID: " + inv.id + ") - ehtimoliy qo'shilish",
                                        text = "Update_diff_qaytarish_ehtimoliy",
                                        auth_user_creator_id = inv.TegirmonAuthid ?? 0,
                                        userId = inv.id,
                                        user_name = inv.user_name,
                                        operation_id = inv.id,
                                        operation_type = "Invoice",
                                        create_date = DateTime.Now
                                    };

                                    _context.TegirmonSkladQtyLog.Add(qtyLog);
                                }

                                _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                            }
                        }
                    }

                    // 2️⃣ History yozuvlarini o'chiramiz
                    var histories = await _context.TegirmonSkladHistory
                        .Where(h => h.userId == inv.id && h.text.StartsWith("Sklad_diff"))
                        .ToListAsync();

                    if (histories.Any())
                    {
                        _context.TegirmonSkladHistory.RemoveRange(histories);
                    }

                    // 3️⃣ Invoice item va o'zini o'chiramiz
                    if (inv.item_list != null && inv.item_list.Count > 0)
                    {
                        _context.TegirmonInvoiceItem.RemoveRange(inv.item_list);
                    }

                    _context.TegirmonInvoice.Remove(inv);
                }

                // 4️⃣ Eski diff yozuvlarini o'chiramiz
                _context.TegirmonSkladDiff.RemoveRange(oldDiffs);

                await _context.SaveChangesAsync();
            }

            // 5️⃣ Yangi difflarni odatdagi logika bo'yicha qo'llaymiz
            return await AddDiffAndPrixod(model);
        }


        [HttpGet("get-by-date")]
        public async Task<ActionResult<List<TegirmonSkladDiff>>> GetByDate(string date, int? sklad_id)
        {
            try
            {
                // string → DateTime ga o'giramiz
                var parsedDate = DateTime.Parse(date);

                var query = _context.TegirmonSkladDiff
                    .Where(x => x.create_date.Date == parsedDate.Date);

                // Agar sklad_id berilgan bo'lsa, shu sklad bo'yicha filter qilamiz
                if (sklad_id.HasValue && sklad_id.Value > 0)
                {
                    query = query.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                var list = await query
                    .Include(x => x.Product)
                    .ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Shu sana va sklad bo'yicha barcha diff larni o'chiradi
        /// </summary>
        [HttpDelete("delete-by-date-and-sklad")]
        public async Task<IActionResult> DeleteByDateAndSklad([FromQuery] string date, [FromQuery] int sklad_id)
        {
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    return BadRequest(new { message = "Sana parametri kerak" });
                }

                var parsedDate = DateTime.Parse(date);

                var diffsToDelete = await _context.TegirmonSkladDiff
                    .Where(x => x.create_date.Date == parsedDate.Date && x.TegirmonSkladId == sklad_id)
                    .ToListAsync();

                if (diffsToDelete.Count == 0)
                {
                    return Ok(new { success = true, message = "O'chirish uchun diff topilmadi", count = 0 });
                }

                _context.TegirmonSkladDiff.RemoveRange(diffsToDelete);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    success = true, 
                    message = $"{diffsToDelete.Count} ta diff muvaffaqiyatli o'chirildi",
                    count = diffsToDelete.Count 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    success = false, 
                    message = ex.Message 
                });
            }
        }

        /// <summary>
        /// Shu sana va sklad bo'yicha auth_user_creator_id = 1 bo'lgan diff larni o'chiradi (sklad_count sahifasidan yozilgan)
        /// </summary>
        [HttpDelete("delete-by-date-sklad-and-auth-creator")]
        public async Task<IActionResult> DeleteByDateSkladAndAuthCreator([FromQuery] string date, [FromQuery] int sklad_id, [FromQuery] long auth_user_creator_id = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    return BadRequest(new { message = "Sana parametri kerak" });
                }

                var parsedDate = DateTime.Parse(date);

                var diffsToDelete = await _context.TegirmonSkladDiff
                    .Where(x => x.create_date.Date == parsedDate.Date 
                             && x.TegirmonSkladId == sklad_id 
                             && x.auth_user_creator_id == auth_user_creator_id)
                    .ToListAsync();

                if (diffsToDelete.Count == 0)
                {
                    return Ok(new { success = true, message = "O'chirish uchun diff topilmadi", count = 0 });
                }

                _context.TegirmonSkladDiff.RemoveRange(diffsToDelete);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    success = true, 
                    message = $"{diffsToDelete.Count} ta diff muvaffaqiyatli o'chirildi",
                    count = diffsToDelete.Count 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    success = false, 
                    message = ex.Message 
                });
            }
        }




        // DELETE: api/TegirmonSkladDiffControler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonSkladDiff>> DeleteTegirmonSkladDiff(long id)
        {
            var tegirmonSkladDiff = await _context.TegirmonSkladDiff.FindAsync(id);
            if (tegirmonSkladDiff == null)
            {
                return NotFound();
            }

            _context.TegirmonSkladDiff.Remove(tegirmonSkladDiff);
            await _context.SaveChangesAsync();

            return tegirmonSkladDiff;
        }

        private bool TegirmonSkladDiffExists(long id)
        {
            return _context.TegirmonSkladDiff.Any(e => e.id == id);
        }
    }
}
