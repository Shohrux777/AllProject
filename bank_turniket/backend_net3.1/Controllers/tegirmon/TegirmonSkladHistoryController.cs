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
    public class TegirmonSkladHistoryController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonSkladHistoryController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonSkladHistory>>> GetTegirmonSkladHistory()
        {
            return await _context.TegirmonSkladHistory.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladHistory> categoryList = await _context.TegirmonSkladHistory
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladHistory>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSkladHistory.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getByDateRange")]
        public async Task<ActionResult<TexPaginationModel>> GetByDateRange(
            [FromQuery] DateTime beginDate,
            [FromQuery] DateTime endDate
        )
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            DateTime endOfDay = endDate.Date.AddDays(1).AddTicks(-1);

            var list = await _context.TegirmonSkladHistory
                .Where(p =>
                    p.active_status == true &&
                    p.text == "skladdan_boshqa_skladga" &&
                    p.create_date >= beginDate.Date &&
                    p.create_date <= endOfDay
                )
                .Include(p => p.Sklad)
                .Include(p => p.Product)
                .OrderByDescending(p => p.id)
                .ToListAsync();

            var skladIds = list
                .Select(x => x.auth_user_updator_id)
                .Distinct()
                .ToList();

            var sklads = await _context.TegirmonSklad
                .Where(s => skladIds.Contains(s.id))
                .ToListAsync();

            var result = list.Select(h => new
            {
                History = h,
                FromSklad = h.Sklad,
                ToSklad = sklads.FirstOrDefault(s => s.id == h.auth_user_updator_id)
            }).ToList();

            // 🔴 ENG MUHIM JOY — result qaytadi
            paginationModel.items_list = JArray.FromObject(result);
            paginationModel.items_count = result.Count;
            paginationModel.current_item_count = result.Count;
            paginationModel.current_page = 0;

            return paginationModel;
        }


        [HttpGet("getSkladStatistika")]
        public async Task<IActionResult> GetSkladStatistika([FromQuery] int sklad_id)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var result = await _context.TegirmonSkladTovar
                .Where(x => x.TegirmonSkladId == sklad_id)
                .Include(x => x.Product) // product nomini olish uchun
                .ThenInclude(x => x.unitMeasurment)
                .Select(tovar => new
                {
                    productId = tovar.TegirmonProductId,
                    productName = tovar.Product.name,
                    product = tovar.Product,
                    currentQty = tovar.qty,
                    unitM = tovar.Product.unitMeasurment,

                    kirim = _context.TegirmonSkladHistory
                        .Where(h => h.TegirmonProductId == tovar.TegirmonProductId
                                    && h.TegirmonSkladId == sklad_id
                                    && h.type == 1
                                    && h.create_date >= today && h.create_date < tomorrow)
                        .Sum(h => (int?)h.qty) ?? 0,

                    chiqim = _context.TegirmonSkladHistory
                        .Where(h => h.TegirmonProductId == tovar.TegirmonProductId
                                    && h.TegirmonSkladId == sklad_id
                                    && h.type == 2
                                    && h.create_date >= today && h.create_date < tomorrow)
                        .Sum(h => (int?)h.qty) ?? 0
                })
                .OrderBy(x => x.currentQty) // 🔹 Kamdan ko‘pga qarab chiqarish
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("getSkladStatistikaNotSkladId")]
        public async Task<IActionResult> GetSkladStatistikaNotSkladId()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var result = await _context.TegirmonSkladTovar
                .Include(x => x.Product) // product nomini olish uchun
                .ThenInclude(x => x.unitMeasurment)
                .Select(tovar => new
                {
                    productId = tovar.TegirmonProductId,
                    productName = tovar.Product.name,
                    product = tovar.Product,
                    currentQty = tovar.qty,
                    unitM = tovar.Product.unitMeasurment,

                    kirim = _context.TegirmonSkladHistory
                        .Where(h => h.TegirmonProductId == tovar.TegirmonProductId
                                    && h.type == 1
                                    && h.create_date >= today && h.create_date < tomorrow )
                        .Sum(h => (int?)h.qty) ?? 0,

                    chiqim = _context.TegirmonSkladHistory
                        .Where(h => h.TegirmonProductId == tovar.TegirmonProductId
                                    && h.type == 2
                                    && h.create_date >= today && h.create_date < tomorrow )
                        .Sum(h => (int?)h.qty) ?? 0
                })
                .OrderBy(x => x.currentQty) // 🔹 Kamdan ko‘pga qarab chiqarish
                .ToListAsync();

                var grouped = result
                .GroupBy(x => x.productId)
                .Select(g => new
                {
                    productId = g.Key,
                    productName = g.First().productName,
                    product = g.First().product,
                    unitM = g.First().unitM,

                    currentQty = g.Sum(x => x.currentQty),
                    kirim = g.Sum(x => x.kirim),
                    chiqim = g.Sum(x => x.chiqim)
                })
                .OrderBy(x => x.currentQty)
                .ToList();

            return Ok(grouped);
        }

        [HttpGet("getSkladHaftalikStatistika")]
            public async Task<IActionResult> GetSkladHaftalikStatistika([FromQuery] int sklad_id)
            {
                var today = DateTime.Today;
                var startDate = today.AddDays(-6); // oxirgi 7 kun

                var history = await _context.TegirmonSkladHistory
                    .Where(h => h.TegirmonSkladId == sklad_id && h.create_date >= startDate)
                    .Include(h => h.Product)
                    .ToListAsync();

                var result = history
                    .GroupBy(h => h.create_date.Date)
                    .Select(g => new
                    {
                        date = g.Key.ToString("yyyy-MM-dd"),
                        kirimSum = g.Where(x => x.type == 1) // 1 = kirim
                                    .Sum(x => x.qty * (x.Product?.real_qty ?? 1)), // kg bo'yicha
                        chiqimSum = g.Where(x => x.type == 2) // 2 = chiqim
                                    .Sum(x => x.qty * (x.Product?.real_qty ?? 1)) // kg bo'yicha
                    })
                    .OrderBy(x => x.date)
                    .ToList();

                return Ok(result);
            }

        public class TopProductDto
        {
            public string product_name { get; set; }
            public double Qty { get; set; }
        }

        [HttpGet("getSkladStatistikaTop")]
        public async Task<IActionResult> GetSkladStatistikaTop([FromQuery] int sklad_id)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var result = await _context.TegirmonSkladHistory
                .Where(x => x.type == 2 && x.create_date >= today && x.create_date < tomorrow && x.TegirmonSkladId == sklad_id ) // chiqimlarni olish
                .GroupBy(x => new { x.TegirmonProductId, x.Product.name }) // product name bilan guruhlash
                .Select(g => new TopProductDto
                {
                    product_name = g.Key.name,
                    Qty = g.Sum(x => x.qty)
                })
                .OrderByDescending(x => x.Qty)
                .Take(5)
                .ToListAsync();

            return Ok(result);
        }

        // YANGI API: Tarixiy kun uchun sklad statistika
        // date - tanlangan kun (yyyy-MM-dd formatida)
        // sklad_id - ixtiyoriy, berilsa faqat shu sklad, berilmasa barcha skladlar
        // TegirmonSkladDailyBalance dan kun oxiridagi qoldiqni oladi
        // TegirmonSkladHistory dan shu kungi kirim/chiqimlarni hisoblaydi
        [HttpGet("getSkladStatistikaByDate")]
        public async Task<IActionResult> GetSkladStatistikaByDate([FromQuery] string date, [FromQuery] int? sklad_id = null)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest(new { message = "Sana parametri kerak" });
            }

            if (!DateTime.TryParse(date, out var selectedDate))
            {
                return BadRequest(new { message = "Noto'g'ri sana formati" });
            }

            var startDate = selectedDate.Date;
            var endDate = startDate.AddDays(1);

            // TegirmonSkladDailyBalance dan tanlangan kundagi oxirgi qoldiqlarni olish
            // Har bir soatda yozilgan, oxirgisi kerak (har bir product + sklad kombinatsiyasi uchun)
            var dailyBalanceQuery = _context.TegirmonSkladDailyBalance
                .Where(x => x.Date >= startDate && x.Date < endDate);

            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                dailyBalanceQuery = dailyBalanceQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
            }

            var dailyBalances = await dailyBalanceQuery
                .Include(x => x.Product)
                    .ThenInclude(p => p.unitMeasurment)
                .ToListAsync();

            // Har bir product + sklad kombinatsiyasi uchun oxirgi qoldiqni olish
            // Dictionary kaliti sifatida oddiy string ishlatamiz: "productId_skladId"
            var endQtyDict = dailyBalances
                .GroupBy(x => new { x.TegirmonProductId, x.TegirmonSkladId })
                .ToDictionary(
                    g => $"{g.Key.TegirmonProductId}_{g.Key.TegirmonSkladId}",
                    g => g.OrderByDescending(x => x.Date).ThenByDescending(x => x.id).First().qty
                );

            // TegirmonSkladHistory dan shu kungi kirim/chiqimlarni olish
            var historyQuery = _context.TegirmonSkladHistory
                .Where(h => h.create_date >= startDate && h.create_date < endDate);

            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                historyQuery = historyQuery.Where(h => h.TegirmonSkladId == sklad_id.Value);
            }

            var historyStats = await historyQuery
                .GroupBy(h => new { h.TegirmonProductId, h.TegirmonSkladId })
                .Select(g => new
                {
                    g.Key.TegirmonProductId,
                    g.Key.TegirmonSkladId,
                    kirim = g.Where(x => x.type == 1).Sum(x => (double?)x.qty) ?? 0,
                    chiqim = g.Where(x => x.type == 2).Sum(x => (double?)x.qty) ?? 0
                })
                .ToListAsync();

            // Barcha mahsulotlarni olish - DailyBalance va History dan
            var allProductKeys = new HashSet<(long ProductId, long SkladId)>();
            
            foreach (var db in dailyBalances)
            {
                allProductKeys.Add((db.TegirmonProductId, db.TegirmonSkladId));
            }
            
            foreach (var hs in historyStats)
            {
                allProductKeys.Add((hs.TegirmonProductId, hs.TegirmonSkladId));
            }

            // Product ma'lumotlarini olish
            var productIds = allProductKeys.Select(k => k.ProductId).Distinct().ToList();
            var products = await _context.TegirmonProduct
                .Where(p => productIds.Contains(p.id))
                .Include(p => p.unitMeasurment)
                .ToListAsync();

            var productDict = products.ToDictionary(p => p.id);

            // Natijani tayyorlash
            var result = allProductKeys
                .Select(key => {
                    if (!productDict.TryGetValue(key.ProductId, out var product) || product == null) 
                        return null;

                    var dictKey = $"{key.ProductId}_{key.SkladId}";
                    var endQty = endQtyDict.ContainsKey(dictKey) 
                        ? endQtyDict[dictKey] 
                        : 0;

                    var historyStat = historyStats
                        .FirstOrDefault(h => h.TegirmonProductId == key.ProductId 
                                          && h.TegirmonSkladId == key.SkladId);

                    return new
                    {
                        productId = key.ProductId,
                        productName = product.name,
                        product = product,
                        unitM = product.unitMeasurment,
                        TegirmonSkladId = key.SkladId,
                        // Kun oxiridagi qoldiq (DailyBalance dan)
                        currentQty = endQty,
                        // Shu kungi kirim
                        kirim = historyStat?.kirim ?? 0,
                        // Shu kungi chiqim
                        chiqim = historyStat?.chiqim ?? 0
                    };
                })
                .Where(x => x != null)
                .ToList();

            // Agar sklad_id berilmagan bo'lsa, mahsulotlar bo'yicha guruhlash (barcha skladlar uchun)
            if (!sklad_id.HasValue || sklad_id.Value == 0)
            {
                result = result
                    .GroupBy(x => x.productId)
                    .Select(g => new
                    {
                        productId = g.Key,
                        productName = g.First().productName,
                        product = g.First().product,
                        unitM = g.First().unitM,
                        // Obshiy ko'rinishda sklad bo'yicha ajratmaymiz, shuning uchun 0 qo'yamiz
                        TegirmonSkladId = 0L,
                        currentQty = g.Sum(x => x.currentQty),
                        kirim = g.Sum(x => x.kirim),
                        chiqim = g.Sum(x => x.chiqim)
                    })
                    .ToList();
            }

            // Kamdan ko'pga qarab tartiblash
            result = result
                .OrderBy(x => x.currentQty)
                .ToList();

            return Ok(result);
        }

        // YANGI API: Top-5 mahsulotlar kg bo'yicha (qty * product.real_qty)
        // sklad_id berilsa - faqat shu skladdan, berilmasa yoki 0 bo'lsa - barcha skladdan obshiy
        // date berilsa o'sha kun bo'yicha, berilmasa bugungi kun bo'yicha
        // GET: api/TegirmonSkladHistory/getProductHistoryDetails?product_id={id}&sklad_id={id?}&date={date}&type={type?}
        [HttpGet("getProductHistoryDetails")]
        public async Task<IActionResult> GetProductHistoryDetails(
            [FromQuery] long product_id,
            [FromQuery] int? sklad_id,
            [FromQuery] string date,
            [FromQuery] int? type)
        {
            var query = _context.TegirmonSkladHistory
                .Include(h => h.Product)
                    .ThenInclude(p => p.unitMeasurment)
                .Include(h => h.Sklad)
                .Where(h => h.TegirmonProductId == product_id);

            // Sklad filter
            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                query = query.Where(h => h.TegirmonSkladId == sklad_id.Value);
            }

            // Date filter
            if (!string.IsNullOrEmpty(date))
            {
                var selectedDate = DateTime.Parse(date);
                var startDate = selectedDate.Date;
                var endDate = startDate.AddDays(1);
                query = query.Where(h => h.create_date >= startDate && h.create_date < endDate);
            }

            // Type filter (1-kirim, 2-chiqim, 3-kirim ko'chirish, 4-chiqim ko'chirish)
            if (type.HasValue)
            {
                query = query.Where(h => h.type == type.Value);
            }

            var result = await query
                .OrderByDescending(h => h.create_date)
                .Select(h => new
                {
                    id = h.id,
                    skladId = h.TegirmonSkladId,
                    skladName = h.Sklad != null ? h.Sklad.name : "Noma'lum",
                    productId = h.TegirmonProductId,
                    productName = h.Product != null ? h.Product.name : "Noma'lum",
                    unitM = h.Product != null && h.Product.unitMeasurment != null ? h.Product.unitMeasurment.name : "",
                    qty = h.qty,
                    type = h.type,
                    typeName = h.type == 1 ? "Kirim" : h.type == 2 ? "Chiqim" : h.type == 3 ? "Kirim ko'chirish" : h.type == 4 ? "Chiqim ko'chirish" : "Noma'lum",
                    text = h.text,
                    price = h.price,
                    userId = h.userId,
                    authId = h.authId,
                    userName = h.user_name,
                    photoUrl = h.photo_url,
                    createDate = h.create_date,
                    note = h.note
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("getTopProductsKg")]
        public async Task<IActionResult> GetTopProductsKg([FromQuery] int? sklad_id = null, [FromQuery] string date = null)
        {
            DateTime startDate;
            DateTime endDate;

            if (!string.IsNullOrEmpty(date))
            {
                // Frontenddan kelgan sanani (yyyy-MM-dd) formatida deb hisoblaymiz
                if (!DateTime.TryParse(date, out startDate))
                {
                    return BadRequest(new { message = "Noto'g'ri sana formati" });
                }
                startDate = startDate.Date;
                endDate = startDate.AddDays(1);
            }
            else
            {
                startDate = DateTime.Today;
                endDate = startDate.AddDays(1);
            }

            var query = _context.TegirmonSkladHistory
                .Include(x => x.Product)
                .Where(x => x.type == 2 && x.create_date >= startDate && x.create_date < endDate);

            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                query = query.Where(x => x.TegirmonSkladId == sklad_id.Value);
            }

            var result = await query
                .GroupBy(x => new { x.TegirmonProductId, x.Product.name, x.Product.real_qty })
                .Select(g => new
                {
                    productId = g.Key.TegirmonProductId,
                    product_name = g.Key.name,
                    qty = g.Sum(x => x.qty),
                    product_kg = g.Sum(x => x.qty * (g.Key.real_qty ?? 1.0))
                })
                .OrderByDescending(x => x.product_kg)
                .Take(5)
                .ToListAsync();

            return Ok(result);
        }


        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonSkladHistory>> GetTegirmonSkladHistory(long id)
        {
            var tegirmonSkladHistory = await _context.TegirmonSkladHistory.FindAsync(id);

            if (tegirmonSkladHistory == null)
            {
                return NotFound();
            }

            return tegirmonSkladHistory;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonSkladHistory(long id, TegirmonSkladHistory tegirmonSkladHistory)
        {
            if (id != tegirmonSkladHistory.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonSkladHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonSkladHistoryExists(id))
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
        public async Task<ActionResult<TegirmonSkladHistory>> PostTegirmonSkladHistory(TegirmonSkladHistory tegirmonSkladHistory)
        {
            _context.TegirmonSkladHistory.Update(tegirmonSkladHistory);
            await _context.SaveChangesAsync();
            return tegirmonSkladHistory;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonSkladHistory>> DeleteTegirmonSkladHistory(long id)
        {
            var tegirmonSkladHistory = await _context.TegirmonSkladHistory.FindAsync(id);
            if (tegirmonSkladHistory == null)
            {
                return NotFound();
            }

            _context.TegirmonSkladHistory.Remove(tegirmonSkladHistory);
            await _context.SaveChangesAsync();

            return tegirmonSkladHistory;
        }

        /// <summary>
        /// Tanlangan kundagi boshlang'ich qoldiq (Начальный остаток) bilan bir kun oldingi kundagi qoldiq (Остаток) ni solishtiradi
        /// </summary>
        [HttpGet("getSkladOstatkaComparison")]
        public async Task<IActionResult> GetSkladOstatkaComparison([FromQuery] string date, [FromQuery] int? sklad_id = null)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest(new { message = "Sana parametri kerak" });
            }

            if (!DateTime.TryParse(date, out var selectedDate))
            {
                return BadRequest(new { message = "Noto'g'ri sana formati" });
            }

            var selectedDateStart = selectedDate.Date;
            var selectedDateEnd = selectedDateStart.AddDays(1);
            var today = DateTime.Today;
            var isToday = selectedDateStart.Date == today.Date;
            
            // Bir kun oldingi kun uchun bazada mavjud bo'lgan eng oxirgi DailyBalance sanasini topish
            // Agar kecha kuni yozilmagan bo'lsa, undan oldingi oxirgi sanani olamiz
            var prevBalanceBaseQuery = _context.TegirmonSkladDailyBalance
                .Where(x => x.Date < selectedDateStart);

            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                prevBalanceBaseQuery = prevBalanceBaseQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
            }

            var lastPrevBalanceDate = await prevBalanceBaseQuery
                .OrderByDescending(x => x.Date)
                .Select(x => (DateTime?)x.Date)
                .FirstOrDefaultAsync();

            DateTime? previousDateStart = null;
            DateTime? previousDateEnd = null;

            if (lastPrevBalanceDate.HasValue)
            {
                previousDateStart = lastPrevBalanceDate.Value.Date;
                previousDateEnd = previousDateStart.Value.AddDays(1);
            }

            // Tanlangan kundagi ma'lumotlar
            // Agar bugungi kun bo'lsa, TegirmonSkladTovar dan currentQty ni olamiz (getSkladStatistika kabi)
            // Aks holda TegirmonSkladDailyBalance dan olamiz
            Dictionary<string, double> endQtyDict = new Dictionary<string, double>();
            
            if (isToday)
            {
                // Bugungi kun uchun TegirmonSkladTovar dan currentQty ni olish
                var skladTovarQuery = _context.TegirmonSkladTovar.AsQueryable();
                
                if (sklad_id.HasValue && sklad_id.Value > 0)
                {
                    skladTovarQuery = skladTovarQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                var skladTovars = await skladTovarQuery
                    .Include(x => x.Product)
                        .ThenInclude(p => p.unitMeasurment)
                    .ToListAsync();

                endQtyDict = skladTovars
                    .ToDictionary(
                        x => $"{x.TegirmonProductId}_{x.TegirmonSkladId}",
                        x => (double)x.qty
                    );
            }
            else
            {
                // Kechagi kunlar uchun TegirmonSkladDailyBalance dan olish
                var dailyBalanceQuery = _context.TegirmonSkladDailyBalance
                    .Where(x => x.Date >= selectedDateStart && x.Date < selectedDateEnd);

                if (sklad_id.HasValue && sklad_id.Value > 0)
                {
                    dailyBalanceQuery = dailyBalanceQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                var dailyBalances = await dailyBalanceQuery
                    .Include(x => x.Product)
                        .ThenInclude(p => p.unitMeasurment)
                    .ToListAsync();

                endQtyDict = dailyBalances
                    .GroupBy(x => new { x.TegirmonProductId, x.TegirmonSkladId })
                    .ToDictionary(
                        g => $"{g.Key.TegirmonProductId}_{g.Key.TegirmonSkladId}",
                        g => g.OrderByDescending(x => x.Date).ThenByDescending(x => x.id).First().qty
                    );
            }

            var historyQuery = _context.TegirmonSkladHistory
                .Where(h => h.create_date >= selectedDateStart && h.create_date < selectedDateEnd);

            if (sklad_id.HasValue && sklad_id.Value > 0)
            {
                historyQuery = historyQuery.Where(h => h.TegirmonSkladId == sklad_id.Value);
            }

            var historyStats = await historyQuery
                .GroupBy(h => new { h.TegirmonProductId, h.TegirmonSkladId })
                .Select(g => new
                {
                    g.Key.TegirmonProductId,
                    g.Key.TegirmonSkladId,
                    kirim = g.Where(x => x.type == 1).Sum(x => (double?)x.qty) ?? 0,
                    chiqim = g.Where(x => x.type == 2).Sum(x => (double?)x.qty) ?? 0
                })
                .ToListAsync();

            // Bir kun oldingi (yoki undan ham oldingi oxirgi) kundagi qoldiq
            // TegirmonSkladDailyBalance dagi qty ni bevosita olamiz
            var previousEndQtyDict = new Dictionary<string, double>();
            var previousDailyBalances = new List<TegirmonSkladDailyBalance>();

            if (previousDateStart.HasValue && previousDateEnd.HasValue)
            {
                var previousDailyBalanceQuery = _context.TegirmonSkladDailyBalance
                    .Where(x => x.Date >= previousDateStart.Value && x.Date < previousDateEnd.Value);

                if (sklad_id.HasValue && sklad_id.Value > 0)
                {
                    previousDailyBalanceQuery = previousDailyBalanceQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                previousDailyBalances = await previousDailyBalanceQuery
                    .Include(x => x.Product)
                        .ThenInclude(p => p.unitMeasurment)
                    .ToListAsync();

                previousEndQtyDict = previousDailyBalances
                    .GroupBy(x => new { x.TegirmonProductId, x.TegirmonSkladId })
                    .ToDictionary(
                        g => $"{g.Key.TegirmonProductId}_{g.Key.TegirmonSkladId}",
                        g => g.OrderByDescending(x => x.Date).ThenByDescending(x => x.id).First().qty
                    );
            }

            // Barcha mahsulotlarni olish
            var allProductKeys = new HashSet<(long ProductId, long SkladId)>();
            
            // endQtyDict dan kalitlarni parse qilib olish
            foreach (var key in endQtyDict.Keys)
            {
                var parts = key.Split('_');
                if (parts.Length == 2 && long.TryParse(parts[0], out var productId) && long.TryParse(parts[1], out var skladId))
                {
                    allProductKeys.Add((productId, skladId));
                }
            }
            
            foreach (var hs in historyStats)
            {
                allProductKeys.Add((hs.TegirmonProductId, hs.TegirmonSkladId));
            }

            foreach (var pdb in previousDailyBalances)
            {
                allProductKeys.Add((pdb.TegirmonProductId, pdb.TegirmonSkladId));
            }

            var productIds = allProductKeys.Select(k => k.ProductId).Distinct().ToList();
            var products = await _context.TegirmonProduct
                .Where(p => productIds.Contains(p.id))
                .Include(p => p.unitMeasurment)
                .ToListAsync();

            var productDict = products.ToDictionary(p => p.id);

            // Natijani tayyorlash - faqat farq bo'lgan mahsulotlarni qaytarish
            var result = allProductKeys
                .Select(key => {
                    if (!productDict.TryGetValue(key.ProductId, out var product) || product == null) 
                        return null;

                    var dictKey = $"{key.ProductId}_{key.SkladId}";

                    // Tanlangan kun oxiridagi qoldiq (DailyBalance dan)
                    var selectedEndQty = endQtyDict.ContainsKey(dictKey) ? endQtyDict[dictKey] : 0;

                    // Bir kun oldingi (yoki undan ham oldingi oxirgi) kun oxiridagi qoldiq (DailyBalance dan)
                    var previousEndQty = previousEndQtyDict.ContainsKey(dictKey) ? previousEndQtyDict[dictKey] : 0;

                    var historyStat = historyStats
                        .FirstOrDefault(h => h.TegirmonProductId == key.ProductId 
                                          && h.TegirmonSkladId == key.SkladId);

                    var kirim = historyStat?.kirim ?? 0;
                    var chiqim = historyStat?.chiqim ?? 0;

                    // Начальный остаток (tanlangan kun uchun) = currentQty - kirim + chiqim
                    var nachalniyOstatok = selectedEndQty - kirim + chiqim;

                    // Bir kun oldingi (yoki oxirgi mavjud) kundagi Остаток = DailyBalance dagi qty
                    double previousOstatok = previousEndQty;

                    // Farq = Начальный остаток - bir kun oldingi/oxirgi kundagi Остаток
                    double diff = nachalniyOstatok - previousOstatok;

                    // Faqat farq 1 ga teng yoki 1 dan katta bo'lgan mahsulotlarni qaytarish
                    if (Math.Abs(diff) < 1) // 1 dan kichik farqlarni e'tiborsiz qoldiramiz
                        return null;

                    return new
                    {
                        productId = key.ProductId,
                        productName = product.name,
                        product = product,
                        unitM = product.unitMeasurment,
                        TegirmonSkladId = key.SkladId,
                        nachalniyOstatok = nachalniyOstatok, // Начальный остаток (tanlangan kundagi)
                        previousOstatok = previousOstatok, // Bir kun oldingi kundagi Остаток
                        diff = diff // Farq
                    };
                })
                .Where(x => x != null)
                .ToList();

            // Agar sklad_id berilmagan bo'lsa, mahsulotlar bo'yicha guruhlash
            if (!sklad_id.HasValue || sklad_id.Value == 0)
            {
                result = result
                    .GroupBy(x => x.productId)
                    .Select(g => new
                    {
                        productId = g.Key,
                        productName = g.First().productName,
                        product = g.First().product,
                        unitM = g.First().unitM,
                        TegirmonSkladId = 0L,
                        nachalniyOstatok = g.Sum(x => x.nachalniyOstatok),
                        previousOstatok = g.Sum(x => x.previousOstatok),
                        diff = g.Sum(x => x.diff)
                    })
                    .Where(x => Math.Abs(x.diff) >= 1) // Farq 1 ga teng yoki 1 dan katta bo'lganlarini qaytarish
                    .ToList();
            }

            // Farq bo'yicha tartiblash (katta farqdan boshlab)
            result = result
                .OrderByDescending(x => Math.Abs(x.diff))
                .ToList();

            return Ok(result);
        }

        /// <summary>
        /// Sklad rasxod (chiqim) bilan sotuv miqdorini solishtiradi
        /// Faqat sklad_id = 1 uchun ishlaydi
        /// </summary>
        [HttpGet("getSkladRasxodVsSellComparison")]
        public async Task<IActionResult> GetSkladRasxodVsSellComparison([FromQuery] string date, [FromQuery] int sklad_id = 1)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest(new { message = "Sana parametri kerak" });
            }

            if (!DateTime.TryParse(date, out var selectedDate))
            {
                return BadRequest(new { message = "Noto'g'ri sana formati" });
            }

            // Faqat sklad_id = 1 uchun
            if (sklad_id != 1)
            {
                return BadRequest(new { message = "Bu API faqat sklad_id = 1 uchun ishlaydi" });
            }

            var selectedDateStart = selectedDate.Date;
            var selectedDateEnd = selectedDateStart.AddDays(1);

            // Sklad rasxod (chiqim) ni olish - TegirmonSkladHistory dan type = 2
            var skladRasxodData = await _context.TegirmonSkladHistory
                .Where(h => h.TegirmonSkladId == sklad_id
                         && h.type == 2 // chiqim
                         && h.create_date >= selectedDateStart 
                         && h.create_date < selectedDateEnd)
                .Include(h => h.Product)
                    .ThenInclude(p => p.unitMeasurment)
                .ToListAsync();

            var skladRasxodList = skladRasxodData
                .GroupBy(h => h.TegirmonProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Product = g.First().Product,
                    UnitM = g.First().Product != null && g.First().Product.unitMeasurment != null ? g.First().Product.unitMeasurment : null,
                    SkladRasxod = g.Sum(x => (double?)x.qty) ?? 0
                })
                .ToList();

            // Sotuv miqdorini olish - barcha turlardan (order-items-summary logikasi)
            // 🟢 1️⃣ Sotuv bo'limi (TegirmonPayments)
            var orderItems = await _context.TegirmonPayments
                .Include(i => i.product)
                    .ThenInclude(i => i.unitMeasurment)
                .Include(i => i.check)
                .Where(i => i.active_status == true
                         && i.check != null
                         && i.check.active_status == true
                         && i.check.create_date >= selectedDateStart 
                         && i.check.create_date < selectedDateEnd
                         && i.check.TegirmonContragentid != 1)
                .ToListAsync();

            // 🟣 2️⃣ Zaxira bo'limi (TegirmonInvoiceItem)
            var invoiceItems = await _context.TegirmonInvoiceItem
                .Include(i => i.product)
                    .ThenInclude(i => i.unitMeasurment)
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= selectedDateStart && i.created_date_time < selectedDateEnd)
                .ToListAsync();

            // 🔵 3️⃣ TegirmonSkladHistory dan AnotherQty ni olish
            var skladHistoryItems = await _context.TegirmonSkladHistory
                .Where(h => h.create_date >= selectedDateStart 
                    && h.create_date < selectedDateEnd
                    && h.TegirmonSkladId == 1
                    && (h.text == "skladdan_boshqa_skladga" || h.text == "ichki_mahsulot_berish"))
                .ToListAsync();

            // 🧮 Sotuv bo'yicha guruhlash
            var sellSummary = orderItems
                .GroupBy(i => i.TegirmonProductid)
                .Select(g => new
                {
                    ProductId = g.Key,
                    SellQty = g.Sum(x => x.qty)
                })
                .ToList();

            // 🧮 Zaxira accepted (inv_accepted_status = true)
            var invoiceAccepted = invoiceItems
                .Where(i => i.inv_accepted_status == true && i.active_status == true && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .GroupBy(i => i.TegirmonProductid)
                .Select(g => new
                {
                    ProductId = g.Key,
                    InvoiceAcceptedQty = g.Sum(x => x.qty)
                })
                .ToList();

            // 🧮 Zaxira only_change (inv_accepted_status = false)
            var invoiceOnlyChange = invoiceItems
                .Where(i => i.inv_accepted_status == false && i.active_status == true && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .GroupBy(i => i.TegirmonProductid)
                .Select(g => new
                {
                    ProductId = g.Key,
                    InvoiceOnlyChangeQty = g.Sum(x => x.qty)
                })
                .ToList();

            // 🧮 SkladHistory bo'yicha AnotherQty guruhlash
            var skladHistorySummary = skladHistoryItems
                .GroupBy(h => h.TegirmonProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    AnotherQty = g.Sum(x => x.qty)
                })
                .ToList();

            // 🔗 Barchasini birlashtirish va umumiy hisoblash
            var allProductIdsForSell = new HashSet<long>();
            sellSummary.ForEach(x => allProductIdsForSell.Add(x.ProductId));
            invoiceAccepted.ForEach(x => allProductIdsForSell.Add(x.ProductId));
            invoiceOnlyChange.ForEach(x => allProductIdsForSell.Add(x.ProductId));
            skladHistorySummary.ForEach(x => allProductIdsForSell.Add(x.ProductId));

            var sellDict = sellSummary.ToDictionary(x => x.ProductId, x => x.SellQty);
            var invoiceAcceptedDict = invoiceAccepted.ToDictionary(x => x.ProductId, x => x.InvoiceAcceptedQty);
            var invoiceOnlyChangeDict = invoiceOnlyChange.ToDictionary(x => x.ProductId, x => x.InvoiceOnlyChangeQty);
            var anotherQtyDict = skladHistorySummary.ToDictionary(x => x.ProductId, x => x.AnotherQty);

            var sellList = allProductIdsForSell
                .Select(productId =>
                {
                    var sellQty = sellDict.ContainsKey(productId) ? sellDict[productId] : 0;
                    var invoiceAcceptedQty = invoiceAcceptedDict.ContainsKey(productId) ? invoiceAcceptedDict[productId] : 0;
                    var invoiceOnlyChangeQty = invoiceOnlyChangeDict.ContainsKey(productId) ? invoiceOnlyChangeDict[productId] : 0;
                    var anotherQty = anotherQtyDict.ContainsKey(productId) ? anotherQtyDict[productId] : 0;
                    var totalQty = sellQty + invoiceAcceptedQty + invoiceOnlyChangeQty + anotherQty;

                    return new
                    {
                        ProductId = productId,
                        TotalQty = totalQty
                    };
                })
                .ToList();

            // Barcha product ID larni birlashtirish
            var allProductIds = new HashSet<long>();
            foreach (var item in skladRasxodList)
            {
                allProductIds.Add(item.ProductId);
            }
            foreach (var item in sellList)
            {
                allProductIds.Add(item.ProductId);
            }

            // Dictionary lar yaratish
            var skladRasxodDict = skladRasxodList.ToDictionary(x => x.ProductId, x => x);
            var sellTotalQtyDict = sellList.ToDictionary(x => x.ProductId, x => x.TotalQty);

            // Product ma'lumotlarini olish
            var productIds = allProductIds.ToList();
            var products = await _context.TegirmonProduct
                .Where(p => productIds.Contains(p.id))
                .Include(p => p.unitMeasurment)
                .ToListAsync();

            var productDict = products.ToDictionary(p => p.id);

            // Natijani tayyorlash
            var result = allProductIds
                .Select(productId =>
                {
                    if (!productDict.TryGetValue(productId, out var product) || product == null)
                        return null;

                    var skladRasxod = skladRasxodDict.ContainsKey(productId) ? skladRasxodDict[productId].SkladRasxod : 0;
                    var totalSellQty = sellTotalQtyDict.ContainsKey(productId) ? sellTotalQtyDict[productId] : 0;
                    var diff = skladRasxod - totalSellQty;

                    return new
                    {
                        productId = productId,
                        productName = product.name,
                        product = product,
                        unitM = product.unitMeasurment,
                        skladRasxod = skladRasxod,
                        sellQty = totalSellQty, // Barcha turlardan kelgan umumiy miqdor
                        diff = diff
                    };
                })
                .Where(x => x != null)
                .Where(x => x.skladRasxod > 0 || x.sellQty > 0) // Faqat rasxod yoki sotuv bo'lganlarini
                .OrderByDescending(x => Math.Abs(x.diff)) // Farq bo'yicha tartiblash
                .ToList();

            return Ok(result);
        }

        /// <summary>
        /// Sklad ostatka bo'yicha umumiy statistika va kategoriya bo'yicha statistika
        /// date - tanlangan kun (yyyy-MM-dd formatida)
        /// sklad_id - ixtiyoriy, berilsa faqat shu sklad, berilmasa barcha skladlar bo'yicha umumiy
        /// </summary>
        [HttpGet("getSkladOstatkaCategorySummary")]
        public async Task<IActionResult> GetSkladOstatkaCategorySummary([FromQuery] string date, [FromQuery] int? sklad_id = null)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest(new { message = "Sana parametri kerak" });
            }

            if (!DateTime.TryParse(date, out var selectedDate))
            {
                return BadRequest(new { message = "Noto'g'ri sana formati" });
            }

            var startDate = selectedDate.Date;
            var endDate = startDate.AddDays(1);
            var today = DateTime.Today;
            var isToday = startDate.Date == today.Date;
            var hasSkladFilter = sklad_id.HasValue && sklad_id.Value > 0;

            // Ostatka ma'lumotlarini olish
            var ostatkaList = new List<(long productId, TegirmonProduct product, double currentQty)>();

            if (isToday)
            {
                // Bugungi kun uchun TegirmonSkladTovar dan
                var skladTovarQuery = _context.TegirmonSkladTovar.AsQueryable();
                
                if (hasSkladFilter)
                {
                    skladTovarQuery = skladTovarQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                var skladTovars = await skladTovarQuery
                    .Include(x => x.Product)
                        .ThenInclude(p => p.unitMeasurment)
                    .ToListAsync();

                // Agar sklad filter yo'q bo'lsa, mahsulotlar bo'yicha guruhlash (barcha skladlar uchun)
                if (!hasSkladFilter)
                {
                    ostatkaList = skladTovars
                        .GroupBy(x => x.TegirmonProductId)
                        .Select(g => (
                            g.Key,
                            g.First().Product,
                            g.Sum(x => (double)x.qty)
                        ))
                        .ToList();
                }
                else
                {
                    ostatkaList = skladTovars.Select(tovar => 
                        (tovar.TegirmonProductId, tovar.Product, (double)tovar.qty)
                    ).ToList();
                }
            }
            else
            {
                // Kechagi kunlar uchun TegirmonSkladDailyBalance dan
                var dailyBalanceQuery = _context.TegirmonSkladDailyBalance
                    .Where(x => x.Date >= startDate && x.Date < endDate);

                if (hasSkladFilter)
                {
                    dailyBalanceQuery = dailyBalanceQuery.Where(x => x.TegirmonSkladId == sklad_id.Value);
                }

                var dailyBalances = await dailyBalanceQuery
                    .Include(x => x.Product)
                        .ThenInclude(p => p.unitMeasurment)
                    .ToListAsync();

                // Har bir product+sklad kombinatsiyasi uchun oxirgi qoldiqni olish
                var endQtyByProductSklad = dailyBalances
                    .GroupBy(x => new { x.TegirmonProductId, x.TegirmonSkladId })
                    .ToDictionary(
                        g => g.Key,
                        g => g.OrderByDescending(x => x.Date).ThenByDescending(x => x.id).First().qty
                    );

                // Mahsulotlar bo'yicha guruhlash - agar sklad filter yo'q bo'lsa, barcha skladlar bo'yicha yig'indini olish
                var endQtyDict = endQtyByProductSklad
                    .GroupBy(x => x.Key.TegirmonProductId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(x => x.Value) // Barcha skladlar bo'yicha yig'indi (yoki bitta sklad bo'lsa ham to'g'ri ishlaydi)
                    );

                var productIds = endQtyDict.Keys.ToList();
                var products = await _context.TegirmonProduct
                    .Where(p => productIds.Contains(p.id))
                    .Include(p => p.unitMeasurment)
                    .ToListAsync();

                var productDict = products.ToDictionary(p => p.id);

                ostatkaList = productIds
                    .Where(productId => productDict.ContainsKey(productId))
                    .Select(productId => 
                    {
                        var product = productDict[productId];
                        var currentQty = endQtyDict.ContainsKey(productId) ? endQtyDict[productId] : 0;
                        return (productId, product, currentQty);
                    })
                    .ToList();
            }

            // Umumiy statistika
            double totalOstatka = 0;
            double totalKg = 0;

            foreach (var item in ostatkaList)
            {
                var realQty = item.product.real_qty ?? 1.0;
                totalOstatka += item.currentQty;
                totalKg += item.currentQty * realQty;
            }

            // Kategoriya bo'yicha statistika
            var categories = await _context.TegirmonCategory
                .Where(c => c.active_status == true)
                .ToListAsync();

            var categoryStats = categories.Select(category =>
            {
                double categorySht = 0;
                double categoryKg = 0;

                foreach (var item in ostatkaList)
                {
                    if (item.product.bot_id == category.id)
                    {
                        var realQty = item.product.real_qty ?? 1.0;
                        categorySht += item.currentQty;
                        categoryKg += item.currentQty * realQty;
                    }
                }

                return new
                {
                    id = category.id,
                    name = category.name,
                    color = category.color,
                    sht = categorySht,
                    kg = categoryKg
                };
            })
            .Where(c => c.sht > 0 || c.kg > 0) // Faqat ma'lumot bor kategoriyalarni qaytarish
            .ToList();

            var result = new
            {
                totalOstatka = totalOstatka,
                totalKg = totalKg,
                categories = categoryStats
            };

            return Ok(result);
        }

        private bool TegirmonSkladHistoryExists(long id)
        {
            return _context.TegirmonSkladHistory.Any(e => e.id == id);
        }
    }
}
