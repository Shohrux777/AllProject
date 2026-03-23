using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonCategoryControler : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonCategoryControler(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonCategoryControler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonCategory>>> GetTegirmonCategory()
        {
            return await _context.TegirmonCategory.OrderBy(x => x.id).ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonCategory> categoryList = await _context.TegirmonCategory
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonCategory>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonCategory.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonCategoryControler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonCategory>> GetTegirmonCategory(long id)
        {
            var tegirmonCategory = await _context.TegirmonCategory.FindAsync(id);

            if (tegirmonCategory == null)
            {
                return NotFound();
            }

            return tegirmonCategory;
        }

        // PUT: api/TegirmonCategoryControler/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonCategory(long id, TegirmonCategory tegirmonCategory)
        {
            if (id != tegirmonCategory.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonCategoryExists(id))
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

        // POST: api/TegirmonCategoryControler
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonCategory>> PostTegirmonCategory(TegirmonCategory tegirmonCategory)
        {
            _context.TegirmonCategory.Update(tegirmonCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonCategory", new { id = tegirmonCategory.id }, tegirmonCategory);
        }

        // DELETE: api/TegirmonCategoryControler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonCategory>> DeleteTegirmonCategory(long id)
        {
            var tegirmonCategory = await _context.TegirmonCategory.FindAsync(id);
            if (tegirmonCategory == null)
            {
                return NotFound();
            }

            _context.TegirmonCategory.Remove(tegirmonCategory);
            await _context.SaveChangesAsync();

            return tegirmonCategory;
        }

        private bool TegirmonCategoryExists(long id)
        {
            return _context.TegirmonCategory.Any(e => e.id == id);
        }
    }
}
