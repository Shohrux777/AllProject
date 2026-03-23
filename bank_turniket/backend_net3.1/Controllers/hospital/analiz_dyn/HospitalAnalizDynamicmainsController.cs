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
    public class HospitalAnalizDynamicmainsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicmainsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicmains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicmain>>> GetHospitalAnalizDynamicmain()
        {
            return await _context.HospitalAnalizDynamicmain.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicmain> itemList = await _context.HospitalAnalizDynamicmain
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicmain>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicmain.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDynamicmains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicmain>> GetHospitalAnalizDynamicmain(long id)
        {
            var hospitalAnalizDynamicmain = await _context.HospitalAnalizDynamicmain.FindAsync(id);

            if (hospitalAnalizDynamicmain == null)
            {
                return NotFound();
            }

            return hospitalAnalizDynamicmain;
        }



        // PUT: api/HospitalAnalizDynamicmains/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicmain(long id, HospitalAnalizDynamicmain hospitalAnalizDynamicmain)
        {
            if (id != hospitalAnalizDynamicmain.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicmain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicmainExists(id))
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

        // POST: api/HospitalAnalizDynamicmains
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicmain>> PostHospitalAnalizDynamicmain(HospitalAnalizDynamicmain hospitalAnalizDynamicmain)
        {
            _context.HospitalAnalizDynamicmain.Update(hospitalAnalizDynamicmain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicmain", new { id = hospitalAnalizDynamicmain.Id }, hospitalAnalizDynamicmain);
        }

        // DELETE: api/HospitalAnalizDynamicmains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicmain>> DeleteHospitalAnalizDynamicmain(long id)
        {
            var hospitalAnalizDynamicmain = await _context.HospitalAnalizDynamicmain.FindAsync(id);
            if (hospitalAnalizDynamicmain == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicmain.Remove(hospitalAnalizDynamicmain);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicmain;
        }

        private bool HospitalAnalizDynamicmainExists(long id)
        {
            return _context.HospitalAnalizDynamicmain.Any(e => e.Id == id);
        }
    }
}
