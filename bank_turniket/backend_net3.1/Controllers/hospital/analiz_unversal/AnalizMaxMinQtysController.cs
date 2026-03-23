using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.analiz;

namespace ApiAll.Controllers.hospital.analiz_unversal
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalizMaxMinQtysController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnalizMaxMinQtysController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/AnalizMaxMinQtys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalizMaxMinQtys>>> GetAnalizMaxMinQtys()
        {
            return await _context.AnalizMaxMinQtys.ToListAsync();
        }

        [HttpGet("getBySearchId")]
        public async Task<ActionResult<IEnumerable<AnalizMaxMinQtys>>> getBySearchId([FromQuery] long search_id)
        {
            return await _context.AnalizMaxMinQtys.Where(p => p.tab_search_id == search_id).ToListAsync();
        }

        // GET: api/AnalizMaxMinQtys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalizMaxMinQtys>> GetAnalizMaxMinQtys(long id)
        {
            var analizMaxMinQtys = await _context.AnalizMaxMinQtys.FindAsync(id);

            if (analizMaxMinQtys == null)
            {
                return NotFound();
            }

            return analizMaxMinQtys;
        }

        // PUT: api/AnalizMaxMinQtys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalizMaxMinQtys(long id, AnalizMaxMinQtys analizMaxMinQtys)
        {
            if (id != analizMaxMinQtys.Id)
            {
                return BadRequest();
            }

            _context.Entry(analizMaxMinQtys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalizMaxMinQtysExists(id))
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

        // POST: api/AnalizMaxMinQtys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AnalizMaxMinQtys>> PostAnalizMaxMinQtys(AnalizMaxMinQtys analizMaxMinQtys)
        {
            _context.AnalizMaxMinQtys.Update(analizMaxMinQtys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalizMaxMinQtys", new { id = analizMaxMinQtys.Id }, analizMaxMinQtys);
        }

        // DELETE: api/AnalizMaxMinQtys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnalizMaxMinQtys>> DeleteAnalizMaxMinQtys(long id)
        {
            var analizMaxMinQtys = await _context.AnalizMaxMinQtys.FindAsync(id);
            if (analizMaxMinQtys == null)
            {
                return NotFound();
            }

            _context.AnalizMaxMinQtys.Remove(analizMaxMinQtys);
            await _context.SaveChangesAsync();

            return analizMaxMinQtys;
        }

        private bool AnalizMaxMinQtysExists(long id)
        {
            return _context.AnalizMaxMinQtys.Any(e => e.Id == id);
        }
    }
}
