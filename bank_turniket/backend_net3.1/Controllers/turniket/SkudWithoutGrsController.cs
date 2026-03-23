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
    public class SkudWithoutGrsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudWithoutGrsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudWithoutGrs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudWithoutGr>>> GetSkudWithoutGr()
        {
            return await _context.SkudWithoutGr.ToListAsync();
        }

        // GET: api/SkudWithoutGrs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudWithoutGr>> GetSkudWithoutGr(long id)
        {
            var skudWithoutGr = await _context.SkudWithoutGr.FindAsync(id);

            if (skudWithoutGr == null)
            {
                return NotFound();
            }

            return skudWithoutGr;
        }

        // PUT: api/SkudWithoutGrs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudWithoutGr(long id, SkudWithoutGr skudWithoutGr)
        {
            if (id != skudWithoutGr.id)
            {
                return BadRequest();
            }

            _context.Entry(skudWithoutGr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudWithoutGrExists(id))
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

        // POST: api/SkudWithoutGrs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudWithoutGr>> PostSkudWithoutGr(SkudWithoutGr skudWithoutGr)
        {
            _context.SkudWithoutGr.Update(skudWithoutGr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudWithoutGr", new { id = skudWithoutGr.id }, skudWithoutGr);
        }

        // DELETE: api/SkudWithoutGrs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudWithoutGr>> DeleteSkudWithoutGr(long id)
        {
            var skudWithoutGr = await _context.SkudWithoutGr.FindAsync(id);
            if (skudWithoutGr == null)
            {
                return NotFound();
            }

            _context.SkudWithoutGr.Remove(skudWithoutGr);
            await _context.SaveChangesAsync();

            return skudWithoutGr;
        }

        private bool SkudWithoutGrExists(long id)
        {
            return _context.SkudWithoutGr.Any(e => e.id == id);
        }
    }
}
