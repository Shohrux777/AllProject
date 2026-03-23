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
    public class HospitalAnalizDynamicFirstNameSecondsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicFirstNameSecondsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicFirstNameSeconds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicFirstNameSecond>>> GetHospitalAnalizDynamicFirstNameSecond()
        {
            return await _context.HospitalAnalizDynamicFirstNameSecond.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicFirstNameSecond> itemList = await _context.HospitalAnalizDynamicFirstNameSecond
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicFirstNameSecond>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicFirstNameSecond.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicFirstNameSecond> itemList = await _context.HospitalAnalizDynamicFirstNameSecond
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicFirstNameSecond>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicFirstNameSecond
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicFirstNameSeconds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicFirstNameSecond>> GetHospitalAnalizDynamicFirstNameSecond(long id)
        {
            var hospitalAnalizDynamicFirstNameSecond = await _context.HospitalAnalizDynamicFirstNameSecond.FindAsync(id);

            if (hospitalAnalizDynamicFirstNameSecond == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicFirstNameSecond;
        }

        // PUT: api/HospitalAnalizDynamicFirstNameSeconds/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicFirstNameSecond(long id, HospitalAnalizDynamicFirstNameSecond hospitalAnalizDynamicFirstNameSecond)
        {
            if (id != hospitalAnalizDynamicFirstNameSecond.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicFirstNameSecond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicFirstNameSecondExists(id))
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

        // POST: api/HospitalAnalizDynamicFirstNameSeconds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicFirstNameSecond>> PostHospitalAnalizDynamicFirstNameSecond(HospitalAnalizDynamicFirstNameSecond hospitalAnalizDynamicFirstNameSecond)
        {
            _context.HospitalAnalizDynamicFirstNameSecond.Update(hospitalAnalizDynamicFirstNameSecond);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicFirstNameSecond", new { id = hospitalAnalizDynamicFirstNameSecond.Id }, hospitalAnalizDynamicFirstNameSecond);
        }



        // DELETE: api/HospitalAnalizDynamicFirstNameSeconds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicFirstNameSecond>> DeleteHospitalAnalizDynamicFirstNameSecond(long id)
        {
            var hospitalAnalizDynamicFirstNameSecond = await _context.HospitalAnalizDynamicFirstNameSecond.FindAsync(id);
            if (hospitalAnalizDynamicFirstNameSecond == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicFirstNameSecond.Remove(hospitalAnalizDynamicFirstNameSecond);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicFirstNameSecond;
        }

        private bool HospitalAnalizDynamicFirstNameSecondExists(long id)
        {
            return _context.HospitalAnalizDynamicFirstNameSecond.Any(e => e.Id == id);
        }
    }
}
