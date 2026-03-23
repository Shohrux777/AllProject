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
    public class PosOrdersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosOrdersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getPaginationListByOrderTypeId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByOrderTypeId([FromQuery] int page,
[FromQuery] int size, [FromQuery] long pos_user_permission_work_type_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosUserPermisionWorkTypeid == pos_user_permission_work_type_id)
                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosUserPermisionWorkTypeid == pos_user_permission_work_type_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListByUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByUserId([FromQuery] int page,
[FromQuery] int size, [FromQuery] long pos_kim_yaratgan_zakazdi_oshani_user_ids)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosUserid == pos_kim_yaratgan_zakazdi_oshani_user_ids)
                 .Include(p => p.posUser)
                 .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosUserid == pos_kim_yaratgan_zakazdi_oshani_user_ids).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListSkladId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListSkladId([FromQuery] int page,
[FromQuery] int size, [FromQuery] long pos_sklad_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosSkladid == pos_sklad_id)
                                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && p.PosSkladid == pos_sklad_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListByOrderNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByOrderNumber([FromQuery] int page,
    [FromQuery] int size, [FromQuery] String order_number_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && p.order_number == order_number_str)
                                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && p.order_number == order_number_str ).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationList([FromQuery] int page,
            [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true)
                                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListGetByFinishStatus")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListGetByFinishStatus([FromQuery] int page,
    [FromQuery] int size, [FromQuery] bool finish_status_true_or_false)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && p.finish_status == finish_status_true_or_false)
                                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && p.finish_status == finish_status_true_or_false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationListByDate([FromQuery] int page,
    [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosOrder> itemList = await _context.PosOrder
                .Where(p => p.active_status == true && (p.for_date.Date >= begin_date.Date && p.for_date.Date<=end_date.Date))
                                .Include(p => p.posUser)
                .Include(p => p.workType)
                .Include(p => p.sklad)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosOrder>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosOrder
                .Where(p => p.active_status == true && (p.for_date.Date >= begin_date.Date && p.for_date.Date <= end_date.Date)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosOrder>>> GetPosOrder()
        {
            return await _context.PosOrder.ToListAsync();
        }

        // GET: api/PosOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosOrder>> GetPosOrder(long id)
        {
            var posOrder = await _context.PosOrder.FindAsync(id);

            if (posOrder == null)
            {
                return NotFound();
            }

            return posOrder;
        }

        // PUT: api/PosOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosOrder(long id, PosOrder posOrder)
        {
            if (id != posOrder.id)
            {
                return BadRequest();
            }

            _context.Entry(posOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosOrderExists(id))
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

        // POST: api/PosOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosOrder>> PostPosOrder(PosOrder posOrder)
        {


            try
            {
                _context.PosOrder.Update(posOrder);
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

            return CreatedAtAction("GetPosOrder", new { id = posOrder.id }, posOrder);
        }

        // DELETE: api/PosOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosOrder>> DeletePosOrder(long id)
        {
            var posOrder = await _context.PosOrder.FindAsync(id);
            if (posOrder == null)
            {
                return NotFound();
            }

            _context.PosOrder.Remove(posOrder);
            await _context.SaveChangesAsync();

            return posOrder;
        }

        private bool PosOrderExists(long id)
        {
            return _context.PosOrder.Any(e => e.id == id);
        }
    }
}
