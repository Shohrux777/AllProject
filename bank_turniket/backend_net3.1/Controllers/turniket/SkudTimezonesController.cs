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
    public class SkudTimezonesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudTimezonesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudTimezones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudTimezone>>> GetSkudTimezone()
        {
            return await _context.SkudTimezone.ToListAsync();
        }

        // GET: api/SkudTimezones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudTimezone>> GetSkudTimezone(long id)
        {
            var skudTimezone = await _context.SkudTimezone.FindAsync(id);

            if (skudTimezone == null)
            {
                return NotFound();
            }

            return skudTimezone;
        }

        // PUT: api/SkudTimezones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudTimezone(long id, SkudTimezone skudTimezone)
        {
            if (id != skudTimezone.id)
            {
                return BadRequest();
            }

            _context.Entry(skudTimezone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudTimezoneExists(id))
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

        // POST: api/SkudTimezones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudTimezone>> PostSkudTimezone(SkudTimezone skudTimezone)
        {
            _context.SkudTimezone.Update(skudTimezone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudTimezone", new { id = skudTimezone.id }, skudTimezone);
        }

        // DELETE: api/SkudTimezones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudTimezone>> DeleteSkudTimezone(long id)
        {
            var skudTimezone = await _context.SkudTimezone.FindAsync(id);
            if (skudTimezone == null)
            {
                return NotFound();
            }

            _context.SkudTimezone.Remove(skudTimezone);
            await _context.SaveChangesAsync();

            return skudTimezone;
        }

        private bool SkudTimezoneExists(long id)
        {
            return _context.SkudTimezone.Any(e => e.id == id);
        }
    }
}
