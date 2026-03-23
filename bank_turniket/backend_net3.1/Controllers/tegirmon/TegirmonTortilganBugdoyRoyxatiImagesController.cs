using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonTortilganBugdoyRoyxatiImagesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonTortilganBugdoyRoyxatiImagesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonTortilganBugdoyRoyxatiImage>>> GetTegirmonTortilganBugdoyRoyxatiImage()
        {
            return await _context.TegirmonTortilganBugdoyRoyxatiImage.ToListAsync();
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiImage>> GetTegirmonTortilganBugdoyRoyxatiImage(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiImage = await _context.TegirmonTortilganBugdoyRoyxatiImage.FindAsync(id);

            if (tegirmonTortilganBugdoyRoyxatiImage == null)
            {
                return NotFound();
            }

            return tegirmonTortilganBugdoyRoyxatiImage;
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiImages/getByRoyxatiId?royxati_id=5
        [HttpGet("getByRoyxatiId")]
        public async Task<ActionResult<IEnumerable<TegirmonTortilganBugdoyRoyxatiImage>>> GetByRoyxatiId([FromQuery] long royxati_id)
        {
            return await _context.TegirmonTortilganBugdoyRoyxatiImage
                .Where(p => p.TegirmonTortilganBugdoyRoyxatiid == royxati_id && p.active_status == true)
                .OrderByDescending(p => p.id)
                .ToListAsync();
        }

        // PUT: api/TegirmonTortilganBugdoyRoyxatiImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonTortilganBugdoyRoyxatiImage(long id, TegirmonTortilganBugdoyRoyxatiImage tegirmonTortilganBugdoyRoyxatiImage)
        {
            if (id != tegirmonTortilganBugdoyRoyxatiImage.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonTortilganBugdoyRoyxatiImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonTortilganBugdoyRoyxatiImageExists(id))
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

        // POST: api/TegirmonTortilganBugdoyRoyxatiImages
        [HttpPost]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiImage>> PostTegirmonTortilganBugdoyRoyxatiImage(TegirmonTortilganBugdoyRoyxatiImage tegirmonTortilganBugdoyRoyxatiImage)
        {
            _context.TegirmonTortilganBugdoyRoyxatiImage.Update(tegirmonTortilganBugdoyRoyxatiImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonTortilganBugdoyRoyxatiImage", new { id = tegirmonTortilganBugdoyRoyxatiImage.id }, tegirmonTortilganBugdoyRoyxatiImage);
        }

        // DELETE: api/TegirmonTortilganBugdoyRoyxatiImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiImage>> DeleteTegirmonTortilganBugdoyRoyxatiImage(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiImage = await _context.TegirmonTortilganBugdoyRoyxatiImage.FindAsync(id);
            if (tegirmonTortilganBugdoyRoyxatiImage == null)
            {
                return NotFound();
            }

            _context.TegirmonTortilganBugdoyRoyxatiImage.Remove(tegirmonTortilganBugdoyRoyxatiImage);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiImage;
        }

        private bool TegirmonTortilganBugdoyRoyxatiImageExists(long id)
        {
            return _context.TegirmonTortilganBugdoyRoyxatiImage.Any(e => e.id == id);
        }
    }
}
