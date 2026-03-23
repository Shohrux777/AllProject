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
    public class TegirmonKassaInfoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonKassaInfoController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonKassaInfo>>> GetTegirmonKassaInfo()
        {
            return await _context.TegirmonKassaInfo.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaInfo> categoryList = await _context.TegirmonKassaInfo
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaInfo.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationKassaId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationKassaId([FromQuery] int page, [FromQuery] int size, [FromQuery] long kassa_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaInfo> categoryList = await _context.TegirmonKassaInfo
                // .Include(p => p.department)
                .Where(p => p.active_status == true && p.TegirmonKassaid == kassa_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaInfo.Where(p => p.active_status == true && p.TegirmonKassaid == kassa_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByBeatWeenTwoDateKassaInfo")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDateKassaInfo([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaInfo> categoryList = await _context.TegirmonKassaInfo
                .Include(p => p.kassa)
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaInfo
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByBeatWeenTwoDateKassaInfoKassaID")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeatWeenTwoDateKassaInfoKassaID([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaInfo> categoryList = await _context.TegirmonKassaInfo
                .Include(p => p.kassa)
                .Where(p => p.active_status == true && p.TegirmonKassaid == kassa_id
                && (p.created_date_time >= begin_date  && p.created_date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaInfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaInfo
                .Where(p => p.active_status == true && p.TegirmonKassaid == kassa_id
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getKassaInfoLastKassaId")]
        public async Task<ActionResult<TegirmonKassaInfo>> getKassaInfoLastKassaId([FromQuery]long kassa_id)
        {
            var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Include(p => p.kassa)
                .Where(p =>p.TegirmonKassaid == kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

            if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
            {
                    // var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    // .Include(p => p.kassa)
                    // .Where(p =>p.TegirmonKassaid == kassa_id).Take(1).OrderByDescending(p => p.id).ToListAsync();
                    

                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Include(p => p.kassa)
                    .Where(p => p.TegirmonKassaid == kassa_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();


                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.dollor = 0;
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = kassa_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                        return tegirmon_kassa_info;
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;
                        tegirmon_kassa_info.TegirmonKassaid = kassa_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                        return tegirmon_kassa_info;

                    }
                // return NotFound();
            }

            return tegirmonKassaInfo.Last();
        }


        // GET: api/TegirmonKassaInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonKassaInfo>> GetTegirmonKassaInfo(long id)
        {
            var tegirmonKassaInfo = await _context.TegirmonKassaInfo.FindAsync(id);

            if (tegirmonKassaInfo == null)
            {
                return NotFound();
            }

            return tegirmonKassaInfo;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonKassaInfo(long id, TegirmonKassaInfo tegirmonKassaInfo)
        {
            if (id != tegirmonKassaInfo.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonKassaInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonKassaInfoExists(id))
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
        public async Task<ActionResult<TegirmonKassaInfo>> PostTegirmonKassaInfo(TegirmonKassaInfo tegirmonKassaInfo)
        {
            _context.TegirmonKassaInfo.Update(tegirmonKassaInfo);
            await _context.SaveChangesAsync();
            return tegirmonKassaInfo;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonKassaInfo>> DeleteTegirmonKassaInfo(long id)
        {
            var tegirmonKassaInfo = await _context.TegirmonKassaInfo.FindAsync(id);
            if (tegirmonKassaInfo == null)
            {
                return NotFound();
            }

            _context.TegirmonKassaInfo.Remove(tegirmonKassaInfo);
            await _context.SaveChangesAsync();

            return tegirmonKassaInfo;
        }

        private bool TegirmonKassaInfoExists(long id)
        {
            return _context.TegirmonKassaInfo.Any(e => e.id == id);
        }
    }
}
