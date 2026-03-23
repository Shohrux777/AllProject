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
    public class HospitalBronRoomPaymentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalBronRoomPaymentsController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet("getPaginationByPatientIdYangi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByPatientIdYangi([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long patient_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.PatientsId == patient_id)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationVozvratQilganlar")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationVozvratQilganlar([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.reserved_number_db_5 > 0.0)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.reserved_number_db_5 > 0.0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiOtganlarBugungiSanaBoyichaTolovTugamaganOxirigacha")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiOtganlarBugungiSanaBoyichaTolovTugamaganOxirigacha([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron.Date <= DateTime.Now.Date && p.finish_payment == false)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron.Date <= DateTime.Now.Date && p.finish_payment == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiOtganlarBugungiSanaBoyicha")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiOtganlarBugungiSanaBoyicha([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron.Date < DateTime.Now.Date)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron.Date < DateTime.Now.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiOtganlarBugungiSanaVaVaqtBoyicha")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiOtganlarBugungiSanaVaVaqtBoyicha([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron < DateTime.Now)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron < DateTime.Now).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiBugungilarLekinTolamaganlar")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiBugungilarLekinTolamaganlar([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron.Date == DateTime.Now.Date && p.finish_payment == false)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron.Date == DateTime.Now.Date && p.finish_payment == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiBugungilar")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiBugungilar([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron.Date == DateTime.Now.Date)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron.Date == DateTime.Now.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSrogiBerilganVaqtBoyichaSoatsiz")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSrogiBerilganVaqtBoyichaSoatsis([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime sana)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Where(p => p.end_date_bron.Date <= sana.Date)
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .OrderByDescending(p => p.end_date_bron)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.end_date_bron.Date <= sana.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOnlyFinishTrue")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyFinishTrue([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.finish_payment == true)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.finish_payment == true).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationPulTolashTarixiYangi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationPulTolashTarixiYangi([FromQuery] int page, [FromQuery] int size,
    [FromQuery] DateTime beginDate, [FromQuery] DateTime endDate)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String beginDateStr = beginDate.ToString("yyyy-MM-dd");
            String endDateStr = endDate.ToString("yyyy-MM-dd");
            List<HospitalBronRoomPaymentsDetailTemp> itemList = await _context.HospitalBronRoomPaymentsDetailTemp
                .FromSqlRaw("SELECT " +
                " \r\nbrmd.\"HospitalBronRoomPaymentsid\" as id_payment, " +
                " \r\nsum(card_summ) as card_sum , " +
                " \r\nsum(vozvrat_sum) as vozvrat_sum , " +
                " \r\nsum(cash_summ) as cash_sum" +
                " \r\nFROM public.\"HospitalBronRoomPaymentsDetail\" as brmd" +
                " \r\nWHERE brmd.reg_date_time::date >= '"+ beginDateStr + "' AND brmd.reg_date_time::date < '"+ endDateStr + "'" +
                " \r\nGROUP BY id_payment ORDER BY id_payment DESC")
                .OrderByDescending(p => p.id_payment)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsDetailTemp>();
            }

            foreach (HospitalBronRoomPaymentsDetailTemp it in itemList) {
                it.hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(it.id_payment);

            }

            foreach (HospitalBronRoomPaymentsDetailTemp itm in itemList) {
                if (itm.price_for_one_day > 0.0) {
                    itm.ovqat_puli = Math.Round((itm.reserved_number_2 * (Convert.ToDouble(itm.card_sum) + Convert.ToDouble(itm.cash_sum) - Convert.ToDouble(itm.vozvrat_sum)) / itm.price_for_one_day),4);
                    itm.qolgan_pul = Math.Round(((Convert.ToDouble(itm.card_sum) + Convert.ToDouble(itm.cash_sum) - Convert.ToDouble(itm.vozvrat_sum)) - itm.ovqat_puli),4);
                }
                if(itm.vozvrat_sum > 0) {
                    itm.cash_sum = itm.cash_sum - itm.vozvrat_sum;
                }
                
                
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationPulTolashTarixi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationPulTolashTarixi([FromQuery] int page, [FromQuery] int size,
            [FromQuery] DateTime beginDate, [FromQuery] DateTime endDate)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsDetail> itemList = await _context.HospitalBronRoomPaymentsDetail
                .Include(p => p.hospitalBronRoomPayments)
                .Where(p => p.reg_date_time.Date >= beginDate.Date && p.reg_date_time.Date <= endDate.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsDetail>();
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsDetail
                .Where(p => p.reg_date_time.Date >= beginDate.Date && p.reg_date_time.Date <= endDate.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationPulTolashTarixiVozvrat")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationPulTolashTarixiVozvrat([FromQuery] int page, [FromQuery] int size,
     [FromQuery] DateTime beginDate, [FromQuery] DateTime endDate)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsDetail> itemList = await _context.HospitalBronRoomPaymentsDetail
                .Include(p => p.hospitalBronRoomPayments)
                .Where(p => p.reg_date_time.Date >= beginDate.Date && p.reg_date_time.Date <= endDate.Date && p.vozvrat_sum > 0.0)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsDetail>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsDetail.Where(p => p.reg_date_time.Date >= beginDate.Date && p.reg_date_time.Date <= endDate.Date && p.vozvrat_sum > 0.0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOnlyFinishFalse")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyFinishFalse([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.finish_payment == false)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments.Where(p => p.finish_payment == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationKravatgaYotganOdamlartarixi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationKravatgaYotganOdamlartarixi([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long hospital_room_bron_item_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.HospitalBronRoomItemNid == hospital_room_bron_item_id)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments
                .Where(p => p.HospitalBronRoomItemNid == hospital_room_bron_item_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationKravatBoyichaQarzdorliklar")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationKravatBoyichaQarzdorliklar([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long hospital_room_bron_item_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.HospitalBronRoomItemNid == hospital_room_bron_item_id 
                && p.finish_payment == false)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments
                .Where(p => p.HospitalBronRoomItemNid == hospital_room_bron_item_id && p.finish_payment == false).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationNeedpayedSummNotZero")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationNeedpayedSummNotZero([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPayments> itemList = await _context.HospitalBronRoomPayments
                .Include(p => p.patients)
                .Include(p => p.hospitalBronRoom)
                .Include(p => p.hospitalBronRoomItemN)
                .Include(p => p.users)
                .Include(p => p.serviceType)
                .Where(p => p.finish_payment == false && p.need_payed_summ > 0)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPayments>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPayments
                .Where(p => p.finish_payment == false && p.need_payed_summ > 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalBronRoomPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalBronRoomPayments>>> GetHospitalBronRoomPayments()
        {
            return await _context.HospitalBronRoomPayments.ToListAsync();
        }

        // GET: api/HospitalBronRoomPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalBronRoomPayments>> GetHospitalBronRoomPayments(long id)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(id);

            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }

            return hospitalBronRoomPayments;
        }

        [HttpGet("finishPayment")]
        public async Task<ActionResult<HospitalBronRoomPayments>> finishPayment([FromQuery]long id)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(id);

            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }
            hospitalBronRoomPayments.finish_payment = true;
            _context.HospitalBronRoomPayments.Update(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();
            return hospitalBronRoomPayments;
        }

        [HttpGet("addPayment")]
        public async Task<ActionResult<HospitalBronRoomPayments>> addPayment(
            [FromQuery] long id, [FromQuery] double cash, [FromQuery] double card, [FromQuery] double auth_id)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(id);

            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }
            if (hospitalBronRoomPayments.need_payed_summ == (card+cash)) {
                hospitalBronRoomPayments.finish_payment = false;
            }
            hospitalBronRoomPayments.reserved_number_db_2 = auth_id;  // tulov qilgan user
            hospitalBronRoomPayments.payed_summ = hospitalBronRoomPayments.payed_summ + cash + card;
            hospitalBronRoomPayments.need_payed_summ = hospitalBronRoomPayments.need_payed_summ - (cash + card);
            hospitalBronRoomPayments.card_summ = hospitalBronRoomPayments.card_summ +  card;
            hospitalBronRoomPayments.cash_summ = hospitalBronRoomPayments.cash_summ + cash;

            //payment yaratib yuboriladi
            HospitalBronRoomPaymentsDetail paymentsDetail = new HospitalBronRoomPaymentsDetail();
            paymentsDetail.discount_payed_summ = auth_id;
            paymentsDetail.card_summ = card;
            paymentsDetail.cash_summ = cash;
            paymentsDetail.HospitalBronRoomPaymentsid = hospitalBronRoomPayments.id;
            paymentsDetail.need_payed_summ = hospitalBronRoomPayments.need_payed_summ;

            _context.HospitalBronRoomPaymentsDetail.Update(paymentsDetail);
            await _context.SaveChangesAsync();
            // pul update qilindi

            _context.HospitalBronRoomPayments.Update(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();
            return hospitalBronRoomPayments;
        }
        [HttpGet("addPaymentVozvrat")]
        public async Task<ActionResult<HospitalBronRoomPayments>> addPaymentVozvrat(
    [FromQuery] long payment_id, [FromQuery] double vozvrat_sum, [FromQuery] long days, [FromQuery] long pay_auth)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(payment_id);

            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }
            hospitalBronRoomPayments.reserved_number_db_5 = vozvrat_sum;

            //vozvrat tablitsa yaratildi
            HospitalBronRoomPaymentsVozvrat paymentsVozvrat = new HospitalBronRoomPaymentsVozvrat();
            paymentsVozvrat.days = days;
            paymentsVozvrat.vozvrat_sum = vozvrat_sum;
            paymentsVozvrat.HospitalBronRoomPaymentsid = payment_id;
            paymentsVozvrat.reserved_number_1 = pay_auth;


            _context.HospitalBronRoomPaymentsVozvrat.Update(paymentsVozvrat);
            await _context.SaveChangesAsync();

            //payment yaratib yuboriladi
            HospitalBronRoomPaymentsDetail paymentsDetail = new HospitalBronRoomPaymentsDetail();
            paymentsDetail.card_summ = 0.0;
            paymentsDetail.cash_summ = 0.0;
            paymentsDetail.HospitalBronRoomPaymentsid = hospitalBronRoomPayments.id;
            paymentsDetail.need_payed_summ = hospitalBronRoomPayments.need_payed_summ;
            paymentsDetail.vozvrat_sum = vozvrat_sum;
            paymentsDetail.AuthorizationId = pay_auth;
            paymentsDetail.discount_payed_summ = pay_auth;

            _context.HospitalBronRoomPaymentsDetail.Update(paymentsDetail);
            await _context.SaveChangesAsync();
            // pul update qilindi

            _context.HospitalBronRoomPayments.Update(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();
            return hospitalBronRoomPayments;
        }

        [HttpGet("addPaymentVozvratWithSababi")]
        public async Task<ActionResult<HospitalBronRoomPayments>> addPaymentVozvratWithSababi(
[FromQuery] long payment_id, [FromQuery] double vozvrat_sum, [FromQuery] long days, [FromQuery] String note)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(payment_id);

            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }
            hospitalBronRoomPayments.reserved_number_db_5 = vozvrat_sum;

            //vozvrat tablitsa yaratildi
            HospitalBronRoomPaymentsVozvrat paymentsVozvrat = new HospitalBronRoomPaymentsVozvrat();
            paymentsVozvrat.days = days;
            paymentsVozvrat.vozvrat_sum = vozvrat_sum;
            paymentsVozvrat.HospitalBronRoomPaymentsid = payment_id;
            paymentsVozvrat.reserved_note_1 = note;

            _context.HospitalBronRoomPaymentsVozvrat.Update(paymentsVozvrat);
            await _context.SaveChangesAsync();

            //payment yaratib yuboriladi
            HospitalBronRoomPaymentsDetail paymentsDetail = new HospitalBronRoomPaymentsDetail();
            paymentsDetail.card_summ = 0.0;
            paymentsDetail.cash_summ = 0.0;
            paymentsDetail.HospitalBronRoomPaymentsid = hospitalBronRoomPayments.id;
            paymentsDetail.need_payed_summ = hospitalBronRoomPayments.need_payed_summ;
            paymentsDetail.vozvrat_sum = vozvrat_sum;

            _context.HospitalBronRoomPaymentsDetail.Update(paymentsDetail);
            await _context.SaveChangesAsync();
            // pul update qilindi

            _context.HospitalBronRoomPayments.Update(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();
            return hospitalBronRoomPayments;
        }
        // PUT: api/HospitalBronRoomPayments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalBronRoomPayments(long id, HospitalBronRoomPayments hospitalBronRoomPayments)
        {
            if (id != hospitalBronRoomPayments.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalBronRoomPayments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalBronRoomPaymentsExists(id))
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

        // POST: api/HospitalBronRoomPayments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalBronRoomPayments>> PostHospitalBronRoomPayments(HospitalBronRoomPayments hospitalBronRoomPayments)
        {
            _context.HospitalBronRoomPayments.Update(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalBronRoomPayments", new { id = hospitalBronRoomPayments.id }, hospitalBronRoomPayments);
        }

        // DELETE: api/HospitalBronRoomPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalBronRoomPayments>> DeleteHospitalBronRoomPayments(long id)
        {
            var hospitalBronRoomPayments = await _context.HospitalBronRoomPayments.FindAsync(id);
            if (hospitalBronRoomPayments == null)
            {
                return NotFound();
            }

            _context.HospitalBronRoomPayments.Remove(hospitalBronRoomPayments);
            await _context.SaveChangesAsync();

            return hospitalBronRoomPayments;
        }

        private bool HospitalBronRoomPaymentsExists(long id)
        {
            return _context.HospitalBronRoomPayments.Any(e => e.id == id);
        }
    }
}
