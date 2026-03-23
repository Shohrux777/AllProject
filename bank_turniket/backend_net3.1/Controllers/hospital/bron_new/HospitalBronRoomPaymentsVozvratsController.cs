using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.bron_new;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.bron_new
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalBronRoomPaymentsVozvratsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalBronRoomPaymentsVozvratsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalBronRoomPaymentsVozvrats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalBronRoomPaymentsVozvrat>>> GetHospitalBronRoomPaymentsVozvrat()
        {
            return await _context.HospitalBronRoomPaymentsVozvrat.ToListAsync();
        }

        [HttpGet("getPaginationByPatientIdYangiVozvrat")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientIdYangiVozvrat([FromQuery] int page,
      [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsVozvrat> itemList = await _context.HospitalBronRoomPaymentsVozvrat
                .Include(p => p.hospitalBronRoomPayments)
                .ThenInclude(p => p.patients)
                .Where(p => p.hospitalBronRoomPayments.PatientsId == patient_id)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsVozvrat>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsVozvrat.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByBeginAndEndDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBeginAndEndDate([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsVozvrat> itemList = await _context.HospitalBronRoomPaymentsVozvrat
                .Include(p => p.hospitalBronRoomPayments)
                .ThenInclude(p => p.patients)
                .Where(p => p.reg_date_time.Date >=begin_date.Date && p.reg_date_time.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsVozvrat>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsVozvrat.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page,
[FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsVozvrat> itemList = await _context.HospitalBronRoomPaymentsVozvrat
                .Include(p => p.hospitalBronRoomPayments)
                .ThenInclude(p => p.patients)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsVozvrat>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsVozvrat.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalBronRoomPaymentsVozvrats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalBronRoomPaymentsVozvrat>> GetHospitalBronRoomPaymentsVozvrat(long id)
        {
            var hospitalBronRoomPaymentsVozvrat = await _context.HospitalBronRoomPaymentsVozvrat.FindAsync(id);

            if (hospitalBronRoomPaymentsVozvrat == null)
            {
                return NotFound();
            }

            return hospitalBronRoomPaymentsVozvrat;
        }

        // PUT: api/HospitalBronRoomPaymentsVozvrats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalBronRoomPaymentsVozvrat(long id, HospitalBronRoomPaymentsVozvrat hospitalBronRoomPaymentsVozvrat)
        {
            if (id != hospitalBronRoomPaymentsVozvrat.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalBronRoomPaymentsVozvrat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalBronRoomPaymentsVozvratExists(id))
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

        // POST: api/HospitalBronRoomPaymentsVozvrats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalBronRoomPaymentsVozvrat>> PostHospitalBronRoomPaymentsVozvrat(HospitalBronRoomPaymentsVozvrat hospitalBronRoomPaymentsVozvrat)
        {
            _context.HospitalBronRoomPaymentsVozvrat.Update(hospitalBronRoomPaymentsVozvrat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalBronRoomPaymentsVozvrat", new { id = hospitalBronRoomPaymentsVozvrat.id }, hospitalBronRoomPaymentsVozvrat);
        }

        // DELETE: api/HospitalBronRoomPaymentsVozvrats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalBronRoomPaymentsVozvrat>> DeleteHospitalBronRoomPaymentsVozvrat(long id)
        {
            var hospitalBronRoomPaymentsVozvrat = await _context.HospitalBronRoomPaymentsVozvrat.FindAsync(id);
            if (hospitalBronRoomPaymentsVozvrat == null)
            {
                return NotFound();
            }

            _context.HospitalBronRoomPaymentsVozvrat.Remove(hospitalBronRoomPaymentsVozvrat);
            await _context.SaveChangesAsync();

            return hospitalBronRoomPaymentsVozvrat;
        }

        private bool HospitalBronRoomPaymentsVozvratExists(long id)
        {
            return _context.HospitalBronRoomPaymentsVozvrat.Any(e => e.id == id);
        }
    }
}
