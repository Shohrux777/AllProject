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

namespace ApiAll.Controllers.hospital.analiz
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAnalizDynamicmain_itemController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicmain_itemController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicNormas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynmain_item>>> GetHospitalAnalizDynmain_item()
        {
            return await _context.HospitalAnalizDynmain_item.ToListAsync();
        }

        // GET: api/HospitalAnalizDynmain_items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynmain_item>> GetHospitalAnalizDynmain_item(long id)
        {
            var hospitalAnalizDynmain_item = await _context.HospitalAnalizDynmain_item.FindAsync(id);

            if (hospitalAnalizDynmain_item == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynmain_item;
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynmain_item> itemList = await _context.HospitalAnalizDynmain_item
                .OrderByDescending(p => p.Id)
                .Include(p => p.HospitalAnalizDynmainId)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynmain_item>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynmain_item.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynmain_item> itemList = await _context.HospitalAnalizDynmain_item
                .OrderBy(p => p.Id)
                .Include(p => p.HospitalAnalizDynmain)
                .Where(p => p.HospitalAnalizDynmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynmain_item>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynmain_item
                .Where(p => p.HospitalAnalizDynmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // PUT: api/HospitalAnalizDynmain_items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynmain_item(long id, HospitalAnalizDynmain_item hospitalAnalizDynmain_item)
        {
            if (id != hospitalAnalizDynmain_item.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynmain_item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynmain_itemExists(id))
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

        // POST: api/HospitalAnalizDynamicNormas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynmain_item>> PostHospitalAnalizDynmain_item(HospitalAnalizDynmain_item hospitalAnalizDynmain_item)
        {
            _context.HospitalAnalizDynmain_item.Update(hospitalAnalizDynmain_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynmain_item", new { id = hospitalAnalizDynmain_item.Id }, hospitalAnalizDynmain_item);
        }

        // DELETE: api/HospitalAnalizDynmain_items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynmain_item>> DeleteHospitalAnalizDynmain_item(long id)
        {
            var hospitalAnalizDynmain_item = await _context.HospitalAnalizDynmain_item.FindAsync(id);
            if (hospitalAnalizDynmain_item == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynmain_item.Remove(hospitalAnalizDynmain_item);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynmain_item;
        }

        private bool HospitalAnalizDynmain_itemExists(long id)
        {
            return _context.HospitalAnalizDynmain_item.Any(e => e.Id == id);
        }
    }
}
