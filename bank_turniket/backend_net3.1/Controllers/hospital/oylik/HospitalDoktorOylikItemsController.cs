using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.bron;

namespace ApiAll.Controllers.hospital.oylik
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalDoktorOylikItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalDoktorOylikItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalDoktorOylikItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalDoktorOylikItem>>> GetHospitalDoktorOylikItem()
        {
            return await _context.HospitalDoktorOylikItem.ToListAsync();
        }

        // GET: api/HospitalDoktorOylikItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDoktorOylikItem>> GetHospitalDoktorOylikItem(long id)
        {
            var hospitalDoktorOylikItem = await _context.HospitalDoktorOylikItem.FindAsync(id);

            if (hospitalDoktorOylikItem == null)
            {
                return NotFound();
            }

            return hospitalDoktorOylikItem;
        }

        // PUT: api/HospitalDoktorOylikItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalDoktorOylikItem(long id, HospitalDoktorOylikItem hospitalDoktorOylikItem)
        {
            if (id != hospitalDoktorOylikItem.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalDoktorOylikItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalDoktorOylikItemExists(id))
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

        // POST: api/HospitalDoktorOylikItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalDoktorOylikItem>> PostHospitalDoktorOylikItem(HospitalDoktorOylikItem hospitalDoktorOylikItem)
        {
            _context.HospitalDoktorOylikItem.Update(hospitalDoktorOylikItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalDoktorOylikItem", new { id = hospitalDoktorOylikItem.id }, hospitalDoktorOylikItem);
        }

        // DELETE: api/HospitalDoktorOylikItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalDoktorOylikItem>> DeleteHospitalDoktorOylikItem(long id)
        {
            var hospitalDoktorOylikItem = await _context.HospitalDoktorOylikItem.FindAsync(id);
            if (hospitalDoktorOylikItem == null)
            {
                return NotFound();
            }

            _context.HospitalDoktorOylikItem.Remove(hospitalDoktorOylikItem);
            await _context.SaveChangesAsync();

            return hospitalDoktorOylikItem;
        }

        private bool HospitalDoktorOylikItemExists(long id)
        {
            return _context.HospitalDoktorOylikItem.Any(e => e.id == id);
        }
    }
}
