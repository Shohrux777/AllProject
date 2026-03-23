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
    public class SkudIshGrafigisController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudIshGrafigisController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudIshGrafigis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudIshGrafigi>>> GetSkudIshGrafigi()
        {
            return await _context.SkudIshGrafigi.ToListAsync();
        }
        
        // GET: api/SkudIshGrafigis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudIshGrafigi>> GetSkudIshGrafigi(long id)
        {
            var skudIshGrafigi = await _context.SkudIshGrafigi.FindAsync(id);

            if (skudIshGrafigi == null)
            {
                return NotFound();
            }

            return skudIshGrafigi;
        }

        // PUT: api/SkudIshGrafigis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudIshGrafigi(long id, SkudIshGrafigi skudIshGrafigi)
        {
            if (id != skudIshGrafigi.id)
            {
                return BadRequest();
            }

            _context.Entry(skudIshGrafigi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudIshGrafigiExists(id))
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

        // POST: api/SkudIshGrafigis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudIshGrafigi>> PostSkudIshGrafigi(SkudIshGrafigi skudIshGrafigi)
        {
            _context.SkudIshGrafigi.Update(skudIshGrafigi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudIshGrafigi", new { id = skudIshGrafigi.id }, skudIshGrafigi);
        }

        // DELETE: api/SkudIshGrafigis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudIshGrafigi>> DeleteSkudIshGrafigi(long id)
        {
            var skudIshGrafigi = await _context.SkudIshGrafigi.FindAsync(id);
            if (skudIshGrafigi == null)
            {
                return NotFound();
            }

            _context.SkudIshGrafigi.Remove(skudIshGrafigi);
            await _context.SaveChangesAsync();

            return skudIshGrafigi;
        }

        private bool SkudIshGrafigiExists(long id)
        {
            return _context.SkudIshGrafigi.Any(e => e.id == id);
        }
    }
}
