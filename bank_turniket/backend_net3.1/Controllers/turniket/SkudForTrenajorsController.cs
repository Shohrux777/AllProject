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
    public class SkudForTrenajorsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudForTrenajorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudForTrenajors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudForTrenajor>>> GetSkudForTrenajor()
        {
            return await _context.SkudForTrenajor.ToListAsync();
        }

        // GET: api/SkudForTrenajors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudForTrenajor>> GetSkudForTrenajor(long id)
        {
            var skudForTrenajor = await _context.SkudForTrenajor.FindAsync(id);

            if (skudForTrenajor == null)
            {
                return NotFound();
            }

            return skudForTrenajor;
        }

        // PUT: api/SkudForTrenajors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudForTrenajor(long id, SkudForTrenajor skudForTrenajor)
        {
            if (id != skudForTrenajor.userid)
            {
                return BadRequest();
            }

            _context.Entry(skudForTrenajor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudForTrenajorExists(id))
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

        // POST: api/SkudForTrenajors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudForTrenajor>> PostSkudForTrenajor(SkudForTrenajor skudForTrenajor)
        {
            _context.SkudForTrenajor.Update(skudForTrenajor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudForTrenajor", new { id = skudForTrenajor.userid }, skudForTrenajor);
        }

        // DELETE: api/SkudForTrenajors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudForTrenajor>> DeleteSkudForTrenajor(long id)
        {
            var skudForTrenajor = await _context.SkudForTrenajor.FindAsync(id);
            if (skudForTrenajor == null)
            {
                return NotFound();
            }

            _context.SkudForTrenajor.Remove(skudForTrenajor);
            await _context.SaveChangesAsync();

            return skudForTrenajor;
        }

        private bool SkudForTrenajorExists(long id)
        {
            return _context.SkudForTrenajor.Any(e => e.userid == id);
        }
    }
}
