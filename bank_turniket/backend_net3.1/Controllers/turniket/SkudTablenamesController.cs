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
    public class SkudTablenamesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudTablenamesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudTablenames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudTablename>>> GetSkudTablename()
        {
            return await _context.SkudTablename.ToListAsync();
        }

        // GET: api/SkudTablenames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudTablename>> GetSkudTablename(long id)
        {
            var skudTablename = await _context.SkudTablename.FindAsync(id);

            if (skudTablename == null)
            {
                return NotFound();
            }

            return skudTablename;
        }

        // PUT: api/SkudTablenames/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudTablename(long id, SkudTablename skudTablename)
        {
            if (id != skudTablename.id)
            {
                return BadRequest();
            }

            _context.Entry(skudTablename).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudTablenameExists(id))
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

        // POST: api/SkudTablenames
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudTablename>> PostSkudTablename(SkudTablename skudTablename)
        {
            _context.SkudTablename.Update(skudTablename);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudTablename", new { id = skudTablename.id }, skudTablename);
        }

        // DELETE: api/SkudTablenames/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudTablename>> DeleteSkudTablename(long id)
        {
            var skudTablename = await _context.SkudTablename.FindAsync(id);
            if (skudTablename == null)
            {
                return NotFound();
            }

            _context.SkudTablename.Remove(skudTablename);
            await _context.SaveChangesAsync();

            return skudTablename;
        }

        private bool SkudTablenameExists(long id)
        {
            return _context.SkudTablename.Any(e => e.id == id);
        }
    }
}
