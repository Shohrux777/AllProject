using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital;

namespace ApiAll.Controllers.hospital.service_type
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalServiceTypePackItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalServiceTypePackItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalServiceTypePackItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalServiceTypePackItem>>> GetHospitalServiceTypePackItem()
        {
            return await _context.HospitalServiceTypePackItem.ToListAsync();
        }

        // GET: api/HospitalServiceTypePackItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalServiceTypePackItem>> GetHospitalServiceTypePackItem(long id)
        {
            var hospitalServiceTypePackItem = await _context.HospitalServiceTypePackItem.FindAsync(id);

            if (hospitalServiceTypePackItem == null)
            {
                return NotFound();
            }

            return hospitalServiceTypePackItem;
        }

        // PUT: api/HospitalServiceTypePackItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalServiceTypePackItem(long id, HospitalServiceTypePackItem hospitalServiceTypePackItem)
        {
            if (id != hospitalServiceTypePackItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalServiceTypePackItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalServiceTypePackItemExists(id))
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

        // POST: api/HospitalServiceTypePackItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalServiceTypePackItem>> PostHospitalServiceTypePackItem(HospitalServiceTypePackItem hospitalServiceTypePackItem)
        {
            _context.HospitalServiceTypePackItem.Update(hospitalServiceTypePackItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalServiceTypePackItem", new { id = hospitalServiceTypePackItem.Id }, hospitalServiceTypePackItem);
        }

        // DELETE: api/HospitalServiceTypePackItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalServiceTypePackItem>> DeleteHospitalServiceTypePackItem(long id)
        {
            var hospitalServiceTypePackItem = await _context.HospitalServiceTypePackItem.FindAsync(id);
            if (hospitalServiceTypePackItem == null)
            {
                return NotFound();
            }

            _context.HospitalServiceTypePackItem.Remove(hospitalServiceTypePackItem);
            await _context.SaveChangesAsync();

            return hospitalServiceTypePackItem;
        }

        private bool HospitalServiceTypePackItemExists(long id)
        {
            return _context.HospitalServiceTypePackItem.Any(e => e.Id == id);
        }
    }
}
