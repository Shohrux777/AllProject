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
    public class TegirmonUserYuqlamaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserYuqlamaController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserYuqlama
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserYuqlama>>> GetTegirmonUserYuqlama()
        {
            return await _context.TegirmonUserYuqlama.ToListAsync();
        }

        [HttpGet("getPagination_user_vaqrOraligida")]
        public async Task<ActionResult<TexPaginationModel>> getPagination_user_vaqrOraligida([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserYuqlama> categoryList = await _context.TegirmonUserYuqlama
                .Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserYuqlama>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserYuqlama.Where(p => p.userid == userid && (p.K_date >= begin_date && p.K_date <= end_date)).CountAsync();
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

            var categoryList = await _context.TegirmonUserYuqlama
                .Where(p => p.userid == userid && (p.K_date >= begin && p.K_date < end) && p.status == true)
                .OrderByDescending(p => p.K_date)
                .Skip(page * size).Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserYuqlama
                .Where(p => p.userid == userid && (p.K_date >= begin && p.K_date < end))
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;

            return paginationModel;
        }

        // GET: api/TegirmonUserYuqlama/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserYuqlama>> GetTegirmonUserYuqlama(long id)
        {
            var tegirmonUserYuqlama = await _context.TegirmonUserYuqlama.FindAsync(id);

            if (tegirmonUserYuqlama == null)
            {
                return NotFound();
            }

            return tegirmonUserYuqlama;
        }

        // PUT: api/TegirmonUserYuqlama/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserYuqlama(long id, TegirmonUserYuqlama tegirmonUserYuqlama)
        {
            if (id != tegirmonUserYuqlama.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserYuqlama).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserYuqlamaExists(id))
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
        // [HttpPost]
        // public async Task<ActionResult<TegirmonUserYuqlama>> PostTegirmonUserYuqlama(TegirmonUserYuqlama tegirmonUserYuqlama)
        // {
        //     _context.TegirmonUserYuqlama.Update(tegirmonUserYuqlama);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTegirmonUserYuqlama", new { id = tegirmonUserYuqlama.id }, tegirmonUserYuqlama);
        // }


        [HttpPost]
        public async Task<ActionResult<TegirmonUserYuqlama>> PostTegirmonUserYuqlama(TegirmonUserYuqlama tegirmonUserYuqlama)
        {
            var sana = tegirmonUserYuqlama.K_date.Date; // faqat sana qismi
            var userId = tegirmonUserYuqlama.userid;

            var existing = await _context.TegirmonUserYuqlama
                .FirstOrDefaultAsync(x => x.userid == userId && x.K_date.Date == sana);

            if (existing != null)
            {
                // Ma'lumot bor – yangilaymiz
                existing.status = tegirmonUserYuqlama.status;
                

                _context.TegirmonUserYuqlama.Update(existing);
                await _context.SaveChangesAsync();

                return Ok(existing);
            }
            else
            {
                // Ma'lumot yo‘q – yangi qo‘shamiz
                _context.TegirmonUserYuqlama.Add(tegirmonUserYuqlama);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTegirmonUserYuqlama", new { id = tegirmonUserYuqlama.id }, tegirmonUserYuqlama);
            }
        }

        // DELETE: api/TegirmonUserIshlaganVaqt/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserYuqlama>> DeleteTegirmonUserYuqlama(long id)
        {
            var tegirmonUserYuqlama = await _context.TegirmonUserYuqlama.FindAsync(id);
            if (tegirmonUserYuqlama == null)
            {
                return NotFound();
            }

            _context.TegirmonUserYuqlama.Remove(tegirmonUserYuqlama);
            await _context.SaveChangesAsync();

            return tegirmonUserYuqlama;
        }

        private bool TegirmonUserYuqlamaExists(long id)
        {
            return _context.TegirmonUserYuqlama.Any(e => e.id == id);
        }
    }
}
