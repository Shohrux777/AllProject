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
    public class TegirmonOrderItemController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderItemController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderItem>>> GetTegirmonOrderItem()
        {
            return await _context.TegirmonOrderItem.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderItem> categoryList = await _context.TegirmonOrderItem
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderItem>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderItem
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderItem>> GetTegirmonOrderItem(long id)
        {
            var tegirmonOrderItem = await _context.TegirmonOrderItem.FindAsync(id);

            if (tegirmonOrderItem == null)
            {
                return NotFound();
            }

            return tegirmonOrderItem;
        }

        // PUT: api/TegirmonOrderItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderItem(long id, TegirmonOrderItem tegirmonOrderItem)
        {
            if (id != tegirmonOrderItem.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderItemExists(id))
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

        // POST: api/TegirmonOrderItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderItem>> PostTegirmonOrderItem(TegirmonOrderItem tegirmonOrderItem)
        {
            _context.TegirmonOrderItem.Update(tegirmonOrderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderItem", new { id = tegirmonOrderItem.id }, tegirmonOrderItem);
        }

        // DELETE: api/TegirmonOrderItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderItem>> DeleteTegirmonOrderItem(long id)
        {
            var tegirmonOrderItem = await _context.TegirmonOrderItem.FindAsync(id);
            if (tegirmonOrderItem == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderItem.Remove(tegirmonOrderItem);
            await _context.SaveChangesAsync();

            return tegirmonOrderItem;
        }

        private bool TegirmonOrderItemExists(long id)
        {
            return _context.TegirmonOrderItem.Any(e => e.id == id);
        }
    }
}
