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
    public class SkudUdpsendsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUdpsendsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudUdpsends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUdpsend>>> GetSkudUdpsend()
        {
            return await _context.SkudUdpsend.ToListAsync();
        }

        // GET: api/SkudUdpsends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUdpsend>> GetSkudUdpsend(long id)
        {
            var skudUdpsend = await _context.SkudUdpsend.FindAsync(id);

            if (skudUdpsend == null)
            {
                return NotFound();
            }

            return skudUdpsend;
        }

        // PUT: api/SkudUdpsends/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudUdpsend(long id, SkudUdpsend skudUdpsend)
        {
            if (id != skudUdpsend.id)
            {
                return BadRequest();
            }

            _context.Entry(skudUdpsend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudUdpsendExists(id))
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

        // POST: api/SkudUdpsends
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudUdpsend>> PostSkudUdpsend(SkudUdpsend skudUdpsend)
        {
            _context.SkudUdpsend.Update(skudUdpsend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUdpsend", new { id = skudUdpsend.id }, skudUdpsend);
        }

        // DELETE: api/SkudUdpsends/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudUdpsend>> DeleteSkudUdpsend(long id)
        {
            var skudUdpsend = await _context.SkudUdpsend.FindAsync(id);
            if (skudUdpsend == null)
            {
                return NotFound();
            }

            _context.SkudUdpsend.Remove(skudUdpsend);
            await _context.SaveChangesAsync();

            return skudUdpsend;
        }

        private bool SkudUdpsendExists(long id)
        {
            return _context.SkudUdpsend.Any(e => e.id == id);
        }
    }
}
