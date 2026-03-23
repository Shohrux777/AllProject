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
    public class TegirmonSkladController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonSkladController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonSklad>>> GetTegirmonSklad()
        {
            return await _context.TegirmonSklad.ToListAsync();
        }

        public class SkladTransferDto
        {
            public string Name { get; set; }
            public string User_Name { get; set; }
            public string Note { get; set; }
            public string Photo { get; set; }

            public int Auth_Id { get; set; }
            public int Sklad_Id { get; set; }
            public List<SkladItemDto> Item_List { get; set; }
        }

        public class SkladItemDto
        {
            public string Product_Name { get; set; }
            // Miqdor uchun double, chunki frontend 2.1 kabi kasr son yuborishi mumkin
            public double Qty { get; set; }
            public int Product_Id { get; set; }
        }

        [HttpPost("transfer_oshxona")]
        public async Task<IActionResult> Transfer([FromBody] SkladTransferDto dto)
        {
            foreach (var item in dto.Item_List)
            {
                // Ombordagi mahsulotni topamiz
                var skladTovarList = await _context.TegirmonSkladTovar
                    .Where(p => p.TegirmonProductId == item.Product_Id
                            && p.TegirmonSkladId == dto.Sklad_Id)
                    .ToListAsync();

                if (skladTovarList != null && skladTovarList.Count > 0)
                {
                    var skladItem = skladTovarList.First();

                    // oldingi minus bo’lgan qty ni qaytamiz
                    skladItem.qty -= item.Qty;
                    skladItem.real_qty -= item.Qty;

                    _context.TegirmonSkladTovar.Update(skladItem);
                    await _context.SaveChangesAsync();
                }

                // Sklad tarixiga oshxonaga berilayotgan mahsulot yozilyabdi
                TegirmonSkladHistory history = new TegirmonSkladHistory();
                history.active_status = true;
                history.qty = item.Qty;
                history.type = 2;
                history.TegirmonProductId = item.Product_Id;
                history.TegirmonSkladId = dto.Sklad_Id;
                history.note = dto.Name + " ( " +  dto.Note + " ) ";
                history.text = "ichki_mahsulot_berish";
                history.auth_user_creator_id = dto.Auth_Id;
                history.user_name = dto.User_Name;
                history.photo_url = dto.Photo;

                _context.TegirmonSkladHistory.Add(history);
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Qabul qilindi" });
        }

        // YANGI API: Bugungi kun uchun ichki mahsulot berilgan ro'yxat
        // TegirmonSkladHistory dan text = "ichki_mahsulot_berish" bo'lgan yozuvlarni oladi
        [HttpGet("getTodayGivenProducts")]
        public async Task<IActionResult> GetTodayGivenProducts()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var result = await _context.TegirmonSkladHistory
                .Where(h => h.text == "ichki_mahsulot_berish"
                         && h.create_date >= today 
                         && h.create_date < tomorrow)
                .Include(h => h.Product)
                    .ThenInclude(p => p.unitMeasurment)
                .OrderByDescending(h => h.create_date)
                .Select(h => new
                {
                    id = h.id,
                    productId = h.TegirmonProductId,
                    productName = h.Product.name,
                    product = h.Product,
                    unitM = h.Product.unitMeasurment,
                    qty = h.qty,
                    name = h.note != null && h.note.Contains(" ( ") 
                        ? h.note.Split(new[] { " ( " }, StringSplitOptions.None)[0] 
                        : h.note ?? "",
                    note = h.note != null && h.note.Contains(" ( ") && h.note.Contains(" )")
                        ? h.note.Substring(h.note.IndexOf(" ( ") + 3, h.note.IndexOf(" )") - h.note.IndexOf(" ( ") - 3)
                        : "",
                    user_name = h.user_name,
                    photo_url = h.photo_url,
                    create_date = h.create_date
                })
                .ToListAsync();

            // Bir xil name va note bo'lganlarini guruhlash (barcha mahsulotlar bilan)
            var grouped = result
                .GroupBy(r => new { r.name, r.note })
                .Select(g => new
                {
                    name = g.Key.name,
                    note = g.Key.note,
                    user_name = g.First().user_name,
                    photo_url = g.First().photo_url,
                    create_date = g.OrderByDescending(x => x.create_date).First().create_date,
                    products = g.GroupBy(p => p.productId)
                        .Select(pg => new
                        {
                            productId = pg.Key,
                            productName = pg.First().productName,
                            product = pg.First().product,
                            unitM = pg.First().unitM,
                            totalQty = pg.Sum(x => x.qty)
                        }).ToList()
                })
                .OrderByDescending(x => x.create_date)
                .ToList();

            return Ok(grouped);
        }

        public class SkladTransfertoSkladDto
        {
            public string user_name { get; set; }
            public string note { get; set; }
            public string photo_url { get; set; }

            public int auth_id { get; set; }
            public int from_sklad_id { get; set; }
            public int to_sklad_id { get; set; }
            public List<SkladToSkladItemDto> item_list { get; set; }
        }

        public class SkladToSkladItemDto
        {
            public string product_name { get; set; }
            public int qty { get; set; }
            public int tegirmonProductid { get; set; }
        }

        [HttpPost("transfer_sklad_to_sklad")]
        public async Task<IActionResult> TransferSklad([FromBody] SkladTransfertoSkladDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var invoiceItems = new List<TegirmonInvoiceItem>();
                double invoiceTotal = 0.0;

                foreach (var item in dto.item_list)
                {
                    // 1️⃣ Product mavjudligini tekshirish
                    var product = await _context.TegirmonProduct
                        .FirstOrDefaultAsync(p => p.id == item.tegirmonProductid);

                    if (product == null)
                        return BadRequest($"ProductId {item.tegirmonProductid} tegirmon_product jadvalida topilmadi.");

                    // 2️⃣ Birinchi skladdan mahsulotni kamaytirish
                    var fromItem = await _context.TegirmonSkladTovar
                        .FirstOrDefaultAsync(p => p.TegirmonProductId == item.tegirmonProductid
                                            && p.TegirmonSkladId == dto.from_sklad_id);

                    if (fromItem == null || fromItem.qty < item.qty)
                        return BadRequest($"Birinchi skladda product {item.product_name} yetarli emas.");

                    fromItem.qty -= item.qty;
                    fromItem.real_qty -= item.qty;
                    _context.TegirmonSkladTovar.Update(fromItem);

                    // 3️⃣ Ikkinchi skladga mahsulot qo‘shish
                    var toItem = await _context.TegirmonSkladTovar
                        .FirstOrDefaultAsync(p => p.TegirmonProductId == item.tegirmonProductid
                                            && p.TegirmonSkladId == dto.to_sklad_id);

                    if (toItem != null)
                    {
                        toItem.qty += item.qty;
                        toItem.real_qty += item.qty;
                        _context.TegirmonSkladTovar.Update(toItem);
                    }
                    else
                    {
                        _context.TegirmonSkladTovar.Add(new TegirmonSkladTovar
                        {
                            TegirmonProductId = item.tegirmonProductid,
                            TegirmonSkladId = dto.to_sklad_id,
                            qty = item.qty,
                            real_qty = item.qty
                        });
                    }

                    // Yangi qty_log tablega yozamiz (ehtimoliy qo'shilish uchun)
                    var qtyLog = new TegirmonSkladQtyLog
                    {
                        active_status = true,
                        qty = item.qty,
                        type = 1, // 1 = kirim (skladdan skladga ko'chirish)
                        TegirmonProductId = item.tegirmonProductid,
                        TegirmonSkladId = dto.to_sklad_id,
                        note = "Skladdan skladga ko'chirish (from: " + dto.from_sklad_id + ", to: " + dto.to_sklad_id + ") - ehtimoliy qo'shilish",
                        text = "Sklad_transfer_ehtimoliy",
                        auth_user_creator_id = dto.auth_id,
                        user_name = dto.user_name,
                        operation_id = 0,
                        operation_type = "SkladTransfer",
                        create_date = DateTime.Now
                    };

                    _context.TegirmonSkladQtyLog.Add(qtyLog);

                    // 4️⃣ Birinchi skladdan tarix (history) yozish
                    _context.TegirmonSkladHistory.Add(new TegirmonSkladHistory
                    {
                        active_status = true,
                        qty = item.qty,
                        type = 2, // chiqim
                        TegirmonProductId = item.tegirmonProductid,
                        TegirmonSkladId = dto.from_sklad_id,
                        note = dto.note,
                        text = "skladdan_boshqa_skladga",
                        auth_user_creator_id = dto.auth_id,
                        auth_user_updator_id = dto.to_sklad_id,
                        user_name = dto.user_name,
                        photo_url = dto.photo_url
                    });

                    // 5️⃣ Ikkinchi skladga tarix (history) yozish
                    _context.TegirmonSkladHistory.Add(new TegirmonSkladHistory
                    {
                        active_status = true,
                        qty = item.qty,
                        type = 1, // kirim
                        TegirmonProductId = item.tegirmonProductid,
                        TegirmonSkladId = dto.to_sklad_id,
                        note = dto.note,
                        text = "skladga_boshqa_skladdan",
                        auth_user_creator_id = dto.auth_id,
                        auth_user_updator_id = dto.from_sklad_id,
                        user_name = dto.user_name,
                        photo_url = dto.photo_url
                    });

                    // 6️⃣ Invoice itemlar uchun tayyorlash
                    var price = product.price;
                    var lineSum = price * item.qty;
                    invoiceItems.Add(new TegirmonInvoiceItem
                    {
                        active_status = false,
                        TegirmonProductid = item.tegirmonProductid,
                        qty = item.qty,
                        real_qty = item.qty,
                        sum = lineSum,
                        real_sum = lineSum,
                        all_sum = lineSum,
                        product_price = price,
                        buy_dollor = 0,
                        buy_sum = 0,
                        for_money = false,
                        created_date_time = DateTime.Now,
                        updated_date_time = DateTime.Now
                    });
                    invoiceTotal += lineSum;
                }

                // 7️⃣ Invoice yozish (prixod)
                if (invoiceItems.Count > 0)
                {
                    var fromSkladName = "";
                    var fromSklad = await _context.TegirmonSklad.FindAsync((long)dto.from_sklad_id);
                    if (fromSklad != null && !string.IsNullOrWhiteSpace(fromSklad.name))
                    {
                        fromSkladName = fromSklad.name;
                    }

                    var invoice = new TegirmonInvoice
                    {
                        active_status = true,
                        note = string.IsNullOrWhiteSpace(fromSkladName) ? "" : (fromSkladName + " dan o'tkazma qilindi."),
                        user_name = dto.user_name,
                        auth_user_name = dto.user_name,
                        summ = invoiceTotal,
                        pol_all_sum = 0,
                        status_inv_type_name = "SKLADGA_BOSHQA_SKLADDAN",
                        inv_accepted_status = true,
                        TegirmonSkladid = dto.to_sklad_id,
                        prixod_status = 3,
                        TegirmonAuthid = dto.auth_id,
                        auth_user_creator_id = dto.auth_id,
                        auth_user_updator_id = dto.auth_id,
                        created_date_time = DateTime.Now,
                        updated_date_time = DateTime.Now,
                        item_list = invoiceItems
                    };

                    _context.TegirmonInvoice.Add(invoice);
                    await _context.SaveChangesAsync();
                }

                // 8️⃣ Barchasini DB ga saqlash
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Transfer muvaffaqiyatli bajarildi" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Transferda xatolik yuz berdi", error = ex.Message });
            }
        }



        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSklad> categoryList = await _context.TegirmonSklad
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSklad>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSklad.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonSklad>> GetTegirmonSklad(long id)
        {
            var tegirmonSklad = await _context.TegirmonSklad.FindAsync(id);

            if (tegirmonSklad == null)
            {
                return NotFound();
            }

            return tegirmonSklad;
        }



        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonSklad(long id, TegirmonSklad tegirmonSklad)
        {
            if (id != tegirmonSklad.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonSklad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonSkladExists(id))
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

        // POST: api/TegirmonUser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonSklad>> PostTegirmonSklad(TegirmonSklad tegirmonSklad)
        {
            _context.TegirmonSklad.Update(tegirmonSklad);
            await _context.SaveChangesAsync();
            return tegirmonSklad;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonSklad>> DeleteTegirmonSklad(long id)
        {
            var tegirmonSklad = await _context.TegirmonSklad.FindAsync(id);
            if (tegirmonSklad == null)
            {
                return NotFound();
            }

            _context.TegirmonSklad.Remove(tegirmonSklad);
            await _context.SaveChangesAsync();

            return tegirmonSklad;
        }

        private bool TegirmonSkladExists(long id)
        {
            return _context.TegirmonSklad.Any(e => e.id == id);
        }
    }
}
