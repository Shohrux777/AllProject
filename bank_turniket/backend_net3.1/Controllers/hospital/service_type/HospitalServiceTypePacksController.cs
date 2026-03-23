using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital;
using ApiAll.Model.tekistil;
using ApiAll.Model;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.service_type
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalServiceTypePacksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalServiceTypePacksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalServiceTypePacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalServiceTypePack>>> GetHospitalServiceTypePack()
        {
            return await _context.HospitalServiceTypePack.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalServiceTypePack> categoryList = await _context.HospitalServiceTypePack
                .Include(p => p.items)
                .ThenInclude(p => p.serviceType)
                .Skip(page * size).Take(size).OrderByDescending(p => p.Id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<HospitalServiceTypePack>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.HospitalServiceTypePack.CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalServiceTypePacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalServiceTypePack>> GetHospitalServiceTypePack(long id)
        {
            var hospitalServiceTypePack = await _context.HospitalServiceTypePack.FindAsync(id);

            if (hospitalServiceTypePack == null)
            {
                return NotFound();
            }

            hospitalServiceTypePack.items = await _context.HospitalServiceTypePackItem
                .Where(p => p.HospitalServiceTypePackId == hospitalServiceTypePack.Id)
                .Include( p => p.serviceType)
                .Include( p => p.authorization)
                .ThenInclude(p => p.users)
                .ToListAsync();


            return hospitalServiceTypePack;
        }

        // PUT: api/HospitalServiceTypePacks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalServiceTypePack(long id, HospitalServiceTypePack hospitalServiceTypePack)
        {
            if (id != hospitalServiceTypePack.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalServiceTypePack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalServiceTypePackExists(id))
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

        // POST: api/HospitalServiceTypePacks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalServiceTypePack>> PostHospitalServiceTypePack(HospitalServiceTypePack hospitalServiceTypePack)
        {
            _context.HospitalServiceTypePack.Update(hospitalServiceTypePack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalServiceTypePack", new { id = hospitalServiceTypePack.Id }, hospitalServiceTypePack);
        }

        // DELETE: api/HospitalServiceTypePacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalServiceTypePack>> DeleteHospitalServiceTypePack(long id)
        {
            var hospitalServiceTypePack = await _context.HospitalServiceTypePack.FindAsync(id);
            if (hospitalServiceTypePack == null)
            {
                return NotFound();
            }

            _context.HospitalServiceTypePack.Remove(hospitalServiceTypePack);
            await _context.SaveChangesAsync();

            return hospitalServiceTypePack;
        }

        private bool HospitalServiceTypePackExists(long id)
        {
            return _context.HospitalServiceTypePack.Any(e => e.Id == id);
        }
    }
}
