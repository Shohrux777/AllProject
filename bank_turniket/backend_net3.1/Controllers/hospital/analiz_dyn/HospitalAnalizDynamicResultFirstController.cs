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
    public class HospitalAnalizDynamicResultFirstController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicResultFirstController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicResultFirsts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicResultFirst>>> GetHospitalAnalizDynamicResultFirst()
        {
            return await _context.HospitalAnalizDynamicResultFirst.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResultFirst> itemList = await _context.HospitalAnalizDynamicResultFirst
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResultFirst>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResultFirst.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResultFirst> itemList = await _context.HospitalAnalizDynamicResultFirst
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResultFirst>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResultFirst
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicResultFirsts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResultFirst>> GetHospitalAnalizDynamicResultFirst(long id)
        {
            var HospitalAnalizDynamicResultFirst = await _context.HospitalAnalizDynamicResultFirst.FindAsync(id);

            if (HospitalAnalizDynamicResultFirst == null)
            {
                return NotFound();
            }

            return HospitalAnalizDynamicResultFirst;
        }

        // PUT: api/HospitalAnalizDynamicResultFirsts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicResultFirst(long id, HospitalAnalizDynamicResultFirst HospitalAnalizDynamicResultFirst)
        {
            if (id != HospitalAnalizDynamicResultFirst.Id)
            {
                return BadRequest();
            }

            _context.Entry(HospitalAnalizDynamicResultFirst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicResultFirstExists(id))
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

        // POST: api/HospitalAnalizDynamicResultFirsts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicResultFirst>> PostHospitalAnalizDynamicResultFirst(HospitalAnalizDynamicResultFirst HospitalAnalizDynamicResultFirst)
        {
            _context.HospitalAnalizDynamicResultFirst.Update(HospitalAnalizDynamicResultFirst);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicResultFirst", new { id = HospitalAnalizDynamicResultFirst.Id }, HospitalAnalizDynamicResultFirst);
        }



        // DELETE: api/HospitalAnalizDynamicResultFirsts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResultFirst>> DeleteHospitalAnalizDynamicResultFirst(long id)
        {
            var HospitalAnalizDynamicResultFirst = await _context.HospitalAnalizDynamicResultFirst.FindAsync(id);
            if (HospitalAnalizDynamicResultFirst == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicResultFirst.Remove(HospitalAnalizDynamicResultFirst);
            await _context.SaveChangesAsync();

            return HospitalAnalizDynamicResultFirst;
        }

        private bool HospitalAnalizDynamicResultFirstExists(long id)
        {
            return _context.HospitalAnalizDynamicResultFirst.Any(e => e.Id == id);
        }
    }
}
