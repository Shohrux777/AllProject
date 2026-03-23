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
    public class TegirmonHisoblarInfoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonHisoblarInfoController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonHisoblarInfo>>> GetTegirmonHisoblarInfo()
        {
            return await _context.TegirmonHisoblarInfo.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarInfo> categoryList = await _context.TegirmonHisoblarInfo
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarInfo.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationHisobId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationHisobId([FromQuery] int page, [FromQuery] int size, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarInfo> categoryList = await _context.TegirmonHisoblarInfo
                // .Include(p => p.department)
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarInfo.Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByBeatWeenTwoDateHisobInfo")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDateHisobInfo([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarInfo> categoryList = await _context.TegirmonHisoblarInfo
                .Include(p => p.hisob)
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarInfo
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByBeatWeenTwoDateKassaInfoHisobID")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDateKassaInfoHisobID([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarInfo> categoryList = await _context.TegirmonHisoblarInfo
                .Include(p => p.hisob)
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarInfo
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getKassaInfoLastHisobId")]
        public async Task<ActionResult<TegirmonHisoblarInfo>> getKassaInfoLastHisobId([FromQuery]long hisob_id)
        {
            var tegirmonHisoblarInfo = await _context.TegirmonHisoblarInfo
                .Include(p => p.hisob)
                .Where(p =>p.TegirmonHisoblarid == hisob_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

            if (tegirmonHisoblarInfo == null || tegirmonHisoblarInfo.Count == 0)
            {
                    var tegirmonHisoblarInfoLast = await _context.TegirmonHisoblarInfo
                    .Include(p => p.hisob)
                    .Where(p =>p.TegirmonHisoblarid == hisob_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();

                    if(tegirmonHisoblarInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonHisoblarInfo tegirmon_hisob_info = new TegirmonHisoblarInfo();
                        tegirmon_hisob_info.cash = 0;
                        tegirmon_hisob_info.dollor = 0;
                        // tegirmon_hisob_info.id = 0;
                        tegirmon_hisob_info.TegirmonHisoblarid = hisob_id;
                        tegirmon_hisob_info.before_cash = 0;
                        tegirmon_hisob_info.before_dollor = 0;
                        _context.TegirmonHisoblarInfo.Update(tegirmon_hisob_info);
                        await _context.SaveChangesAsync();
                        return tegirmon_hisob_info;
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonHisoblarInfo tegirmon_hisob_info = new TegirmonHisoblarInfo();
                        tegirmon_hisob_info.cash = tegirmonHisoblarInfoLast.cash;
                        tegirmon_hisob_info.dollor = tegirmonHisoblarInfoLast.dollor;
                        tegirmon_hisob_info.TegirmonHisoblarid = hisob_id;
                        tegirmon_hisob_info.before_cash = tegirmonHisoblarInfoLast.cash;
                        tegirmon_hisob_info.before_dollor = tegirmonHisoblarInfoLast.dollor;
                        _context.TegirmonHisoblarInfo.Update(tegirmon_hisob_info);
                        await _context.SaveChangesAsync();
                        return tegirmon_hisob_info;

                    }
                // return NotFound();
            }

            return tegirmonHisoblarInfo.Last();
        }


        // GET: api/TegirmonHisoblarInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonHisoblarInfo>> GetTegirmonHisoblarInfo(long id)
        {
            var tegirmonHisoblarInfo = await _context.TegirmonHisoblarInfo.FindAsync(id);

            if (tegirmonHisoblarInfo == null)
            {
                return NotFound();
            }

            return tegirmonHisoblarInfo;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonHisoblarInfo(long id, TegirmonHisoblarInfo tegirmonHisoblarInfo)
        {
            if (id != tegirmonHisoblarInfo.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonHisoblarInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonHisoblarInfoExists(id))
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
        public async Task<ActionResult<TegirmonHisoblarInfo>> PostTegirmonHisoblarInfo(TegirmonHisoblarInfo tegirmonHisoblarInfo)
        {
            _context.TegirmonHisoblarInfo.Update(tegirmonHisoblarInfo);
            await _context.SaveChangesAsync();
            return tegirmonHisoblarInfo;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonHisoblarInfo>> DeleteTegirmonHisoblarInfo(long id)
        {
            var tegirmonHisoblarInfo = await _context.TegirmonHisoblarInfo.FindAsync(id);
            if (tegirmonHisoblarInfo == null)
            {
                return NotFound();
            }

            _context.TegirmonHisoblarInfo.Remove(tegirmonHisoblarInfo);
            await _context.SaveChangesAsync();

            return tegirmonHisoblarInfo;
        }

        private bool TegirmonHisoblarInfoExists(long id)
        {
            return _context.TegirmonHisoblarInfo.Any(e => e.id == id);
        }
    }
}
