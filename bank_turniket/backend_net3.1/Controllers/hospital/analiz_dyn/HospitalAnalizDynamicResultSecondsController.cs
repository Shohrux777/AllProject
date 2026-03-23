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
    public class HospitalAnalizDynamicResultSecondsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicResultSecondsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicResultSeconds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicResultSecond>>> GetHospitalAnalizDynamicResultSecond()
        {
            return await _context.HospitalAnalizDynamicResultSecond.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResultSecond> itemList = await _context.HospitalAnalizDynamicResultSecond
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResultSecond>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResultSecond.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResultSecond> itemList = await _context.HospitalAnalizDynamicResultSecond
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResultSecond>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResultSecond
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicResultSeconds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResultSecond>> GetHospitalAnalizDynamicResultSecond(long id)
        {
            var HospitalAnalizDynamicResultSecond = await _context.HospitalAnalizDynamicResultSecond.FindAsync(id);

            if (HospitalAnalizDynamicResultSecond == null)
            {
                return NotFound();
            }

            return HospitalAnalizDynamicResultSecond;
        }

        // PUT: api/HospitalAnalizDynamicResultSeconds/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicResultSecond(long id, HospitalAnalizDynamicResultSecond HospitalAnalizDynamicResultSecond)
        {
            if (id != HospitalAnalizDynamicResultSecond.Id)
            {
                return BadRequest();
            }

            _context.Entry(HospitalAnalizDynamicResultSecond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicResultSecondExists(id))
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

        // POST: api/HospitalAnalizDynamicResultSeconds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicResultSecond>> PostHospitalAnalizDynamicResultSecond(HospitalAnalizDynamicResultSecond HospitalAnalizDynamicResultSecond)
        {
            _context.HospitalAnalizDynamicResultSecond.Update(HospitalAnalizDynamicResultSecond);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicResultSecond", new { id = HospitalAnalizDynamicResultSecond.Id }, HospitalAnalizDynamicResultSecond);
        }



        // DELETE: api/HospitalAnalizDynamicResultSeconds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResultSecond>> DeleteHospitalAnalizDynamicResultSecond(long id)
        {
            var HospitalAnalizDynamicResultSecond = await _context.HospitalAnalizDynamicResultSecond.FindAsync(id);
            if (HospitalAnalizDynamicResultSecond == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicResultSecond.Remove(HospitalAnalizDynamicResultSecond);
            await _context.SaveChangesAsync();

            return HospitalAnalizDynamicResultSecond;
        }

        private bool HospitalAnalizDynamicResultSecondExists(long id)
        {
            return _context.HospitalAnalizDynamicResultSecond.Any(e => e.Id == id);
        }
    }
}
