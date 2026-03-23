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
    public class PosOrderItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosOrderItemsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getPaginationList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationList([FromQuery] int page,
         [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrderItem> itemList = await _context.PosOrderItem
                .Where(p => p.active_status == true)
                 .Include(p => p.posProduct)
                 .Include(p => p.unitMeasurment)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrderItem>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrderItem
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListByProductCode")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByProductCode([FromQuery] int page,
   [FromQuery] int size, [FromQuery] String product_code)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrderItem> itemList = await _context.PosOrderItem
                 .Include(p => p.posProduct)
                 .Include(p => p.unitMeasurment)
                 .Where(p => p.active_status == true && p.posProduct.code == product_code)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrderItem>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrderItem
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListByorderId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByorderId([FromQuery] int page,
 [FromQuery] int size, [FromQuery] long pos_order_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrderItem> itemList = await _context.PosOrderItem
                .Where(p => p.active_status == true && p.PosOrderid == pos_order_id)
                 .Include(p => p.posProduct)
                 .Include(p => p.unitMeasurment)
                .OrderBy(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrderItem>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrderItem
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosOrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosOrderItem>>> GetPosOrderItem()
        {
            return await _context.PosOrderItem.ToListAsync();
        }

        // GET: api/PosOrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosOrderItem>> GetPosOrderItem(long id)
        {
            var posOrderItem = await _context.PosOrderItem.FindAsync(id);

            if (posOrderItem == null)
            {
                return NotFound();
            }

            return posOrderItem;
        }

        // PUT: api/PosOrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosOrderItem(long id, PosOrderItem posOrderItem)
        {
            if (id != posOrderItem.id)
            {
                return BadRequest();
            }

            _context.Entry(posOrderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosOrderItemExists(id))
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

        // POST: api/PosOrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosOrderItem>> PostPosOrderItem(PosOrderItem posOrderItem)
        {

            try
            {
                _context.PosOrderItem.Update(posOrderItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return NotFound(e?.InnerException?.Message);

            }
            catch (Exception e)
            {
                return NotFound(e?.InnerException?.Message);
            }

            return CreatedAtAction("GetPosOrderItem", new { id = posOrderItem.id }, posOrderItem);
        }

        // DELETE: api/PosOrderItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosOrderItem>> DeletePosOrderItem(long id)
        {
            var posOrderItem = await _context.PosOrderItem.FindAsync(id);
            if (posOrderItem == null)
            {
                return NotFound();
            }

            _context.PosOrderItem.Remove(posOrderItem);
            await _context.SaveChangesAsync();

            return posOrderItem;
        }

        private bool PosOrderItemExists(long id)
        {
            return _context.PosOrderItem.Any(e => e.id == id);
        }
    }
}
