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
    public class TegirmonCheckController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonCheckController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonCheck
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonCheck>>> GetTegirmonCheck()
        {
            return await _context.TegirmonCheck.ToListAsync();
        }

        [HttpGet("getKassaCurrentRealTegirmonPositionKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInfoTemp>>> getKassaCurrentRealTegirmonPositionKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInfoTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(card) as card,   " +
                " sum(cash) as cash,   " +
                " sum(summ) as summ,   " +
                " sum(profit_summ) as profit_sum,   " +
                " sum(real_sum) as real_sum,   " +
                " sum(humo) as humo,   " +
                " sum(uz_card) as uz_card,   " +
                " sum(perchisleniya) as perchisleniya,   " +
                " sum(dolg) as dolg,   " +
                " sum(dolg_payed) as dolg_payed,   " +
                " sum(creadit_payed) as creadit_payed,   " +
                " sum(rasxod) as rasxod,   " +
                " sum(online) as online,   " +
                " sum(salary) as salary,   " +
                " sum(for_buy_tovar_rasxod) as for_buy_tovar_rasxod,   " +
                " sum(srogi_otganlar_uchun_rasxod) as srogi_otganlar_uchun_rasxod   " +
                "FROM public.tegirmon_check WHERE auth_user_updator_id = " + kassa_id + " AND create_date BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();

        }


        [HttpGet("getKassaCurrentRealTegirmonPosition")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInfoTemp>>> getKassaCurrentRealTegirmonPosition([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInfoTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(card) as card,   " +
                " sum(cash) as cash,   " +
                " sum(summ) as summ,   " +
                " sum(profit_summ) as profit_sum,   " +
                " sum(real_sum) as real_sum,   " +
                " sum(humo) as humo,   " +
                " sum(uz_card) as uz_card,   " +
                " sum(perchisleniya) as perchisleniya,   " +
                " sum(dolg) as dolg,   " +
                " sum(dolg_payed) as dolg_payed,   " +
                " sum(creadit_payed) as creadit_payed,   " +
                " sum(rasxod) as rasxod,   " +
                " sum(online) as online,   " +
                " sum(salary) as salary,   " +
                " sum(for_buy_tovar_rasxod) as for_buy_tovar_rasxod,   " +
                " sum(srogi_otganlar_uchun_rasxod) as srogi_otganlar_uchun_rasxod   " +
                "FROM public.tegirmon_check WHERE create_date BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();

        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonCheck> categoryList = await _context.TegirmonCheck
                .Include(p => p.client)
                .Include(p => p.contragent)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonCheck.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        
        [HttpGet("GetByDateAndKassaSell")] // 👉 Explicit route va HTTP method qo‘yildi
        public async Task<IActionResult> GetByDateAndKassaSell([FromQuery] string date, [FromQuery] long? kassaId)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto‘g‘ri. To‘g‘ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var checks = await _context.TegirmonCheck
                .AsNoTracking()
                .Where(c =>
                    c.create_date >= start &&
                    c.create_date < end &&
                    (kassaId == null || c.auth_user_updator_id == kassaId) &&
                    // Faqat contragent bog'lanmagan checklar
                    (c.TegirmonContragentid == null || c.TegirmonContragentid == 0)
                )
                .Include(c=>c.payments)
                .ThenInclude(p=>p.product)
                .OrderByDescending(c => c.create_date)
                .ToListAsync();

            return Ok(checks);
        }

        // Zahira va Taroz список to'lovlari uchun:
        // faqat TegirmonContragentid mavjud bo'lgan checklar
        [HttpGet("GetByDateAndKassaSellContragent")]
        public async Task<IActionResult> GetByDateAndKassaSellContragent([FromQuery] string date, [FromQuery] long? kassaId)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto‘g‘ri. To‘g‘ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var checks = await _context.TegirmonCheck
                .AsNoTracking()
                .Where(c =>
                    c.create_date >= start &&
                    c.create_date < end &&
                    (kassaId == null || c.auth_user_updator_id == kassaId) &&
                    // faqat contragent bog'langanlar
                    (c.TegirmonContragentid != null && c.TegirmonContragentid != 0)
                )
                .Include(c => c.payments)
                .ThenInclude(p => p.product)
                .OrderByDescending(c => c.create_date)
                .ToListAsync();

            return Ok(checks);
        }


        [HttpGet("getPaginationByBeatWeenTwoDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDate([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonCheck> categoryList = await _context.TegirmonCheck
                .Include(p => p.client)
                .Include(p => p.contragent)
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonCheck
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByBeatWeenTwoDateGetNextDay")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDateGetNextDay([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonCheck> categoryList = await _context.TegirmonCheck
                .Where(p => p.active_status == true && p.credit_status == true 
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonCheck
                .Where(p => p.active_status == true  && p.credit_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationRasxod")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationRasxod([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonCheck> categoryList = await _context.TegirmonCheck
                .Include(p => p.client)
                .Include(p => p.contragent)
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonCheck>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonCheck.Where(p => p.active_status == true
            && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonCheck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonCheck>> GetTegirmonCheck(long id)
        {
            var tegirmonCheck = await _context.TegirmonCheck.FindAsync(id);

            if (tegirmonCheck == null)
            {
                return NotFound();
            }

            return tegirmonCheck;
        }



        [HttpGet("getcheckFullInfoById")]
        public async Task<ActionResult<TegirmonCheck>> getcheckFullInfoById([FromQuery]long check_id)
        {
            var tegirmonCheck = await _context.TegirmonCheck
                .Include(p => p.contragent)
                .Include(p =>p.client)
                .Include(p =>p.credit)
                .Include(p =>p.auth)
                .ThenInclude(p=>p.user)
                .Where(p => p.id == check_id).ToListAsync();

            if (tegirmonCheck == null || tegirmonCheck.Count == 0)
            {
                return NotFound();
            }
            tegirmonCheck.First().payments = await _context.TegirmonPayments
                .Include(p =>p.product)
                .Where(p => p.TegirmonCheckid == check_id).ToListAsync();

            return tegirmonCheck.First();
        }

        // [HttpGet("getcheckInfoGroupId")]
        // public async Task<ActionResult<TegirmonCheck>> getcheckInfoGroupId([FromQuery]long group_id)
        // {
        //     var tegirmonCheck = await _context.TegirmonCheck
        //         .Where(p => p.rasxod == group_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

        //     if (tegirmonCheck == null || tegirmonCheck.Count == 0)
        //     {
        //         return NotFound();
        //     }
            

        //     var tegirmonKassaInfo = await _context.TegirmonKassaInfo
        //     .Where(p =>p.TegirmonKassaid == tegirmonCheck.First().auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
        //     if(tegirmonKassaInfo != 0 && tegirmonKassaInfo.Count > 0){
        //         tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonCheck.First().for_buy_tovar_rasxod;

        //         _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
        //         await _context.SaveChangesAsync();
        //     }
        //     // 

        //     _context.TegirmonCheck.Remove(tegirmonCheck.First());
        //     await _context.SaveChangesAsync();

        //     return tegirmonCheck.First();
        // }

        // PUT: api/TegirmonCheck/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonCheck(long id, TegirmonCheck tegirmonCheck)
        {
            if (id != tegirmonCheck.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonCheck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonCheckExists(id))
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

        // POST: api/TegirmonCheck
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonCheck>> PostTegirmonCheck(TegirmonCheck tegirmonCheck)
        {
            if (tegirmonCheck.id > 0)
            {
                var oldCheck = await _context.TegirmonCheck
                .Include(x => x.payments)
                .Include(x => x.auth)
                .FirstOrDefaultAsync(x => x.id == tegirmonCheck.id);

                 if(oldCheck != null)
                {
                    if (oldCheck.payments != null && oldCheck.payments.Count > 0 && oldCheck.TegirmonContragentid != 1 )
                    {
                        foreach (var oldPay in oldCheck.payments)
                        {
                            var skladItem = await _context.TegirmonSkladTovar
                                .FirstOrDefaultAsync(p =>
                                    p.TegirmonProductId == oldPay.TegirmonProductid &&
                                    p.TegirmonSkladId == oldCheck.auth_user_creator_id);
                            if (skladItem != null)
                            {
                                skladItem.qty += oldPay.qty;
                                skladItem.real_qty += oldPay.qty;
                                _context.TegirmonSkladTovar.Update(skladItem);
                            }

                            // Shu invoice ga bog'langan history larni topish
                            var histories = await _context.TegirmonSkladHistory
                                .Where(h => h.userId == oldCheck.id && h.text == "Sotuv")
                                .ToListAsync();
                            // Avval history larni uchirish
                            _context.TegirmonSkladHistory.RemoveRange(histories);

                            // Yangi qty_log tablega yozamiz
                            TegirmonSkladQtyLog qtyLog = new TegirmonSkladQtyLog();
                            qtyLog.active_status = true;
                            qtyLog.qty = oldPay.qty;
                            qtyLog.type = 1; // 1 = kirim (check yangilanganda qty qaytariladi)
                            qtyLog.TegirmonProductId = oldPay.TegirmonProductid;
                            qtyLog.TegirmonSkladId = oldCheck.auth_user_creator_id ?? 0;
                            qtyLog.note = "Check yangilanganda eski qty qaytarildi (Check ID: " + oldCheck.id + ")";
                            qtyLog.text = "Check_update_qaytarish";
                            qtyLog.auth_user_creator_id = oldCheck.auth_user_creator_id;
                            qtyLog.userId = oldCheck.id;
                            qtyLog.user_name = oldCheck.image_base_64 ?? ""; // TegirmonCheck da user_name yo'q, image_base_64 ishlatamiz (481-qatorda shunday ishlatilgan)
                            qtyLog.operation_id = oldCheck.id;
                            qtyLog.operation_type = "Check";
                            qtyLog.create_date = DateTime.Now;

                            _context.TegirmonSkladQtyLog.Add(qtyLog);
                        }

                        await _context.SaveChangesAsync();

                    }
                    if (oldCheck.auth_user_updator_id != 0)
                    {
                        var kassaInfo = await _context.TegirmonKassaInfo
                            .Where(p => p.TegirmonKassaid == oldCheck.auth_user_updator_id)
                            .OrderByDescending(p => p.created_date_time)
                            .FirstOrDefaultAsync();

                        if (kassaInfo != null)
                        {
                            kassaInfo.cash -= oldCheck.cash;
                            kassaInfo.dollor -= oldCheck.real_sum;
                            _context.TegirmonKassaInfo.Update(kassaInfo);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // 3. Eski payments ni o‘chirish ❗
                    _context.TegirmonPayments.RemoveRange(oldCheck.payments);
                    await _context.SaveChangesAsync();

                    // 4. EF eski checkni kuzatishni to‘xtatsin
                    _context.Entry(oldCheck).State = EntityState.Detached;
                }
            }
            


            
            //tovar sotsa ayrib qoyish kerak real qtydan
            if (tegirmonCheck.payments != null
                && tegirmonCheck.payments.Count > 0)
            {
                foreach (TegirmonPayments item_check in tegirmonCheck.payments)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item_check.TegirmonProductid && p.TegirmonSkladId == tegirmonCheck.auth_user_creator_id)
                   .ToListAsync();
                    if (skladTovarList == null || skladTovarList.Count == 0)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";
                        return BadRequest($"{productName} skladda mavjud emas!");
                    }

                    var skladTovar = skladTovarList.First();

                    if (skladTovar.qty < item_check.qty)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";

                        return BadRequest($"{productName} yetarli emas! Omborda faqat {skladTovar.qty} dona bor.");
                    }
                }

                // checkni saqlash qismi / agar bironta ham mahsulot kam bulmasa checkni saqlab skladdan mahsulotlarni ayiradi
                _context.TegirmonCheck.Update(tegirmonCheck);
                await _context.SaveChangesAsync();
                if(tegirmonCheck.TegirmonContragentid != 1)
                {
                    foreach (TegirmonPayments item in tegirmonCheck.payments)
                    {
                        List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                    .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonCheck.auth_user_creator_id)
                    .ToListAsync();

                        // agar yetarli bo‘lsa kamaytirish
                        skladTovarList.First().qty = skladTovarList.First().qty - item.qty;
                        skladTovarList.First().real_qty = skladTovarList.First().real_qty - item.qty; // bu nimaga kerakligini rosti tushunmadim

                        _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                        await _context.SaveChangesAsync();


                        // bu sklad tarixiga yozib quyadi
                        TegirmonSkladHistory history = new TegirmonSkladHistory();
                        history.active_status = true;
                        history.qty = item.qty;
                        history.type = 2; // chiqim
                        history.price = item.summa;
                        history.TegirmonProductId = item.TegirmonProductid;
                        history.TegirmonSkladId = tegirmonCheck.auth_user_creator_id ?? 0; // bu sklad id si
                        history.note = "<<Sotuv bulimidan sotildi>>";
                        history.text = "Sotuv";
                        history.userId = tegirmonCheck.id;
                        history.user_name = tegirmonCheck.image_base_64; // bu user_name
                        history.auth_user_creator_id = tegirmonCheck.TegirmonAuthid; // bu user auth is si

                        _context.TegirmonSkladHistory.Update(history);
                        await _context.SaveChangesAsync();

                    }

                }
                
            }

            if(tegirmonCheck.auth_user_updator_id != 0){
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p =>p.TegirmonKassaid == tegirmonCheck.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                
                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p =>p.TegirmonKassaid == tegirmonCheck.auth_user_updator_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();

                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonCheck.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonCheck.for_buy_tovar_rasxod;
                        tegirmon_kassa_info.dollor = tegirmonCheck.real_sum;
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonCheck.auth_user_updator_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonCheck.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonCheck.for_buy_tovar_rasxod;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + tegirmonCheck.real_sum;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonCheck.auth_user_updator_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                }
                else{
                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonCheck.cash;
                    tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonCheck.real_sum;
                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonCheck.for_buy_tovar_rasxod;

                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                    await _context.SaveChangesAsync();
                }

            
                // return tegirmonKassaInfo.Last();
            }
        
            return tegirmonCheck;
        }



        // DELETE: api/TegirmonCheck/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonCheck>> DeleteTegirmonCheck(long id)
        {
            var tegirmonCheck = await _context.TegirmonCheck.FindAsync(id);
            if (tegirmonCheck == null)
            {
                return NotFound();
            }

            _context.TegirmonCheck.Remove(tegirmonCheck);
            await _context.SaveChangesAsync();

            return tegirmonCheck;
        }

        private bool TegirmonCheckExists(long id)
        {
            return _context.TegirmonCheck.Any(e => e.id == id);
        }
    }
}
