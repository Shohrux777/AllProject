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
    public class TegirmonOrderController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrder>>> GetTegirmonOrder()
        {
            return await _context.TegirmonOrder.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrder> categoryList = await _context.TegirmonOrder
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrder>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrder
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrder>> GetTegirmonOrder(long id)
        {
            var tegirmonOrder = await _context.TegirmonOrder
                .Include(p => p.client)
                .Include(p => p.item_list)
                .ThenInclude(p => p.product)// bog‘langan narxlar ham chiqadi
                .FirstOrDefaultAsync(p => p.id == id);

            if (tegirmonOrder == null)
            {
                return NotFound();
            }

            // Listni teskari qilish
            tegirmonOrder.item_list = tegirmonOrder.item_list
                .OrderBy(pp => pp.id) // yoki boshqa maydon bo‘yicha
                .ToList();

            return tegirmonOrder;
        }

        // Yopilmagan zakazlar
        [HttpGet("getByDateNotClosed")]
        public async Task<IActionResult> getByDateNotClosed(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrder
                .Include(x => x.client)
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true  && o.IsClosed == false);

            // agar clientId 0 bo'lmasa, filterlash
            if (clientId != 0)
            {
                order = order.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

        // To'lov qilinga lekin yopilmagan zakazlar
        [HttpGet("getByDateNotClosedButPaid")]
        public async Task<IActionResult> getByDateNotClosedButPaid(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrder
                .Include(x => x.client)
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true  && o.IsClosed == false && o.IsPaid == true );

            // agar clientId 0 bo'lmasa, filterlash
            if (clientId != 0)
            {
                order = order.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

         // Yuklangan lekin Yopilmagan
        [HttpGet("getByDateNotClosedButLoaded")]
        public async Task<IActionResult> getByDateNotClosedButLoaded(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrder
                .Include(x => x.client)
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true  && o.IsClosed == false && o.IsLoaded == true );

            // agar clientId 0 bo'lmasa, filterlash
            if (clientId != 0)
            {
                order = order.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }

        // zakazlar sonini chiqaradi
        [HttpGet("getOrderStatsByDate")]
        public async Task<IActionResult> GetOrderStatsByDate(DateTime date, long clientId = 0)
        {
            // bazaga query tayyorlash
            var query = _context.TegirmonOrder.AsQueryable();

            // filterlash: sana va active_status
            query = query.Where(o => o.pickUpDate.Date == date.Date && o.active_status == true);

            // agar clientId berilgan bo‘lsa, filterlash
            if (clientId != 0)
            {
                query = query.Where(o => o.TegirmonOrderClientid == clientId);
            }

            // barcha mos kelgan orderlarni olish
            var orders = await query.ToListAsync();

            // statistika hisoblash
            var totalCount = orders.Count;
            var notClosedCount = orders.Count(o => o.IsClosed == false);
            var notClosedPaidCount = orders.Count(o => o.IsClosed == false && o.IsPaid == true);
            var notClosedLoadedCount = orders.Count(o => o.IsClosed == false && o.IsLoaded == true);
            var closedCount = orders.Count(o => o.IsClosed == true);

            // javob qaytarish
            return Ok(new
            {
                totalCount,
                notClosedCount,
                notClosedPaidCount,
                notClosedLoadedCount,
                closedCount
            });
        }

         [HttpGet("getByDateClosedOrder")]
        public async Task<IActionResult> getByDateClosedOrder(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrder
                .Include(x => x.client)
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true  && o.IsClosed == true);

            // agar clientId 0 bo'lmasa, filterlash
            if (clientId != 0)
            {
                order = order.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await order.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }


        [HttpGet("getByDate")]
        public async Task<IActionResult> GetOrdersByDate(DateTime date, long clientId = 0)
        {
            // faqat kunni tekshirish (soatlarni inobatga olmay)
            var order = _context.TegirmonOrder
                .Include(x => x.client)
                .Include(x => x.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)

                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true);

            // agar clientId 0 bo'lmasa, filterlash
            if (clientId != 0)
            {
                order = order.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await order
            .OrderBy(o => o.IsPaid)      // false -> true
            .ThenBy(o => o.IsLoaded)     // false -> true
            .ThenBy(o => o.id)           // keyin id bo‘yicha
            .ToListAsync();

            return Ok(result);
        }
        // ortilgan tulov qilingan lekin puli kassaga kelmaganlar
        [HttpGet("getLoadedUnpaidOrders")]
        public async Task<IActionResult> GetLoadedUnpaidOrders()
        {
            var orders = await _context.TegirmonOrder
                .Include(o => o.client)
                .Include(o => o.item_list)
                .ThenInclude(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Where(o => (o.IsLoaded == true
                            && o.IsPaid == false
                            && o.pay_progress == 100
                            && o.active_status == true)
                             ||
                            (o.is_save_client == true 
                            && o.IsPaid == false 
                            && o.pay_progress == 100)
                            )
                .ToListAsync();
            return Ok(orders);
        }


        // oldingi kunda yopilmagan zakazlarni bugunga utkazish
        [HttpPost("checkAndUpdateOldOrders")]
        public async Task<IActionResult> CheckAndUpdateOldOrders()
        {
            var today = DateTime.Now.Date;
            var fiveDaysAgo = today.AddDays(-10);

            // 1. pickUpDate 5 kun oldingi bo‘lgan orderlarni topish
            var oldOrders = await _context.TegirmonOrder
                .Where(o => o.pickUpDate.Date >= fiveDaysAgo && o.pickUpDate.Date < today && o.active_status == true
                            && (o.IsLoaded == false || o.pay_progress < 100))
                .ToListAsync();

            // pickUpDate ni bugungi kunga yangilash
            foreach (var order in oldOrders)
            {
                order.pickUpDate = today;
                // xohlovchi bo‘lsa boshqa fieldlarni ham loglash mumkin
            }

            // bazaga saqlash
            await _context.SaveChangesAsync();

            return Ok(oldOrders);
        }



        [HttpGet("order-items-summary")]
        public async Task<IActionResult> GetOrderItemsSummary([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            // Agar end_date berilgan bo'lsa, vaqt oralig'ida ishlaydi, aks holda bitta kun
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1) // 4-chi sana olinmasligi uchun AddDays(1) va < finish shartida
                : begin_date.Date.AddDays(1);
            Console.WriteLine($"begin_date: {begin_date}, end_date: {end_date}, start: {start}, finish: {finish}");

            // 1️⃣ Asosiy zakaz (order) elementlari — qaysi mahsulotdan qancha buyurtma berilgan
            var orderItems = await _context.TegirmonOrderItem
                .Include(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Include(i => i.order)
                .Where(i => i.order.pickUpDate >= start && i.order.pickUpDate < finish && i.order.active_status == true)
                .ToListAsync();

            // 2️⃣ Mashinaga yuklanayotgan yoki yuklanib bo‘lgan mahsulotlar
            var carItems = await _context.TegirmonOrderCarItem
                .Include(ci => ci.product)
                .Include(ci => ci.orderCar)
                    .ThenInclude(oc => oc.order)
                .Where(ci => ci.orderCar.order.pickUpDate >= start && ci.orderCar.order.pickUpDate < finish && ci.orderCar.order.active_status == true)
                .ToListAsync();

            // 3️⃣ Natijani mahsulot bo‘yicha guruhlab hisoblash
            var result = orderItems
                .GroupBy(i => i.TegirmonProductid)
                .Select(g =>
                {
                    var product = g.First().product;
                    var totalQty = g.Sum(x => x.qty);

                    // Shu mahsulotga tegishli yuklangan/yuklanayotgan yozuvlar
                    var carGroup = carItems.Where(c => c.TegirmonProductid == g.Key);

                    var loadedQty = carGroup
                        .Where(c => c.orderCar.is_begin == true) // ✅ ortib bo‘lganlar
                        .Sum(c => c.qty);

                    var loadingQty = carGroup
                        .Where(c => c.orderCar.is_begin == false) // ⏳ hozir ortilayotganlar
                        .Sum(c => c.qty);

                    return new
                    {
                        ProductId = g.Key,
                        ProductName = product?.name,
                        Unit = product?.unitMeasurment.name,
                        product = product,
                        TotalQty = totalQty,
                        LoadedQty = loadedQty,
                        LoadingQty = loadingQty,
                        RemainingQty = totalQty - (loadedQty + loadingQty)
                    };
                })
                .OrderByDescending(r => r.TotalQty * r.product.real_qty)
                .ToList();

            return Ok(result);
        }

        // clientning oxirgi 30 kundagi olgan mahsulotlari kg si

        [HttpGet("last30days-products/{clientId}")]
        public async Task<IActionResult> GetLast30DaysProducts(long clientId)
        {
            var fromDate = DateTime.Now.AddDays(-30);

            var result = await _context.TegirmonOrder
                .Where(o => o.TegirmonOrderClientid == clientId && o.pickUpDate >= fromDate)
                .SelectMany(o => o.item_list)
                .Where(i => i.product != null) // product null bo‘lsa xatolik chiqmasligi uchun
                .GroupBy(i => new { i.TegirmonProductid, i.product.name, i.product.real_qty })
                .Select(g => new
                {
                    productId = g.Key.TegirmonProductid,
                    product_name = g.Key.name,
                    product_kg = g.Sum(x => x.qty * g.Key.real_qty),
                    // kg/hajmni hisoblash
                    qty = g.Sum(x => x.qty)
                })
                .OrderByDescending(x => x.qty)
                .ToListAsync();

            return Ok(result);
        }




    [HttpGet("client-stats/{clientId}")]
    public async Task<IActionResult> GetClientStats(int clientId)
    {
        // 1. Zakazlar soni
        var totalOrders = await _context.TegirmonOrder
            .Where(o => o.TegirmonOrderClientid == clientId)
            .CountAsync();

        if (totalOrders == 0)
        {
            return Ok(new
            {
                clientId,
                totalOrders = 0,
                totalProducts = 0,
                totalAmountPaid = 0,
                daysSinceLastOrder = 0,
            });
        }

            // 2. Umumiy mahsulot (qty yoki real_qty)
            var totalProducts = await _context.TegirmonOrderItem
            .Where(i => i.order.TegirmonOrderClientid == clientId)
            .SumAsync(i => (decimal?)(i.qty * i.product.real_qty)) ?? 0;


            // var totalProducts = await _context.TegirmonOrderItem
            //     .Where(i => i.order.TegirmonOrderClientid == clientId)
            //     .SumAsync(i => (decimal?)i.qty) ?? 0;


            // Agar siz `real_qty` ishlatmoqchi bo'lsangiz:
            // .SumAsync(i => (decimal?)i.real_qty) ?? 0

            // 3. Jami to'langan summa (TegirmonOrderCheck dan)
            var totalAmountPaid = await _context.TegirmonOrderCheck
                .Where(c => c.order.TegirmonOrderClientid == clientId)
                .SumAsync(c => (decimal?)c.summ) ?? 0;

            // 4. Oxirgi zakaz sanasi
            var lastOrderDate = await _context.TegirmonOrder
                .Where(o => o.TegirmonOrderClientid == clientId)
                .OrderByDescending(o => o.pickUpDate) // yoki created_at bo‘lsa shu bilan
                .Select(o => o.pickUpDate)
                .FirstOrDefaultAsync();

            int? daysSinceLastOrder = null;
            if (lastOrderDate != null)
            {
                daysSinceLastOrder = (DateTime.UtcNow.Date - lastOrderDate.Date).Days;
            }

            return Ok(new
            {
                clientId,
                totalOrders,
                totalProducts,
                totalAmountPaid,
                daysSinceLastOrder
            });
        }




        [HttpGet("order-items-summary-total")]
        public async Task<IActionResult> GetOrderItemsSummaryTotal(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            // Agar end_date berilgan bo'lsa, vaqt oralig'ida ishlaydi, aks holda bitta kun
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1) // 4-chi sana olinmasligi uchun AddDays(1) va < finish shartida
                : begin_date.Date.AddDays(1);

            Console.WriteLine($"begin_date: {begin_date}, end_date: {end_date}, start: {start}, finish: {finish}");
            // 🔹 1. Barcha zakazdagi mahsulotlar (va product bilan birga)
            var orderItems = await _context.TegirmonOrderItem
                .Include(i => i.product)
                .Include(i => i.order)
                .Where(i => i.order.pickUpDate >= start && i.order.pickUpDate < finish && i.order.active_status == true)
                .ToListAsync();

            // 🔹 2. Barcha yuk mashinalaridagi ortilgan/ortilayotgan mahsulotlar
            var carItems = await _context.TegirmonOrderCarItem
                .Include(ci => ci.product)
                .Include(ci => ci.orderCar)
                    .ThenInclude(oc => oc.order) // <-- orderni ham include qilamiz
                .Where(ci => ci.orderCar.order.pickUpDate >= start 
                        && ci.orderCar.order.pickUpDate < finish && ci.orderCar.order.active_status == true)
                .ToListAsync();

            // 🔹 3. Jami zakaz qilingan mahsulotlar
            double totalQty = orderItems.Sum(x => x.qty);
            double totalKg = orderItems.Sum(x => x.qty * (x.product?.real_qty ?? 0));

            // 🔹 4. Ortilgan mahsulotlar (is_begin == true)
            double loadedQty = carItems
                .Where(c => c.orderCar.is_begin == true)
                .Sum(c => c.qty);
            double loadedKg = carItems
                .Where(c => c.orderCar.is_begin == true)
                .Sum(c => c.qty * (c.product?.real_qty ?? 0));

            // 🔹 5. Hozir ortilayotgan mahsulotlar (is_begin == false)
            double loadingQty = carItems
                .Where(c => c.orderCar.is_begin == false)
                .Sum(c => c.qty);
            double loadingKg = carItems
                .Where(c => c.orderCar.is_begin == false)
                .Sum(c => c.qty * (c.product?.real_qty ?? 0));

            // 🔹 6. Hali mashinaga chiqmagan (omborda qolgan) mahsulotlar
            double remainingQty = totalQty - (loadedQty + loadingQty);
            double remainingKg = totalKg - (loadedKg + loadingKg);

            // 🔹 7. Natijani qaytarish
            return Ok(new
            {
                TotalQty = totalQty,
                TotalKg = totalKg,
                LoadedQty = loadedQty,
                LoadedKg = loadedKg,
                LoadingQty = loadingQty,
                LoadingKg = loadingKg,
                RemainingQty = remainingQty,
                RemainingKg = remainingKg
            });
        }


        [HttpPost("save-to-client-reserve")]
        public async Task<IActionResult> SaveToClientReserve([FromBody] long orderId)
        {
            // 1️⃣ Orderni topamiz
            var order = await _context.TegirmonOrder
                .Include(x => x.item_list)
                .FirstOrDefaultAsync(x => x.id == orderId & x.active_status == true);

            if (order == null)
                return NotFound("Order topilmadi.");

            if (order.TegirmonOrderClientid == null)
                return BadRequest("Orderda client biriktirilmagan.");

            // 2️⃣ Belgilarni o‘zgartiramiz
            order.is_save_client = true;
            order.active_status = false;
            order.paid_status = "Zaxiraga mahsulot qo'shildi << Optom savdo >>";

            // 3️⃣ Har bir mahsulot uchun tekshiramiz
            foreach (var item in order.item_list)
            {
                // Avval mavjud yozuvni topamiz
                var existingProduct = await _context.TegirmonOrderClientProduct
                    .FirstOrDefaultAsync(p =>
                        p.TegirmonOrderClientId == order.TegirmonOrderClientid &&
                        p.TegirmonProductid == item.TegirmonProductid);

                if (existingProduct != null)
                {
                    // 🔹 Mavjud bo‘lsa miqdorni oshiramiz
                    existingProduct.qty += item.qty;
                    existingProduct.real_qty += item.qty;
                }
                else
                {
                    // 🔹 Bo‘lmasa yangi yozuv qo‘shamiz
                    var clientProduct = new TegirmonOrderClientProduct
                    {
                        TegirmonOrderClientId = order.TegirmonOrderClientid.Value,
                        client_name = order.client_name,
                        TegirmonProductid = item.TegirmonProductid,
                        qty = item.qty,
                        real_qty = item.qty,
                        created_date = DateTime.Now
                    };

                    await _context.TegirmonOrderClientProduct.AddAsync(clientProduct);
                }
            }

            // 4️⃣ O‘zgarishlarni saqlaymiz
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Zakazdagi mahsulotlar klient zahirasiga muvaffaqiyatli qo‘shildi."
            });
        }





        // GET: api/TegirmonOrder/GetOrderSummaryByDate?date=2025-09-15&clientId=0 // zakazlarda bor yuklar soni
        [HttpGet("GetOrderSummaryByDate")]
        public async Task<IActionResult> GetOrderSummaryByDate(DateTime date, long clientId = 0)
        {
            // asosiy query
            var ordersQuery = _context.TegirmonOrder
                .Include(x => x.item_list)
                    .ThenInclude(i => i.product)
                        .ThenInclude(p => p.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true);

            // agar clientId berilgan bo'lsa filtrlaymiz
            if (clientId != 0)
            {
                ordersQuery = ordersQuery.Where(o => o.TegirmonOrderClientid == clientId);
            }

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
                    .FirstOrDefault() ?? 0
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

        // GET: api/TegirmonOrder/GetNotClosedOrderRemainingSummaryByDate?date=2025-09-15&clientId=0
        [HttpGet("GetNotClosedOrderRemainingSummaryByDate")]
        public async Task<IActionResult> GetNotClosedOrderRemainingSummaryByDate(DateTime date, long clientId = 0)
        {
            var ordersQuery = _context.TegirmonOrder
                .Include(x => x.item_list)
                    .ThenInclude(i => i.product)
                        .ThenInclude(p => p.unitMeasurment)
                .Where(o => o.pickUpDate.Date == date.Date && o.active_status == true && o.IsClosed == false);

            if (clientId != 0)
            {
                ordersQuery = ordersQuery.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var orderItems = await ordersQuery
                .SelectMany(o => o.item_list)
                .ToListAsync();

            var carItemsQuery = _context.TegirmonOrderCarItem
                .Include(ci => ci.orderCar)
                    .ThenInclude(oc => oc.order)
                .Where(ci => ci.orderCar.order.pickUpDate.Date == date.Date
                    && ci.orderCar.order.active_status == true
                    && ci.orderCar.order.IsClosed == false);

            if (clientId != 0)
            {
                carItemsQuery = carItemsQuery.Where(ci => ci.orderCar.order.TegirmonOrderClientid == clientId);
            }

            var carItems = await carItemsQuery.ToListAsync();

            var summary = orderItems
                .GroupBy(i => new
                {
                    ProductId = i.product.id,
                    ProductName = i.product.name,
                    UnitMeasurmentId = i.product.TegirmonUnitMeasurmentid,
                    UnitMeasurmentName = i.product.unitMeasurment != null ? i.product.unitMeasurment.name : null,
                    StockQty = _context.TegirmonSkladTovar
                    .Where(s => s.TegirmonProductId == i.product.id && s.TegirmonSkladId == 2)
                    .Select(s => (decimal?)s.qty)
                    .FirstOrDefault() ?? 0
                })
                .Select(g =>
                {
                    var totalQty = g.Sum(x => x.qty);
                    var carGroup = carItems.Where(c => c.TegirmonProductid == g.Key.ProductId);
                    var loadedQty = carGroup.Where(c => c.orderCar.is_begin == true).Sum(c => c.qty);
                    var loadingQty = carGroup.Where(c => c.orderCar.is_begin == false).Sum(c => c.qty);
                    var remainingQty = totalQty - (loadedQty + loadingQty);
                    if (remainingQty < 0) remainingQty = 0;

                    return new
                    {
                        g.Key.ProductId,
                        g.Key.ProductName,
                        g.Key.UnitMeasurmentId,
                        g.Key.UnitMeasurmentName,
                        StockQty = g.Key.StockQty,
                        TotalQty = remainingQty,
                        RemainingQty = remainingQty,
                        LoadedQty = loadedQty,
                        LoadingQty = loadingQty
                    };
                })
                .Where(x => x.TotalQty > 0)
                .OrderByDescending(x => x.TotalQty)
                .ToList();

            return Ok(summary);
        }

        // [HttpGet("getByMonth")]
        // public async Task<IActionResult> GetOrdersByMonth(DateTime date, long clientId = 0)
        // {
        //     int year = date.Year;
        //     int month = date.Month;

        //     var orders = _context.TegirmonOrder
        //         .AsNoTracking()
        //         .Include(x => x.client)
        //         .Include(x => x.item_list)
        //         .Where(o => o.pickUpDate.Year == year && o.pickUpDate.Month == month && o.active_status == true);

        //     if (clientId != 0)
        //     {
        //         orders = orders.Where(o => o.TegirmonOrderClientid == clientId);
        //     }

        //     var result = await orders.OrderBy(p => p.id).ToListAsync();

        //     return Ok(result);
        // }


        [HttpGet("getByMonth")]
        public async Task<IActionResult> GetOrdersByMonth(DateTime date, long clientId = 0)
        {
            int year = date.Year;
            int month = date.Month;

            // Oyning boshini topamiz
            var firstDayOfMonth = new DateTime(year, month, 1);
            // Oyning oxirgi kunini topamiz
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // 6 kun oldin va 6 kun keyin
            var fromDate = firstDayOfMonth.AddDays(-6);
            var toDate = lastDayOfMonth.AddDays(6);

            var orders = _context.TegirmonOrder
                .AsNoTracking()
                .Include(x => x.client)
                .Include(x => x.item_list)
                .Where(o =>
                    o.pickUpDate >= fromDate &&
                    o.pickUpDate <= toDate &&
                    o.active_status == true);

            if (clientId != 0)
            {
                orders = orders.Where(o => o.TegirmonOrderClientid == clientId);
            }

            var result = await orders.OrderBy(p => p.id).ToListAsync();

            return Ok(result);
        }


        // PUT: api/TegirmonOrder/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrder(long id, TegirmonOrder tegirmonOrder)
        {
            if (id != tegirmonOrder.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderExists(id))
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

        // POST: api/TegirmonOrder
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<TegirmonOrder>> PostTegirmonOrder(TegirmonOrder tegirmonOrder)
        // {
        //     _context.TegirmonOrder.Update(tegirmonOrder);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTegirmonOrder", new { id = tegirmonOrder.id }, tegirmonOrder);
        // }

        //Yangi productPrice bilan update qilish 
        [HttpPost]
        public async Task<ActionResult<TegirmonOrder>> PostTegirmonOrder(TegirmonOrder tegirmonOrder)
        {
            if (tegirmonOrder.id == 0)
            {
                // null kelgan bo‘lsa, bo‘sh list qilib olamiz
                tegirmonOrder.item_list ??= new List<TegirmonOrderItem>();
                // yangi product yaratish
                _context.TegirmonOrder.Add(tegirmonOrder);
                if(tegirmonOrder.sum == 0 && tegirmonOrder.dollor == 0)
                {
                    tegirmonOrder.pay_progress = 100;
                    tegirmonOrder.IsPaid = true;
                }
                await _context.SaveChangesAsync();

                foreach (var item in tegirmonOrder.item_list.Where(i => i.zaxira == true))
                {
                    var clientProduct = await _context.TegirmonOrderClientProduct
                        .FirstOrDefaultAsync(x =>
                            x.TegirmonOrderClientId == tegirmonOrder.TegirmonOrderClientid &&
                            x.TegirmonProductid == item.TegirmonProductid);

                    if (clientProduct != null)
                    {
                        // qty dan ayiramiz
                        clientProduct.qty = clientProduct.qty - (item.new_qty ?? 0);
                    }
                }
                await _context.SaveChangesAsync();

            }
            else
            {
                // Eski productni bazadan chaqiramiz
                var existingProduct = await _context.TegirmonOrder
                    .Include(p => p.item_list)
                    .FirstOrDefaultAsync(p => p.id == tegirmonOrder.id);

                if (existingProduct == null)
                    return NotFound();

                // Product ma'lumotlarini update qilamiz
                _context.Entry(existingProduct).CurrentValues.SetValues(tegirmonOrder);

                // Eski narxlarni o‘chirib tashlaymiz
                _context.TegirmonOrderItem.RemoveRange(existingProduct.item_list);

                // Yangi narxlarni qo‘shamiz
                if (tegirmonOrder.item_list != null && tegirmonOrder.item_list.Any())
                {
                    foreach (var price in tegirmonOrder.item_list)
                    {
                        // product bilan bog‘lashni unutmang
                        price.TegirmonOrderid = existingProduct.id;
                        existingProduct.item_list.Add(price);
                    }
                }

                if(existingProduct.sum == 0 && existingProduct.dollor == 0)
                {
                    existingProduct.pay_progress = 100;
                    existingProduct.IsPaid = true;
                }
                await _context.SaveChangesAsync();

                foreach (var item in tegirmonOrder.item_list.Where(i => i.zaxira == true))
                {
                    var clientProduct = await _context.TegirmonOrderClientProduct
                        .FirstOrDefaultAsync(x =>
                            x.TegirmonOrderClientId == tegirmonOrder.TegirmonOrderClientid &&
                            x.TegirmonProductid == item.TegirmonProductid);

                    if (clientProduct != null)
                    {
                        // qty dan ayiramiz
                        clientProduct.qty = clientProduct.qty - (item.new_qty ?? 0);
                    }
                }
                
                await _context.SaveChangesAsync();


                // 🔽 faqat update qilinganda kerak bo‘ladi

                // OrderCarlarni olib kelamiz
                var orderCars = await _context.TegirmonOrderCar
                    .Include(c => c.item_list)
                    .Where(c => c.TegirmonOrderid == tegirmonOrder.id && c.active_status == true)
                    .ToListAsync();

                // Order itemlarni faqat ProductId orqali tekshiramiz
                var validProductIds = tegirmonOrder.item_list.Select(i => i.TegirmonProductid).ToHashSet();

                var existingOrderwaiting = await _context.TegirmonOrder
                    .FirstOrDefaultAsync(o => o.id == tegirmonOrder.id);
                existingOrderwaiting.is_loading = orderCars.Any(c => c.is_begin == false);
                await _context.SaveChangesAsync();
                
                foreach (var car in orderCars)
                {
                    Console.WriteLine("CarItem ichiga kirdi");
                    // Car ichidagi productlar orasidan orderda mavjud bo‘lmaganlarini topamiz
                    var itemsToRemove = car.item_list
                        .Where(ci => !validProductIds.Contains(ci.TegirmonProductid))
                        .ToList();

                    if (itemsToRemove.Any())
                    {
                        Console.WriteLine("CarItem ichidan uchirdi");
                        _context.TegirmonOrderCarItem.RemoveRange(itemsToRemove);
                        await _context.SaveChangesAsync();
                    }
                }

                // 2. Order item qty yig'indisi
                var orderItemQtySum = tegirmonOrder.item_list.Sum(i => i.qty);

                // 3. OrderCar item qty yig'indisi
                var orderCarItemQtySum = await _context.TegirmonOrderCarItem
                    .Where(ci => ci.orderCar.TegirmonOrderid == tegirmonOrder.id && ci.active_status == true)
                    .SumAsync(ci => (double?)ci.qty) ?? 0.0;

                // 4. Foiz hisoblash
                double percentLoaded = 0;
                if (orderItemQtySum > 0)
                    percentLoaded = Math.Round((orderCarItemQtySum / orderItemQtySum) * 100.0, 0);

                // Bazadan olib kelamiz
                var existingOrder = await _context.TegirmonOrder
                    .FirstOrDefaultAsync(o => o.id == tegirmonOrder.id);

                if (existingOrder != null)
                {
                    existingOrder.load_progress = percentLoaded;
                    existingOrder.IsLoaded = (percentLoaded == 100);
                    if (percentLoaded != 100)
                        existingOrder.IsClosed = false;
                    await _context.SaveChangesAsync();
                }

                // 🔽 faqat update qilinganda to'lovlar foizi hisoblanadi pay_progress hisoblanadi

                // Zakazga tegishli birinchi checkni olish
                var firstCheck = await _context.TegirmonOrderCheck
                    .Where(c => c.TegirmonOrderid == tegirmonOrder.id)
                    .OrderBy(c => c.create_date) // eng eski checkni olish uchun
                    .FirstOrDefaultAsync();

                double kurs = 1.0; // default kurs
                if (firstCheck != null)
                {
                    kurs = firstCheck.kurs;
                    // Zakazdagi jami summa = sum + (dollor * kurs)
                    var orderTotalAmount = tegirmonOrder.sum + (tegirmonOrder.dollor * kurs);

                    // Zakazga qilingan to'lovlar summasi
                    var paidAmount = await _context.TegirmonOrderCheck
                        .Where(c => c.TegirmonOrderid == tegirmonOrder.id)
                        .SumAsync(c => (double?)
                            (
                                (c.cash + c.uz_card + c.click + c.humo + c.payme + c.paynet + c.uzumpay + c.sum_balance + c.skidka - c.online) +
                                ((c.dollor + c.dollor_balance - c.perchisleniya) * kurs)
                            )
                        ) ?? 0.0;
                    
                    var notInCashboxCount = await _context.TegirmonOrderCheck
                    .Where(c => c.TegirmonOrderid == tegirmonOrder.id && c.isInCashbox == false)
                    .CountAsync();
                    // Foiz hisoblash
                    double payPercent = 0;
                    if (orderTotalAmount > 0)
                        payPercent = (int)Math.Truncate((paidAmount / orderTotalAmount) * 100.0);

                    // Zakazga yozish
                    var existingOrderP = await _context.TegirmonOrder
                        .FirstOrDefaultAsync(o => o.id == tegirmonOrder.id);

                    if (existingOrderP != null)
                    {
                        existingOrderP.pay_progress = payPercent;
                        existingOrderP.IsPaid = (payPercent == 100 && notInCashboxCount == 0);
                        if (percentLoaded != 100)
                        {
                            existingOrder.IsClosed = false;
                        }
                        else if(existingOrderP.IsPaid == true && existingOrderP.IsLoaded == true)
                        {
                            existingOrder.IsClosed = true;
                        }
                            
                        await _context.SaveChangesAsync();
                    }
                    
                }
                
            }

            return CreatedAtAction("GetTegirmonOrder", new { id = tegirmonOrder.id }, tegirmonOrder);
        }

        // DELETE: api/TegirmonOrder/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrder>> DeleteTegirmonOrder(long id)
        {
            var tegirmonOrder = await _context.TegirmonOrder.FindAsync(id);
            if (tegirmonOrder == null)
            {
                return NotFound();
            }
            if (tegirmonOrder.pay_progress > 0 || tegirmonOrder.IsPaid == true)
            {
                return BadRequest("Zakazga to'lov qilingan");
            }

            // 1️⃣ Avvalo shu orderga ulangan mashinalarni o'chiramiz
            var orderCars = _context.TegirmonOrderCar
                .Where(c => c.TegirmonOrderid == id)
                .ToList();
            if (orderCars.Any())
                _context.TegirmonOrderCar.RemoveRange(orderCars);

            

            _context.TegirmonOrder.Remove(tegirmonOrder);
            await _context.SaveChangesAsync();

            return tegirmonOrder;
        }

        // GET: api/TegirmonOrder/getGeneralStatistics
        // Umumiy orderlar statistikasi - pickUpDate bo'yicha filter qiladi
        [HttpGet("getGeneralStatistics")]
        public async Task<IActionResult> GetGeneralStatistics([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] int productsPage = 0, [FromQuery] int clientsPage = 0)
        {
            try
            {
                var query = _context.TegirmonOrder
                    .Where(o => o.active_status == true || (o.is_save_client == true && o.active_status == false))
                    .AsQueryable();

                // pickUpDate bo'yicha filter qilish
                if (startDate.HasValue)
                {
                    var start = startDate.Value.Date;
                    query = query.Where(o => o.pickUpDate.Date >= start);
                }
                if (endDate.HasValue)
                {
                    // Tugash sanasining kun oxirigacha (23:59:59)
                    var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(o => o.pickUpDate <= end);
                }

                // Barcha statistika shu vaqt oraligidagi zakazlardan olinadi
                var orders = await query
                    .Include(o => o.item_list)
                    .ThenInclude(i => i.product)
                    .ThenInclude(p => p.unitMeasurment)
                    .Include(o => o.client)
                    .ToListAsync();

                // Umumiy statistika
                var totalOrders = orders.Count(o => o.active_status == true);
                var closedOrders = orders.Count(o => o.IsClosed == true && o.active_status == true);
                var notClosedOrders = orders.Count(o => o.IsClosed == false && o.active_status == true);
                var paidOrders = orders.Count(o => o.IsPaid == true && o.active_status == true);
                var loadedOrders = orders.Count(o => o.IsLoaded == true && o.active_status == true);
                var notPaidOrders = orders.Count(o => o.IsPaid == false && o.IsClosed == false && o.active_status == true);

                // Summa statistika
                var totalSum = orders.Sum(o => o.sum);
                var totalDollor = orders.Sum(o => o.dollor);
                var paidSum = orders.Where(o => o.IsPaid == true).Sum(o => o.sum);
                var paidDollor = orders.Where(o => o.IsPaid == true).Sum(o => o.dollor);
                var unpaidSum = orders.Where(o => o.IsPaid == false && o.IsClosed == false).Sum(o => o.sum);
                var unpaidDollor = orders.Where(o => o.IsPaid == false && o.IsClosed == false).Sum(o => o.dollor);

                // Mahsulotlar statistika - faqat item_list null bo'lmagan va product null bo'lmaganlar
                var allItems = orders
                    .Where(o => o.item_list != null && o.active_status == true)
                    .SelectMany(o => o.item_list)
                    .Where(i => i != null && i.product != null)
                    .ToList();

                // Umumiy mahsulot soni (qty)
                var totalProducts = allItems.Sum(i => i.qty);

                // Mahsulotlar bo'yicha guruhlab
                var productsByType = allItems
                    .GroupBy(i => new
                    {
                        ProductId = i.TegirmonProductid,
                        ProductName = i.product.name ?? "Noma'lum",
                        Unit = i.product.unitMeasurment != null ? i.product.unitMeasurment.name : "",
                        RealQty = i.product.real_qty ?? 1.0
                    })
                    .Select(g => new
                    {
                        productId = g.Key.ProductId,
                        productName = g.Key.ProductName,
                        unit = g.Key.Unit,
                        // Shtuk miqdori
                        totalQty = g.Sum(i => i.qty),
                        // Kg hisobida miqdor (qty * real_qty)
                        totalQtyKg = g.Sum(i => i.qty * g.Key.RealQty),
                        totalOrders = g.Select(i => i.TegirmonOrderid).Distinct().Count()
                    })
                    // Top mahsulotlarni kg bo'yicha tartiblash
                    .OrderByDescending(p => p.totalQtyKg)
                    .Skip(productsPage * 10) // Pagination: har bir sahifada 10 ta
                    .Take(10) // Top 10 mahsulotlar
                    .ToList();

                // Clientlar bo'yicha statistika - faqat client_id bor bo'lganlar
                var clientsByOrders = orders
                    .Where(o => o.TegirmonOrderClientid.HasValue && o.TegirmonOrderClientid.Value > 0)
                    .GroupBy(o => o.TegirmonOrderClientid.Value)
                    .Select(g => new
                    {
                        ClientId = g.Key,
                        ClientName = !string.IsNullOrEmpty(g.First().client_name) 
                            ? g.First().client_name 
                            : (g.First().client != null && !string.IsNullOrEmpty(g.First().client.fio))
                                ? g.First().client.fio
                                : (g.First().client != null && !string.IsNullOrEmpty(g.First().client.name))
                                    ? g.First().client.name
                                    : "Noma'lum client",
                        TotalOrders = g.Count(),
                        TotalSum = g.Sum(o => o.sum),
                        TotalDollor = g.Sum(o => o.dollor),
                        ClosedOrders = g.Count(o => o.IsClosed == true),
                        UnpaidOrders = g.Count(o => o.IsPaid == false && o.IsClosed == false),
                        // Bu clientga tegishli order IDlar
                        OrderIds = g.Select(o => o.id).ToList()
                    })
                    .Where(c => c.TotalSum > 0 || c.TotalDollor > 0 || c.TotalOrders > 0) // Faqat ma'lumot bor bo'lganlar
                    .ToList();

                // Barcha order IDlarni olamiz
                var allOrderIds = clientsByOrders.SelectMany(c => c.OrderIds).Distinct().ToList();

                // Barcha TegirmonOrderCheck larni bir marta olamiz (optimizatsiya)
                // summ maydoni allaqachon barcha to'lov turlarini qo'shib hisoblab yozilgan
                var allChecks = await _context.TegirmonOrderCheck
                    .Where(check => check.TegirmonOrderid.HasValue && allOrderIds.Contains(check.TegirmonOrderid.Value))
                    .Select(check => new { check.TegirmonOrderid, check.summ })
                    .ToListAsync();

                // Har bir client uchun unga tegishli order IDlar bo'yicha TegirmonOrderCheck dagi checklardagi summ ni yig'indisini hisoblaymiz
                var clientsWithTotalSumma = clientsByOrders.Select(c => new
                {
                    c.ClientId,
                    c.ClientName,
                    c.TotalOrders,
                    c.TotalSum,
                    c.TotalDollor,
                    c.ClosedOrders,
                    c.UnpaidOrders,
                    // Bu clientga tegishli order IDlar bo'yicha TegirmonOrderCheck dagi barcha checklardagi summ yig'indisi
                    TotalSumma = allChecks
                        .Where(check => check.TegirmonOrderid.HasValue && c.OrderIds.Contains(check.TegirmonOrderid.Value))
                        .Sum(check => check.summ)
                })
                .OrderByDescending(c => c.TotalSumma) // TotalSumma bo'yicha tartiblash
                .Skip(clientsPage * 10) // Pagination: har bir sahifada 10 ta
                .Take(10) // Top 10 clientlar
                .ToList();

                return Ok(new
                {
                    // Umumiy statistika
                    totalOrders,
                    closedOrders,
                    notClosedOrders,
                    paidOrders,
                    loadedOrders,
                    notPaidOrders,
                    
                    // Summa statistika
                    totalSum,
                    totalDollor,
                    paidSum,
                    paidDollor,
                    unpaidSum,
                    unpaidDollor,
                    
                    // Mahsulotlar statistika
                    totalProducts,
                    topProducts = productsByType,
                    
                    // Clientlar statistika
                    topClients = clientsWithTotalSumma
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/TegirmonOrder/getClientProductsStatistics
        // Client mahsulotlari bo'yicha statistika - qaysi mahsulotdan qancha olgan
        [HttpGet("getClientProductsStatistics")]
        public async Task<IActionResult> GetClientProductsStatistics([FromQuery] long clientId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var query = _context.TegirmonOrder
                    .Where(o => o.active_status == true && 
                           o.TegirmonOrderClientid.HasValue && 
                           o.TegirmonOrderClientid.Value == clientId)
                    .AsQueryable();

                // pickUpDate bo'yicha filter qilish
                if (startDate.HasValue)
                {
                    var start = startDate.Value.Date;
                    query = query.Where(o => o.pickUpDate.Date >= start);
                }
                if (endDate.HasValue)
                {
                    var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(o => o.pickUpDate <= end);
                }

                // Orderlarni olish
                var orders = await query
                    .Include(o => o.item_list)
                    .ThenInclude(i => i.product)
                    .ThenInclude(p => p.unitMeasurment)
                    .Include(o => o.client)
                    .ToListAsync();

                // Client ma'lumotlari
                var client = orders.FirstOrDefault()?.client;
                var clientName = !string.IsNullOrEmpty(orders.FirstOrDefault()?.client_name)
                    ? orders.FirstOrDefault().client_name
                    : (client != null && !string.IsNullOrEmpty(client.fio))
                        ? client.fio
                        : (client != null && !string.IsNullOrEmpty(client.name))
                            ? client.name
                            : "Noma'lum client";

                // Mahsulotlar bo'yicha guruhlab, qty va kg hisoblaymiz
                var allItems = orders
                    .Where(o => o.item_list != null)
                    .SelectMany(o => o.item_list)
                    .Where(i => i != null && i.product != null)
                    .ToList();

                var productsByType = allItems
                    .GroupBy(i => new
                    {
                        ProductId = i.TegirmonProductid,
                        ProductName = i.product.name ?? "Noma'lum",
                        Unit = i.product.unitMeasurment != null ? i.product.unitMeasurment.name : "",
                        RealQty = i.product.real_qty ?? 1.0,
                        ShitrixCode = i.product.shitrix_code ?? ""
                    })
                    .Select(g => new
                    {
                        productId = g.Key.ProductId,
                        productName = g.Key.ProductName,
                        unit = g.Key.Unit,
                        shitrixCode = g.Key.ShitrixCode,
                        // Shtuk miqdori
                        totalQty = g.Sum(i => i.qty),
                        // Kg hisobida miqdor (qty * real_qty)
                        totalQtyKg = g.Sum(i => i.qty * g.Key.RealQty),
                        totalOrders = g.Select(i => i.TegirmonOrderid).Distinct().Count()
                    })
                    // Top mahsulotlarni kg bo'yicha tartiblash
                    .OrderByDescending(p => p.totalQtyKg)
                    .ToList();

                return Ok(new
                {
                    clientId = clientId,
                    clientName = clientName,
                    totalOrders = orders.Count,
                    products = productsByType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/TegirmonOrder/getProductClientsStatistics
        // Mahsulot bo'yicha clientlar statistika - kimlar qanchadan olgan
        [HttpGet("getProductClientsStatistics")]
        public async Task<IActionResult> GetProductClientsStatistics([FromQuery] long productId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var query = _context.TegirmonOrder
                    .Where(o => o.active_status == true)
                    .AsQueryable();

                // pickUpDate bo'yicha filter qilish
                if (startDate.HasValue)
                {
                    var start = startDate.Value.Date;
                    query = query.Where(o => o.pickUpDate.Date >= start);
                }
                if (endDate.HasValue)
                {
                    var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(o => o.pickUpDate <= end);
                }

                // Orderlarni olish
                var orders = await query
                    .Include(o => o.item_list)
                    .ThenInclude(i => i.product)
                    .ThenInclude(p => p.unitMeasurment)
                    .Include(o => o.client)
                    .ToListAsync();

                // Product ma'lumotlari
                var product = await _context.TegirmonProduct
                    .Include(p => p.unitMeasurment)
                    .FirstOrDefaultAsync(p => p.id == productId);

                if (product == null)
                {
                    return NotFound(new { error = "Mahsulot topilmadi" });
                }

                var productName = product.name ?? "Noma'lum";
                var unit = product.unitMeasurment != null ? product.unitMeasurment.name : "";
                var realQty = product.real_qty ?? 1.0;
                var shitrixCode = product.shitrix_code ?? "";

                // Bu mahsulotni olgan order itemlarni topamiz
                var productItems = orders
                    .Where(o => o.item_list != null)
                    .SelectMany(o => o.item_list)
                    .Where(i => i != null && 
                           i.product != null && 
                           i.TegirmonProductid == productId)
                    .ToList();

                // Clientlar bo'yicha guruhlab, qty va kg hisoblaymiz
                // Client bo'lmagan orderlar uchun 0 ishlatamiz
                var clientsByProduct = productItems
                    .GroupBy(i => i.order.TegirmonOrderClientid ?? 0)
                    .Select(g => new
                    {
                        ClientId = g.Key,
                        ClientName = g.Key == 0
                            ? "Noma'lum"
                            : (!string.IsNullOrEmpty(g.First().order.client_name)
                                ? g.First().order.client_name
                                : (g.First().order.client != null && !string.IsNullOrEmpty(g.First().order.client.fio))
                                    ? g.First().order.client.fio
                                    : (g.First().order.client != null && !string.IsNullOrEmpty(g.First().order.client.name))
                                        ? g.First().order.client.name
                                        : "Noma'lum"),
                        // Shtuk miqdori
                        totalQty = g.Sum(i => i.qty),
                        // Kg hisobida miqdor (qty * real_qty)
                        totalQtyKg = g.Sum(i => i.qty * realQty),
                        totalOrders = g.Select(i => i.TegirmonOrderid).Distinct().Count()
                    })
                    // Top clientlarni kg bo'yicha tartiblash
                    .OrderByDescending(c => c.totalQtyKg)
                    .ToList();

                return Ok(new
                {
                    productId = productId,
                    productName = productName,
                    unit = unit,
                    shitrixCode = shitrixCode,
                    totalClients = clientsByProduct.Count,
                    clients = clientsByProduct
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/TegirmonOrder/getOrderDailyStats
        // pickUpDate bo'yicha kunlik zakazlar soni va kg hisobida mahsulotlar
        [HttpGet("getOrderDailyStats")]
        public async Task<IActionResult> GetOrderDailyStats([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var query = _context.TegirmonOrder
                    .Where(o => o.active_status == true)
                    .AsQueryable();

                if (startDate.HasValue)
                {
                    var start = startDate.Value.Date;
                    query = query.Where(o => o.pickUpDate.Date >= start);
                }
                if (endDate.HasValue)
                {
                    var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(o => o.pickUpDate <= end);
                }

                var orders = await query.ToListAsync();

                // Shu sanali orderlar uchun order itemlarni ham olib kelamiz
                var orderIds = orders.Select(o => o.id).ToList();

                var orderItems = await _context.TegirmonOrderItem
                    .Include(i => i.product)
                    .Include(i => i.order)
                    .Where(i => orderIds.Contains(i.TegirmonOrderid))
                    .ToListAsync();

                // Itemlarni kun bo'yicha guruhlab, kg hisobida jami miqdorni topamiz
                var itemsByDate = orderItems
                    .GroupBy(i => i.order.pickUpDate.Date)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(i => i.qty * (i.product.real_qty ?? 1.0))
                    );

                var dailyStats = orders
                    .GroupBy(o => o.pickUpDate.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new
                    {
                        date = g.Key,
                        totalOrders = g.Count(),
                        closedOrders = g.Count(o => o.IsClosed == true),
                        paidOrders = g.Count(o => o.IsPaid == true),
                        totalProductsKg = itemsByDate.ContainsKey(g.Key) ? itemsByDate[g.Key] : 0.0
                    })
                    .ToList();

                return Ok(dailyStats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        private bool TegirmonOrderExists(long id)
        {
            return _context.TegirmonOrder.Any(e => e.id == id);
        }
    }
}
