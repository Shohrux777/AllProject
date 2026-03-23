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
    public class SkudPeriodsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudPeriodsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudPeriod>>> GetSkudPeriod()
        {
            return await _context.SkudPeriod.ToListAsync();
        }

        // GET: api/SkudPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudPeriod>> GetSkudPeriod(long id)
        {
            var skudPeriod = await _context.SkudPeriod.FindAsync(id);

            if (skudPeriod == null)
            {
                return NotFound();
            }

            return skudPeriod;
        }

        // PUT: api/SkudPeriods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudPeriod(long id, SkudPeriod skudPeriod)
        {
            if (id != skudPeriod.id)
            {
                return BadRequest();
            }

            _context.Entry(skudPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudPeriodExists(id))
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

        // POST: api/SkudPeriods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudPeriod>> PostSkudPeriod(SkudPeriod skudPeriod)
        {
            _context.SkudPeriod.Update(skudPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudPeriod", new { id = skudPeriod.id }, skudPeriod);
        }

        // DELETE: api/SkudPeriods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudPeriod>> DeleteSkudPeriod(long id)
        {
            var skudPeriod = await _context.SkudPeriod.FindAsync(id);
            if (skudPeriod == null)
            {
                return NotFound();
            }

            _context.SkudPeriod.Remove(skudPeriod);
            await _context.SaveChangesAsync();

            return skudPeriod;
        }

        private bool SkudPeriodExists(long id)
        {
            return _context.SkudPeriod.Any(e => e.id == id);
        }
    }
}
