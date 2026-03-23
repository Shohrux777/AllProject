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
    public class HospitalAnalizDynamicLabTitlesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicLabTitlesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicLabTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicLabTitle>>> GetHospitalAnalizDynamicLabTitle()
        {
            return await _context.HospitalAnalizDynamicLabTitle.ToListAsync();
        }

        // GET: api/HospitalAnalizDynamicLabTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicLabTitle>> GetHospitalAnalizDynamicLabTitle(long id)
        {
            var hospitalAnalizDynamicLabTitle = await _context.HospitalAnalizDynamicLabTitle.FindAsync(id);

            if (hospitalAnalizDynamicLabTitle == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicLabTitle;
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicLabTitle> itemList = await _context.HospitalAnalizDynamicLabTitle
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicLabTitle>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicLabTitle.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicLabTitle> itemList = await _context.HospitalAnalizDynamicLabTitle
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicLabTitle>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicLabTitle
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // PUT: api/HospitalAnalizDynamicLabTitles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicLabTitle(long id, HospitalAnalizDynamicLabTitle hospitalAnalizDynamicLabTitle)
        {
            if (id != hospitalAnalizDynamicLabTitle.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicLabTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicLabTitleExists(id))
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

        // POST: api/HospitalAnalizDynamicLabTitles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicLabTitle>> PostHospitalAnalizDynamicLabTitle(HospitalAnalizDynamicLabTitle hospitalAnalizDynamicLabTitle)
        {
            _context.HospitalAnalizDynamicLabTitle.Update(hospitalAnalizDynamicLabTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicLabTitle", new { id = hospitalAnalizDynamicLabTitle.Id }, hospitalAnalizDynamicLabTitle);
        }

        // DELETE: api/HospitalAnalizDynamicLabTitles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicLabTitle>> DeleteHospitalAnalizDynamicLabTitle(long id)
        {
            var hospitalAnalizDynamicLabTitle = await _context.HospitalAnalizDynamicLabTitle.FindAsync(id);
            if (hospitalAnalizDynamicLabTitle == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicLabTitle.Remove(hospitalAnalizDynamicLabTitle);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicLabTitle;
        }

        private bool HospitalAnalizDynamicLabTitleExists(long id)
        {
            return _context.HospitalAnalizDynamicLabTitle.Any(e => e.Id == id);
        }
    }
}
