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
    public class TegirmonOrderCheckController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderCheckController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderCheck
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderCheck>>> GetTegirmonOrderCheck()
        {
            return await _context.TegirmonOrderCheck.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderCheck> categoryList = await _context.TegirmonOrderCheck
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderCheck
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getNotInCashboxTotal")]
        public async Task<ActionResult<object>> GetNotInCashboxTotal(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            // Boshlanish va tugash vaqtini to‘liq kun oraliqqa sozlaymiz
            // var start = begin_date.Date;
            // var finish = end_date.Date.AddDays(1).AddTicks(-1);

            // isInCashbox = false bo‘lgan yozuvlarni filtrlaymiz
            var query = _context.TegirmonOrderCheck
                .Where(x => !x.isInCashbox && x.create_date >= begin_date && x.create_date <= end_date);

            // Yig‘indilarni hisoblaymiz
            var result = await query
                .GroupBy(x => 1)
                .Select(g => new
                {
                    total_cash = g.Sum(x => x.cash),
                    total_dollor = g.Sum(x => x.dollor),
                })
                .FirstOrDefaultAsync();

            // Agar hech narsa topilmasa, 0 qiymatlar bilan qaytaramiz
            if (result == null)
            {
                result = new { total_cash = 0.0, total_dollor = 0.0 };
            }

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrders([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("search parametri kerak.");

            // 1️⃣ Bazadan kerakli ma'lumotlarni olib kelamiz (faqat keraklilarni)
            var query = _context.TegirmonOrderCheck
                .Where(x => EF.Functions.ILike(x.status_name, $"%{text}%"))
                .Include(x => x.order).ThenInclude(o => o.client)
                .Include(x => x.order).ThenInclude(o => o.item_list).ThenInclude(i => i.product).ThenInclude(p => p.unitMeasurment);

            // 2️⃣ Endi bu yerda GroupBy ni EF emas, C# bajaradi
            var list = await query.ToListAsync();

            // 3️⃣ Endi biz xotirada guruhlaymiz
            var result = list
                .GroupBy(x => x.order.id)
                .Select(g => g.First().order)
                .OrderByDescending(o => o.pickUpDate)
                .ToList();

            return Ok(result);
        }


        [HttpGet("getNotInCashboxTotalCash")]
        public async Task<ActionResult<object>> GetNotInCashboxTotalCash(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            // Boshlanish va tugash vaqtini to‘liq kun oraliqqa sozlaymiz
            var start = begin_date.Date;
            var finish = end_date.Date.AddDays(1).AddTicks(-1);

            // isInCashbox = false bo‘lgan yozuvlarni filtrlaymiz
            var query = _context.TegirmonOrderCheck
                .Where(x => !x.isInCashbox && x.create_date >= start && x.create_date <= finish && x.hisob_id == 0);

            // Yig‘indilarni hisoblaymiz
            var result = await query
                .GroupBy(x => 1)
                .Select(g => new
                {
                    total_cash = g.Sum(x => x.cash),
                    total_dollor = g.Sum(x => x.dollor),
                })
                .FirstOrDefaultAsync();

            // Agar hech narsa topilmasa, 0 qiymatlar bilan qaytaramiz
            if (result == null)
            {
                result = new { total_cash = 0.0, total_dollor = 0.0 };
            }

            return Ok(result);
        }

        [HttpGet("getNotInCashboxTotalCashHisob")]
        public async Task<ActionResult<object>> GetNotInCashboxTotalCashHisob(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            // Boshlanish va tugash vaqtini to‘liq kun oraliqqa sozlaymiz
            var start = begin_date.Date;
            var finish = end_date.Date.AddDays(1).AddTicks(-1);

            // isInCashbox = false bo‘lgan yozuvlarni filtrlaymiz
            var query = _context.TegirmonOrderCheck
                .Where(x => !x.isInCashbox && x.create_date >= start && x.create_date <= finish && x.hisob_id > 0);

            // Yig‘indilarni hisoblaymiz
            var result = await query
                .GroupBy(x => 1)
                .Select(g => new
                {
                    total_cash = g.Sum(x => x.cash),
                    total_dollor = g.Sum(x => x.dollor),
                })
                .FirstOrDefaultAsync();

            // Agar hech narsa topilmasa, 0 qiymatlar bilan qaytaramiz
            if (result == null)
            {
                result = new { total_cash = 0.0, total_dollor = 0.0 };
            }

            return Ok(result);
        }

        [HttpGet("getAllChecksList")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderCheck>>> GetAllChecksList(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {


            var result = await _context.TegirmonOrderCheck
                .Where(x => x.create_date >= begin_date && x.create_date <= end_date)
                .Include(x => x.client)
                .OrderBy(x => x.create_date) // vaqt bo‘yicha tartiblash foydali bo‘ladi
                .ToListAsync();

            return Ok(result);
        }
        
        [HttpGet("GetByDateAndKassa")] // 👉 Explicit route va HTTP method qo‘yildi
        public async Task<IActionResult> GetByDateAndKassa([FromQuery] string date, [FromQuery] long? kassaId)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto‘g‘ri. To‘g‘ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var checks = await _context.TegirmonOrderCheck
                .AsNoTracking()
                .Where(c =>
                    c.create_date >= start &&
                    c.create_date < end &&
                    (kassaId == null || c.kassa_id == kassaId)
                )
                .OrderByDescending(c => c.create_date)
                .ToListAsync();

            return Ok(checks);
        }


        [HttpGet("getKassaCurrentRealTegirmonOrderKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrderKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonOrderMoneyTemp.FromSqlRaw("" +
                " SELECT   " +
                " sum(summ) as summ,   " +
                " sum(cash) as cash,   " +
                " sum(dollor) as dollor,   " +
                " sum(uz_card) as uz_card,   " +
                " sum(click) as click,   " +
                " sum(humo) as humo,   " +
                " sum(payme) as payme,   " +
                " sum(skidka) as skidka,   " +
                " sum(paynet) as paynet,   " +
                " sum(uzumpay) as uzumpay,   " +
                " sum(real_sum) as real_sum,   " +
                " sum(sum_balance) as sum_balance,   " +
                " sum(dollor_balance) as dollor_balance,   " +
                " sum(profit_summ) as profit_summ   " +
                "FROM public.tegirmon_order_check WHERE kassa_id = " + kassa_id+ " AND hisob_id > 0 " + " AND create_date BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentRealTegirmonOrderKassaIdNotHisob")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrderKassaIdNotHisob(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date,
            [FromQuery] long kassa_id)
        {
            // Boshlanish kuni 00:00:00
            // var start = begin_date.Date;

            // // Tugash kuni 23:59:59.999 (ya’ni o‘sha kunning oxiri)
            // var finish = end_date.Date.AddDays(1).AddTicks(-1);

            return await _context.TegirmonOrderMoneyTemp.FromSqlRaw(@"
                SELECT
                    sum(summ) as summ,
                    sum(cash) as cash,
                    sum(dollor) as dollor,
                    sum(uz_card) as uz_card,
                    sum(click) as click,
                    sum(humo) as humo,
                    sum(payme) as payme,
                    sum(skidka) as skidka,
                    sum(paynet) as paynet,
                    sum(uzumpay) as uzumpay,
                    sum(real_sum) as real_sum,
                    sum(profit_summ) as profit_summ,
                    sum(sum_balance) as sum_balance,
                    sum(dollor_balance) as dollor_balance 
                FROM public.tegirmon_order_check
                WHERE kassa_id = {0}
                AND hisob_id = 0
                AND ""isInCashbox"" = TRUE
                AND create_date BETWEEN {1} AND {2}
            ", kassa_id, begin_date, end_date).ToListAsync();
        }

        [HttpGet("getKassaCurrentRealTegirmonOrderAllKassa")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrderAllKassa([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonOrderMoneyTemp.FromSqlRaw("" +
                " SELECT   " +
                " sum(summ) as summ,   " +
                " sum(cash) as cash,   " +
                " sum(dollor) as dollor,   " +
                " sum(uz_card) as uz_card,   " +
                " sum(click) as click,   " +
                " sum(humo) as humo,   " +
                " sum(payme) as payme,   " +
                " sum(skidka) as skidka,   " +
                " sum(paynet) as paynet,   " +
                " sum(uzumpay) as uzumpay,   " +
                " sum(real_sum) as real_sum,   " +
                " sum(sum_balance) as sum_balance,   " +
                " sum(dollor_balance) as dollor_balance,   " +
                " sum(profit_summ) as profit_summ   " +
                "FROM public.tegirmon_order_check WHERE hisob_id > 0 " + " AND create_date BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentRealTegirmonOrderAllKassaNotHisob")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrderAllKassaNotHisob(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            // Boshlanish kuni 00:00:00
            // var start = begin_date.Date;

            // // Tugash kuni 23:59:59.999 (ya’ni o‘sha kunning oxiri)
            // var finish = end_date.Date.AddDays(1).AddTicks(-1);

            return await _context.TegirmonOrderMoneyTemp.FromSqlRaw(@"
                SELECT
                    sum(summ) as summ,
                    sum(cash) as cash,
                    sum(dollor) as dollor,
                    sum(uz_card) as uz_card,
                    sum(click) as click,
                    sum(humo) as humo,
                    sum(payme) as payme,
                    sum(skidka) as skidka,
                    sum(paynet) as paynet,
                    sum(uzumpay) as uzumpay,
                    sum(real_sum) as real_sum,
                    sum(profit_summ) as profit_summ,
                    sum(sum_balance) as sum_balance,
                    sum(dollor_balance) as dollor_balance 
                FROM public.tegirmon_order_check
                WHERE hisob_id = 0
                AND ""isInCashbox"" = TRUE
                AND create_date BETWEEN {0} AND {1}
            ", begin_date, end_date).ToListAsync();
        }




        // [HttpGet("getKassaCurrentRealTegirmonOrder")]
        // public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrder([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        // {
        //     String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
        //     String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
        //     return await _context.TegirmonOrderMoneyTemp.FromSqlRaw("" +
        //         " SELECT   " +
        //         " sum(summ) as summ,   " +
        //         " sum(cash) as cash,   " +
        //         " sum(dollor) as dollor,   " +
        //         " sum(uz_card) as uz_card,   " +
        //         " sum(click) as click,   " +
        //         " sum(humo) as humo,   " +
        //         " sum(payme) as payme,   " +
        //         " sum(skidka) as skidka,   " +
        //         " sum(paynet) as paynet,   " +
        //         " sum(uzumpay) as uzumpay,   " +
        //         " sum(real_sum) as real_sum,   " +
        //         " sum(sum_balance) as sum_balance,   " +
        //         " sum(dollor_balance) as dollor_balance,   " +
        //         " sum(profit_summ) as profit_summ   " +

        //         "FROM public.tegirmon_order_check WHERE hisob_id > 0  AND create_date BETWEEN '" + beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        // }

        [HttpGet("getKassaCurrentRealTegirmonOrder")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrder(
        [FromQuery] DateTime begin_date,
        [FromQuery] DateTime end_date)
            {
                // var start = begin_date.Date;
                // var finish = end_date.Date.AddDays(1).AddTicks(-1);

                return await _context.TegirmonOrderMoneyTemp.FromSqlRaw(@"
            SELECT
                sum(summ) as summ,
                sum(cash) as cash,
                sum(dollor) as dollor,
                sum(uz_card) as uz_card,
                sum(click) as click,
                sum(humo) as humo,
                sum(payme) as payme,
                sum(skidka) as skidka,
                sum(paynet) as paynet,
                sum(uzumpay) as uzumpay,
                sum(real_sum) as real_sum,
                sum(sum_balance) as sum_balance,
                sum(dollor_balance) as dollor_balance,
                sum(profit_summ) as profit_summ
            FROM public.tegirmon_order_check
            WHERE hisob_id > 0
            AND create_date BETWEEN {0} AND {1}
        ", begin_date, end_date).ToListAsync();
            }


        [HttpGet("getKassaCurrentRealTegirmonOrderNotHisob")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderMoneyTemp>>> getKassaCurrentRealTegirmonOrderNotHisob(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            // // Boshlanish kuni 00:00:00
            // var start = begin_date.Date;

            // // Tugash kuni 23:59:59.999 (ya’ni o‘sha kunning oxiri)
            // var finish = end_date.Date.AddDays(1).AddTicks(-1);

            return await _context.TegirmonOrderMoneyTemp.FromSqlRaw(@"
                SELECT
                    sum(summ) as summ,
                    sum(cash) as cash,
                    sum(dollor) as dollor,
                    sum(uz_card) as uz_card,
                    sum(click) as click,
                    sum(humo) as humo,
                    sum(payme) as payme,
                    sum(skidka) as skidka,
                    sum(paynet) as paynet,
                    sum(uzumpay) as uzumpay,
                    sum(real_sum) as real_sum,
                    sum(profit_summ) as profit_summ,
                    sum(sum_balance) as sum_balance,
                    sum(dollor_balance) as dollor_balance
                FROM public.tegirmon_order_check
                WHERE hisob_id = 0
                AND create_date BETWEEN {0} AND {1}
            ", begin_date, end_date).ToListAsync();
        }
        // AND ""isInCashbox"" = TRUE


        [HttpGet("getPaginationOrderId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOrderId([FromQuery] int page, [FromQuery] int size, [FromQuery] long order_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderCheck> categoryList = await _context.TegirmonOrderCheck
                .Where(p => p.active_status == true && p.TegirmonOrderid == order_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderCheck
                .Where(p => p.active_status == true && p.TegirmonOrderid == order_id ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderCheck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderCheck>> GetTegirmonOrderCheck(long id)
        {
            var tegirmonOrderCheck = await _context.TegirmonOrderCheck.FindAsync(id);

            if (tegirmonOrderCheck == null)
            {
                return NotFound();
            }

            return tegirmonOrderCheck;
        }


        // Oxirgi 30 kundagi klient tulovlari diagramma uchun

        [HttpGet("last30days/{clientId}")]
        public async Task<IActionResult> GetLast30Days(long clientId)
        {

            var fromDate = DateTime.Now.AddDays(-30);


            var result = await _context.TegirmonOrderCheck
            .Where(x => x.TegirmonOrderClientid == clientId && x.create_date >= fromDate)
            .GroupBy(x => x.create_date.Date)
            .Select(g => new
            {
                date = g.Key,
                total = g.Sum(x => x.summ)
            })
            .OrderBy(x => x.date)
            .ToListAsync();

            return Ok(result);
        }

        


        // PUT: api/TegirmonOrderCheck/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderCheck(long id, TegirmonOrderCheck tegirmonOrderCheck)
        {
            if (id != tegirmonOrderCheck.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderCheck).State = EntityState.Modified;

            try
            {
                var tegirmonOrder = await _context.TegirmonOrder.FindAsync(tegirmonOrderCheck.TegirmonOrderid);
                
                if (tegirmonOrderCheck.salary == 1)
                {
                    tegirmonOrder.IsPrepaid = false;
                }
                if (tegirmonOrder.pay_progress == 100 && tegirmonOrderCheck.salary == 1)
                {
                    tegirmonOrder.kassir_name = tegirmonOrderCheck.image_url;
                    tegirmonOrder.IsPaid = true;
                    tegirmonOrder.PaidAt = DateTime.Now;


                    if (tegirmonOrder.IsLoaded == true)
                    {
                        tegirmonOrder.IsClosed = true;
                        tegirmonOrder.ClosedAt = DateTime.Now;
                        tegirmonOrder.paid_status = "Zakaz yopildi";
                    }
                }
                if (tegirmonOrder.IsClosed == false)
                {
                    tegirmonOrder.paid_status = "To'lov tushdi.";
                }

                tegirmonOrderCheck.create_date = DateTime.Now;

                
                await _context.SaveChangesAsync();


                if (tegirmonOrderCheck.isInCashbox == true) // kassaga yoki hisobga tulov qachonki true bulsa bajariladi
                {

                    if (tegirmonOrderCheck.hisob_id > 0) // hisobga pul tushganda
                    {
                        String client_name = "Optom savdo (Klient tanlanmagan)";
                        String client_phone_number = "";
                        if (tegirmonOrderCheck.TegirmonOrderClientid != null && tegirmonOrderCheck.TegirmonOrderClientid != 0) // client malumotlarini olish
                        {
                            var tegirmonOrderClient = await _context.TegirmonOrderClient
                                .Where(p => p.id == tegirmonOrderCheck.TegirmonOrderClientid)
                                .FirstOrDefaultAsync();
                            if (tegirmonOrderClient != null)
                            {
                                client_name = tegirmonOrderClient.fio;
                                client_phone_number = tegirmonOrderClient.phone_number;
                            }
                        }

                            TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                        tegirmon_hisob_prixod.status_rasxod = 1;
                        tegirmon_hisob_prixod.worker_name = client_name;
                        tegirmon_hisob_prixod.phone_number = client_phone_number;
                        tegirmon_hisob_prixod.auth_user_creator_id = tegirmonOrderCheck.auth_user_creator_id;
                        tegirmon_hisob_prixod.note = tegirmonOrderCheck.status_name;
                        tegirmon_hisob_prixod.addiotionala_information = tegirmonOrderCheck.image_url;// bunga kassirni nomini aniqlab yozish kerak;

                        tegirmon_hisob_prixod.all_summ = (double)tegirmonOrderCheck.summ;
                        tegirmon_hisob_prixod.all_summ_string = tegirmonOrderCheck.summ.ToString("N0");
                        tegirmon_hisob_prixod.dollor = (double)tegirmonOrderCheck.dollor;
                        tegirmon_hisob_prixod.dollor_string = tegirmonOrderCheck.dollor.ToString("N0");
                        tegirmon_hisob_prixod.kurs = (double)tegirmonOrderCheck.kurs;
                        tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonOrderCheck.hisob_id;
                        tegirmon_hisob_prixod.prixod = (double)tegirmonOrderCheck.cash;
                        tegirmon_hisob_prixod.image_url = tegirmonOrderCheck.cash.ToString("N0");
                        

                        _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                        await _context.SaveChangesAsync();

                        var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                            .Where(p => p.TegirmonHisoblarid == tegirmonOrderCheck.hisob_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                            if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                            {
                                // bu bugungi kunga dalni qushilmay qolgan bulsa
                                var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                                .Where(p => p.TegirmonHisoblarid == tegirmonOrderCheck.hisob_id)
                                .OrderByDescending(p => p.created_date_time)
                                .FirstOrDefaultAsync();

                                if (tegirmonKassaInfoLast == null)
                                {
                                    // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                                    TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                                    tegirmon_kassa_info.cash = 0;
                                    tegirmon_kassa_info.dollor = 0;
                                    
                                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderCheck.cash;
                                        tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonOrderCheck.dollor;


                                    // tegirmon_kassa_info.id = 0;
                                    tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderCheck.hisob_id;
                                    tegirmon_kassa_info.before_cash = 0;
                                    tegirmon_kassa_info.before_dollor = 0;
                                    _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                                    TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                                    tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                                    
                                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonOrderCheck.dollor;
                                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderCheck.cash;
                                    


                                    tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderCheck.hisob_id;
                                    tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                                    tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                                    _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                                    await _context.SaveChangesAsync();

                                }
                            }
                            else
                            {
                                
                                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonOrderCheck.cash;
                                    tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonOrderCheck.dollor;

                                _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                                await _context.SaveChangesAsync();

                            }
                    }
                    else
                    {
                        // ==> kassadan pul qushish qismi buldi kassirni hisobiga tushadi naqd tulov qilsa <==
                        if (tegirmonOrderCheck.kassa_id != 0)
                        {
                            var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                            .Where(p => p.TegirmonKassaid == tegirmonOrderCheck.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                            if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                            {
                                // bu bugungi kunga dalni qushilmay qolgan bulsa
                                var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                                .Where(p => p.TegirmonKassaid == tegirmonOrderCheck.kassa_id)
                                .OrderByDescending(p => p.created_date_time)
                                .FirstOrDefaultAsync();

                                if (tegirmonKassaInfoLast == null)
                                {
                                    // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                                    TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                    tegirmon_kassa_info.cash = 0;
                                    tegirmon_kassa_info.dollor = 0;

                                    tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderCheck.cash;
                                    tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderCheck.dollor;


                                    // tegirmon_kassa_info.id = 0;
                                    tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderCheck.kassa_id;
                                    tegirmon_kassa_info.before_cash = 0;
                                    tegirmon_kassa_info.before_dollor = 0;
                                    _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                                    TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                    tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                                    tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;


                                    tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderCheck.cash;
                                    tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderCheck.dollor;


                                    tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderCheck.kassa_id;
                                    tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                                    tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                                    _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                    await _context.SaveChangesAsync();

                                }
                            }
                            else
                            {
                                tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonOrderCheck.cash;
                                tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonOrderCheck.dollor;

                                _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                                await _context.SaveChangesAsync();
                            }
                            
                            if(tegirmonOrderCheck.cash > 0 || tegirmonOrderCheck.dollor > 0)
                            {
                                TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                                tegirmon_kassa_prixod.auth_user_updator_id = tegirmonOrderCheck.kassa_id; // kassir id
                                tegirmon_kassa_prixod.auth_user_creator_id = tegirmonOrderCheck.auth_user_creator_id; // bu kassadan kassaga utkazishda qabul qiluvchi kasir idsi
                                tegirmon_kassa_prixod.worker_name = tegirmonOrderCheck.client_name;
                                // tegirmon_kassa_prixod.phone_number = phone_number;
                                tegirmon_kassa_prixod.dollor = tegirmonOrderCheck.dollor;
                                tegirmon_kassa_prixod.dollor_string = tegirmonOrderCheck.dollor.ToString("N0");
                                tegirmon_kassa_prixod.note = tegirmonOrderCheck.status_name;
                                tegirmon_kassa_prixod.addiotionala_information = tegirmonOrderCheck.image_url; 
                                tegirmon_kassa_prixod.all_summ = tegirmonOrderCheck.cash + (tegirmonOrderCheck.dollor * tegirmonOrderCheck.kurs);
                                tegirmon_kassa_prixod.all_summ_string = (tegirmonOrderCheck.cash + (tegirmonOrderCheck.dollor * tegirmonOrderCheck.kurs)).ToString("N0");
                                tegirmon_kassa_prixod.reserve_val_l = tegirmonOrderCheck.TegirmonOrderClientid ?? 0;
                                tegirmon_kassa_prixod.reserve_val = 4; // optom savdo zakaz uchun tulov
                                
                                    tegirmon_kassa_prixod.status_rasxod = 1;
                                    tegirmon_kassa_prixod.prixod = tegirmonOrderCheck.cash;
                                    tegirmon_kassa_prixod.image_url = tegirmonOrderCheck.cash.ToString("N0");
                                    tegirmon_kassa_prixod.rasxod = 0;


                                _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderCheckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tegirmonOrderCheck);

            // return NoContent(); bu 204 qaytaradi frontend hech narsa kurinmaydi
        }

        // POST: api/TegirmonOrderCheck
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderCheck>> PostTegirmonOrderCheck(TegirmonOrderCheck tegirmonOrderCheck)
        {
            _context.TegirmonOrderCheck.Update(tegirmonOrderCheck);
            await _context.SaveChangesAsync();
            
            // zakaz qismiga tulangan summani foizini chiqarish uchun. va tulov statuslarini uzgartirish orderda
            var tegirmonOrder = await _context.TegirmonOrder.FindAsync(tegirmonOrderCheck.TegirmonOrderid);
                if (tegirmonOrder != null) {
                    double procent = 0;
                    double order_sum = 0;
                    double order_remainded_sum = 0;
                    tegirmonOrder.kassir_name = tegirmonOrderCheck.image_url;
                    order_sum = tegirmonOrder.sum + (tegirmonOrder.dollor * tegirmonOrderCheck.kurs);
                order_remainded_sum = tegirmonOrderCheck.remaining_sum + (tegirmonOrderCheck.remaining_usd * tegirmonOrderCheck.kurs);
                // Console.WriteLine("order_sum" + order_sum);
                // Console.WriteLine("order_remainded_sum" + order_remainded_sum);
                    procent = (order_remainded_sum * 100) / order_sum;
                    tegirmonOrder.pay_progress = (int)Math.Truncate(100 - procent);
                    tegirmonOrder.remaining_sum = tegirmonOrderCheck.remaining_sum;
                    tegirmonOrder.remaining_usd = tegirmonOrderCheck.remaining_usd;

                    if (tegirmonOrderCheck.remaining_sum == 0 && tegirmonOrderCheck.remaining_usd == 0
                     && tegirmonOrderCheck.salary == 0 && tegirmonOrderCheck.isInCashbox == true)
                    {
                        tegirmonOrder.IsPaid = true;
                        tegirmonOrder.PaidAt = DateTime.Now;
                        tegirmonOrder.IsPrepaid = false;

                    if (tegirmonOrder.IsLoaded == true)
                    {
                            tegirmonOrder.IsClosed = true;
                            tegirmonOrder.ClosedAt = DateTime.Now;
                            tegirmonOrder.paid_status = "Zakaz yopildi";
                        }
                    }
                    if (!string.IsNullOrEmpty(tegirmonOrderCheck.status_name) && tegirmonOrder.IsClosed == false)
                    {
                        tegirmonOrder.paid_status = tegirmonOrderCheck.status_name;
                    }
                    if (tegirmonOrderCheck.isInCashbox == false) {
                        tegirmonOrder.IsPrepaid = true; // bu tulov qilingan lekin kassaga tushmaganini anglatadi
                    }

                    // _context.TegirmonOrder.Update(tegirmonOrder);
                await _context.SaveChangesAsync();
                }


            // bu klientni hisobidan olingan bulsa
            if (tegirmonOrderCheck.sum_balance > 0 || tegirmonOrderCheck.dollor_balance > 0)
            {

                var tegirmonOrderClient = await _context.TegirmonOrderClient
                    .FindAsync(tegirmonOrderCheck.TegirmonOrderClientid);


                if (tegirmonOrderClient == null)
                {
                    Console.WriteLine("Client topilmadi");
                }
                else
                {
                    tegirmonOrderClient.sum -= tegirmonOrderCheck.sum_balance;
                    tegirmonOrderClient.dollor -= tegirmonOrderCheck.dollor_balance;

                    TegirmonOrderClientRasxod tegirmon_order_client_rasxod = new TegirmonOrderClientRasxod();
                    tegirmon_order_client_rasxod.auth_user_updator_id = tegirmonOrderCheck.kassa_id;
                    tegirmon_order_client_rasxod.auth_user_creator_id = tegirmonOrderCheck.TegirmonAuthid;
                    tegirmon_order_client_rasxod.client_name = tegirmonOrderClient.fio;
                    tegirmon_order_client_rasxod.phone_number = tegirmonOrderClient.phone_number; // bu rasm quyiladigan qism ekan lekin checkda rasm olinmaydi
                    tegirmon_order_client_rasxod.addiotionala_information = tegirmonOrderCheck.image_url; // kassir nomi
                    tegirmon_order_client_rasxod.kurs = tegirmonOrderCheck.kurs;
                    tegirmon_order_client_rasxod.dollor = tegirmonOrderCheck.dollor_balance;
                    tegirmon_order_client_rasxod.dollor_string = tegirmonOrderCheck.dollor_balance.ToString("N0");
                    tegirmon_order_client_rasxod.note = tegirmonOrderCheck.status_name + " (Balancedan tovar olindi)";
                    tegirmon_order_client_rasxod.all_summ = tegirmonOrderCheck.sum_balance + (tegirmonOrderCheck.kurs * tegirmonOrderCheck.dollor_balance);
                    tegirmon_order_client_rasxod.all_summ_string = tegirmon_order_client_rasxod.all_summ.ToString("N0");
                    tegirmon_order_client_rasxod.TegirmonOrderClientId = tegirmonOrderCheck.TegirmonOrderClientid ?? 0;
                    tegirmon_order_client_rasxod.cash_or_product = 2; // product olinsa
                    tegirmon_order_client_rasxod.status_rasxod = 0;
                    tegirmon_order_client_rasxod.sum = tegirmonOrderCheck.sum_balance;
                    tegirmon_order_client_rasxod.sum_str = tegirmonOrderCheck.sum_balance.ToString("N0");
                    tegirmon_order_client_rasxod.hisob_sum = tegirmonOrderCheck.sum_balance;
                    tegirmon_order_client_rasxod.hisob_dollor = tegirmonOrderCheck.dollor_balance;
                    tegirmon_order_client_rasxod.order_id = tegirmonOrderCheck.TegirmonOrderid;
                    tegirmon_order_client_rasxod.reserve_val_1 = tegirmonOrderClient.sum;
                    tegirmon_order_client_rasxod.reserve_val_2 = tegirmonOrderClient.dollor;

                    _context.TegirmonOrderClientRasxod.Update(tegirmon_order_client_rasxod);

                    
                }
            
                await _context.SaveChangesAsync();
            }

            // bu klientni balancega kup pul berib qaytarganda
            if (tegirmonOrderCheck.online > 0 || tegirmonOrderCheck.perchisleniya > 0)
            {
                var tegirmonOrderClient = await _context.TegirmonOrderClient
                    .FindAsync(tegirmonOrderCheck.TegirmonOrderClientid);

                if (tegirmonOrderClient == null)
                {
                    Console.WriteLine("Client topilmadi");
                }
                else
                {
                    Console.WriteLine("Client topildi");
                    tegirmonOrderClient.sum += tegirmonOrderCheck.online;
                    tegirmonOrderClient.dollor += tegirmonOrderCheck.perchisleniya;

                    TegirmonOrderClientRasxod tegirmon_order_client_rasxod_return = new TegirmonOrderClientRasxod();
                    tegirmon_order_client_rasxod_return.auth_user_updator_id = tegirmonOrderCheck.kassa_id;
                    tegirmon_order_client_rasxod_return.auth_user_creator_id = tegirmonOrderCheck.TegirmonAuthid;
                    tegirmon_order_client_rasxod_return.client_name = tegirmonOrderClient.fio;
                    tegirmon_order_client_rasxod_return.phone_number = tegirmonOrderClient.phone_number; // bu rasm quyiladigan qism ekan lekin checkda rasm olinmaydi
                    tegirmon_order_client_rasxod_return.addiotionala_information = tegirmonOrderCheck.image_url; // kassir nomi
                    tegirmon_order_client_rasxod_return.kurs = tegirmonOrderCheck.kurs;
                    tegirmon_order_client_rasxod_return.dollor = tegirmonOrderCheck.perchisleniya;
                    tegirmon_order_client_rasxod_return.dollor_string = tegirmonOrderCheck.perchisleniya.ToString("N0");
                    tegirmon_order_client_rasxod_return.note = "Balancega ortiqcha summa qushildi";
                    tegirmon_order_client_rasxod_return.all_summ = tegirmonOrderCheck.online + (tegirmonOrderCheck.kurs * tegirmonOrderCheck.perchisleniya);
                    tegirmon_order_client_rasxod_return.all_summ_string = tegirmon_order_client_rasxod_return.all_summ.ToString("N0");
                    tegirmon_order_client_rasxod_return.TegirmonOrderClientId = tegirmonOrderCheck.TegirmonOrderClientid ?? 0;
                    tegirmon_order_client_rasxod_return.cash_or_product = 0; // product olinsa
                    tegirmon_order_client_rasxod_return.status_rasxod = 1;
                    tegirmon_order_client_rasxod_return.sum = tegirmonOrderCheck.online;
                    tegirmon_order_client_rasxod_return.sum_str = tegirmonOrderCheck.online.ToString("N0");
                    tegirmon_order_client_rasxod_return.hisob_sum = tegirmonOrderCheck.online;
                    tegirmon_order_client_rasxod_return.hisob_dollor = tegirmonOrderCheck.perchisleniya;
                    tegirmon_order_client_rasxod_return.order_id = tegirmonOrderCheck.TegirmonOrderid;

                    tegirmon_order_client_rasxod_return.reserve_val_1 = tegirmonOrderClient.sum;
                    tegirmon_order_client_rasxod_return.reserve_val_2 = tegirmonOrderClient.dollor;

                    _context.TegirmonOrderClientRasxod.Update(tegirmon_order_client_rasxod_return);
                }
                await _context.SaveChangesAsync();

            }


            if (tegirmonOrderCheck.isInCashbox == true) // kassaga yoki hisobga tulov qachonki true bulsa bajariladi
            {
                if (tegirmonOrderCheck.hisob_id > 0) // hisobga pul tushganda
                {
                    String client_name = "Optom savdo (Klient tanlanmagan)";
                    String client_phone_number = "";
                    if (tegirmonOrderCheck.TegirmonOrderClientid != null && tegirmonOrderCheck.TegirmonOrderClientid != 0) // client malumotlarini olish
                    {
                        var tegirmonOrderClient = await _context.TegirmonOrderClient
                            .Where(p => p.id == tegirmonOrderCheck.TegirmonOrderClientid)
                            .FirstOrDefaultAsync();
                        if (tegirmonOrderClient != null)
                        {
                            client_name = tegirmonOrderClient.fio;
                            client_phone_number = tegirmonOrderClient.phone_number;
                        }
                    }

                    TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                    tegirmon_hisob_prixod.status_rasxod = 1;
                    tegirmon_hisob_prixod.worker_name = client_name;
                    tegirmon_hisob_prixod.phone_number = client_phone_number;
                    tegirmon_hisob_prixod.auth_user_creator_id = tegirmonOrderCheck.auth_user_creator_id;
                    tegirmon_hisob_prixod.note = tegirmonOrderCheck.status_name;
                    tegirmon_hisob_prixod.addiotionala_information = tegirmonOrderCheck.image_url;// bunga kassirni nomini aniqlab yozish kerak;

                    tegirmon_hisob_prixod.all_summ = (double)tegirmonOrderCheck.summ;
                    tegirmon_hisob_prixod.all_summ_string = tegirmonOrderCheck.summ.ToString("N0");
                    tegirmon_hisob_prixod.dollor = (double)tegirmonOrderCheck.dollor;
                    tegirmon_hisob_prixod.dollor_string = tegirmonOrderCheck.dollor.ToString("N0");
                    tegirmon_hisob_prixod.kurs = (double)tegirmonOrderCheck.kurs;
                    tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonOrderCheck.hisob_id;
                    tegirmon_hisob_prixod.prixod = (double)tegirmonOrderCheck.cash;
                    tegirmon_hisob_prixod.image_url = tegirmonOrderCheck.cash.ToString("N0");


                    _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                    await _context.SaveChangesAsync();

                    var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                        .Where(p => p.TegirmonHisoblarid == tegirmonOrderCheck.hisob_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                        .Where(p => p.TegirmonHisoblarid == tegirmonOrderCheck.hisob_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;

                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderCheck.cash;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonOrderCheck.dollor;


                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderCheck.hisob_id;
                            tegirmon_kassa_info.before_cash = 0;
                            tegirmon_kassa_info.before_dollor = 0;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;

                            tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonOrderCheck.dollor;
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderCheck.cash;



                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderCheck.hisob_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {

                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonOrderCheck.cash;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonOrderCheck.dollor;

                        _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();

                    }
                }
                else
                {
                    // ==> kassadan pul qushish qismi buldi kassirni hisobiga tushadi naqd tulov qilsa <==
                    if (tegirmonOrderCheck.kassa_id != 0)
                    {
                        var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                        .Where(p => p.TegirmonKassaid == tegirmonOrderCheck.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                        if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                        {
                            // bu bugungi kunga dalni qushilmay qolgan bulsa
                            var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                            .Where(p => p.TegirmonKassaid == tegirmonOrderCheck.kassa_id)
                            .OrderByDescending(p => p.created_date_time)
                            .FirstOrDefaultAsync();

                            if (tegirmonKassaInfoLast == null)
                            {
                                // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                                TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                tegirmon_kassa_info.cash = 0;
                                tegirmon_kassa_info.dollor = 0;

                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderCheck.cash;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderCheck.dollor;


                                // tegirmon_kassa_info.id = 0;
                                tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderCheck.kassa_id;
                                tegirmon_kassa_info.before_cash = 0;
                                tegirmon_kassa_info.before_dollor = 0;
                                _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                                TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;


                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderCheck.cash;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderCheck.dollor;


                                tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderCheck.kassa_id;
                                tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                                _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();

                            }
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonOrderCheck.cash;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonOrderCheck.dollor;

                            _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                            await _context.SaveChangesAsync();
                        }
                        if(tegirmonOrderCheck.cash > 0 || tegirmonOrderCheck.dollor > 0)
                        {
                            TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                            tegirmon_kassa_prixod.auth_user_updator_id = tegirmonOrderCheck.kassa_id; // kassir id
                            tegirmon_kassa_prixod.auth_user_creator_id = tegirmonOrderCheck.auth_user_creator_id; // bu kassadan kassaga utkazishda qabul qiluvchi kasir idsi
                            tegirmon_kassa_prixod.worker_name = tegirmonOrderCheck.client_name;
                            // tegirmon_kassa_prixod.phone_number = phone_number;
                            tegirmon_kassa_prixod.dollor = tegirmonOrderCheck.dollor;
                            tegirmon_kassa_prixod.dollor_string = tegirmonOrderCheck.dollor.ToString("N0");
                            tegirmon_kassa_prixod.note = tegirmonOrderCheck.status_name;
                            tegirmon_kassa_prixod.addiotionala_information = tegirmonOrderCheck.image_url; 
                            tegirmon_kassa_prixod.all_summ = tegirmonOrderCheck.cash + (tegirmonOrderCheck.dollor * tegirmonOrderCheck.kurs);
                            tegirmon_kassa_prixod.all_summ_string = (tegirmonOrderCheck.cash + (tegirmonOrderCheck.dollor * tegirmonOrderCheck.kurs)).ToString("N0");
                            tegirmon_kassa_prixod.reserve_val_l = tegirmonOrderCheck.TegirmonOrderClientid ?? 0;
                            tegirmon_kassa_prixod.reserve_val = 4; // optom savdo zakaz uchun tulov
                            
                                tegirmon_kassa_prixod.status_rasxod = 1;
                                tegirmon_kassa_prixod.prixod = tegirmonOrderCheck.cash;
                                tegirmon_kassa_prixod.image_url = tegirmonOrderCheck.cash.ToString("N0");
                                tegirmon_kassa_prixod.rasxod = 0;


                            _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
                            await _context.SaveChangesAsync();
                        }

                        
                    }
                }
            }

            return CreatedAtAction("GetTegirmonOrderCheck", new { id = tegirmonOrderCheck.id }, tegirmonOrderCheck);
        }

        // DELETE: api/TegirmonOrderCheck/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderCheck>> DeleteTegirmonOrderCheck(long id)
        {
            var tegirmonOrderCheck = await _context.TegirmonOrderCheck.FindAsync(id);
            if (tegirmonOrderCheck == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderCheck.Remove(tegirmonOrderCheck);
            await _context.SaveChangesAsync();

            return tegirmonOrderCheck;
        }

        private bool TegirmonOrderCheckExists(long id)
        {
            return _context.TegirmonOrderCheck.Any(e => e.id == id);
        }
    }
}
