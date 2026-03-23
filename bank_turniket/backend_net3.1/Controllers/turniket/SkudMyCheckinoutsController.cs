using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudMyCheckinoutsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudMyCheckinoutsController(ApplicationContext context)
        {
            _context = context;
        }

         public class UserCheckDto
            {
                public long UserId { get; set; }
                public string Fio { get; set; }
                public string Kirish { get; set; }
                public string Chiqish { get; set; }
                public double Sum { get; set; }        // ishlagan summa
                public string WorkedTime { get; set; } // ishlagan vaqt
                public bool IsLate { get; set; }
                public bool IsYuqlama { get; set; }
                public SkudMyUserinfo User { get; set; }
            }

        public class UserCheckResponse
            {
                public int TotalUsers { get; set; }
                public int CheckedInUsers { get; set; }
                public List<UserCheckDto> Users { get; set; }
            }

        [HttpGet("by-date")]
        public async Task<ActionResult<List<UserCheckDto>>> GetByDate([FromQuery] DateTime sana, [FromQuery] int smena_id, [FromQuery] int dept_id)
        {
            var users = await _context.SkudMyUserinfo.ToListAsync();

            users = users
                .Where(u => (dept_id == 0 || u.departid == dept_id) && (smena_id == 0 || u.group_id == smena_id))
                .ToList();
                
            var checklar = await _context.SkudMyCheckinout
                .Where(c => c.sana == sana)
                .ToListAsync();

            var yuqlama = await _context.TegirmonUserYuqlama
            .Where(p => p.K_date.Date == sana.Date)
            .ToListAsync();


            var ishlar = await _context.TegirmonUserIshlaganPuli
            .Where(p => p.K_date.Date == sana.Date && p.num == 1)
            .ToListAsync();

            var ishlagan_vaqt = await _context.TegirmonUserIshlaganVaqt
            .Where(p => p.K_date.Date == sana.Date)
            .ToListAsync();

            var natija = new List<UserCheckDto>();

            foreach (var user in users)
            {
                var userChecklar = checklar
                    .Where(c => c.userid == user.userid)
                    .ToList();

                var kirish = userChecklar
                    .Where(c => c.checktype == "K")
                    .OrderBy(c => c.checktime)
                    .FirstOrDefault();

                bool isLate = false;

                if (kirish != null && user.group_id != null)
                {
                    var smena = await _context.SkudSmena
                        .FirstOrDefaultAsync(s => s.id == user.group_id);

                    if (smena != null)
                    {
                        if (kirish.checktime > smena.boshlanishi)
                        {
                            isLate = true;
                        }
                    }
                }

                var chiqish = userChecklar
                    .Where(c => c.checktype == "C")
                    .OrderByDescending(c => c.checktime)
                    .FirstOrDefault();

                // Shu userning shu kundagi ishlari
                var userIshlar = ishlar
                    .Where(i => i.userid == user.userid)
                    .ToList();

                double jamiSum = userIshlar.Sum(i => i.sum);
                
                
                // Ishlangan vaqti
                var ish_vaqt = ishlagan_vaqt
                    .Where(c => c.userid == user.userid)
                    .FirstOrDefault();
                
                // Yuqlamdan javob olgan yoki yuqligi haqida
                bool isYuqlama = false;

                var user_yuqlama = yuqlama
                    .Where(c => c.userid == user.userid && c.status == true)
                    .FirstOrDefault();

                if(user_yuqlama != null){
                    isYuqlama = true;
                }

                natija.Add(new UserCheckDto
                {
                    UserId = (long)user.userid,
                    Fio = user.ism,
                    Kirish = kirish != null ? kirish.checktime.ToString(@"hh\:mm") : "",
                    Chiqish = chiqish != null ? chiqish.checktime.ToString(@"hh\:mm") : "",
                    Sum = Math.Round(jamiSum, 0),
                    WorkedTime = ish_vaqt?.work_time.ToString(@"hh\:mm") ?? "", // ⬅️ bu yer e’tiborli,
                    IsLate = isLate,
                    IsYuqlama = isYuqlama,
                    User = user
                });
            }
            var checkedInCount = natija.Count(u => !string.IsNullOrEmpty(u.Kirish));

            natija = natija
                .OrderByDescending(u => !string.IsNullOrEmpty(u.Kirish) && !string.IsNullOrEmpty(u.Chiqish))
                .ThenByDescending(u => !string.IsNullOrEmpty(u.Kirish) && string.IsNullOrEmpty(u.Chiqish))
                .ThenByDescending(u => string.IsNullOrEmpty(u.Kirish) && !string.IsNullOrEmpty(u.Chiqish))
                .ToList();

            var response = new UserCheckResponse
                {
                    TotalUsers = users.Count,
                    CheckedInUsers = checkedInCount,
                    Users = natija
                };
            return Ok(response);
        }


        // xodimlarni bir oylik hisoboti

        public class UserCheckDtoMonth
            {
                public long UserId { get; set; }
                public string Fio { get; set; }
                public string Kirish { get; set; }
                public string Chiqish { get; set; }
                public double Sum { get; set; }        // ishlagan summa
                public string WorkedTime { get; set; } // ishlagan vaqt
                public bool IsLate { get; set; }
                public bool IsYuqlama { get; set; }
                public DateTime day { get; set; }
                public SkudMyUserinfo User { get; set; }
            }

        [HttpGet("by-date_month")]
        public async Task<ActionResult<List<UserCheckDtoMonth>>> GetByDateMonth([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int user_id)
        {

            // var user = await _context.SkudMyUserinfo
            //     .Where(c => c.userid == user_id)
            //     .FirstOrDefault();

            var user = await _context.SkudMyUserinfo.FirstOrDefaultAsync(x => x.userid == user_id);
            
            var checklar = await _context.SkudMyCheckinout
                .Where(p => p.userid == user_id && p.sana >= startDate.Date && p.sana <= endDate.Date)
                .ToListAsync();

            var yuqlama = await _context.TegirmonUserYuqlama
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date)
            .ToListAsync();

            var ishlagan_puli = await _context.TegirmonUserIshlaganPuli
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date && p.num == 1)
            .ToListAsync();

            var ishlagan_vaqt = await _context.TegirmonUserIshlaganVaqt
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date)
            .ToListAsync();

            var natija = new List<UserCheckDtoMonth>();
            

            DateTime sana = startDate.Date;
            while (sana <= endDate.Date)
            {

                var userChecklar = checklar
                    .Where(c => c.sana == sana.Date)
                    .ToList();

                var kirish = userChecklar
                    .Where(c => c.checktype == "K")
                    .OrderBy(c => c.checktime)
                    .FirstOrDefault();

                bool isLate = false;

                if (kirish != null && user.group_id != null)
                {
                    var smena = await _context.SkudSmena
                        .FirstOrDefaultAsync(s => s.id == user.group_id);

                    if (smena != null)
                    {
                        if (kirish.checktime > smena.boshlanishi)
                        {
                            isLate = true;
                        }
                    }
                }

                var chiqish = userChecklar
                    .Where(c => c.checktype == "C")
                    .OrderByDescending(c => c.checktime)
                    .FirstOrDefault();

                // Shu userning shu kundagi ishlari
                var userIshlar = ishlagan_puli
                    .Where(i => i.K_date.Date == sana.Date)
                    .ToList();

                double jamiSum = userIshlar.Sum(i => i.sum);

                var userIshlarPuli = ishlagan_puli
                    .Where(i => i.K_date.Date == sana.Date)
                    .FirstOrDefault();

                var kirish_vaqt = "";
                var chiqish_vaqt = "";

                if (userIshlarPuli != null)
                {
                    kirish_vaqt = userIshlarPuli.K_date.ToString("HH:mm");
                    chiqish_vaqt = userIshlarPuli.created_date.ToString("HH:mm");
                }
                else
                {
                    kirish_vaqt = kirish != null ? kirish.checktime.ToString(@"hh\:mm") : "--:--";
                    chiqish_vaqt = chiqish != null ? chiqish.checktime.ToString(@"hh\:mm") : "--:--";
                }

                // Ishlangan vaqti
                var ish_vaqt = ishlagan_vaqt
                    .Where(c => c.K_date.Date == sana.Date)
                    .FirstOrDefault();

                // Yuqlamdan javob olgan yoki yuqligi haqida
                bool isYuqlama = false;

                var user_yuqlama = yuqlama
                    .Where(c => c.K_date.Date == sana.Date && c.status == true)
                    .FirstOrDefault();

                if (user_yuqlama != null)
                {
                    isYuqlama = true;
                }

                natija.Add(new UserCheckDtoMonth
                {
                    UserId = (long)user.userid,
                    Fio = user.ism,
                    Kirish = kirish_vaqt,
                    Chiqish = chiqish_vaqt,
                    Sum = Math.Round(jamiSum, 0),
                    WorkedTime = ish_vaqt?.work_time.ToString(@"hh\:mm") ?? "", // ⬅️ bu yer e’tiborli,
                    IsLate = isLate,
                    IsYuqlama = isYuqlama,
                    day = sana,
                    User = user
                });
                

                sana = sana.AddDays(1);
            }

            natija = natija
                .OrderBy(u => u.day)
                .ToList();

            return Ok(natija);
        }
        // xodimlarni bir oylik hisoboti tugashi


        [HttpGet("by-date_month_payedSalary")]
        public async Task<ActionResult<List<UserCheckDtoMonth>>> GetByDateMonthPayedSalary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int user_id)
        {

            // var user = await _context.SkudMyUserinfo
            //     .Where(c => c.userid == user_id)
            //     .FirstOrDefault();

            var user = await _context.SkudMyUserinfo.FirstOrDefaultAsync(x => x.userid == user_id);
            
            var checklar = await _context.SkudMyCheckinout
                .Where(p => p.userid == user_id && p.sana >= startDate.Date && p.sana <= endDate.Date)
                .ToListAsync();

            var yuqlama = await _context.TegirmonUserYuqlama
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date)
            .ToListAsync();

            var ishlagan_puli = await _context.TegirmonUserIshlaganPuli
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date && p.num == 1 && p.inv_accepted_status == true)
            .ToListAsync();

            var ishlagan_vaqt = await _context.TegirmonUserIshlaganVaqt
            .Where(p => p.userid == user_id && p.K_date.Date >= startDate.Date && p.K_date.Date <= endDate.Date)
            .ToListAsync();

            var natija = new List<UserCheckDtoMonth>();
            

            DateTime sana = startDate.Date;
            while (sana <= endDate.Date)
            {

                var userChecklar = checklar
                    .Where(c => c.sana == sana.Date)
                    .ToList();

                var kirish = userChecklar
                    .Where(c => c.checktype == "K")
                    .OrderBy(c => c.checktime)
                    .FirstOrDefault();

                bool isLate = false;

                if (kirish != null && user.group_id != null)
                {
                    var smena = await _context.SkudSmena
                        .FirstOrDefaultAsync(s => s.id == user.group_id);

                    if (smena != null)
                    {
                        if (kirish.checktime > smena.boshlanishi)
                        {
                            isLate = true;
                        }
                    }
                }

                var chiqish = userChecklar
                    .Where(c => c.checktype == "C")
                    .OrderByDescending(c => c.checktime)
                    .FirstOrDefault();

                // Shu userning shu kundagi ishlari
                var userIshlar = ishlagan_puli
                    .Where(i => i.K_date.Date == sana.Date)
                    .ToList();

                double jamiSum = userIshlar.Sum(i => i.sum);

                var userIshlarPuli = ishlagan_puli
                    .Where(i => i.K_date.Date == sana.Date)
                    .FirstOrDefault();

                var kirish_vaqt = "";
                var chiqish_vaqt = "";

                if (userIshlarPuli != null)
                {
                    kirish_vaqt = userIshlarPuli.K_date.ToString("HH:mm");
                    chiqish_vaqt = userIshlarPuli.created_date.ToString("HH:mm");
                }
                else
                {
                    kirish_vaqt = kirish != null ? kirish.checktime.ToString(@"hh\:mm") : "--:--";
                    chiqish_vaqt = chiqish != null ? chiqish.checktime.ToString(@"hh\:mm") : "--:--";
                }

                // Ishlangan vaqti
                var ish_vaqt = ishlagan_vaqt
                    .Where(c => c.K_date.Date == sana.Date)
                    .FirstOrDefault();

                // Yuqlamdan javob olgan yoki yuqligi haqida
                bool isYuqlama = false;

                var user_yuqlama = yuqlama
                    .Where(c => c.K_date.Date == sana.Date && c.status == true)
                    .FirstOrDefault();

                if (user_yuqlama != null)
                {
                    isYuqlama = true;
                }

                natija.Add(new UserCheckDtoMonth
                {
                    UserId = (long)user.userid,
                    Fio = user.ism,
                    Kirish = kirish_vaqt,
                    Chiqish = chiqish_vaqt,
                    Sum = Math.Round(jamiSum, 0),
                    WorkedTime = ish_vaqt?.work_time.ToString(@"hh\:mm") ?? "", // ⬅️ bu yer e’tiborli,
                    IsLate = isLate,
                    IsYuqlama = isYuqlama,
                    day = sana,
                    User = user
                });
                

                sana = sana.AddDays(1);
            }

            natija = natija
                .OrderBy(u => u.day)
                .ToList();

            return Ok(natija);
        }
        // xodimlarni bir oylik hisoboti tugashi


        // GET: api/SkudMyCheckinouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudMyCheckinout>>> GetSkudMyCheckinout()
        {
            return await _context.SkudMyCheckinout.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .OrderByDescending(p => p.code)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList) {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);
            
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }      
        
        [HttpGet("getPaginationWithAllWorkingDaysInfo")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWithAllWorkingDaysInfo([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");

            List<SkudWorkingDaysTemp> itemList = await _context.SkudWorkingDaysTemp.FromSqlRaw("SELECT " +
                " \r\nmu.userid as user_id, " +
                " \r\nmu.ism as full_name," +
                " \r\nCOALESCE((SELECT COUNT(mc.userid) FROM my_checkinout mc WHERE mc.sana = CURRENT_DATE  AND mc.userid=mu.userid),0) as cur_day_count," +
                " \r\nCOALESCE((SELECT COUNT(*) FROM " +
                " \r\n(SELECT t.userid as idt FROM my_checkinout t WHERE t.sana " +
                " \r\nBETWEEN '"+ begin_date_str + "' AND '"+ end_date_str + "' GROUP BY t.userid, t.sana) " +
                " \r\nk WHERE idt = mu.userid),0) as working_days_count" +
                " \r\nFROM my_userinfo mu" +
                " \r\nORDER BY mu.ism")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudWorkingDaysTemp>();
            }




            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationKeganlarVaqtiBilan")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationKeganlarVaqtiBilan([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p=>p.skudOylik)
                .OrderByDescending(p => p.userid)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            List<SkudMyUserinfo> myUserinfos= new List<SkudMyUserinfo>();

            foreach (SkudMyUserinfo itm in itemList)
            {
                List<SkudMyCheckinout> skudMyCheckinouts = await _context.SkudMyCheckinout
                    .Where(p => p.sana == need_date.Date
                    && (p.checktype.Contains("K") || p.checktype.Contains("I"))
                    && p.userid == itm.userid)
                    .OrderBy(p => p.checktime)
                    .ToListAsync();

                if (skudMyCheckinouts != null && skudMyCheckinouts.Count > 0) {
                    SkudMyCheckinout skudMy = skudMyCheckinouts[0];
                    itm.vaqt_kegan = skudMy.checktime;
                    itm.skudMyCheckinouts = skudMyCheckinouts;
                    myUserinfos.Add(itm);

                }

            }


            paginationModel.items_list = JArray.FromObject(myUserinfos);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = myUserinfos.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetByDateTime")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByDateTime([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .Where(p =>p.sana >= begin_date.Date && p.sana <= end_date.Date)
                .OrderByDescending(p => p.code)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.Where(p => p.sana >= begin_date.Date && p.sana <= end_date.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetByDateTimeByUserID")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByDateTimeByUserID([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime date, [FromQuery] int user_id )
        {
            var begin_date = date.Date; // 2026-06-24 00:00:00
            var end_date = date.Date.AddDays(1).AddTicks(-1); // 2026-06-24 23:59:59.9999999

            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .Where(p => p.userid == user_id && (p.sana >= begin_date.Date && p.sana <= end_date.Date))
                .OrderBy(p => p.sana)
                .ThenBy(p => p.checktime)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.Where(p => p.userid == user_id && (p.sana >= begin_date.Date && p.sana <= end_date.Date)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetByUserID")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByUserID([FromQuery] int page,
            [FromQuery] int size, [FromQuery] int user_id )
        {

            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .Where(p => p.userid == user_id )
                .OrderBy(p => p.sana)
                .ThenBy(p => p.checktime)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            // foreach (SkudMyCheckinout itm in itemList)
            // {
            //     itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            // }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.Where(p => p.userid == user_id ).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/SkudMyCheckinouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudMyCheckinout>> GetSkudMyCheckinout(long id)
        {
            var skudMyCheckinout = await _context.SkudMyCheckinout.FindAsync(id);

            if (skudMyCheckinout == null)
            {
                return NotFound();
            }

            return skudMyCheckinout;
        }

        // PUT: api/SkudMyCheckinouts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudMyCheckinout(long id, SkudMyCheckinout skudMyCheckinout)
        {
            if (id != skudMyCheckinout.code)
            {
                return BadRequest();
            }

            _context.Entry(skudMyCheckinout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudMyCheckinoutExists(id))
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

        // POST: api/SkudMyCheckinouts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudMyCheckinout>> PostSkudMyCheckinout(SkudMyCheckinout skudMyCheckinout)
        {
            _context.SkudMyCheckinout.Update(skudMyCheckinout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudMyCheckinout", new { id = skudMyCheckinout.code }, skudMyCheckinout);
        }

        // DELETE: api/SkudMyCheckinouts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudMyCheckinout>> DeleteSkudMyCheckinout(long id)
        {
            var skudMyCheckinout = await _context.SkudMyCheckinout.FindAsync(id);
            if (skudMyCheckinout == null)
            {
                return NotFound();
            }

            _context.SkudMyCheckinout.Remove(skudMyCheckinout);
            await _context.SaveChangesAsync();

            return skudMyCheckinout;
        }

        private bool SkudMyCheckinoutExists(long id)
        {
            return _context.SkudMyCheckinout.Any(e => e.code == id);
        }
    }
}
