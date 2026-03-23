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
    public class TegirmonUserIshlaganVaqtController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserIshlaganVaqtController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserIshlaganVaqt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserIshlaganVaqt>>> GetTegirmonUserIshlaganVaqt()
        {
            return await _context.TegirmonUserIshlaganVaqt.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserIshlaganVaqt> categoryList = await _context.TegirmonUserIshlaganVaqt
                .Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserIshlaganVaqt>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganVaqt.Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getUserWorkedDays")]
        public async Task<ActionResult<TexPaginationModel>> getUserWorkedDays([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid, [FromQuery] DateTime month)
        {
            var begin = new DateTime(month.Year, month.Month, 1);
            var end = begin.AddMonths(1);

            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserIshlaganVaqt
                .Where(p => p.userid == userid && (p.K_date >= begin && p.K_date < end))
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .Select(p => new {
                    p.id,
                    p.userid,
                    p.work_time,
                    p.K_date
                })
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserIshlaganVaqt
                .Where(p => p.userid == userid && (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        // vaqt oraligida ishlagan vaqtlarni chiqaradi
        [HttpGet("getUserWorkedDayBeginEnd")]
        public async Task<IActionResult> GetUserWorkedDayBeginEnd(
            [FromQuery] int userid, 
            [FromQuery] DateTime begin_date, 
            [FromQuery] DateTime end_date)
        {
            var workedDays = await _context.TegirmonUserIshlaganVaqt
                .Where(p => p.userid == userid && p.K_date >= begin_date && p.K_date < end_date)
                .OrderByDescending(p => p.K_date)
                .Select(p => new {
                    p.id,
                    p.userid,
                    p.work_time,
                    p.K_date
                })
                .ToListAsync();

            return Ok(workedDays);
        }

        // GET: api/TegirmonUserIshlaganVaqt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserIshlaganVaqt>> GetTegirmonUserIshlaganVaqt(long id)
        {
            var tegirmonUserIshlaganVaqt = await _context.TegirmonUserIshlaganVaqt.FindAsync(id);

            if (tegirmonUserIshlaganVaqt == null)
            {
                return NotFound();
            }

            return tegirmonUserIshlaganVaqt;
        }

        // PUT: api/TegirmonUserIshlaganVaqt/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserIshlaganVaqt(long id, TegirmonUserIshlaganVaqt tegirmonUserIshlaganVaqt)
        {
            if (id != tegirmonUserIshlaganVaqt.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserIshlaganVaqt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserIshlaganVaqtExists(id))
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

        // POST: api/TegirmonUserIshlaganVaqt
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonUserIshlaganVaqt>> PostTegirmonUserIshlaganVaqt(TegirmonUserIshlaganVaqt tegirmonUserIshlaganVaqt)
        {
            _context.TegirmonUserIshlaganVaqt.Update(tegirmonUserIshlaganVaqt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonUserIshlaganVaqt", new { id = tegirmonUserIshlaganVaqt.id }, tegirmonUserIshlaganVaqt);
        }

        // DELETE: api/TegirmonUserIshlaganVaqt/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserIshlaganVaqt>> DeleteTegirmonUserIshlaganVaqt(long id)
        {
            var tegirmonUserIshlaganVaqt = await _context.TegirmonUserIshlaganVaqt.FindAsync(id);
            if (tegirmonUserIshlaganVaqt == null)
            {
                return NotFound();
            }

            _context.TegirmonUserIshlaganVaqt.Remove(tegirmonUserIshlaganVaqt);
            await _context.SaveChangesAsync();

            return tegirmonUserIshlaganVaqt;
        }

        private bool TegirmonUserIshlaganVaqtExists(long id)
        {
            return _context.TegirmonUserIshlaganVaqt.Any(e => e.id == id);
        }
    }
}
