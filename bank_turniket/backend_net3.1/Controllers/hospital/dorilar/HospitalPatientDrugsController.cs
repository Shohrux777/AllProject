using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital;
using ApiAll.Model.pos;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.dorilar
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPatientDrugsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalPatientDrugsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalPatientDrugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalPatientDrugs>>> GetHospitalPatientDrugs()
        {
            return await _context.HospitalPatientDrugs.ToListAsync();
        }

        [HttpGet("doriOlmaganBemorlarRoyixati")]
        public async Task<ActionResult<IEnumerable<HospitalPatientSearch>>> doriOlmaganBemorlarRoyixati()
        {
            return await _context.HospitalPatientSearch.FromSqlRaw("SELECT hpd.\"PatientsId\"  as bemor_id,\r\np. \"FIO\" as bemor_fio\r\nFROM hospital_patient_drugs hpd \r\nLEFT JOIN \"Patients\" p ON p.\"Id\" = hpd.\"PatientsId\" \r\nWHERE hpd.get_drugs_status = false\r\nGROUP BY hpd.\"PatientsId\",p. \"FIO\"").ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true )
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p =>p.authorization)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }     
        
        [HttpGet("getPaginationByDateTime")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDateTime([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true &&(p.created_date_time >= begin_date && p.created_date_time <= end_date))
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p =>p.authorization)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByDateTimeWithPatientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDateTimeWithPatientId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] DateTime begin_date,
    [FromQuery] DateTime end_date, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true
                && (p.created_date_time >= begin_date && p.created_date_time <= end_date)
                && p.PatientsId == patient_id)
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) && p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationWithPatientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWithPatientId([FromQuery] int page,
[FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true
                && p.PatientsId == patient_id)
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true  && p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationWithPatientIdNotGetDrugsList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWithPatientIdNotGetDrugsList([FromQuery] int page,
[FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true
                && p.PatientsId == patient_id && p.get_drugs_status == false)
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true && p.PatientsId == patient_id && p.get_drugs_status == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAlreadyGetDrugsTrue")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAlreadyGetDrugsTrue([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .Where(p => p.get_drugs_status == true)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true).Where(p => p.get_drugs_status == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAlreadyGetDrugsFalse")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAlreadyGetDrugsFalse([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .Where(p => p.get_drugs_status == false)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true).Where(p => p.get_drugs_status == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAlreadyGetDrugsFalseByPatientAndAuthId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAlreadyGetDrugsFalseByPatientAndAuthId([FromQuery] int page, 
            [FromQuery] int size, [FromQuery] long patient_id, [FromQuery] long auth_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .Where(p => p.get_drugs_status == false && (p.PatientsId == patient_id && p.AuthorizationId == auth_id))
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true).Where(p => p.get_drugs_status == false && (p.PatientsId == patient_id && p.AuthorizationId == auth_id)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAlreadyGetDrugsTrueByPatientAndAuthId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAlreadyGetDrugsTrueByPatientAndAuthId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] long patient_id, [FromQuery] long auth_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDrugs> itemList = await _context.HospitalPatientDrugs
                .Include(p => p.patients)
                .Include(p => p.posProduct)
                .Include(p => p.authorization)
                .Where(p => p.get_drugs_status == true && (p.PatientsId == patient_id && p.AuthorizationId == auth_id))
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDrugs>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDrugs
                .Where(p => p.active_status == true).Where(p => p.get_drugs_status == true && (p.PatientsId == patient_id && p.AuthorizationId == auth_id)).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalPatientDrugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalPatientDrugs>> GetHospitalPatientDrugs(long id)
        {
            var hospitalPatientDrugs = await _context.HospitalPatientDrugs.FindAsync(id);

            if (hospitalPatientDrugs == null)
            {
                return NotFound();
            }

            return hospitalPatientDrugs;
        }

        [HttpGet("getDrugsStatusSetTrue")]
        public async Task<ActionResult<HospitalPatientDrugs>> getDrugsStatusSetTrue([FromQuery] long patient_drugs_id)
        {
            var hospitalPatientDrugs = await _context.HospitalPatientDrugs.FindAsync(patient_drugs_id);

            if (hospitalPatientDrugs == null)
            {
                return NotFound();
            }
            hospitalPatientDrugs.get_drugs_status= true;
            _context.HospitalPatientDrugs.Update(hospitalPatientDrugs);
            await _context.SaveChangesAsync();

            return hospitalPatientDrugs;
        }

        [HttpGet("getDrugsStatusSetTrueBypatientAndAuthId")]
        public async Task<ActionResult<IEnumerable<HospitalPatientDrugs>>> getDrugsStatusSetTrueBypatientAndAuthId([FromQuery] long patient_id, [FromQuery] long auth_id)
        {
            var hospitalPatientDrugs = await _context.HospitalPatientDrugs.Where(p => p.PatientsId == patient_id && p.AuthorizationId == auth_id).ToListAsync();

            if (hospitalPatientDrugs == null)
            {
                return NotFound();
            }

            foreach (HospitalPatientDrugs it in hospitalPatientDrugs) {
                it.get_drugs_status = true;
            }
        
            _context.HospitalPatientDrugs.UpdateRange(hospitalPatientDrugs);
            await _context.SaveChangesAsync();

            return hospitalPatientDrugs;
        }

        [HttpGet("getDrugsStatusSetFalse")]
        public async Task<ActionResult<HospitalPatientDrugs>> getDrugsStatusSetFalse([FromQuery] long patient_drugs_id)
        {
            var hospitalPatientDrugs = await _context.HospitalPatientDrugs.FindAsync(patient_drugs_id);

            if (hospitalPatientDrugs == null)
            {
                return NotFound();
            }
            hospitalPatientDrugs.get_drugs_status = false;
            _context.HospitalPatientDrugs.Update(hospitalPatientDrugs);
            await _context.SaveChangesAsync();

            return hospitalPatientDrugs;
        }

        // PUT: api/HospitalPatientDrugs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalPatientDrugs(long id, HospitalPatientDrugs hospitalPatientDrugs)
        {
            if (id != hospitalPatientDrugs.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalPatientDrugs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalPatientDrugsExists(id))
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

        // POST: api/HospitalPatientDrugs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalPatientDrugs>> PostHospitalPatientDrugs(HospitalPatientDrugs hospitalPatientDrugs)
        {
            _context.HospitalPatientDrugs.Update(hospitalPatientDrugs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalPatientDrugs", new { id = hospitalPatientDrugs.id }, hospitalPatientDrugs);
        }

        [HttpPost("addPostListDrugs")]
        public async Task<ActionResult<IEnumerable<HospitalPatientDrugs>>> addPostListDrugs(List<HospitalPatientDrugs> hospitalPatientDrugsList)
        {
            _context.HospitalPatientDrugs.UpdateRange(hospitalPatientDrugsList);
            await _context.SaveChangesAsync();

            return hospitalPatientDrugsList;
        }

        [HttpPost("addPostListDrugsSetStatusTrue")]
        public async Task<ActionResult<IEnumerable<long>>> addPostListDrugsSetStatusTrue(List<long> id_list)
        {
            List<HospitalPatientDrugs> hospitalPatientDrugsList = new List<HospitalPatientDrugs>();
            foreach (long it in id_list) {

                var hospitalPatientDrugs = await _context.HospitalPatientDrugs.FindAsync(it);
                if (hospitalPatientDrugs != null)
                {
                    hospitalPatientDrugs.get_drugs_status = true;
                    hospitalPatientDrugsList.Add(hospitalPatientDrugs);
                }

            }


            _context.HospitalPatientDrugs.UpdateRange(hospitalPatientDrugsList);
            await _context.SaveChangesAsync();

            return id_list;
        }

        // DELETE: api/HospitalPatientDrugs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalPatientDrugs>> DeleteHospitalPatientDrugs(long id)
        {
            var hospitalPatientDrugs = await _context.HospitalPatientDrugs.FindAsync(id);
            if (hospitalPatientDrugs == null)
            {
                return NotFound();
            }

            _context.HospitalPatientDrugs.Remove(hospitalPatientDrugs);
            await _context.SaveChangesAsync();

            return hospitalPatientDrugs;
        }

        private bool HospitalPatientDrugsExists(long id)
        {
            return _context.HospitalPatientDrugs.Any(e => e.id == id);
        }
    }
}
