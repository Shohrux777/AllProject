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
    public class SkudAccessLevelsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudAccessLevelsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudAccessLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudAccessLevel>>> GetSkudAccessLevel()
        {
            return await _context.SkudAccessLevel.ToListAsync();
        }

        // GET: api/SkudAccessLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudAccessLevel>> GetSkudAccessLevel(long id)
        {
            var skudAccessLevel = await _context.SkudAccessLevel.FindAsync(id);

            if (skudAccessLevel == null)
            {
                return NotFound();
            }

            return skudAccessLevel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudAccessLevel> itemList = await _context.SkudAccessLevel
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudAccessLevel>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudAccessLevel.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudAccessLevels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudAccessLevel(long id, SkudAccessLevel skudAccessLevel)
        {
            if (id != skudAccessLevel.id)
            {
                return BadRequest();
            }

            _context.Entry(skudAccessLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudAccessLevelExists(id))
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

        // POST: api/SkudAccessLevels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudAccessLevel>> PostSkudAccessLevel(SkudAccessLevel skudAccessLevel)
        {
            _context.SkudAccessLevel.Update(skudAccessLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudAccessLevel", new { id = skudAccessLevel.id }, skudAccessLevel);
        }

        // DELETE: api/SkudAccessLevels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudAccessLevel>> DeleteSkudAccessLevel(long id)
        {
            var skudAccessLevel = await _context.SkudAccessLevel.FindAsync(id);
            if (skudAccessLevel == null)
            {
                return NotFound();
            }

            _context.SkudAccessLevel.Remove(skudAccessLevel);
            await _context.SaveChangesAsync();

            return skudAccessLevel;
        }

        private bool SkudAccessLevelExists(long id)
        {
            return _context.SkudAccessLevel.Any(e => e.id == id);
        }
    }
}
