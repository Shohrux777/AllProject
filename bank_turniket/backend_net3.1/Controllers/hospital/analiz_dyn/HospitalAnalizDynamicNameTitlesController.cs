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
    public class HospitalAnalizDynamicNameTitlesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicNameTitlesController(ApplicationContext context)
        {
            _context = context;
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
            paginationModel.items_count = await _context.HospitalAnalizDynamicNameTitle.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicNameTitle> itemList = await _context.HospitalAnalizDynamicNameTitle
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicNameTitle>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicNameTitle
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicNameTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicNameTitle>>> GetHospitalAnalizDynamicNameTitle()
        {
            return await _context.HospitalAnalizDynamicNameTitle.ToListAsync();
        }

        // GET: api/HospitalAnalizDynamicNameTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicNameTitle>> GetHospitalAnalizDynamicNameTitle(long id)
        {
            var hospitalAnalizDynamicNameTitle = await _context.HospitalAnalizDynamicNameTitle.FindAsync(id);

            if (hospitalAnalizDynamicNameTitle == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicNameTitle;
        }

        // PUT: api/HospitalAnalizDynamicNameTitles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicNameTitle(long id, HospitalAnalizDynamicNameTitle hospitalAnalizDynamicNameTitle)
        {
            if (id != hospitalAnalizDynamicNameTitle.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicNameTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicNameTitleExists(id))
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

        // POST: api/HospitalAnalizDynamicNameTitles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicNameTitle>> PostHospitalAnalizDynamicNameTitle(HospitalAnalizDynamicNameTitle hospitalAnalizDynamicNameTitle)
        {
            _context.HospitalAnalizDynamicNameTitle.Update(hospitalAnalizDynamicNameTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicNameTitle", new { id = hospitalAnalizDynamicNameTitle.Id }, hospitalAnalizDynamicNameTitle);
        }

        // DELETE: api/HospitalAnalizDynamicNameTitles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicNameTitle>> DeleteHospitalAnalizDynamicNameTitle(long id)
        {
            var hospitalAnalizDynamicNameTitle = await _context.HospitalAnalizDynamicNameTitle.FindAsync(id);
            if (hospitalAnalizDynamicNameTitle == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicNameTitle.Remove(hospitalAnalizDynamicNameTitle);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicNameTitle;
        }

        private bool HospitalAnalizDynamicNameTitleExists(long id)
        {
            return _context.HospitalAnalizDynamicNameTitle.Any(e => e.Id == id);
        }
    }
}
