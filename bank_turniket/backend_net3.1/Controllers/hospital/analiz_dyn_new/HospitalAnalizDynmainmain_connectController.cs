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
    public class HospitalAnalizDynmainmain_connectController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynmainmain_connectController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicmains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDyn_connect_serice>>> GetHospitalAnalizDyn_connect_serice()
        {
            return await _context.HospitalAnalizDyn_connect_serice.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDyn_connect_serice> itemList = await _context.HospitalAnalizDyn_connect_serice
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDyn_connect_serice>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDyn_connect_serice.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationSerciceId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSerciceId([FromQuery] int page, [FromQuery] int size, [FromQuery] long serviceId)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDyn_connect_serice> itemList = await _context.HospitalAnalizDyn_connect_serice
                .OrderBy(p => p.Id)
                .Include(p => p.HospitalAnalizDynmain_item)
                .ThenInclude(p =>p.HospitalAnalizDynmain)
                .Where(p => p.ServiceTypeId == serviceId)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDyn_connect_serice>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDyn_connect_serice.Where(p => p.ServiceTypeId == serviceId).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalAnalizDyn_connect_serices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDyn_connect_serice>> GetHospitalAnalizDyn_connect_serice(long id)
        {
            var hospitalAnalizDyn_connect_serice = await _context.HospitalAnalizDyn_connect_serice.FindAsync(id);

            if (hospitalAnalizDyn_connect_serice == null)
            {
                return NotFound();
            }

            return hospitalAnalizDyn_connect_serice;
        }



        // PUT: api/HospitalAnalizDynamicmains/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDyn_connect_serice(long id, HospitalAnalizDyn_connect_serice hospitalAnalizDyn_connect_serice)
        {
            if (id != hospitalAnalizDyn_connect_serice.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDyn_connect_serice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDyn_connect_sericeExists(id))
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
        public async Task<ActionResult<HospitalAnalizDyn_connect_serice>> PostHospitalAnalizDyn_connect_serice(HospitalAnalizDyn_connect_serice hospitalAnalizDyn_connect_serice)
        {
            _context.HospitalAnalizDyn_connect_serice.Update(hospitalAnalizDyn_connect_serice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDyn_connect_serice", new { id = hospitalAnalizDyn_connect_serice.Id }, hospitalAnalizDyn_connect_serice);
        }

        // DELETE: api/HospitalAnalizDynamicmains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDyn_connect_serice>> DeleteHospitalAnalizDyn_connect_serice(long id)
        {
            var hospitalAnalizDyn_connect_serice = await _context.HospitalAnalizDyn_connect_serice.FindAsync(id);
            if (hospitalAnalizDyn_connect_serice == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDyn_connect_serice.Remove(hospitalAnalizDyn_connect_serice);
            await _context.SaveChangesAsync();

            return hospitalAnalizDyn_connect_serice;
        }

        private bool HospitalAnalizDyn_connect_sericeExists(long id)
        {
            return _context.HospitalAnalizDyn_connect_serice.Any(e => e.Id == id);
        }
    }
}
