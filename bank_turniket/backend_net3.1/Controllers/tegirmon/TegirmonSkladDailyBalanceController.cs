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
    public class TegirmonSkladDailyBalanceController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonSkladDailyBalanceController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonSkladDailyBalance>>> GetTegirmonSkladDailyBalance()
        {
            return await _context.TegirmonSkladDailyBalance.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladDailyBalance> categoryList = await _context.TegirmonSkladDailyBalance
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladDailyBalance>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSkladDailyBalance.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonSkladDailyBalance>> GetTegirmonSkladDailyBalance(long id)
        {
            var tegirmonSkladDailyBalance = await _context.TegirmonSkladDailyBalance.FindAsync(id);

            if (tegirmonSkladDailyBalance == null)
            {
                return NotFound();
            }

            return tegirmonSkladDailyBalance;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonSkladDailyBalance(long id, TegirmonSkladDailyBalance tegirmonSkladDailyBalance)
        {
            if (id != tegirmonSkladDailyBalance.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonSkladDailyBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonSkladDailyBalanceExists(id))
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
        public async Task<ActionResult<TegirmonSkladDailyBalance>> PostTegirmonSkladDailyBalance(TegirmonSkladDailyBalance tegirmonSkladDailyBalance)
        {
            _context.TegirmonSkladDailyBalance.Update(tegirmonSkladDailyBalance);
            await _context.SaveChangesAsync();
            return tegirmonSkladDailyBalance;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonSkladDailyBalance>> DeleteTegirmonSkladDailyBalance(long id)
        {
            var tegirmonSkladDailyBalance = await _context.TegirmonSkladDailyBalance.FindAsync(id);
            if (tegirmonSkladDailyBalance == null)
            {
                return NotFound();
            }

            _context.TegirmonSkladDailyBalance.Remove(tegirmonSkladDailyBalance);
            await _context.SaveChangesAsync();

            return tegirmonSkladDailyBalance;
        }

        private bool TegirmonSkladDailyBalanceExists(long id)
        {
            return _context.TegirmonSkladDailyBalance.Any(e => e.id == id);
        }
    }
}
