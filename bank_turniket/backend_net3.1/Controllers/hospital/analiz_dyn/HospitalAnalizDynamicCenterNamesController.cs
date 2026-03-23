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
    public class HospitalAnalizDynamicCenterNamesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicCenterNamesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicCenterNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicCenterName>>> GetHospitalAnalizDynamicCenterName()
        {
            return await _context.HospitalAnalizDynamicCenterName.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicCenterName> itemList = await _context.HospitalAnalizDynamicCenterName
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicCenterName>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicCenterName.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicCenterName> itemList = await _context.HospitalAnalizDynamicCenterName
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicCenterName>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicCenterName
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicCenterNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicCenterName>> GetHospitalAnalizDynamicCenterName(long id)
        {
            var hospitalAnalizDynamicCenterName = await _context.HospitalAnalizDynamicCenterName.FindAsync(id);

            if (hospitalAnalizDynamicCenterName == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicCenterName;
        }

        // PUT: api/HospitalAnalizDynamicCenterNames/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicCenterName(long id, HospitalAnalizDynamicCenterName hospitalAnalizDynamicCenterName)
        {
            if (id != hospitalAnalizDynamicCenterName.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicCenterName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicCenterNameExists(id))
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

        // POST: api/HospitalAnalizDynamicCenterNames
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicCenterName>> PostHospitalAnalizDynamicCenterName(HospitalAnalizDynamicCenterName hospitalAnalizDynamicCenterName)
        {
            _context.HospitalAnalizDynamicCenterName.Update(hospitalAnalizDynamicCenterName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicCenterName", new { id = hospitalAnalizDynamicCenterName.Id }, hospitalAnalizDynamicCenterName);
        }

        // DELETE: api/HospitalAnalizDynamicCenterNames/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicCenterName>> DeleteHospitalAnalizDynamicCenterName(long id)
        {
            var hospitalAnalizDynamicCenterName = await _context.HospitalAnalizDynamicCenterName.FindAsync(id);
            if (hospitalAnalizDynamicCenterName == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicCenterName.Remove(hospitalAnalizDynamicCenterName);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicCenterName;
        }

        private bool HospitalAnalizDynamicCenterNameExists(long id)
        {
            return _context.HospitalAnalizDynamicCenterName.Any(e => e.Id == id);
        }
    }
}
