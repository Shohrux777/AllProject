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
    public class HospitalAnalizDynamicResultsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalAnalizDynamicResultsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAnalizDynamicResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAnalizDynamicResult>>> GetHospitalAnalizDynamicResult()
        {
            return await _context.HospitalAnalizDynamicResult.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult
                .Where(p => p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientId([FromQuery] int page, [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Where(p => p.PatientsId == patient_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult
                .Where(p => p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByHosBlankId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByHosBlankId([FromQuery] int page, [FromQuery] int size, [FromQuery] long hos_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Where(p => p.analiz_hos_id == hos_id &&(p.create_date.Date == DateTime.Now.Date))
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult
                .Where(p => p.analiz_hos_id == hos_id &&(p.create_date.Date == DateTime.Now.Date)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDoctorId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDoctorId([FromQuery] int page, [FromQuery] int size, [FromQuery] long doktor_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Where(p => p.auth_id == doktor_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult
                .Where(p => p.auth_id == doktor_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientIdAndMainTableId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientIdAndMainTableId([FromQuery] int page, [FromQuery] int size, [FromQuery] long patient_id, long main_table_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalAnalizDynamicResult> itemList = await _context.HospitalAnalizDynamicResult
                .OrderByDescending(p => p.Id)
                .Include(p => p.hospitalAnalizDynamicmain)
                .Include(p => p.patients)
                .Where(p => p.PatientsId == patient_id && p.HospitalAnalizDynamicmainId == main_table_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalAnalizDynamicResult>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalAnalizDynamicResult
                .Where(p => p.PatientsId == patient_id && p.HospitalAnalizDynamicmainId == main_table_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // GET: api/HospitalAnalizDynamicResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResult>> GetHospitalAnalizDynamicResult(long id)
        {
            var hospitalAnalizDynamicResult = await _context.HospitalAnalizDynamicResult.FindAsync(id);

            if (hospitalAnalizDynamicResult == null)
            {
                return NotFound();
            }
            hospitalAnalizDynamicResult.patients = await _context.Patients.FindAsync(hospitalAnalizDynamicResult.PatientsId);
            hospitalAnalizDynamicResult.hospitalAnalizDynamicmain = await _context.HospitalAnalizDynamicmain.FindAsync(hospitalAnalizDynamicResult.HospitalAnalizDynamicmainId);

            return hospitalAnalizDynamicResult;
        }

        // PUT: api/HospitalAnalizDynamicResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAnalizDynamicResult(long id, HospitalAnalizDynamicResult hospitalAnalizDynamicResult)
        {
            if (id != hospitalAnalizDynamicResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAnalizDynamicResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAnalizDynamicResultExists(id))
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

        // POST: api/HospitalAnalizDynamicResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalAnalizDynamicResult>> PostHospitalAnalizDynamicResult(HospitalAnalizDynamicResult hospitalAnalizDynamicResult)
        {
            _context.HospitalAnalizDynamicResult.Update(hospitalAnalizDynamicResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAnalizDynamicResult", new { id = hospitalAnalizDynamicResult.Id }, hospitalAnalizDynamicResult);
        }

        // DELETE: api/HospitalAnalizDynamicResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalAnalizDynamicResult>> DeleteHospitalAnalizDynamicResult(long id)
        {
            var hospitalAnalizDynamicResult = await _context.HospitalAnalizDynamicResult.FindAsync(id);
            if (hospitalAnalizDynamicResult == null)
            {
                return NotFound();
            }

            _context.HospitalAnalizDynamicResult.Remove(hospitalAnalizDynamicResult);
            await _context.SaveChangesAsync();

            return hospitalAnalizDynamicResult;
        }

        private bool HospitalAnalizDynamicResultExists(long id)
        {
            return _context.HospitalAnalizDynamicResult.Any(e => e.Id == id);
        }
    }
}
