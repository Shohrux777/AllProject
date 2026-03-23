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
    public class SkudPictureCheckinoutsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudPictureCheckinoutsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudPictureCheckinouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudPictureCheckinout>>> GetSkudPictureCheckinout()
        {
            return await _context.SkudPictureCheckinout.ToListAsync();
        }

        // GET: api/SkudPictureCheckinouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudPictureCheckinout>> GetSkudPictureCheckinout(long id)
        {
            var skudPictureCheckinout = await _context.SkudPictureCheckinout.FindAsync(id);

            if (skudPictureCheckinout == null)
            {
                return NotFound();
            }

            return skudPictureCheckinout;
        }

        // PUT: api/SkudPictureCheckinouts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudPictureCheckinout(long id, SkudPictureCheckinout skudPictureCheckinout)
        {
            if (id != skudPictureCheckinout.id)
            {
                return BadRequest();
            }

            _context.Entry(skudPictureCheckinout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudPictureCheckinoutExists(id))
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

        // POST: api/SkudPictureCheckinouts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudPictureCheckinout>> PostSkudPictureCheckinout(SkudPictureCheckinout skudPictureCheckinout)
        {
            _context.SkudPictureCheckinout.Update(skudPictureCheckinout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudPictureCheckinout", new { id = skudPictureCheckinout.id }, skudPictureCheckinout);
        }

        // DELETE: api/SkudPictureCheckinouts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudPictureCheckinout>> DeleteSkudPictureCheckinout(long id)
        {
            var skudPictureCheckinout = await _context.SkudPictureCheckinout.FindAsync(id);
            if (skudPictureCheckinout == null)
            {
                return NotFound();
            }

            _context.SkudPictureCheckinout.Remove(skudPictureCheckinout);
            await _context.SaveChangesAsync();

            return skudPictureCheckinout;
        }

        private bool SkudPictureCheckinoutExists(long id)
        {
            return _context.SkudPictureCheckinout.Any(e => e.id == id);
        }
    }
}
