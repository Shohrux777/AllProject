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
    public class HospitalAnalizDynamicStatusColorController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicStatusColorController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicStatusColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicStatusColor>>> GetHospitalAnalizDynamicStatusColor()
        {
            return await _context.HospitalAnalizDynamicStatusColor.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicStatusColor> itemList = await _context.HospitalAnalizDynamicStatusColor
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicStatusColor>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicStatusColor.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicStatusColor> itemList = await _context.HospitalAnalizDynamicStatusColor
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicStatusColor>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicStatusColor
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicStatusColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicStatusColor>> GetHospitalAnalizDynamicStatusColor(long id)
        {
            var HospitalAnalizDynamicStatusColor = await _context.HospitalAnalizDynamicStatusColor.FindAsync(id);

            if (HospitalAnalizDynamicStatusColor == null)
            {
                return NotFound();
            }

            return HospitalAnalizDynamicStatusColor;
        }

        // PUT: api/HospitalAnalizDynamicStatusColors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicStatusColor(long id, HospitalAnalizDynamicStatusColor HospitalAnalizDynamicStatusColor)
        {
            if (id != HospitalAnalizDynamicStatusColor.Id)
            {
                return BadRequest();
            }

            _context.Entry(HospitalAnalizDynamicStatusColor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicStatusColorExists(id))
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

        // POST: api/HospitalAnalizDynamicStatusColors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicStatusColor>> PostHospitalAnalizDynamicStatusColor(HospitalAnalizDynamicStatusColor HospitalAnalizDynamicStatusColor)
        {
            _context.HospitalAnalizDynamicStatusColor.Update(HospitalAnalizDynamicStatusColor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicStatusColor", new { id = HospitalAnalizDynamicStatusColor.Id }, HospitalAnalizDynamicStatusColor);
        }



        // DELETE: api/HospitalAnalizDynamicStatusColors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicStatusColor>> DeleteHospitalAnalizDynamicStatusColor(long id)
        {
            var HospitalAnalizDynamicStatusColor = await _context.HospitalAnalizDynamicStatusColor.FindAsync(id);
            if (HospitalAnalizDynamicStatusColor == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicStatusColor.Remove(HospitalAnalizDynamicStatusColor);
            await _context.SaveChangesAsync();

            return HospitalAnalizDynamicStatusColor;
        }

        private bool HospitalAnalizDynamicStatusColorExists(long id)
        {
            return _context.HospitalAnalizDynamicStatusColor.Any(e => e.Id == id);
        }
    }
}
