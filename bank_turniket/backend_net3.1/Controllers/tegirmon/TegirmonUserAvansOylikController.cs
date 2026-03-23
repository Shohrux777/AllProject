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
    public class TegirmonUserAvansOylikController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserAvansOylikController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserIshlaganVaqt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserAvansOylik>>> GetTegirmonUserAvansOylik()
        {
            return await _context.TegirmonUserAvansOylik.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserAvansOylik
                .Where(p => p.userid == userid)
                .OrderByDescending(p => p.id)
                .Skip(page * size)
                .Take(size)
                .Select(p => new {
                    p.id,
                    p.userid,
                    sum = p.sum.ToString("F0") // mana shu yerda formatlab qaytarilmoqda
                    // kerak bo‘lsa boshqa ustunlarni ham qo‘shing
                })
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserAvansOylik
                .Where(p => p.userid == userid)
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count;
            paginationModel.current_page = page;

            return paginationModel;
        }

        // GET: api/TegirmonUserAvansOylik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserAvansOylik>> GetTegirmonUserAvansOylik(long id)
        {
            var tegirmonUserAvansOylik = await _context.TegirmonUserAvansOylik.FindAsync(id);

            if (tegirmonUserAvansOylik == null)
            {
                return NotFound();
            }

            return tegirmonUserAvansOylik;
        }

        // PUT: api/TegirmonUserIshlaganVaqt/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserAvansOylik(long id, TegirmonUserAvansOylik tegirmonUserAvansOylik)
        {
            if (id != tegirmonUserAvansOylik.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserAvansOylik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserAvansOylikExists(id))
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

        // POST: api/TegirmonUserAvansOylik
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonUserAvansOylik>> PostTegirmonUserAvansOylik(TegirmonUserAvansOylik tegirmonUserAvansOylik)
        {
            _context.TegirmonUserAvansOylik.Update(tegirmonUserAvansOylik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonUserAvansOylik", new { id = tegirmonUserAvansOylik.id }, tegirmonUserAvansOylik);
        }

        // DELETE: api/TegirmonUserIshlaganPuli/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserAvansOylik>> DeleteTegirmonUserAvansOylik(long id)
        {
            var tegirmonUserAvansOylik = await _context.TegirmonUserAvansOylik.FindAsync(id);
            if (tegirmonUserAvansOylik == null)
            {
                return NotFound();
            }

            _context.TegirmonUserAvansOylik.Remove(tegirmonUserAvansOylik);
            await _context.SaveChangesAsync();

            return tegirmonUserAvansOylik;
        }

        private bool TegirmonUserAvansOylikExists(long id)
        {
            return _context.TegirmonUserAvansOylik.Any(e => e.id == id);
        }
    }
}
