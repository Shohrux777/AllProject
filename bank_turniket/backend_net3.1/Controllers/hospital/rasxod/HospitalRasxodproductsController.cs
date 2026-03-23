using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.rasxod
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalRasxodproductsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalRasxodproductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalRasxodproducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalRasxodproducts>>> GetHospitalRasxodproducts()
        {
            return await _context.HospitalRasxodproducts.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalRasxodproducts> itemList = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true)
                .Include(p => p.patients)
                .Include(p => p.marketProduct)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalRasxodproducts>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDateTime")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDateTime([FromQuery] int page, 
            [FromQuery] int size,[FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalRasxodproducts> itemList = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date))
                .Include(p => p.patients)
                .Include(p => p.marketProduct)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalRasxodproducts>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDateTimeAndServiceTypeId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDateTimeAndServiceTypeId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] DateTime begin_date,
    [FromQuery] DateTime end_date, [FromQuery] long service_type_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalRasxodproducts> itemList = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true 
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)
                && p.ServiceTypeId == service_type_id)
                .Include(p => p.patients)
                .Include(p => p.marketProduct)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalRasxodproducts>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalRasxodproducts
                .Where(p => p.active_status == true 
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)
                && p.ServiceTypeId == service_type_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // GET: api/HospitalRasxodproducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalRasxodproducts>> GetHospitalRasxodproducts(long id)
        {
            var hospitalRasxodproducts = await _context.HospitalRasxodproducts.FindAsync(id);

            if (hospitalRasxodproducts == null)
            {
                return NotFound();
            }

            return hospitalRasxodproducts;
        }

        // PUT: api/HospitalRasxodproducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalRasxodproducts(long id, HospitalRasxodproducts hospitalRasxodproducts)
        {
            if (id != hospitalRasxodproducts.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalRasxodproducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalRasxodproductsExists(id))
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

        // POST: api/HospitalRasxodproducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalRasxodproducts>> PostHospitalRasxodproducts(HospitalRasxodproducts hospitalRasxodproducts)
        {
            _context.HospitalRasxodproducts.Update(hospitalRasxodproducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalRasxodproducts", new { id = hospitalRasxodproducts.id }, hospitalRasxodproducts);
        }

        // DELETE: api/HospitalRasxodproducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalRasxodproducts>> DeleteHospitalRasxodproducts(long id)
        {
            var hospitalRasxodproducts = await _context.HospitalRasxodproducts.FindAsync(id);
            if (hospitalRasxodproducts == null)
            {
                return NotFound();
            }

            _context.HospitalRasxodproducts.Remove(hospitalRasxodproducts);
            await _context.SaveChangesAsync();

            return hospitalRasxodproducts;
        }

        private bool HospitalRasxodproductsExists(long id)
        {
            return _context.HospitalRasxodproducts.Any(e => e.id == id);
        }
    }
}
