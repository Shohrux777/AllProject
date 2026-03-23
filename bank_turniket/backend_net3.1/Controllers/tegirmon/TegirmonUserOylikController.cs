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
    public class TegirmonUserOylikController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserOylikController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserIshlaganVaqt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserOylik>>> GetTegirmonUserOylik()
        {
            return await _context.TegirmonUserOylik.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var categoryList = await _context.TegirmonUserOylik
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
            paginationModel.items_count = await _context.TegirmonUserOylik
                .Where(p => p.userid == userid)
                .CountAsync();
            paginationModel.current_item_count = categoryList.Count;
            paginationModel.current_page = page;

            return paginationModel;
        }

        // GET: api/TegirmonUserOylik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserOylik>> GetTegirmonUserOylik(long id)
        {
            var tegirmonUserOylik = await _context.TegirmonUserOylik.FindAsync(id);

            if (tegirmonUserOylik == null)
            {
                return NotFound();
            }

            return tegirmonUserOylik;
        }

        // PUT: api/TegirmonUserIshlaganVaqt/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserOylik(long id, TegirmonUserOylik tegirmonUserOylik)
        {
            if (id != tegirmonUserOylik.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserOylik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserOylikExists(id))
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

        // POST: api/TegirmonUserOylik
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonUserOylik>> PostTegirmonUserOylik(TegirmonUserOylik tegirmonUserOylik)
        {
            _context.TegirmonUserOylik.Update(tegirmonUserOylik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonUserOylik", new { id = tegirmonUserOylik.id }, tegirmonUserOylik);
        }

        // DELETE: api/TegirmonUserIshlaganPuli/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserOylik>> DeleteTegirmonUserOylik(long id)
        {
            var tegirmonUserOylik = await _context.TegirmonUserOylik.FindAsync(id);
            if (tegirmonUserOylik == null)
            {
                return NotFound();
            }

            _context.TegirmonUserOylik.Remove(tegirmonUserOylik);
            await _context.SaveChangesAsync();

            return tegirmonUserOylik;
        }

        private bool TegirmonUserOylikExists(long id)
        {
            return _context.TegirmonUserOylik.Any(e => e.id == id);
        }
    }
}
