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
    public class TegirmonOrderCarItemController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderCarItemController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderCarItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderCarItem>>> GetTegirmonOrderCarItem()
        {
            return await _context.TegirmonOrderCarItem.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderCarItem> categoryList = await _context.TegirmonOrderCarItem
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderCarItem>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderCarItem
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderCarItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderCarItem>> GetTegirmonOrderCarItem(long id)
        {
            var tegirmonOrderCarItem = await _context.TegirmonOrderCarItem.FindAsync(id);

            if (tegirmonOrderCarItem == null)
            {
                return NotFound();
            }

            return tegirmonOrderCarItem;
        }

        // PUT: api/TegirmonOrderCarItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderCarItem(long id, TegirmonOrderCarItem tegirmonOrderCarItem)
        {
            if (id != tegirmonOrderCarItem.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderCarItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderCarItemExists(id))
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

        // POST: api/TegirmonOrderCarItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderCarItem>> PostTegirmonOrderCarItem(TegirmonOrderCarItem tegirmonOrderCarItem)
        {
            _context.TegirmonOrderCarItem.Update(tegirmonOrderCarItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderCarItem", new { id = tegirmonOrderCarItem.id }, tegirmonOrderCarItem);
        }

        // DELETE: api/TegirmonOrderCarItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderCarItem>> DeleteTegirmonOrderCarItem(long id)
        {
            var tegirmonOrderCarItem = await _context.TegirmonOrderCarItem.FindAsync(id);
            if (tegirmonOrderCarItem == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderCarItem.Remove(tegirmonOrderCarItem);
            await _context.SaveChangesAsync();

            return tegirmonOrderCarItem;
        }

        private bool TegirmonOrderCarItemExists(long id)
        {
            return _context.TegirmonOrderCarItem.Any(e => e.id == id);
        }
    }
}
