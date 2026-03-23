using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudFacesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudFacesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudFaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudFaces>>> GetSkudFaces()
        {
            return await _context.SkudFaces.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudFaces> itemList = await _context.SkudFaces
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudFaces>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudFaces.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/SkudFaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudFaces>> GetSkudFaces(long id)
        {
            var skudFaces = await _context.SkudFaces.FindAsync(id);

            if (skudFaces == null)
            {
                return NotFound();
            }

            return skudFaces;
        }

        // PUT: api/SkudFaces/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudFaces(long id, SkudFaces skudFaces)
        {
            if (id != skudFaces.id)
            {
                return BadRequest();
            }

            _context.Entry(skudFaces).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudFacesExists(id))
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

        // POST: api/SkudFaces
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudFaces>> PostSkudFaces(SkudFaces skudFaces)
        {
            _context.SkudFaces.Update(skudFaces);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudFaces", new { id = skudFaces.id }, skudFaces);
        }

        // DELETE: api/SkudFaces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudFaces>> DeleteSkudFaces(long id)
        {
            var skudFaces = await _context.SkudFaces.FindAsync(id);
            if (skudFaces == null)
            {
                return NotFound();
            }

            _context.SkudFaces.Remove(skudFaces);
            await _context.SaveChangesAsync();

            return skudFaces;
        }

        private bool SkudFacesExists(long id)
        {
            return _context.SkudFaces.Any(e => e.id == id);
        }
    }
}
