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
    public class SkudSmenasController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudSmenasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudSmenas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudSmena>>> GetSkudSmena()
        {
            return await _context.SkudSmena.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudSmena> itemList = await _context.SkudSmena
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudSmena>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudSmena.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/SkudSmenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudSmena>> GetSkudSmena(long id)
        {
            var skudSmena = await _context.SkudSmena.FindAsync(id);

            if (skudSmena == null)
            {
                return NotFound();
            }

            return skudSmena;
        }

        // PUT: api/SkudSmenas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudSmena(long id, SkudSmena skudSmena)
        {
            if (id != skudSmena.id)
            {
                return BadRequest();
            }

            _context.Entry(skudSmena).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudSmenaExists(id))
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

        // POST: api/SkudSmenas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudSmena>> PostSkudSmena(SkudSmena skudSmena)
        {
            _context.SkudSmena.Update(skudSmena);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudSmena", new { id = skudSmena.id }, skudSmena);
        }

        // DELETE: api/SkudSmenas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudSmena>> DeleteSkudSmena(long id)
        {
            var skudSmena = await _context.SkudSmena.FindAsync(id);
            if (skudSmena == null)
            {
                return NotFound();
            }

            _context.SkudSmena.Remove(skudSmena);
            await _context.SaveChangesAsync();

            return skudSmena;
        }

        private bool SkudSmenaExists(long id)
        {
            return _context.SkudSmena.Any(e => e.id == id);
        }
    }
}
