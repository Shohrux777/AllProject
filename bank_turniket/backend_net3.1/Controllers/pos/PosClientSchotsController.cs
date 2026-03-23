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
    public class PosClientSchotsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosClientSchotsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientSchot> itemList = await _context.PosClientSchot
                .Where(p => p.active_status == true)
                .Include(p => p.client)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientSchot>();
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientSchot
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationRealQtyNotZeroByClientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationRealQtyNotZeroByClientId([FromQuery] int page, 
            [FromQuery] int size, [FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientSchot> itemList = await _context.PosClientSchot
                .Where(p => p.active_status == true && (p.real_qty > 0 && p.PosClientid == client_id))
                .Include(p => p.client)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientSchot>();
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientSchot
                 .Where(p => p.active_status == true && (p.real_qty > 0 && p.PosClientid == client_id)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationRealQtyNotZero")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationRealQtyNotZero([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientSchot> itemList = await _context.PosClientSchot
                .Where(p => p.active_status == true && p.real_qty > 0)
                .Include(p => p.client)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientSchot>();
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientSchot
                .Where(p => p.active_status == true && p.real_qty > 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosClientSchots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosClientSchot>>> GetPosClientSchot()
        {
            return await _context.PosClientSchot.ToListAsync();
        }

        // GET: api/PosClientSchots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosClientSchot>> GetPosClientSchot(long id)
        {
            var posClientSchot = await _context.PosClientSchot.FindAsync(id);

            if (posClientSchot == null)
            {
                return NotFound();
            }

            posClientSchot.client = await _context.PosClient.FindAsync(posClientSchot.PosClientid);

            return posClientSchot;
        }

        // GET: api/PosClientSchots/5
        [HttpGet("addMoney")]
        public async Task<ActionResult<PosClientSchot>> addMoney([FromQuery] long pos_schot_id, [FromQuery] double qty_summ)
        {
            var posClientSchot = await _context.PosClientSchot.FindAsync(pos_schot_id);

            if (posClientSchot == null)
            {
                return NotFound();
            }
            posClientSchot.qty = posClientSchot.qty+ qty_summ;
            posClientSchot.real_qty = posClientSchot.real_qty + qty_summ;

            posClientSchot.client = await _context.PosClient.FindAsync(posClientSchot.PosClientid);

            return posClientSchot;
        }

        [HttpGet("minusMoney")]
        public async Task<ActionResult<PosClientSchot>> minusMoney([FromQuery] long pos_schot_id, [FromQuery] double qty_summ)
        {
            var posClientSchot = await _context.PosClientSchot.FindAsync(pos_schot_id);

            if (posClientSchot == null)
            {
                return NotFound();
            }
      
            posClientSchot.real_qty = posClientSchot.real_qty - qty_summ;

            posClientSchot.client = await _context.PosClient.FindAsync(posClientSchot.PosClientid);

            return posClientSchot;
        }

        // PUT: api/PosClientSchots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosClientSchot(long id, PosClientSchot posClientSchot)
        {
            if (id != posClientSchot.id)
            {
                return BadRequest();
            }

            _context.Entry(posClientSchot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosClientSchotExists(id))
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

        // POST: api/PosClientSchots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosClientSchot>> PostPosClientSchot(PosClientSchot posClientSchot)
        {
            _context.PosClientSchot.Update(posClientSchot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosClientSchot", new { id = posClientSchot.id }, posClientSchot);
        }

        // DELETE: api/PosClientSchots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosClientSchot>> DeletePosClientSchot(long id)
        {
            var posClientSchot = await _context.PosClientSchot.FindAsync(id);
            if (posClientSchot == null)
            {
                return NotFound();
            }

            _context.PosClientSchot.Remove(posClientSchot);
            await _context.SaveChangesAsync();

            return posClientSchot;
        }

        private bool PosClientSchotExists(long id)
        {
            return _context.PosClientSchot.Any(e => e.id == id);
        }
    }
}
