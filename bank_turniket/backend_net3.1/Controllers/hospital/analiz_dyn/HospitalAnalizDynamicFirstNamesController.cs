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
    public class HospitalAnalizDynamicFirstNamesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicFirstNamesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicFirstName> itemList = await _context.HospitalAnalizDynamicFirstName
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicFirstName>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicFirstName.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicFirstName> itemList = await _context.HospitalAnalizDynamicFirstName
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicFirstName>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicFirstName
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicFirstNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicFirstName>>> GetHospitalAnalizDynamicFirstName()
        {
            return await _context.HospitalAnalizDynamicFirstName.ToListAsync();
        }

        // GET: api/HospitalAnalizDynamicFirstNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicFirstName>> GetHospitalAnalizDynamicFirstName(long id)
        {
            var hospitalAnalizDynamicFirstName = await _context.HospitalAnalizDynamicFirstName.FindAsync(id);

            if (hospitalAnalizDynamicFirstName == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicFirstName;
        }

        // PUT: api/HospitalAnalizDynamicFirstNames/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicFirstName(long id, HospitalAnalizDynamicFirstName hospitalAnalizDynamicFirstName)
        {
            if (id != hospitalAnalizDynamicFirstName.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicFirstName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicFirstNameExists(id))
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

        // POST: api/HospitalAnalizDynamicFirstNames
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicFirstName>> PostHospitalAnalizDynamicFirstName(HospitalAnalizDynamicFirstName hospitalAnalizDynamicFirstName)
        {
            _context.HospitalAnalizDynamicFirstName.Update(hospitalAnalizDynamicFirstName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicFirstName", new { id = hospitalAnalizDynamicFirstName.Id }, hospitalAnalizDynamicFirstName);
        }

        // DELETE: api/HospitalAnalizDynamicFirstNames/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicFirstName>> DeleteHospitalAnalizDynamicFirstName(long id)
        {
            var hospitalAnalizDynamicFirstName = await _context.HospitalAnalizDynamicFirstName.FindAsync(id);
            if (hospitalAnalizDynamicFirstName == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicFirstName.Remove(hospitalAnalizDynamicFirstName);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicFirstName;
        }

        private bool HospitalAnalizDynamicFirstNameExists(long id)
        {
            return _context.HospitalAnalizDynamicFirstName.Any(e => e.Id == id);
        }
    }
}
