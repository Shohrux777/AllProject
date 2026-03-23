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
    public class TegirmonPaymentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonPaymentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonPayments>>> GetTegirmonPayments()
        {
            return await _context.TegirmonPayments.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonPayments> categoryList = await _context.TegirmonPayments
                .Include(p =>p.product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonPayments>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonPayments
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationSaledProductDetails")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSaledProductDetails([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonPayments> categoryList = await _context.TegirmonPayments
                .Include(p => p.check)
                .Include(p =>p.product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id
                && (p.check.TegirmonContragentid == null || p.check.TegirmonContragentid != 1)
                && p.check.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            
            // Circular reference oldini olish uchun check.payments ni null qilamiz
            foreach (var payment in categoryList)
            {
                if (payment.check != null)
                {
                    payment.check.payments = null;
                }
            }
            if (categoryList == null)
            {
                categoryList = new List<TegirmonPayments>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonPayments
                .Include(p => p.check)
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id
                && (p.check.TegirmonContragentid == null || p.check.TegirmonContragentid != 1)
                && p.check.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getSaledProductsQtyBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getSaledProductsQtyBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonSaledProductTemp> itemList = await _context.TegirmonSaledProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_real_qty, "+
                " sum(pit.summa) as total_saled_summa,  "+
                " sum(pit.profit_summ) as total_profit_summ " +
                " FROM tegirmon_payments pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_check c ON c.id = pit.\"TegirmonCheckid\" "+
                " WHERE pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (c.\"TegirmonContragentid\" IS NULL OR c.\"TegirmonContragentid\" != 1) "+
                " AND c.active_status = true "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonSaledProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonSaledProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_real_qty, "+
                " sum(pit.summa) as total_saled_summa,  "+
                " sum(pit.profit_summ) as total_profit_summ " +
                " FROM tegirmon_payments pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_check c ON c.id = pit.\"TegirmonCheckid\" "+
                " WHERE pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (c.\"TegirmonContragentid\" IS NULL OR c.\"TegirmonContragentid\" != 1) "+
                " AND c.active_status = true "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getSaledProductsQtyProductIdBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getSaledProductsQtyProductIdBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonSaledProductTemp> itemList = await _context.TegirmonSaledProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_real_qty, "+
                " sum(pit.summa) as total_saled_summa,  "+
                " sum(pit.profit_summ) as total_profit_summ " +
                " FROM tegirmon_payments pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_check c ON c.id = pit.\"TegirmonCheckid\" "+
                " WHERE p.id= " + product_id + " AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (c.\"TegirmonContragentid\" IS NULL OR c.\"TegirmonContragentid\" != 1) "+
                " AND c.active_status = true "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonSaledProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonSaledProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_real_qty, "+
                " sum(pit.summa) as total_saled_summa,  "+
                " sum(pit.profit_summ) as total_profit_summ " +
                " FROM tegirmon_payments pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_check c ON c.id = pit.\"TegirmonCheckid\" "+
                " WHERE p.id= " + product_id + " AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (c.\"TegirmonContragentid\" IS NULL OR c.\"TegirmonContragentid\" != 1) "+
                " AND c.active_status = true "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonPayments>> GetTegirmonPayments(long id)
        {
            var tegirmonPayments = await _context.TegirmonPayments.FindAsync(id);

            if (tegirmonPayments == null)
            {
                return NotFound();
            }

            return tegirmonPayments;
        }




        // [HttpGet("order-items-summary")]
        // public async Task<IActionResult> GetSellItemsSummary([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        // {
        //     var start = begin_date.Date;
        //     var finish = begin_date.Date.AddDays(1).AddTicks(-1);
        //     Console.WriteLine(end_date);

        //     // 1️⃣ Asosiy zakaz (order) elementlari — qaysi mahsulotdan qancha buyurtma berilgan
        //     var orderItems = await _context.TegirmonPayments
        //         .Include(i => i.product)
        //         .ThenInclude(i => i.unitMeasurment)
        //         .Include(i => i.check)
        //         .Where(i => i.check.create_date >= start && i.check.create_date < finish && i.check.active_status == true)
        //         .ToListAsync();

        //     // 3️⃣ Natijani mahsulot bo‘yicha guruhlab hisoblash
        //     var result = orderItems
        //         .GroupBy(i => i.TegirmonProductid)
        //         .Select(g =>
        //         {
        //             var product = g.First().product;
        //             var totalQty = g.Sum(x => x.qty);
        //             return new
        //             {
        //                 ProductId = g.Key,
        //                 ProductName = product?.name,
        //                 Unit = product?.unitMeasurment.name,
        //                 product = product,
        //                 TotalQty = totalQty,
        //             };
        //         })
        //         .OrderByDescending(r => r.TotalQty * r.product.real_qty)
        //         .ToList();

        //     return Ok(result);
        // }


        [HttpGet("order-items-summary")]
        public async Task<IActionResult> GetSellItemsSummary([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            // Agar end_date berilgan bo'lsa, vaqt oralig'ida ishlaydi, aks holda bitta kun
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1) // 4-chi sana olinmasligi uchun AddDays(1) va < finish shartida
                : begin_date.Date.AddDays(1);
            Console.WriteLine($"begin_date: {begin_date}, end_date: {end_date}, start: {start}, finish: {finish}");

            // 🟢 1️⃣ Sotuv bo‘limi (TegirmonPayments)
            var orderItems = await _context.TegirmonPayments
                .Include(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Include(i => i.check)
                .Where(i => i.check.create_date >= start && i.check.create_date < finish && i.check.TegirmonContragentid != 1 && i.check.active_status == true)
                .ToListAsync();

            // 🟣 2️⃣ Zaxira bo‘limi (TegirmonInvoiceItem)
            var invoiceItems = await _context.TegirmonInvoiceItem
                .Include(i => i.product)
                .ThenInclude(i => i.unitMeasurment)
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= start && i.created_date_time < finish)
                .ToListAsync();

            // 🔵 3️⃣ TegirmonSkladHistory dan AnotherQty ni olish
            // TegirmonSkladId == 1 va text == "skladdan_boshqa_skladga" yoki "ichki_mahsulot_berish"
            var skladHistoryItems = await _context.TegirmonSkladHistory
                .Where(h => h.create_date >= start 
                    && h.create_date < finish
                    && h.TegirmonSkladId == 1
                    && (h.text == "skladdan_boshqa_skladga" || h.text == "ichki_mahsulot_berish"))
                .ToListAsync();

            // 🧮 4️⃣ Sotuv bo‘yicha guruhlash
            var sellSummary = orderItems
                .GroupBy(i => i.TegirmonProductid)
                .Select(g =>
                {
                    var p = g.First().product;
                    return new
                    {
                        ProductId = g.Key,
                        Product = p,
                        SellQty = g.Sum(x => x.qty),
                        InvoiceAcceptedQty = 0.0,
                        InvoiceOnlyChangeQty = 0.0,
                        AnotherQty = 0.0,
                        AnotherTransferQty = 0.0,
                        AnotherInternalQty = 0.0
                    };
                });

            // 🧮 5️⃣ Zaxira accepted (inv_accepted_status = true)
            var invoiceAccepted = invoiceItems
                .Where(i => i.inv_accepted_status == true && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .GroupBy(i => i.TegirmonProductid)
                .Select(g =>
                {
                    var p = g.First().product;
                    return new
                    {
                        ProductId = g.Key,
                        Product = p,
                        SellQty = 0.0,
                        InvoiceAcceptedQty = g.Sum(x => x.qty),
                        InvoiceOnlyChangeQty = 0.0,
                        AnotherQty = 0.0,
                        AnotherTransferQty = 0.0,
                        AnotherInternalQty = 0.0
                    };
                });

            // 🧮 6️⃣ Zaxira only_change (inv_accepted_status = false)
            var invoiceOnlyChange = invoiceItems
                .Where(i => i.inv_accepted_status == false && i.active_status == true && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .GroupBy(i => i.TegirmonProductid)
                .Select(g =>
                {
                    var p = g.First().product;
                    return new
                    {
                        ProductId = g.Key,
                        Product = p,
                        SellQty = 0.0,
                        InvoiceAcceptedQty = 0.0,
                        InvoiceOnlyChangeQty = g.Sum(x => x.qty),
                        AnotherQty = 0.0,
                        AnotherTransferQty = 0.0,
                        AnotherInternalQty = 0.0
                    };
                });

            // 🧮 7️⃣ SkladHistory bo‘yicha AnotherQty guruhlash
            var skladHistorySummary = skladHistoryItems
                .GroupBy(h => h.TegirmonProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Product = (TegirmonProduct)null, // Keyinroq to'ldiriladi
                    SellQty = 0.0,
                    InvoiceAcceptedQty = 0.0,
                    InvoiceOnlyChangeQty = 0.0,
                    AnotherQty = g.Sum(x => x.qty),
                    AnotherTransferQty = g.Where(x => x.text == "skladdan_boshqa_skladga").Sum(x => x.qty),
                    AnotherInternalQty = g.Where(x => x.text == "ichki_mahsulot_berish").Sum(x => x.qty)
                })
                .ToList();

            // 🔗 8️⃣ Barchasini birlashtirish va umumiy hisoblash
            var combinedList = sellSummary
                .Concat(invoiceAccepted)
                .Concat(invoiceOnlyChange)
                .Concat(skladHistorySummary)
                .GroupBy(i => i.ProductId)
                .ToList();

            // Barcha product ID larni yig'ib, productlarni yuklab olamiz
            var allProductIds = combinedList.Select(g => g.Key).Distinct().ToList();
            var products = await _context.TegirmonProduct
                .Include(p => p.unitMeasurment)
                .Where(p => allProductIds.Contains(p.id))
                .ToListAsync();

            var productDict = products.ToDictionary(p => p.id);

            var combined = combinedList
                .Select(g =>
                {
                    var product = productDict.ContainsKey(g.Key) ? productDict[g.Key] : null;
                    var anotherQty = g.Sum(x => x.AnotherQty);
                    var anotherTransferQty = g.Sum(x => x.AnotherTransferQty);
                    var anotherInternalQty = g.Sum(x => x.AnotherInternalQty);
                    var sellQty = g.Sum(x => x.SellQty);
                    var invoiceAcceptedQty = g.Sum(x => x.InvoiceAcceptedQty);
                    var invoiceOnlyChangeQty = g.Sum(x => x.InvoiceOnlyChangeQty);
                    var totalQty = sellQty + invoiceAcceptedQty + invoiceOnlyChangeQty + anotherInternalQty;

                    return new
                    {
                        ProductId = g.Key,
                        product = product,
                        ProductName = product?.name,
                        Unit = product?.unitMeasurment?.name,
                        SellQty = Math.Round(sellQty, 1),
                        InvoiceAcceptedQty = Math.Round(invoiceAcceptedQty, 1),
                        InvoiceOnlyChangeQty = Math.Round(invoiceOnlyChangeQty, 1),
                        AnotherQty = Math.Round(anotherQty, 1),
                        AnotherTransferQty = Math.Round(anotherTransferQty, 1),
                        AnotherInternalQty = Math.Round(anotherInternalQty, 1),
                        TotalQty = Math.Round(totalQty, 1)
                    };
                })
                .OrderByDescending(x => x.TotalQty)
                .ToList();

            return Ok(combined);
        }

        [HttpGet("client-count-summary")]
        public async Task<IActionResult> GetClientCountSummary([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            // Agar end_date berilgan bo'lsa, vaqt oralig'ida ishlaydi, aks holda bitta kun
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1) // 4-chi sana olinmasligi uchun AddDays(1) va < finish shartida
                : begin_date.Date.AddDays(1);
            Console.WriteLine($"begin_date: {begin_date}, end_date: {end_date}, start: {start}, finish: {finish}");

            // Vaqt oralig'i uzunligini hisoblash
            var daysDiff = (finish.Date - start.Date).Days;
            
            // Oldingi vaqt oralig'i (bir xil uzunlikda)
            var prevStart = start.AddDays(-daysDiff);
            var prevFinish = start;

            // 🟢 1️⃣ Sotuv bo'limidan checklar soni (clientId ga bog'liq emas)
            var sellChecksCount = await _context.TegirmonCheck
                .Where(c => c.create_date >= start 
                    && c.create_date < finish && c.TegirmonContragentid != 1
                    && c.active_status == true)
                .CountAsync();

            // 🟢 1️⃣ Oldingi vaqt oralig'idagi checklar soni
            var prevSellChecksCount = await _context.TegirmonCheck
                .Where(c => c.create_date >= prevStart 
                    && c.create_date < prevFinish 
                    && c.active_status == true)
                .CountAsync();

            // 🟣 2️⃣ Zaxiradan olgan mijozlar soni (inv_accepted_status = true, clientId ga bog'liq)
            var invoiceAcceptedClients = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= start 
                    && i.created_date_time < finish
                    && i.inv_accepted_status == true 
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH"
                    && i.invoice.TegirmonClientid != null)
                .Select(i => i.invoice.TegirmonClientid)
                .Distinct()
                .CountAsync();

            // 🟣 2️⃣ Oldingi vaqt oralig'idagi zaxiradan olgan mijozlar soni
            var prevInvoiceAcceptedClients = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= prevStart
                    && i.created_date_time < prevFinish
                    && i.inv_accepted_status == true 
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH"
                    && i.invoice.TegirmonClientid != null)
                .Select(i => i.invoice.TegirmonClientid)
                .Distinct()
                .CountAsync();

            // 🟡 3️⃣ Almashtirib ketgan invoicelar soni (inv_accepted_status = false, clientId ga bog'liq emas)
            var invoiceOnlyChangeCount = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= start 
                    && i.created_date_time < finish
                    && i.inv_accepted_status == false 
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .Select(i => i.invoice.id)
                .Distinct()
                .CountAsync();

            // 🟡 3️⃣ Oldingi vaqt oralig'idagi almashtirib ketgan invoicelar soni
            var prevInvoiceOnlyChangeCount = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                .Where(i => i.created_date_time >= prevStart 
                    && i.created_date_time < prevFinish
                    && i.inv_accepted_status == false 
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .Select(i => i.invoice.id)
                .Distinct()
                .CountAsync();

            return Ok(new
            {
                SellChecksCount = sellChecksCount,
                PrevSellChecksCount = prevSellChecksCount,
                InvoiceAcceptedClientsCount = invoiceAcceptedClients,
                PrevInvoiceAcceptedClientsCount = prevInvoiceAcceptedClients,
                InvoiceOnlyChangeCount = invoiceOnlyChangeCount,
                PrevInvoiceOnlyChangeCount = prevInvoiceOnlyChangeCount
            });
        }

        // GET: api/TegirmonPayments/getProductSellDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductSellDetails")]
        public async Task<IActionResult> GetProductSellDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var payments = await _context.TegirmonPayments
                .Include(p => p.check)
                    .ThenInclude(c => c.auth)
                        .ThenInclude(a => a.user)
                .Include(p => p.product)
                .Where(p => p.TegirmonProductid == product_id
                    && p.check.create_date >= start
                    && p.check.create_date < finish
                    && p.check.TegirmonContragentid != 1
                    && p.check.active_status == true)
                .OrderByDescending(p => p.check.create_date)
                .Select(p => new
                {
                    id = p.id,
                    checkId = p.TegirmonCheckid,
                    checkDate = p.check.create_date,
                    kassirName = p.check.auth != null && p.check.auth.user != null ? p.check.auth.user.fio : "Noma'lum",
                    qty = p.qty,
                    summa = p.summa,
                    profit_summ = p.profit_summ,
                    productName = p.product.name,
                    checkSumm = p.check.summ
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/TegirmonPayments/getProductInvoiceAcceptedDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductInvoiceAcceptedDetails")]
        public async Task<IActionResult> GetProductInvoiceAcceptedDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var invoiceItems = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                    .ThenInclude(inv => inv.client)
                .Include(i => i.product)
                .Where(i => i.TegirmonProductid == product_id
                    && i.created_date_time >= start
                    && i.created_date_time < finish
                    && i.inv_accepted_status == true
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .OrderByDescending(i => i.created_date_time)
                .Select(i => new
                {
                    id = i.id,
                    invoiceId = i.TegirmonInvoiceid,
                    invoiceDate = i.created_date_time,
                    clientName = i.invoice.client != null ? i.invoice.client.fio : "Noma'lum",
                    clientId = i.invoice.TegirmonClientid,
                    qty = i.qty,
                    real_qty = i.real_qty,
                    sum = i.sum,
                    real_sum = i.real_sum,
                    productName = i.product.name
                })
                .ToListAsync();

            return Ok(invoiceItems);
        }

        // GET: api/TegirmonPayments/getProductInvoiceOnlyChangeDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductInvoiceOnlyChangeDetails")]
        public async Task<IActionResult> GetProductInvoiceOnlyChangeDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var invoiceItems = await _context.TegirmonInvoiceItem
                .Include(i => i.invoice)
                .Include(i => i.product)
                .Where(i => i.TegirmonProductid == product_id
                    && i.created_date_time >= start
                    && i.created_date_time < finish
                    && i.inv_accepted_status == false
                    && i.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")
                .OrderByDescending(i => i.created_date_time)
                .Select(i => new
                {
                    id = i.id,
                    invoiceId = i.TegirmonInvoiceid,
                    invoiceDate = i.created_date_time,
                    qty = i.qty,
                    real_qty = i.real_qty,
                    sum = i.sum,
                    real_sum = i.real_sum,
                    productName = i.product.name,
                    note = i.invoice.note
                })
                .ToListAsync();

            return Ok(invoiceItems);
        }

        // GET: api/TegirmonPayments/getProductAnotherQtyDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductAnotherQtyDetails")]
        public async Task<IActionResult> GetProductAnotherQtyDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var historyItems = await _context.TegirmonSkladHistory
                .Where(h => h.TegirmonProductId == product_id
                    && h.create_date >= start
                    && h.create_date < finish
                    && h.TegirmonSkladId == 1
                    && (h.text == "skladdan_boshqa_skladga" || h.text == "ichki_mahsulot_berish"))
                .OrderByDescending(h => h.create_date)
                .Select(h => new
                {
                    id = h.id,
                    date = h.create_date,
                    qty = h.qty,
                    text = h.text,
                    userName = h.user_name,
                    note = h.note
                })
                .ToListAsync();

            return Ok(historyItems);
        }

        // GET: api/TegirmonPayments/getProductSkladTransferQtyDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductSkladTransferQtyDetails")]
        public async Task<IActionResult> GetProductSkladTransferQtyDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var historyItems = await _context.TegirmonSkladHistory
                .Where(h => h.TegirmonProductId == product_id
                    && h.create_date >= start
                    && h.create_date < finish
                    && h.TegirmonSkladId == 1
                    && h.text == "skladdan_boshqa_skladga")
                .OrderByDescending(h => h.create_date)
                .Select(h => new
                {
                    id = h.id,
                    date = h.create_date,
                    qty = h.qty,
                    text = h.text,
                    userName = h.user_name,
                    note = h.note
                })
                .ToListAsync();

            return Ok(historyItems);
        }

        // GET: api/TegirmonPayments/getProductInternalQtyDetails?product_id={id}&begin_date={date}&end_date={date}
        [HttpGet("getProductInternalQtyDetails")]
        public async Task<IActionResult> GetProductInternalQtyDetails([FromQuery] long product_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date != default(DateTime) && end_date.Date > begin_date.Date
                ? end_date.Date.AddDays(1)
                : begin_date.Date.AddDays(1);

            var historyItems = await _context.TegirmonSkladHistory
                .Where(h => h.TegirmonProductId == product_id
                    && h.create_date >= start
                    && h.create_date < finish
                    && h.TegirmonSkladId == 1
                    && h.text == "ichki_mahsulot_berish")
                .OrderByDescending(h => h.create_date)
                .Select(h => new
                {
                    id = h.id,
                    date = h.create_date,
                    qty = h.qty,
                    text = h.text,
                    userName = h.user_name,
                    note = h.note
                })
                .ToListAsync();

            return Ok(historyItems);
        }












        // PUT: api/TegirmonPayments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonPayments(long id, TegirmonPayments tegirmonPayments)
        {
            if (id != tegirmonPayments.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonPayments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonPaymentsExists(id))
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

        // POST: api/TegirmonPayments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonPayments>> PostTegirmonPayments(TegirmonPayments tegirmonPayments)
        {
            _context.TegirmonPayments.Update(tegirmonPayments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonPayments", new { id = tegirmonPayments.id }, tegirmonPayments);
        }

        // DELETE: api/TegirmonPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonPayments>> DeleteTegirmonPayments(long id)
        {
            var tegirmonPayments = await _context.TegirmonPayments.FindAsync(id);
            if (tegirmonPayments == null)
            {
                return NotFound();
            }

            _context.TegirmonPayments.Remove(tegirmonPayments);
            await _context.SaveChangesAsync();

            return tegirmonPayments;
        }

        private bool TegirmonPaymentsExists(long id)
        {
            return _context.TegirmonPayments.Any(e => e.id == id);
        }
    }
}
