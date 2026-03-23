using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudResultGrsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudResultGrsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudResultGrs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudResultGr>>> GetSkudResultGr()
        {
            return await _context.SkudResultGr.ToListAsync();
        }

        // GET: api/SkudResultGrs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudResultGr>> GetSkudResultGr(long id)
        {
            var skudResultGr = await _context.SkudResultGr.FindAsync(id);

            if (skudResultGr == null)
            {
                return NotFound();
            }

            return skudResultGr;
        }

        // PUT: api/SkudResultGrs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudResultGr(long id, SkudResultGr skudResultGr)
        {
            if (id != skudResultGr.id)
            {
                return BadRequest();
            }

            _context.Entry(skudResultGr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudResultGrExists(id))
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

        // POST: api/SkudResultGrs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudResultGr>> PostSkudResultGr(SkudResultGr skudResultGr)
        {
            _context.SkudResultGr.Update(skudResultGr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudResultGr", new { id = skudResultGr.id }, skudResultGr);
        }

        // DELETE: api/SkudResultGrs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudResultGr>> DeleteSkudResultGr(long id)
        {
            var skudResultGr = await _context.SkudResultGr.FindAsync(id);
            if (skudResultGr == null)
            {
                return NotFound();
            }

            _context.SkudResultGr.Remove(skudResultGr);
            await _context.SaveChangesAsync();

            return skudResultGr;
        }

        private bool SkudResultGrExists(long id)
        {
            return _context.SkudResultGr.Any(e => e.id == id);
        }
    }
}
