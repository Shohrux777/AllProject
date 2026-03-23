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
    public class SkudSabablisController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudSabablisController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudSabablis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudSababli>>> GetSkudSababli()
        {
            return await _context.SkudSababli.ToListAsync();
        }

        // GET: api/SkudSabablis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudSababli>> GetSkudSababli(long id)
        {
            var skudSababli = await _context.SkudSababli.FindAsync(id);

            if (skudSababli == null)
            {
                return NotFound();
            }

            return skudSababli;
        }

        // PUT: api/SkudSabablis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudSababli(long id, SkudSababli skudSababli)
        {
            if (id != skudSababli.id)
            {
                return BadRequest();
            }

            _context.Entry(skudSababli).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudSababliExists(id))
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

        // POST: api/SkudSabablis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudSababli>> PostSkudSababli(SkudSababli skudSababli)
        {
            _context.SkudSababli.Update(skudSababli);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudSababli", new { id = skudSababli.id }, skudSababli);
        }

        // DELETE: api/SkudSabablis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudSababli>> DeleteSkudSababli(long id)
        {
            var skudSababli = await _context.SkudSababli.FindAsync(id);
            if (skudSababli == null)
            {
                return NotFound();
            }

            _context.SkudSababli.Remove(skudSababli);
            await _context.SaveChangesAsync();

            return skudSababli;
        }

        private bool SkudSababliExists(long id)
        {
            return _context.SkudSababli.Any(e => e.id == id);
        }
    }
}
