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
    public class SkudSettingsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudSettingsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudSettings>>> GetSkudSettings()
        {
            return await _context.SkudSettings.ToListAsync();
        }

        // GET: api/SkudSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudSettings>> GetSkudSettings(long id)
        {
            var skudSettings = await _context.SkudSettings.FindAsync(id);

            if (skudSettings == null)
            {
                return NotFound();
            }

            return skudSettings;
        }

        // PUT: api/SkudSettings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudSettings(long id, SkudSettings skudSettings)
        {
            if (id != skudSettings.id)
            {
                return BadRequest();
            }

            _context.Entry(skudSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudSettingsExists(id))
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

        // POST: api/SkudSettings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudSettings>> PostSkudSettings(SkudSettings skudSettings)
        {
            _context.SkudSettings.Update(skudSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudSettings", new { id = skudSettings.id }, skudSettings);
        }

        // DELETE: api/SkudSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudSettings>> DeleteSkudSettings(long id)
        {
            var skudSettings = await _context.SkudSettings.FindAsync(id);
            if (skudSettings == null)
            {
                return NotFound();
            }

            _context.SkudSettings.Remove(skudSettings);
            await _context.SaveChangesAsync();

            return skudSettings;
        }

        private bool SkudSettingsExists(long id)
        {
            return _context.SkudSettings.Any(e => e.id == id);
        }
    }
}
