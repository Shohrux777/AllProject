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
    public class SkudLksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudLksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudLks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudLk>>> GetSkudLk()
        {
            return await _context.SkudLk.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudLk> itemList = await _context.SkudLk
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudLk>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudLk.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/SkudLks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudLk>> GetSkudLk(long id)
        {
            var skudLk = await _context.SkudLk.FindAsync(id);

            if (skudLk == null)
            {
                return NotFound();
            }

            return skudLk;
        }

        // PUT: api/SkudLks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudLk(long id, SkudLk skudLk)
        {
            if (id != skudLk.id)
            {
                return BadRequest();
            }

            _context.Entry(skudLk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudLkExists(id))
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

        // POST: api/SkudLks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudLk>> PostSkudLk(SkudLk skudLk)
        {
            _context.SkudLk.Update(skudLk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudLk", new { skudLk.id }, skudLk);
        }

        // DELETE: api/SkudLks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudLk>> DeleteSkudLk(long id)
        {
            var skudLk = await _context.SkudLk.FindAsync(id);
            if (skudLk == null)
            {
                return NotFound();
            }

            _context.SkudLk.Remove(skudLk);
            await _context.SaveChangesAsync();

            return skudLk;
        }

        private bool SkudLkExists(long id)
        {
            return _context.SkudLk.Any(e => e.id == id);
        }
    }
}
