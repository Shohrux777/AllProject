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
    public class HospitalDoktorOyliksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalDoktorOyliksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalDoktorOyliks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalDoktorOylik>>> GetHospitalDoktorOylik()
        {
            return await _context.HospitalDoktorOylik.ToListAsync();
        }

        // GET: api/HospitalDoktorOyliks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDoktorOylik>> GetHospitalDoktorOylik(long id)
        {
            var hospitalDoktorOylik = await _context.HospitalDoktorOylik.FindAsync(id);

            if (hospitalDoktorOylik == null)
            {
                return NotFound();
            }

            return hospitalDoktorOylik;
        }

        // PUT: api/HospitalDoktorOyliks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalDoktorOylik(long id, HospitalDoktorOylik hospitalDoktorOylik)
        {
            if (id != hospitalDoktorOylik.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalDoktorOylik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalDoktorOylikExists(id))
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

        // POST: api/HospitalDoktorOyliks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalDoktorOylik>> PostHospitalDoktorOylik(HospitalDoktorOylik hospitalDoktorOylik)
        {
            _context.HospitalDoktorOylik.Update(hospitalDoktorOylik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalDoktorOylik", new { id = hospitalDoktorOylik.id }, hospitalDoktorOylik);
        }

        // DELETE: api/HospitalDoktorOyliks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalDoktorOylik>> DeleteHospitalDoktorOylik(long id)
        {
            var hospitalDoktorOylik = await _context.HospitalDoktorOylik.FindAsync(id);
            if (hospitalDoktorOylik == null)
            {
                return NotFound();
            }

            _context.HospitalDoktorOylik.Remove(hospitalDoktorOylik);
            await _context.SaveChangesAsync();

            return hospitalDoktorOylik;
        }

        private bool HospitalDoktorOylikExists(long id)
        {
            return _context.HospitalDoktorOylik.Any(e => e.id == id);
        }
    }
}
