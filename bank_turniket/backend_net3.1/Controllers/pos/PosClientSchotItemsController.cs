using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.pos;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.pos
{
    [ApiExplorerSettings(GroupName = "v5")]
    [Route("api/[controller]")]
    [ApiController]
    public class PosClientSchotItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosClientSchotItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosClientSchotItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosClientSchotItem>>> GetPosClientSchotItem()
        {
            return await _context.PosClientSchotItem.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientSchotItem> itemList = await _context.PosClientSchotItem
                .Where(p => p.active_status == true)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientSchotItem>();
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientSchotItem
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

   



        // GET: api/PosClientSchotItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosClientSchotItem>> GetPosClientSchotItem(long id)
        {
            var posClientSchotItem = await _context.PosClientSchotItem.FindAsync(id);

            if (posClientSchotItem == null)
            {
                return NotFound();
            }

            return posClientSchotItem;
        }

        // PUT: api/PosClientSchotItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosClientSchotItem(long id, PosClientSchotItem posClientSchotItem)
        {
            if (id != posClientSchotItem.id)
            {
                return BadRequest();
            }

            _context.Entry(posClientSchotItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosClientSchotItemExists(id))
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

        // POST: api/PosClientSchotItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosClientSchotItem>> PostPosClientSchotItem(PosClientSchotItem posClientSchotItem)
        {
            _context.PosClientSchotItem.Update(posClientSchotItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPosClientSchotItem", new { id = posClientSchotItem.id }, posClientSchotItem);
        }

        // DELETE: api/PosClientSchotItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosClientSchotItem>> DeletePosClientSchotItem(long id)
        {
            var posClientSchotItem = await _context.PosClientSchotItem.FindAsync(id);
            if (posClientSchotItem == null)
            {
                return NotFound();
            }

            _context.PosClientSchotItem.Remove(posClientSchotItem);
            await _context.SaveChangesAsync();

            return posClientSchotItem;
        }

        private bool PosClientSchotItemExists(long id)
        {
            return _context.PosClientSchotItem.Any(e => e.id == id);
        }
    }
}
