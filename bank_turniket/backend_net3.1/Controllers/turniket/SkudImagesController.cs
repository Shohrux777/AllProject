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
    public class SkudImagesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudImagesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudImages>>> GetSkudImages()
        {
            return await _context.SkudImages.ToListAsync();
        }

        // GET: api/SkudImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudImages>> GetSkudImages(long id)
        {
            var skudImages = await _context.SkudImages.FindAsync(id);

            if (skudImages == null)
            {
                return NotFound();
            }

            return skudImages;
        }

        // PUT: api/SkudImages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudImages(long id, SkudImages skudImages)
        {
            if (id != skudImages.id)
            {
                return BadRequest();
            }

            _context.Entry(skudImages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudImagesExists(id))
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

        // POST: api/SkudImages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudImages>> PostSkudImages(SkudImages skudImages)
        {
            _context.SkudImages.Update(skudImages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudImages", new { id = skudImages.id }, skudImages);
        }

        // DELETE: api/SkudImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudImages>> DeleteSkudImages(long id)
        {
            var skudImages = await _context.SkudImages.FindAsync(id);
            if (skudImages == null)
            {
                return NotFound();
            }

            _context.SkudImages.Remove(skudImages);
            await _context.SaveChangesAsync();

            return skudImages;
        }

        private bool SkudImagesExists(long id)
        {
            return _context.SkudImages.Any(e => e.id == id);
        }
    }
}
