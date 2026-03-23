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
using ApiAll.Model.settingsmodel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonOrderCarController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderCarController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderCar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderCar>>> GetTegirmonOrderCar()
        {
            return await _context.TegirmonOrderCar.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderCar> categoryList = await _context.TegirmonOrderCar
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderCar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderCar
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderCar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderCar>> GetTegirmonOrderCar(long id)
        {
            var tegirmonOrderCar = await _context.TegirmonOrderCar
                .Include(p => p.item_list) // bog‘langan narxlar ham chiqadi
                .FirstOrDefaultAsync(p => p.id == id);

            if (tegirmonOrderCar == null)
            {
                return NotFound();
            }

            // Listni teskari qilish
            tegirmonOrderCar.item_list = tegirmonOrderCar.item_list
                .OrderBy(pp => pp.id) // yoki boshqa maydon bo‘yicha
                .ToList();

            return tegirmonOrderCar;
        }

        // GET: api/TegirmonOrderCar/getByDate?date=2025-09-11&clientId=0
        [HttpGet("getTegirmonOrderCarOrderId")]
        public async Task<IActionResult> GetTegirmonOrderCarOrderId(long order_id)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrderCar
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => o.TegirmonOrderid == order_id);
            
            
            if (order == null)
            {
                return NotFound();
            }

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

        // GET: api/TegirmonOrderCar/getByDate?date=2025-09-11&clientId=0
        [HttpGet("getByDate")]
        public async Task<IActionResult> GetOrdersByDate(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrderCar
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .Where(o => o.create_date.Date == date.Date && o.active_status == true);

            

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

        // GET: api/TegirmonOrderCar/GetOrderSummaryByDate?date=2025-09-15&clientId=0
        [HttpGet("GetOrderSummaryByDate")]
        public async Task<IActionResult> GetOrderSummaryByDate(DateTime date, long clientId = 0)
        {
            // asosiy query
            var ordersQuery = _context.TegirmonOrderCar
                .Include(x => x.item_list)
                    .ThenInclude(i => i.product)
                        .ThenInclude(p => p.unitMeasurment)
                .Where(o => o.create_date.Date == date.Date && o.active_status == true);

            

            // orderlardan itemlarni chiqarib olish
            var summary = await ordersQuery
                .SelectMany(o => o.item_list) // barcha itemlarni bir joyga
                .GroupBy(i => new
                {
                    ProductId = i.product.id,
                    ProductName = i.product.name,
                    UnitMeasurmentId = i.product.TegirmonUnitMeasurmentid,
                    UnitMeasurmentName = i.product.unitMeasurment != null ? i.product.unitMeasurment.name : null,
                    StockQty = _context.TegirmonSkladTovar
                    .Where(s => s.TegirmonProductId == i.product.id && s.TegirmonSkladId == 2)
                    .Select(s => (decimal?)s.qty)
                    .FirstOrDefault()?? 0
                    })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    g.Key.UnitMeasurmentId,
                    g.Key.UnitMeasurmentName,
                    StockQty = g.Key.StockQty, // ombordagi bor soni
                    TotalQty = g.Sum(i => i.qty)
                })
                .OrderBy(x => x.ProductName)
                .ToListAsync();

            return Ok(summary);
        }

        [HttpGet("getByMonth")]
        public async Task<IActionResult> GetOrdersByMonth(DateTime date, long clientId = 0)
        {
            int year = date.Year;
            int month = date.Month;

            var orders = _context.TegirmonOrderCar
                .Include(x => x.item_list)
                .Where(o => o.create_date.Year == year && o.create_date.Month == month && o.active_status == true);

            var result = await orders.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

        // PUT: api/TegirmonOrderCar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderCar(long id, TegirmonOrderCar tegirmonOrderCar)
        {
            if (id != tegirmonOrderCar.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderCarExists(id))
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

        // POST: api/TegirmonOrderCar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<TegirmonOrderCar>> PostTegirmonOrderCar(TegirmonOrderCar tegirmonOrderCar)
        // {
        //     _context.TegirmonOrderCar.Update(tegirmonOrderCar);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTegirmonOrderCar", new { id = tegirmonOrderCar.id }, tegirmonOrderCar);
        // }

        //Yangi productPrice bilan update qilish
        [HttpPost("{id}/begin")]
        public async Task<IActionResult> SetOrderCarBegin(long id)
        {
            // car-ni item_list bilan olamiz
            var car = await _context.TegirmonOrderCar
                        .Include(c => c.item_list)
                        .FirstOrDefaultAsync(p => p.id == id);

            if (car == null)
                return NotFound();

            // Transaction boshlaymiz, agar biror joyda xato bo'lsa hammasi rollback bo'ladi
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    car.is_begin = true;
                    _context.TegirmonOrderCar.Update(car);
                    await _context.SaveChangesAsync();

                    TegirmonOrder tegirmonOrder = null;
                    if (car.TegirmonOrderid > 0)
                    {
                        // orderni ham olamiz (jaxoniy o'zgartirishlar uchun)
                        tegirmonOrder = await _context.TegirmonOrder.FirstOrDefaultAsync(o => o.id == car.TegirmonOrderid);
                        if (tegirmonOrder != null)
                        {
                            tegirmonOrder.is_loading = false;

                            if (car.auth_user_updator_id == 100)
                            {
                                tegirmonOrder.IsLoaded = true;
                                tegirmonOrder.LoadedAt = DateTime.Now;
                                tegirmonOrder.paid_status = "Yuk to'liq yuklandi";

                                if (tegirmonOrder.IsPaid)
                                {
                                    tegirmonOrder.IsClosed = true;
                                    tegirmonOrder.ClosedAt = DateTime.Now;
                                    tegirmonOrder.paid_status = "Zakaz yopildi";
                                }
                            }
                            else
                            {
                                tegirmonOrder.paid_status = $"Yuk {car.auth_user_updator_id} % yuklandi";
                            }

                            _context.TegirmonOrder.Update(tegirmonOrder);
                            await _context.SaveChangesAsync();
                        }
                    }


                    // 1️⃣ Avval shu car.id bilan TegirmonSkladHistory yozuvini tekshirish
                    var existingHistory = await _context.TegirmonSkladHistory
                        .Where(h => h.userId == car.id && h.text == "optom")
                        .ToListAsync();

                    
                    if (existingHistory.Any())
                    {
                        foreach (var hist in existingHistory)
                        {
                            // Skladdagi mahsulotga qayta qo‘shish
                            var skladTovars = await _context.TegirmonSkladTovar
                                .FirstOrDefaultAsync(p => p.TegirmonProductId == hist.TegirmonProductId && p.TegirmonSkladId == hist.TegirmonSkladId);
                            if (skladTovars != null)
                            {
                                skladTovars.qty += hist.qty;
                                skladTovars.real_qty = (skladTovars.real_qty ?? 0) + hist.qty;
                                skladTovars.UpdatedDate = DateTime.Now;
                                _context.TegirmonSkladTovar.Update(skladTovars);
                            }

                            // Yangi qty_log tablega yozamiz (ehtimoliy qo'shilish uchun)
                            var qtyLog = new TegirmonSkladQtyLog
                            {
                                active_status = true,
                                qty = hist.qty,
                                type = 1, // 1 = kirim (order car begin - history qaytarish)
                                TegirmonProductId = hist.TegirmonProductId,
                                TegirmonSkladId = hist.TegirmonSkladId,
                                note = "OrderCar begin - history qaytarish (Car ID: " + car.id + ", History ID: " + hist.id + ") - ehtimoliy qo'shilish",
                                text = "OrderCar_begin_qaytarish_ehtimoliy",
                                auth_user_creator_id = car.TegirmonAuthid,
                                userId = car.id,
                                user_name = car.user_name,
                                operation_id = car.id,
                                operation_type = "OrderCar",
                                create_date = DateTime.Now
                            };

                            _context.TegirmonSkladQtyLog.Add(qtyLog);

                            // History yozuvini o'chirish
                            _context.TegirmonSkladHistory.Remove(hist);
                        }

                        await _context.SaveChangesAsync();
                    }

                    // endi TegirmonOrderCar.item_list dagi mahsulotlarni ishlaymiz
                    if (car.item_list != null && car.item_list.Any())
                    {
                        // TegirmonOrderdagi TegirmonSkladid (qaysi skladdan olinishini ko'rsatadi)
                        long skladId = 2;
                        Console.WriteLine($"SkladId: {skladId}");
                        if (skladId == 0)
                        {
                            // agar sklad id yo'q bo'lsa — rollback va xato
                            await transaction.RollbackAsync();
                            return BadRequest(new { message = "TegirmonOrder uchun TegirmonSkladid topilmadi." });
                        }

                        foreach (var item in car.item_list)
                        {
                            // item dan product id va qty olinishini taxmin qilamiz:
                            // item.TegirmonProductid va item.qty mavjud deb hisoblaymiz.
                            long productId = item.TegirmonProductid;
                            double takeQty = item.qty;


                            




                            // TegirmonSkladTovar ro'yxatini topamiz
                            var skladTovarList = await _context.TegirmonSkladTovar
                                .Where(p => p.TegirmonProductId == productId && p.TegirmonSkladId == skladId)
                                .ToListAsync();

                            if (!skladTovarList.Any())
                            {
                                await transaction.RollbackAsync();
                                return BadRequest(new { message = $"Mahsulot (id={productId}) uchun skladda yozuv topilmadi." });
                            }

                            var skladTovar = skladTovarList.First();

                            // Yetarli zaxira bor-yo'qligini tekshiramiz
                            if (skladTovar.qty < takeQty)
                            {
                                await transaction.RollbackAsync();
                                return BadRequest(new { message = $"Mahsulot (id={productId}) zaxirasi yetarli emas. Mavjud: {skladTovar.qty}, talab qilingan: {takeQty}" });
                            }

                            // Skladdan kamaytirish
                            skladTovar.qty -= takeQty;
                            skladTovar.real_qty = (skladTovar.real_qty ?? 0) - takeQty;
                            skladTovar.UpdatedDate = DateTime.Now;

                            _context.TegirmonSkladTovar.Update(skladTovar);

                            // Sklad history yozish
                            TegirmonSkladHistory history = new TegirmonSkladHistory
                            {
                                active_status = true,
                                qty = takeQty,
                                type = 2, // chiqim
                                TegirmonProductId = productId,
                                TegirmonSkladId = skladId,
                                note = (tegirmonOrder?.note ?? "") + " <<Opto savdo>>",
                                text = "optom",
                                userId = car.id, // car id yordamida kim bo'lganini aniqlash mumkin
                                authId = car.TegirmonAuthid,
                                user_name = car.user_name,
                                create_date = DateTime.Now
                            };

                            // Yozamiz
                            await _context.TegirmonSkladHistory.AddAsync(history);
                            // (SkladTovarni ham yangiladik yuqorida)
                        }

                        // barchasini saqlaymiz
                        await _context.SaveChangesAsync();
                    }

                    // hammasi muvaffaqiyatli bo'lsa commit qilamiz
                    await transaction.CommitAsync();

                    return Ok(new { message = "is_begin true qilindi va car.item_list bo‘yicha skladdan ayirildi", id = car.id });
                }
                catch (Exception ex)
                {
                    // xatolikda rollback va xatolik xabari
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Xatolik yuz berdi: " + ex.Message });
                }
            }
        }



        [HttpPost]
        public async Task<ActionResult<TegirmonOrderCar>> PostTegirmonOrderCar(TegirmonOrderCar tegirmonOrderCar)
        {
            if (tegirmonOrderCar.TegirmonOrderid > 0)
            {
                var tegirmonOrder = await _context.TegirmonOrder.FindAsync(tegirmonOrderCar.TegirmonOrderid);
                if (tegirmonOrder != null) {
                    tegirmonOrder.car_nomer = tegirmonOrderCar.car_nomer;
                    tegirmonOrder.load_progress = tegirmonOrderCar.auth_user_updator_id;
                    tegirmonOrder.is_loading = true;
                    tegirmonOrder.paid_status = "Yuk " + tegirmonOrderCar.auth_user_updator_id.ToString() + " % yuk yuklanayabdi.";
                    tegirmonOrder.IsLoaded = false;
                    tegirmonOrder.load_name = tegirmonOrderCar.user_name;
                    // if (tegirmonOrderCar.auth_user_updator_id == 100)
                    // {
                    //     tegirmonOrder.IsLoaded = true;
                    //     tegirmonOrder.LoadedAt = new DateTime();
                    //     tegirmonOrder.paid_status = "Yuk to'liq yuklandi";
                    //     if (tegirmonOrder.IsPaid == true)
                    //     {
                    //         tegirmonOrder.IsClosed = true;
                    //         tegirmonOrder.ClosedAt = DateTime.Now;
                    //         tegirmonOrder.paid_status = "Zakaz yopildi";
                    //     }
                    // }
                    // else
                    // {
                    //     tegirmonOrder.paid_status = "Yuk " + tegirmonOrderCar.auth_user_updator_id.ToString() + " % yuklandi";
                    // }



                    // _context.TegirmonOrder.Update(tegirmonOrder);
                    await _context.SaveChangesAsync();
                }
            }
            if (tegirmonOrderCar.id == 0)
            {
                // null kelgan bo‘lsa, bo‘sh list qilib olamiz
                tegirmonOrderCar.item_list ??= new List<TegirmonOrderCarItem>();
                // yangi product yaratish
                _context.TegirmonOrderCar.Add(tegirmonOrderCar);
            }
            else
            {
                // Eski productni bazadan chaqiramiz
                var existingProduct = await _context.TegirmonOrderCar
                    .Include(p => p.item_list)
                    .FirstOrDefaultAsync(p => p.id == tegirmonOrderCar.id);

                if (existingProduct == null)
                    return NotFound();

                // Product ma'lumotlarini update qilamiz
                _context.Entry(existingProduct).CurrentValues.SetValues(tegirmonOrderCar);

                // Eski narxlarni o‘chirib tashlaymiz
                _context.TegirmonOrderCarItem.RemoveRange(existingProduct.item_list);

                // Yangi narxlarni qo‘shamiz
                if (tegirmonOrderCar.item_list != null && tegirmonOrderCar.item_list.Any())
                {
                    foreach (var price in tegirmonOrderCar.item_list)
                    {
                        // product bilan bog‘lashni unutmang
                        price.TegirmonOrderCarid = existingProduct.id;
                        existingProduct.item_list.Add(price);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderCar", new { id = tegirmonOrderCar.id }, tegirmonOrderCar);
        }

        // DELETE: api/TegirmonOrderCar/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderCar>> DeleteTegirmonOrderCar(long id)
        {
            var tegirmonOrderCar = await _context.TegirmonOrderCar.FindAsync(id);
            if (tegirmonOrderCar == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderCar.Remove(tegirmonOrderCar);
            await _context.SaveChangesAsync();

            return tegirmonOrderCar;
        }

        private bool TegirmonOrderCarExists(long id)
        {
            return _context.TegirmonOrderCar.Any(e => e.id == id);
        }
    }
}
