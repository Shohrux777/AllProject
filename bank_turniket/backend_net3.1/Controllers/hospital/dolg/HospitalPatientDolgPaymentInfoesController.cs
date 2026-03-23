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
using Newtonsoft.Json.Linq;
using ApiAll.Model;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ApiAll.Controllers.hospital.dolg
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPatientDolgPaymentInfoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalPatientDolgPaymentInfoesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalPatientDolgPaymentInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalPatientDolgPaymentInfo>>> GetHospitalPatientDolgPaymentInfo()
        {
            return await _context.HospitalPatientDolgPaymentInfo.ToListAsync();
        }

        [HttpGet("getPaginationQarzdorlik")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationQarzdorlik([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgTemp> itemList = await _context.HospitalPatientDolgTemp
                .FromSqlRaw("SELECT" +
                " \r\npp.\"Id\" as patient_id, " +
                " \r\npp.\"FIO\" as fio," +
                " \r\npp.\"PhoneNumber\" as phone," +
                " \r\nsum(p.dolg_summ) as dolg" +
                " \r\nFROM public.hospital_dolg_inv_payment_info hdi" +
                " \r\nLEFT JOIN payments p ON p.\"Id\" = hdi.\"PaymentsId\"" +
                " \r\nLEFT JOIN \"Patients\" pp ON pp.\"Id\" = hdi.\"PatientsId\"" +
                " \r\nWHERE p.dolg_summ > 0" +
                " \r\nGROUP BY  pp.\"FIO\",pp.\"PhoneNumber\",pp.\"Id\"" +
                " \r\nORDER BY pp.\"FIO\"")
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgTemp>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientIdQarzdorlikTolash")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientIdQarzdorlikTolash([FromQuery] int page,
    [FromQuery] int size, [FromQuery] long patient_id,long cash, long card, long payed_auth_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgPaymentInfo> itemList = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.PatientsId == patient_id && p.payments.dolg_summ > 0)
                .Include(p => p.patients)
                .Include(p => p.payments)
                .ThenInclude(p => p.serviceType)
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgPaymentInfo>();
            }
            List<Payments> payments = new List<Payments>();
            long all_dolg_sum = 0;
            foreach (HospitalPatientDolgPaymentInfo it in itemList) {
                all_dolg_sum = (long)(all_dolg_sum + it.payments.dolg_summ);
            }
            List<Payments> payments_list = new List<Payments>();

                foreach (HospitalPatientDolgPaymentInfo it in itemList)
                {

  

                    Payments n_payment = new Payments();
                    n_payment.Name = it.payments.Name;
                    n_payment.ServiceName = it.payments.ServiceName;
                    n_payment.PatientName = it.payments.PatientName;
                    n_payment.ServiceTypeId = it.payments.ServiceTypeId;
                    n_payment.Summ = (long)it.payments.dolg_summ;
                    n_payment.ReserveSumm = it.payments.ReserveSumm;
                    n_payment.PaymentInCash = 0;
                    n_payment.PaymentInCard = 0;

                if (card > 0)
                {
                    //naqtda tolashdi xal qildik
                    if (card >= it.payments.dolg_summ)
                    {
                        n_payment.PaymentInCard = (long)it.payments.dolg_summ;
                        card = (long)(card - it.payments.dolg_summ);
                        it.payments.dolg_summ = 0;
                    }
                    else {
                        n_payment.PaymentInCard = card;
                        it.payments.dolg_summ = it.payments.dolg_summ - card; ;
                        card = 0;

                    }

                }

                if (cash > 0 && it.payments.dolg_summ > 0) {

                    if (cash >= it.payments.dolg_summ)
                    {
                        n_payment.PaymentInCash = (long)it.payments.dolg_summ;
                        cash = (long)(cash - it.payments.dolg_summ);
                        it.payments.dolg_summ = 0;
                    }
                    else
                    {
                        n_payment.PaymentInCash = cash;
                        it.payments.dolg_summ = it.payments.dolg_summ - cash; ;
                        cash = 0;

                    }

                }

                    n_payment.payed_auth_id = payed_auth_id;
                    n_payment.PatientsId = it.payments.PatientsId;
                    n_payment.Finish = true;
                    n_payment.AuthorizationId = it.payments.AuthorizationId;
                    n_payment.ContragentId = it.payments.ContragentId;
                    n_payment.RegistratedDate = DateTime.Now;
                    n_payment.FinishPayment = true;
                    n_payment.acceptanceDateTime = DateTime.Now;
                    n_payment.PayedDate = DateTime.Now;
                    n_payment.creatorAuthId = it.payments.creatorAuthId;
                    n_payment.discount_qty= it.payments.discount_qty;
                    n_payment.discount_card_qty = it.payments.discount_card_qty;
                    n_payment.discount_real_qty  = 0.0;
                    n_payment.discount_persantage_qty = 0.0;
                    n_payment.dolg_summ = 0;
                    n_payment.dolg_status = true;

                if (n_payment.PaymentInCard > 0 || n_payment.PaymentInCash > 0) {
                    payments_list.Add(n_payment);

                }

                    payments_list.Add(it.payments);
                }




            _context.payments.UpdateRange(payments_list);
            await _context.SaveChangesAsync();

            //update payments


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgPaymentInfo> itemList = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true)
                .Include(p => p.patients)
                .Include(p => p.payments)
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgPaymentInfo>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByPatientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientId([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgPaymentInfo> itemList = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.PatientsId == patient_id && p.payments.dolg_summ > 0)
                .Include(p => p.patients)
                .Include(p => p.payments)
                .ThenInclude(p=>p.serviceType)
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgPaymentInfo>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.PatientsId == patient_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByCreatorId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByCreatorId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] long creator_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgPaymentInfo> itemList = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.creatorAuthId == creator_id)
                .Include(p => p.patients)
                .Include(p => p.payments)
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgPaymentInfo>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.creatorAuthId == creator_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // Get: api doctor id buyicha list olish
        [HttpGet("getPaginationByAuthId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByAuthId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] long auth_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalPatientDolgPaymentInfo> itemList = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.payments.AuthorizationId == auth_id && p.payments.dolg_summ > 0)
                .Include(p => p.patients)
                .Include(p => p.payments)
                .OrderByDescending(p => p.Id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalPatientDolgPaymentInfo>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalPatientDolgPaymentInfo
                .Where(p => p.ActiveStatus == true && p.payments.AuthorizationId == auth_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalPatientDolgPaymentInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalPatientDolgPaymentInfo>> GetHospitalPatientDolgPaymentInfo(long id)
        {
            var hospitalPatientDolgPaymentInfo = await _context.HospitalPatientDolgPaymentInfo.FindAsync(id);

            if (hospitalPatientDolgPaymentInfo == null)
            {
                return NotFound();
            }

            return hospitalPatientDolgPaymentInfo;
        }

        // PUT: api/HospitalPatientDolgPaymentInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalPatientDolgPaymentInfo(long id, HospitalPatientDolgPaymentInfo hospitalPatientDolgPaymentInfo)
        {
            if (id != hospitalPatientDolgPaymentInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalPatientDolgPaymentInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalPatientDolgPaymentInfoExists(id))
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

        // POST: api/HospitalPatientDolgPaymentInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalPatientDolgPaymentInfo>> PostHospitalPatientDolgPaymentInfo(HospitalPatientDolgPaymentInfo hospitalPatientDolgPaymentInfo)
        {
            _context.HospitalPatientDolgPaymentInfo.Update(hospitalPatientDolgPaymentInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalPatientDolgPaymentInfo", new { id = hospitalPatientDolgPaymentInfo.Id }, hospitalPatientDolgPaymentInfo);
        }

        // DELETE: api/HospitalPatientDolgPaymentInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalPatientDolgPaymentInfo>> DeleteHospitalPatientDolgPaymentInfo(long id)
        {
            var hospitalPatientDolgPaymentInfo = await _context.HospitalPatientDolgPaymentInfo.FindAsync(id);
            if (hospitalPatientDolgPaymentInfo == null)
            {
                return NotFound();
            }

            _context.HospitalPatientDolgPaymentInfo.Remove(hospitalPatientDolgPaymentInfo);
            await _context.SaveChangesAsync();

            return hospitalPatientDolgPaymentInfo;
        }

        private bool HospitalPatientDolgPaymentInfoExists(long id)
        {
            return _context.HospitalPatientDolgPaymentInfo.Any(e => e.Id == id);
        }
    }
}
