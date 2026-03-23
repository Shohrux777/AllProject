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
    public class SkudGroupAccessesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudGroupAccessesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudGroupAccesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudGroupAccess>>> GetSkudGroupAccess()
        {
            return await _context.SkudGroupAccess.ToListAsync();
        }

        // GET: api/SkudGroupAccesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudGroupAccess>> GetSkudGroupAccess(long id)
        {
            var skudGroupAccess = await _context.SkudGroupAccess.FindAsync(id);

            if (skudGroupAccess == null)
            {
                return NotFound();
            }

            return skudGroupAccess;
        }

        // PUT: api/SkudGroupAccesses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudGroupAccess(long id, SkudGroupAccess skudGroupAccess)
        {
            if (id != skudGroupAccess.id)
            {
                return BadRequest();
            }

            _context.Entry(skudGroupAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudGroupAccessExists(id))
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

        // POST: api/SkudGroupAccesses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudGroupAccess>> PostSkudGroupAccess(SkudGroupAccess skudGroupAccess)
        {
            _context.SkudGroupAccess.Update(skudGroupAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudGroupAccess", new { id = skudGroupAccess.id }, skudGroupAccess);
        }

        // DELETE: api/SkudGroupAccesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudGroupAccess>> DeleteSkudGroupAccess(long id)
        {
            var skudGroupAccess = await _context.SkudGroupAccess.FindAsync(id);
            if (skudGroupAccess == null)
            {
                return NotFound();
            }

            _context.SkudGroupAccess.Remove(skudGroupAccess);
            await _context.SaveChangesAsync();

            return skudGroupAccess;
        }

        private bool SkudGroupAccessExists(long id)
        {
            return _context.SkudGroupAccess.Any(e => e.id == id);
        }
    }
}
