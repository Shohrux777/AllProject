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

namespace ApiAll.Controllers.hospital.infofile
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalFileInfoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalFileInfoesController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet("getFullInfoById")]
        public async Task<ActionResult<HospitalFileInfo>> getFullInfoById([FromQuery] long id)
        {
            var item = await _context
                .HospitalFileInfo
                .Include(p => p.patients)
                .Where(p => p.id == id).ToListAsync();

            if (item == null || item.Count() == 0)
            {
                return NotFound();
            }

            return item.First();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAll")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAll([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOnlyAnalizTrue")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyCheckTrue([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.analiz_pic == true)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo.Where(p => p.analiz_pic == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOnlyAnalizFalse")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyAnalizFalse([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.analiz_pic == false)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo.Where(p => p.analiz_pic == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatient")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatient([FromQuery] int page, [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.PatientsId == patient_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDoctor")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDoctor([FromQuery] int page, [FromQuery] int size, [FromQuery] long doctor_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.AuthorizationId == doctor_id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.AuthorizationId == doctor_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDoctorAnalizTrue")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDoctorAnalizTrue([FromQuery] int page, [FromQuery] int size, [FromQuery] long doctor_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.AuthorizationId == doctor_id && p.analiz_pic == true)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.AuthorizationId == doctor_id && p.analiz_pic == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDoctorAnalizFalse")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDoctorAnalizFalse([FromQuery] int page, [FromQuery] int size, [FromQuery] long doctor_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.AuthorizationId == doctor_id && p.analiz_pic == false)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.AuthorizationId == doctor_id && p.analiz_pic == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientAllAnalizTrue")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientAllAnalizTrue([FromQuery] int page, [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.PatientsId == patient_id && p.analiz_pic == true)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.PatientsId == patient_id && p.analiz_pic == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientAllAnalizFalse")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientAllAnalizFalse([FromQuery] int page, [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalFileInfo> itemList = await _context.HospitalFileInfo
                .OrderByDescending(p => p.id)
                .Include(p => p.patients)
                .Include(p => p.authorization)
                .Where(p => p.PatientsId == patient_id && p.analiz_pic == false)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalFileInfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalFileInfo
                .Where(p => p.PatientsId == patient_id && p.analiz_pic == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalFileInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalFileInfo>>> GetHospitalFileInfo()
        {
            return await _context.HospitalFileInfo.ToListAsync();
        }

        // GET: api/HospitalFileInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalFileInfo>> GetHospitalFileInfo(long id)
        {
            var hospitalFileInfo = await _context.HospitalFileInfo.FindAsync(id);

            if (hospitalFileInfo == null)
            {
                return NotFound();
            }

            return hospitalFileInfo;
        }

        // PUT: api/HospitalFileInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalFileInfo(long id, HospitalFileInfo hospitalFileInfo)
        {
            if (id != hospitalFileInfo.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalFileInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalFileInfoExists(id))
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

        // POST: api/HospitalFileInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalFileInfo>> PostHospitalFileInfo(HospitalFileInfo hospitalFileInfo)
        {
            _context.HospitalFileInfo.Update(hospitalFileInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalFileInfo", new { id = hospitalFileInfo.id }, hospitalFileInfo);
        }

        // DELETE: api/HospitalFileInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalFileInfo>> DeleteHospitalFileInfo(long id)
        {
            var hospitalFileInfo = await _context.HospitalFileInfo.FindAsync(id);
            if (hospitalFileInfo == null)
            {
                return NotFound();
            }

            _context.HospitalFileInfo.Remove(hospitalFileInfo);
            await _context.SaveChangesAsync();

            return hospitalFileInfo;
        }

        private bool HospitalFileInfoExists(long id)
        {
            return _context.HospitalFileInfo.Any(e => e.id == id);
        }
    }
}
