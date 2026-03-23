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
    public class HospitalAnalizDynmainController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynmainController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicmains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynmain>>> GetHospitalAnalizDynmain()
        {
            return await _context.HospitalAnalizDynmain.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynmain> itemList = await _context.HospitalAnalizDynmain
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynmain>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynmain.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynmains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynmain>> GetHospitalAnalizDynmain(long id)
        {
            var hospitalAnalizDynmain = await _context.HospitalAnalizDynmain.FindAsync(id);

            if (hospitalAnalizDynmain == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynmain;
        }



        // PUT: api/HospitalAnalizDynamicmains/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynmain(long id, HospitalAnalizDynmain hospitalAnalizDynmain)
        {
            if (id != hospitalAnalizDynmain.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynmain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynmainExists(id))
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

        // POST: api/HospitalAnalizDynmains
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynmain>> PostHospitalAnalizDynmain(HospitalAnalizDynmain hospitalAnalizDynmain)
        {
            _context.HospitalAnalizDynmain.Update(hospitalAnalizDynmain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynmain", new { id = hospitalAnalizDynmain.Id }, hospitalAnalizDynmain);
        }

        // DELETE: api/HospitalAnalizDynamicmains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynmain>> DeleteHospitalAnalizDynmain(long id)
        {
            var hospitalAnalizDynmain = await _context.HospitalAnalizDynmain.FindAsync(id);
            if (hospitalAnalizDynmain == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynmain.Remove(hospitalAnalizDynmain);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynmain;
        }

        private bool HospitalAnalizDynmainExists(long id)
        {
            return _context.HospitalAnalizDynmain.Any(e => e.Id == id);
        }
    }
}
