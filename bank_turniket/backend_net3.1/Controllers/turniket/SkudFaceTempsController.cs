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
    public class SkudFaceTempsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudFaceTempsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudFaceTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudFaceTemp>>> GetSkudFaceTemp()
        {
            return await _context.SkudFaceTemp.ToListAsync();
        }

        // GET: api/SkudFaceTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudFaceTemp>> GetSkudFaceTemp(long id)
        {
            var skudFaceTemp = await _context.SkudFaceTemp.FindAsync(id);

            if (skudFaceTemp == null)
            {
                return NotFound();
            }

            return skudFaceTemp;
        }

        // PUT: api/SkudFaceTemps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudFaceTemp(long id, SkudFaceTemp skudFaceTemp)
        {
            if (id != skudFaceTemp.id)
            {
                return BadRequest();
            }

            _context.Entry(skudFaceTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudFaceTempExists(id))
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

        // POST: api/SkudFaceTemps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudFaceTemp>> PostSkudFaceTemp(SkudFaceTemp skudFaceTemp)
        {
            _context.SkudFaceTemp.Update(skudFaceTemp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudFaceTemp", new { id = skudFaceTemp.id }, skudFaceTemp);
        }

        // DELETE: api/SkudFaceTemps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudFaceTemp>> DeleteSkudFaceTemp(long id)
        {
            var skudFaceTemp = await _context.SkudFaceTemp.FindAsync(id);
            if (skudFaceTemp == null)
            {
                return NotFound();
            }

            _context.SkudFaceTemp.Remove(skudFaceTemp);
            await _context.SaveChangesAsync();

            return skudFaceTemp;
        }

        private bool SkudFaceTempExists(long id)
        {
            return _context.SkudFaceTemp.Any(e => e.id == id);
        }
    }
}
