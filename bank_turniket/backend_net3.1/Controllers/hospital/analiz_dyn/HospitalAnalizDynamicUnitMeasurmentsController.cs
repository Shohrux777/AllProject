using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.analiz;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.analiz_dyn
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAnalizDynamicUnitMeasurmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicUnitMeasurmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicUnitMeasurments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicUnitMeasurment>>> GetHospitalAnalizDynamicUnitMeasurment()
        {
            return await _context.HospitalAnalizDynamicUnitMeasurment.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicUnitMeasurment> itemList = await _context.HospitalAnalizDynamicUnitMeasurment
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicUnitMeasurment>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicUnitMeasurment.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicUnitMeasurment> itemList = await _context.HospitalAnalizDynamicUnitMeasurment
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicUnitMeasurment>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicUnitMeasurment
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicUnitMeasurments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicUnitMeasurment>> GetHospitalAnalizDynamicUnitMeasurment(long id)
        {
            var hospitalAnalizDynamicUnitMeasurment = await _context.HospitalAnalizDynamicUnitMeasurment.FindAsync(id);

            if (hospitalAnalizDynamicUnitMeasurment == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicUnitMeasurment;
        }

        // PUT: api/HospitalAnalizDynamicUnitMeasurments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicUnitMeasurment(long id, HospitalAnalizDynamicUnitMeasurment hospitalAnalizDynamicUnitMeasurment)
        {
            if (id != hospitalAnalizDynamicUnitMeasurment.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicUnitMeasurment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicUnitMeasurmentExists(id))
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

        // POST: api/HospitalAnalizDynamicUnitMeasurments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicUnitMeasurment>> PostHospitalAnalizDynamicUnitMeasurment(HospitalAnalizDynamicUnitMeasurment hospitalAnalizDynamicUnitMeasurment)
        {
            _context.HospitalAnalizDynamicUnitMeasurment.Update(hospitalAnalizDynamicUnitMeasurment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicUnitMeasurment", new { id = hospitalAnalizDynamicUnitMeasurment.Id }, hospitalAnalizDynamicUnitMeasurment);
        }

        // DELETE: api/HospitalAnalizDynamicUnitMeasurments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicUnitMeasurment>> DeleteHospitalAnalizDynamicUnitMeasurment(long id)
        {
            var hospitalAnalizDynamicUnitMeasurment = await _context.HospitalAnalizDynamicUnitMeasurment.FindAsync(id);
            if (hospitalAnalizDynamicUnitMeasurment == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicUnitMeasurment.Remove(hospitalAnalizDynamicUnitMeasurment);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicUnitMeasurment;
        }

        private bool HospitalAnalizDynamicUnitMeasurmentExists(long id)
        {
            return _context.HospitalAnalizDynamicUnitMeasurment.Any(e => e.Id == id);
        }
    }
}
