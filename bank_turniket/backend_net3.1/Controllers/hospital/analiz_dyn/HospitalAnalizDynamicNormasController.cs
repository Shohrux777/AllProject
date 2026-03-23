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
    public class HospitalAnalizDynamicNormasController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicNormasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicNormas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicNorma>>> GetHospitalAnalizDynamicNorma()
        {
            return await _context.HospitalAnalizDynamicNorma.ToListAsync();
        }

        // GET: api/HospitalAnalizDynamicNormas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicNorma>> GetHospitalAnalizDynamicNorma(long id)
        {
            var hospitalAnalizDynamicNorma = await _context.HospitalAnalizDynamicNorma.FindAsync(id);

            if (hospitalAnalizDynamicNorma == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicNorma;
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicNorma> itemList = await _context.HospitalAnalizDynamicNorma
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicNorma>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicNorma.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicNorma> itemList = await _context.HospitalAnalizDynamicNorma
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicNorma>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicNorma
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // PUT: api/HospitalAnalizDynamicNormas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicNorma(long id, HospitalAnalizDynamicNorma hospitalAnalizDynamicNorma)
        {
            if (id != hospitalAnalizDynamicNorma.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicNorma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicNormaExists(id))
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
        public async Task<ActionResult<HospitalAnalizDynamicNorma>> PostHospitalAnalizDynamicNorma(HospitalAnalizDynamicNorma hospitalAnalizDynamicNorma)
        {
            _context.HospitalAnalizDynamicNorma.Update(hospitalAnalizDynamicNorma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicNorma", new { id = hospitalAnalizDynamicNorma.Id }, hospitalAnalizDynamicNorma);
        }

        // DELETE: api/HospitalAnalizDynamicNormas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicNorma>> DeleteHospitalAnalizDynamicNorma(long id)
        {
            var hospitalAnalizDynamicNorma = await _context.HospitalAnalizDynamicNorma.FindAsync(id);
            if (hospitalAnalizDynamicNorma == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicNorma.Remove(hospitalAnalizDynamicNorma);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicNorma;
        }

        private bool HospitalAnalizDynamicNormaExists(long id)
        {
            return _context.HospitalAnalizDynamicNorma.Any(e => e.Id == id);
        }
    }
}
