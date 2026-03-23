using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonUserIshlaganPuliController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserIshlaganPuliController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserIshlaganVaqt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> GetTegirmonUserIshlaganPuli()
        {
            return await _context.TegirmonUserIshlaganPuli.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserIshlaganPuli> categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserIshlaganPuli>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli.Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysReports")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysReports([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid  && p.num == 1 && (p.K_date >= begin && p.K_date < end))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 1  && (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysReportsStatus")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysReportsStatus([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num != 5 && (p.K_date >= begin && p.K_date < end))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num != 5 && (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserSelectMonthSalary")]
        public async Task<ActionResult<TexPaginationModel>> getUserSelectMonthSalary([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 5 && (p.created_date_time  >= begin && p.created_date_time  < end))
                .OrderByDescending(p => p.created_date_time)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 5 && (p.created_date_time  >= begin && p.created_date_time  < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysBeginEnd")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysBeginEnd(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] int userid,
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid &&
                            p.num == 1 &&
                            p.K_date >= begin_date &&
                            p.K_date < end_date.AddDays(1)) // ⬅️ e'tibor bering
                .OrderByDescending(p => p.K_date)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid &&
                            p.K_date >= begin_date &&
                            p.K_date < end_date.AddDays(1)) // ⬅️ e'tibor bering
                .CountAsync();

            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysRasxod")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysRasxod([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid  && p.num == 2 && (p.K_date >= begin && p.K_date < end))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 2 &&  (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysRasxodBeginEnd")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysRasxodBeginEnd(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] int userid,
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 2 && (p.K_date >= begin_date && p.K_date < end_date.AddDays(1)))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 2 && (p.K_date >= begin_date && p.K_date < end_date.AddDays(1)))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysRasxodEnd")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysRasxodEnd(
                [FromQuery] int userid,
                [FromQuery] DateTime end_date)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 2 && p.inv_accepted_status == false
                        && (p.K_date.Date >= start_date && p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 2 && p.inv_accepted_status == false
                        && (p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
                
            }


            [HttpGet("getUserWorkedDaysRasxodEndPaidSalary")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysRasxodEndPaidSalary(
                [FromQuery] int userid,
                [FromQuery] DateTime end_date,
                [FromQuery] int invoice_id)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5 && x.id != invoice_id)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 2 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date.Date >= start_date && p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 2 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
                
            }


            [HttpGet("getUserWorkedDaysPrixodEndPaidSalary")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysPrixodEndPaidSalary(
                [FromQuery] int userid,
                [FromQuery] DateTime end_date,
                [FromQuery] int invoice_id)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5 && x.id != invoice_id)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 3 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date.Date >= start_date && p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 3 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
                
            }


        [HttpGet("getUserWorkedDaysPrixodEnd")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysPrixodEnd(
                [FromQuery] int userid,
                [FromQuery] DateTime end_date)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Prixodlarni olish (num == 3)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 3 && p.inv_accepted_status == false
                        && (p.K_date.Date >= start_date && p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 3 && p.inv_accepted_status == false
                        && (p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
                
            }

        [HttpGet("getUserWorkedDaysSalaryEnd")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysSalaryEnd(
                [FromQuery] int userid,
                [FromQuery] DateTime end_date)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == false
                        && (p.K_date.Date >= start_date && p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == false
                        && (p.K_date < end_date.AddDays(1)))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
                
            }

            [HttpGet("getUserWorkedDaysSalaryOldDaysForReports")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysSalaryOldDaysForReports(
                [FromQuery] int userid,
                [FromQuery] DateTime begin_date)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == false
                        && (p.K_date.Date >= start_date && p.K_date < begin_date))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == false
                        && (p.K_date < begin_date))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();
                return categoryList;
            }
                
            }


            // bu oylik berilgandan keyin hisobotini kurish uchun oylik berilganda utgan kunlarni hisoboti
            [HttpGet("getUserWorkedDaysSalaryOldDaysForReportsPaidSalary")]
            public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> getUserWorkedDaysSalaryOldDaysForReportsPaidSalary(
                [FromQuery] int userid,
                [FromQuery] DateTime begin_date,
                [FromQuery] int invoice_id)
            {
                // 1. Oxirgi num == 5 yozuvini topamiz
                var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                    .Where(x => x.userid == userid && x.num == 5 && x.id != invoice_id)
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

            // 2. Agar topilsa, begin_date ni yangilaymiz
            if (lastNum5Record != null)
            {
                // 3. Rasxodlarni olish (num == 2)
                var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;

                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date.Date >= start_date && p.K_date < begin_date))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();

                return categoryList;
            }
            else
            {
                var categoryList = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 1 && p.inv_accepted_status == true && p.auth_user_updator_id == invoice_id
                        && (p.K_date < begin_date))
                    .OrderByDescending(p => p.K_date)
                    .ToListAsync();
                return categoryList;
            }
                
            }


        [HttpGet("getUserWorkedDaysPrixod")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysPrixod([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid  && p.num == 3 && (p.K_date >= begin && p.K_date < end))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 3 && (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getUserWorkedDaysPrixodBeginEnd")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDaysPrixodBeginEnd(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] int userid,
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 3 && (p.K_date >= begin_date && p.K_date < end_date.AddDays(1)))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num == 3 && (p.K_date >= begin_date && p.K_date < end_date.AddDays(1)))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        [HttpGet("getLastUserWorkedOylikSumma")]
            public async Task<ActionResult<TegirmonUserIshlaganPuli>> GetLastUserWorkedOylikSumma([FromQuery] int userid)
            {
                var lastItem = await _context.TegirmonUserIshlaganPuli
                    .Where(p => p.userid == userid && p.num == 5)
                    .OrderByDescending(p => p.K_date) // yoki OrderByDescending(p => p.id)
                    .FirstOrDefaultAsync();

                if (lastItem == null)
                {
                    return NotFound();
                }

                return lastItem;
            }

        [HttpGet("getPaginationUserTulovList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserTulovList([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserIshlaganPuli> categoryList = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.num != 1)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserIshlaganPuli>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganPuli.Where(p => p.userid == userid && p.num != 1).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        
        // tanlangan kundaigi userni malumotlarini chiqaradi
        [HttpGet("getUserSalaryByDay")]
        public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganPuli>>> GetUserSalaryByDay(
            [FromQuery] long userid,
            [FromQuery] DateTime date)
        {
            var startDate = date.Date;
            var endDate = date.Date.AddDays(1); // faqat shu kundagi yozuvlar

            var result = await _context.TegirmonUserIshlaganPuli
                .Where(p => p.userid == userid && p.K_date >= startDate && p.K_date < endDate && p.num == 1)
                .OrderBy(p => p.K_date)
                .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound("Ushbu kunda ma'lumot topilmadi.");
            }
            
            return result;
        }

        // GET: api/TegirmonUserIshlaganPuli/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserIshlaganPuli>> GetTegirmonUserIshlaganPuli(long id)
        {
            var tegirmonUserIshlaganPuli = await _context.TegirmonUserIshlaganPuli.FindAsync(id);

            if (tegirmonUserIshlaganPuli == null)
            {
                return NotFound();
            }

            return tegirmonUserIshlaganPuli;
        }

        // PUT: api/TegirmonUserIshlaganVaqt/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserIshlaganPuli(long id, TegirmonUserIshlaganPuli tegirmonUserIshlaganPuli)
        {
            if (id != tegirmonUserIshlaganPuli.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserIshlaganPuli).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserIshlaganPuliExists(id))
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

        

        // POST: api/TegirmonUserIshlaganPuli
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonUserIshlaganPuli>> PostTegirmonUserIshlaganPuli(TegirmonUserIshlaganPuli tegirmonUserIshlaganPuli)
        {
            var existing = await _context.TegirmonUserIshlaganPuli
                    .FirstOrDefaultAsync(x => x.id == tegirmonUserIshlaganPuli.id);
            
            if(existing != null){
                //  _context.TegirmonUserIshlaganPuli.Update(tegirmonUserIshlaganPuli); // Update emas, Add ishlatilmoqda
                existing.work_time = tegirmonUserIshlaganPuli.work_time;
                existing.sum = tegirmonUserIshlaganPuli.sum;
                existing.K_date = tegirmonUserIshlaganPuli.K_date;
                existing.created_date = tegirmonUserIshlaganPuli.created_date;
                existing.note = tegirmonUserIshlaganPuli.note;
                existing.auth_user_updator_id = tegirmonUserIshlaganPuli.auth_user_updator_id;
                existing.auth_user_creator_id = tegirmonUserIshlaganPuli.auth_user_creator_id;
                 if(tegirmonUserIshlaganPuli.num == 1){
                    
                    var userOylik = await _context.TegirmonUserOylik
                        .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid);
                    
                    if (userOylik != null)
                    {
                        userOylik.sum += (double)tegirmonUserIshlaganPuli.auth_user_creator_id;
                    }
                    else
                    {
                        _context.TegirmonUserOylik.Add(new TegirmonUserOylik
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sum = (double)tegirmonUserIshlaganPuli.auth_user_creator_id
                        });
                    }


                    var existingRecord = await _context.TegirmonUserIshlaganVaqt
                        .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid && x.K_date.Date == tegirmonUserIshlaganPuli.K_date.Date);

                    if (existingRecord != null)
                    {
                        existingRecord.work_time = tegirmonUserIshlaganPuli.work_time;
                        existingRecord.note = "Yangilandi update";
                    }
                    else
                    {
                        _context.TegirmonUserIshlaganVaqt.Add(new TegirmonUserIshlaganVaqt
                        {
                            userid = tegirmonUserIshlaganPuli.userid,
                            work_time = tegirmonUserIshlaganPuli.work_time,
                            K_date = tegirmonUserIshlaganPuli.K_date,
                            created_date = tegirmonUserIshlaganPuli.created_date,
                            note = "Yangi  qo‘shildi update"
                        });
                    }

                    var kirish = await _context.SkudMyCheckinout
                        .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.sana.Date == tegirmonUserIshlaganPuli.K_date.Date && x.checktype == "K" )
                        .OrderByDescending(x => x.sana)
                        .ThenByDescending(x => x.checktime)
                        .FirstOrDefaultAsync();

                    if (kirish != null){
                        kirish.sana = tegirmonUserIshlaganPuli.K_date.Date;
                        kirish.checktime = tegirmonUserIshlaganPuli.K_date.TimeOfDay;
                    }
                    else{
                        var yangi = new SkudMyCheckinout
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sana = tegirmonUserIshlaganPuli.K_date.Date,
                            checktime = tegirmonUserIshlaganPuli.K_date.TimeOfDay,
                            checktype = "K",
                            door_name = "Kirish"
                        };
                        await _context.SkudMyCheckinout.AddAsync(yangi);
                    }

                    var chiqish = await _context.SkudMyCheckinout
                        .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.sana.Date == tegirmonUserIshlaganPuli.created_date.Date && x.checktype == "C" )
                        .OrderBy(x => x.sana)
                        .ThenBy(x => x.checktime)
                        .FirstOrDefaultAsync();

                    if (chiqish != null){
                        chiqish.sana = tegirmonUserIshlaganPuli.created_date.Date;
                        chiqish.checktime = tegirmonUserIshlaganPuli.created_date.TimeOfDay;
                    }
                    else{
                        var yangichiqish = new SkudMyCheckinout
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sana = tegirmonUserIshlaganPuli.created_date.Date,
                            checktime = tegirmonUserIshlaganPuli.created_date.TimeOfDay,
                            checktype = "C",
                            door_name = "user"
                        };
                        await _context.SkudMyCheckinout.AddAsync(yangichiqish);
                    }

                 }
            }
            else{
                 _context.TegirmonUserIshlaganPuli.Add(tegirmonUserIshlaganPuli);
                
                if(tegirmonUserIshlaganPuli.num == 5){
                    
                    // kunlik ishchilarni oylik berish qismi
                    if(tegirmonUserIshlaganPuli.num1 == 1){
                        var userOylik = await _context.TegirmonUserOylik
                            .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid);

                        // var old_debit = 0;

                        // if (tegirmonUserIshlaganPuli.old_debt == 0)
                        // {
                        //     old_debit = tegirmonUserIshlaganPuli.hisoblan_sum + tegirmonUserIshlaganPuli.hisoblan_sum - tegirmonUserIshlaganPuli.hisoblan_sum - tegirmonUserIshlaganPuli.hisoblan_sum;
                        // }
                        
                        if (userOylik != null)
                        {
                            userOylik.sum -= (double)tegirmonUserIshlaganPuli.sum;
                        }
                        else
                        {
                            tegirmonUserIshlaganPuli.sum = -Math.Abs(tegirmonUserIshlaganPuli.sum);
                            _context.TegirmonUserOylik.Add(new TegirmonUserOylik
                            {
                                userid = (long)tegirmonUserIshlaganPuli.userid,
                                sum = (double)tegirmonUserIshlaganPuli.sum
                            });
                        }
                        tegirmonUserIshlaganPuli.sum = Math.Abs(tegirmonUserIshlaganPuli.sum);

                        var lastRecordUser = _context.TegirmonUserIshlaganPuli
                            .OrderByDescending(x => x.id) // eng kattasi birinchi bo'ladi
                            .FirstOrDefault();

                        var newId = (lastRecordUser?.id ?? 0) + 1;

                        
                        DateTime startDate = tegirmonUserIshlaganPuli.created_date_time?.Date ?? DateTime.MinValue;
                        DateTime endDate = (tegirmonUserIshlaganPuli.updated_date_time?.Date.AddDays(1) ?? DateTime.MaxValue);

                        // 1. Shu userning eng oxirgi num == 5 yozuvini topamiz
                        var lastNum5Record = await _context.TegirmonUserIshlaganPuli
                            .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.num == 5)
                            .OrderByDescending(x => x.id) // eng oxirgi sana bo‘yicha
                            .FirstOrDefaultAsync();

                        // 2. Agar topilgan bo‘lsa, keyingi query uchun ishlatamiz
                        if (lastNum5Record != null)
                        {
                            var start_date = lastNum5Record.updated_date_time?.Date ?? DateTime.MinValue;
                            var recordsToUpdate = await _context.TegirmonUserIshlaganPuli
                                .Where(x => x.userid == tegirmonUserIshlaganPuli.userid
                                            && x.inv_accepted_status == false
                                            && x.K_date < endDate
                                            && x.K_date.Date >= start_date) // faqat oxirgi 5 dan keyingi yozuvlar
                                .ToListAsync();

                                foreach (var record in recordsToUpdate)
                                {
                                    record.inv_accepted_status = true;
                                    record.auth_user_updator_id = newId;
                                }

                            // recordsToUpdate bilan ishlash
                        }
                        else
                        {
                            // Agar num == 5 yo‘q bo‘lsa, odatdagidek ishlaymiz
                            var recordsToUpdate = await _context.TegirmonUserIshlaganPuli
                                .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.inv_accepted_status == false
                                            && x.K_date < endDate)
                                .ToListAsync();

                            foreach (var record in recordsToUpdate)
                            {
                                record.inv_accepted_status = true;
                                record.auth_user_updator_id = newId;
                            }
                        }

                        
                    }

                }
                 else if(tegirmonUserIshlaganPuli.num != 1){

                    var userOylik = await _context.TegirmonUserOylik
                        .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid);
                    
                    if (userOylik != null)
                    {
                        if(tegirmonUserIshlaganPuli.num == 2){
                            tegirmonUserIshlaganPuli.sum = -Math.Abs(tegirmonUserIshlaganPuli.sum);
                        }
                        userOylik.sum += (double)tegirmonUserIshlaganPuli.sum;
                    }
                    else
                    {
                        if(tegirmonUserIshlaganPuli.num == 2){
                            tegirmonUserIshlaganPuli.sum = -Math.Abs(tegirmonUserIshlaganPuli.sum);
                        }
                        _context.TegirmonUserOylik.Add(new TegirmonUserOylik
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sum = (double)tegirmonUserIshlaganPuli.sum
                        });
                    }
                    tegirmonUserIshlaganPuli.sum = Math.Abs(tegirmonUserIshlaganPuli.sum);
                 }
                 else{
                    
                    var userOylik = await _context.TegirmonUserOylik
                        .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid);
                    
                    if (userOylik != null)
                    {
                        userOylik.sum += (double)tegirmonUserIshlaganPuli.sum;
                    }
                    else
                    {
                        _context.TegirmonUserOylik.Add(new TegirmonUserOylik
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sum = (double)tegirmonUserIshlaganPuli.sum
                        });
                    }


                    var existingRecord = await _context.TegirmonUserIshlaganVaqt
                        .FirstOrDefaultAsync(x => x.userid == tegirmonUserIshlaganPuli.userid && x.K_date.Date == tegirmonUserIshlaganPuli.K_date.Date);

                    if (existingRecord != null)
                    {
                        existingRecord.work_time = tegirmonUserIshlaganPuli.work_time;
                        existingRecord.note = "Yangilandi update";
                    }
                    else
                    {
                        _context.TegirmonUserIshlaganVaqt.Add(new TegirmonUserIshlaganVaqt
                        {
                            userid = tegirmonUserIshlaganPuli.userid,
                            work_time = tegirmonUserIshlaganPuli.work_time,
                            K_date = tegirmonUserIshlaganPuli.K_date,
                            created_date = tegirmonUserIshlaganPuli.created_date,
                            note = "Yangi  qo‘shildi update"
                        });
                    }
                
                    var kirish = await _context.SkudMyCheckinout
                        .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.sana.Date == tegirmonUserIshlaganPuli.K_date.Date && x.checktype == "K" )
                        .OrderByDescending(x => x.sana)
                        .ThenByDescending(x => x.checktime)
                        .FirstOrDefaultAsync();

                    if (kirish != null){
                        kirish.sana = tegirmonUserIshlaganPuli.K_date.Date;
                        kirish.checktime = tegirmonUserIshlaganPuli.K_date.TimeOfDay;
                    }
                    else{
                        var yangi = new SkudMyCheckinout
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sana = tegirmonUserIshlaganPuli.K_date.Date,
                            checktime = tegirmonUserIshlaganPuli.K_date.TimeOfDay,
                            checktype = "K",
                            door_name = "Kirish"
                        };
                        await _context.SkudMyCheckinout.AddAsync(yangi);
                    }


                    var chiqish = await _context.SkudMyCheckinout
                        .Where(x => x.userid == tegirmonUserIshlaganPuli.userid && x.sana.Date == tegirmonUserIshlaganPuli.created_date.Date && x.checktype == "C" )
                        .OrderBy(x => x.sana)
                        .ThenBy(x => x.checktime)
                        .FirstOrDefaultAsync();

                    if (chiqish != null){
                        chiqish.sana = tegirmonUserIshlaganPuli.created_date.Date;
                        chiqish.checktime = tegirmonUserIshlaganPuli.created_date.TimeOfDay;
                    }
                    else{
                        var yangichiqish = new SkudMyCheckinout
                        {
                            userid = (long)tegirmonUserIshlaganPuli.userid,
                            sana = tegirmonUserIshlaganPuli.created_date.Date,
                            checktime = tegirmonUserIshlaganPuli.created_date.TimeOfDay,
                            checktype = "C",
                            door_name = "user"
                        };
                        await _context.SkudMyCheckinout.AddAsync(yangichiqish);
                    }


                 }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonUserIshlaganPuli", new { id = tegirmonUserIshlaganPuli.id }, tegirmonUserIshlaganPuli);
        }


        // DELETE: api/TegirmonUserIshlaganPuli/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserIshlaganPuli>> DeleteTegirmonUserIshlaganPuli(long id)
        {
            var tegirmonUserIshlaganPuli = await _context.TegirmonUserIshlaganPuli.FindAsync(id);
            if (tegirmonUserIshlaganPuli == null)
            {
                return NotFound();
            }

            _context.TegirmonUserIshlaganPuli.Remove(tegirmonUserIshlaganPuli);
            await _context.SaveChangesAsync();

            return tegirmonUserIshlaganPuli;
        }

        private bool TegirmonUserIshlaganPuliExists(long id)
        {
            return _context.TegirmonUserIshlaganPuli.Any(e => e.id == id);
        }
    }
}
