using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonInvoiceController : ControllerBase
    {
        private readonly ApplicationContext _context;

        //INVOICE TURLARI
        private String INVOICE_BUGDOY_SOTISH_PULGA = "INVOICE_BUGDOY_SOTISH_PULGA";
        private String INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH = "INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH";
        private String INVOICE_BUGDOY_PRIXOD_QILISH = "INVOICE_BUGDOY_PRIXOD_QILISH";
        private String INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR = "INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR";
        private String INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH";
        private String INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH";
        private String INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH";

        public TegirmonInvoiceController(ApplicationContext context)
        {
            _context = context;
        }


        /// <summary>
        /// BUGDOYNI PULGA ALMASHTRISH BOLDI 
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>

        [HttpPost("postInvoiceBugdoyPulgaAlmashibQoyish")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyPulgaAlmashibQoyish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_SOTISH_PULGA;
            if (tegirmonInvoice.TegirmonProductid != null) {
                List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty + tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty + tegirmonInvoice.qty_real;

                    _context.TegirmonOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else {
                    TegirmonOstatka ostatka = new TegirmonOstatka();
                    ostatka.active_status = true;
                    ostatka.qty = tegirmonInvoice.qty_real;
                    ostatka.real_qty = tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);
                    _context.TegirmonOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            }

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        [HttpPost("addChangeBugdoyToMoney")]
        public async Task<ActionResult<TegirmonInvoice>> addChangeBugdoyToMoney(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_SOTISH_PULGA;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        [HttpGet("acceptChangeBugdoyToMoney")]
        public async Task<ActionResult<TegirmonInvoice>> acceptChangeBugdoyToMoney([FromQuery]long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
               .Include(p => p.item_list)
               .Where(p => p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyPulgaAlmashibQoyish(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }





        /// <summary>
        /// Zahiraga olib qoyish klientni bugdoyini
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>

        [HttpPost("postInvoiceBugdoyniZaxiragaOlibQolishClientdan")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyniZaxiragaOlibQolishClientdan(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH;
            if (tegirmonInvoice.TegirmonClientid == null || tegirmonInvoice.TegirmonProductid == null) {

                return NotFound("Client or product not selected");
            }

            
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                // ostatkaga pul berilmagan bulsa aytiladi obshey ostatkadan
                if((tegirmonInvoice.poluchit_summa == 0 || tegirmonInvoice.poluchit_summa == null) 
                    && (tegirmonInvoice.pol_dollor_summa == 0 || tegirmonInvoice.pol_dollor_summa == null)){
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                        .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                        .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty + tegirmonInvoice.qty_real;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty + tegirmonInvoice.qty_real;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty = tegirmonInvoice.qty_real;
                        ostatka.real_qty = tegirmonInvoice.qty_real;
                        ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);
                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }
                }
                // pul bergan holati uchun takshiramiz
                else if((tegirmonInvoice.poluchit_summa > 0 || tegirmonInvoice.pol_dollor_summa > 0) && tegirmonInvoice.hisob_id == 0){
                    if(tegirmonInvoice.kassa_id != 0){
                        var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                        .Where(p =>p.TegirmonKassaid == tegirmonInvoice.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                        
                        if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                            // bu bugungi kunga dalni qushilmay qolgan bulsa
                            var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                            .Where(p =>p.TegirmonKassaid == tegirmonInvoice.kassa_id)
                            .OrderByDescending(p => p.created_date_time) 
                            .FirstOrDefaultAsync();

                            if(tegirmonKassaInfoLast == null){
                                // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                                TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                tegirmon_kassa_info.cash = 0;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonInvoice.poluchit_summa;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonInvoice.pol_dollor_summa;
                                // tegirmon_kassa_info.id = 0;
                                tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                                tegirmon_kassa_info.before_cash = 0;
                                tegirmon_kassa_info.before_dollor = 0;
                                _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();
                            }
                            else{
                                // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                                TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                                tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonInvoice.poluchit_summa;
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonInvoice.pol_dollor_summa;
                                tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                                tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                                _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();
                            }
                        }
                        else{
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonInvoice.poluchit_summa;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonInvoice.pol_dollor_summa;
                            _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                // hisob raqamdan tashlagan holat uchun tekshiramiz
                else if((tegirmonInvoice.poluchit_summa > 0 || tegirmonInvoice.pol_dollor_summa > 0) && tegirmonInvoice.hisob_id>0){
                    TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                    tegirmon_hisob_prixod.status_rasxod = 1;
                    tegirmon_hisob_prixod.worker_name = "Получит продукт";
                    tegirmon_hisob_prixod.phone_number = tegirmonInvoice.image_str_url;
                    tegirmon_hisob_prixod.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid;
                    tegirmon_hisob_prixod.note = tegirmonInvoice.note;
                    tegirmon_hisob_prixod.addiotionala_information = tegirmonInvoice.auth_user_name;// bunga kassirni nomini aniqlab yozish kerak;
                    tegirmon_hisob_prixod.prixod = (double)tegirmonInvoice.poluchit_summa;
                    tegirmon_hisob_prixod.image_url = tegirmonInvoice.poluchit_summa_str;
                    tegirmon_hisob_prixod.all_summ = (double)tegirmonInvoice.pol_all_sum;
                    tegirmon_hisob_prixod.all_summ_string = tegirmonInvoice.pol_all_sum_str;
                    tegirmon_hisob_prixod.dollor = (double)tegirmonInvoice.pol_dollor_summa;
                    tegirmon_hisob_prixod.dollor_string = tegirmonInvoice.pol_dollor_sum_str;
                    tegirmon_hisob_prixod.kurs = (double)tegirmonInvoice.pol_kurs_sum;
                    tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonInvoice.hisob_id;

                    _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                    await _context.SaveChangesAsync();

                    if(tegirmonInvoice.hisob_id != 0){
                        var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                        .Where(p =>p.TegirmonHisoblarid == tegirmonInvoice.hisob_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                        
                        if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                            // bu bugungi kunga dalni qushilmay qolgan bulsa
                            var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                            .Where(p =>p.TegirmonHisoblarid == tegirmonInvoice.hisob_id)
                            .OrderByDescending(p => p.created_date_time)
                            .FirstOrDefaultAsync();

                            if(tegirmonKassaInfoLast == null){
                                // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                                TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                                tegirmon_kassa_info.cash = 0;
                                tegirmon_kassa_info.dollor = 0;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonInvoice.poluchit_summa;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonInvoice.pol_dollor_summa;
                                
                                // tegirmon_kassa_info.id = 0;
                                tegirmon_kassa_info.TegirmonHisoblarid = tegirmonInvoice.hisob_id;
                                tegirmon_kassa_info.before_cash = 0;
                                tegirmon_kassa_info.before_dollor = 0;
                                _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();
                            }
                            else{
                                // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                                TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                                tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonInvoice.pol_dollor_summa;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonInvoice.poluchit_summa;
                                
                                tegirmon_kassa_info.TegirmonHisoblarid = tegirmonInvoice.hisob_id;
                                tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                                tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                                _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                                await _context.SaveChangesAsync();

                            }
                        }
                        else{
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonInvoice.poluchit_summa;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonInvoice.pol_dollor_summa;

                            _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                            await _context.SaveChangesAsync();
                        }

                    
                        // return tegirmonKassaInfo.Last();
                    }

                }



                List<TegirmonClientOstatka> ostatkaClientList = await _context.TegirmonClientOstatka
                    .Where(p =>p.TegirmonClientid == tegirmonInvoice.TegirmonClientid
                    && p.TegirmonProductid == tegirmonInvoice.TegirmonProductid).ToListAsync();

                if (ostatkaClientList != null && ostatkaClientList.Count > 0) {
                    ostatkaClientList.First().real_qty = ostatkaClientList.First().real_qty + tegirmonInvoice.qty_real;
                    ostatkaClientList.First().qty = ostatkaClientList.First().qty + tegirmonInvoice.qty_real;
                    _context.TegirmonClientOstatka.UpdateRange(ostatkaClientList);
                    await _context.SaveChangesAsync();
                }
                else {

                    TegirmonClientOstatka clientOstatka = new TegirmonClientOstatka();
                    clientOstatka.active_status = true;
                    clientOstatka.qty = tegirmonInvoice.qty_real;
                    clientOstatka.real_qty = tegirmonInvoice.qty_real;
                    clientOstatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);
                    clientOstatka.TegirmonClientid = tegirmonInvoice.TegirmonClientid ?? default(long);
                    _context.TegirmonClientOstatka.Update(clientOstatka);
                    await _context.SaveChangesAsync();

                }


            }

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpPost("addBugdoyToClientForZaxira")]
        public async Task<ActionResult<TegirmonInvoice>> addBugdoyToClientForZaxira(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpGet("acceptBugdoyToClientForZaxira")]
        public async Task<ActionResult<TegirmonInvoice>> acceptBugdoyToClientForZaxira([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                 .Include(p => p.item_list)
                 .Where(p => p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyniZaxiragaOlibQolishClientdan(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }



        /// <summary>
        /// Bunda bugdoyni klintni registratsiya qilmasdan almasnhtrib yuborish uchun keak api
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>

        [HttpPost("deAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish")]
        public async Task<ActionResult<TegirmonInvoice>> deAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(TegirmonInvoice tegirmonInvoice)
        {

            if (tegirmonInvoice.inv_accepted_status != true) {
                return NotFound("Accept qilinmagan invoice ");
            }

            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH;
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty - tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty - tegirmonInvoice.qty_real;

                    _context.TegirmonOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TegirmonOstatka ostatka = new TegirmonOstatka();
                    ostatka.active_status = true;
                    ostatka.qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.real_qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);

                    _context.TegirmonOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            }



            //nima sotib olsa oshani ostatkadan ayrib qoyadi
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                   .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                   .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty + item.qty;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty + item.qty;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty =  item.qty;
                        ostatka.real_qty =  item.qty;
                        ostatka.TegirmonProductid = item.TegirmonProductid;

                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }

                }

            }

            tegirmonInvoice.inv_accepted_status = false;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        [HttpGet("getKassaCurrentPoluchitSumm")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitSumm([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.poluchit_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.poluchit_summa > 0 AND p.hisob_id = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentPoluchitDollor")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitDollor([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.pol_dollor_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.pol_dollor_summa > 0 AND p.hisob_id = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentPoluchitSummHisobId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitSummHisobId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.poluchit_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.poluchit_summa > 0 AND p.hisob_id > 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }


        [HttpGet("getKassaCurrentPoluchitSummKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitSummKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.poluchit_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.kassa_id = " + kassa_id + "AND p.poluchit_summa > 0 AND p.hisob_id = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentPoluchitDollorKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitDollorKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.pol_dollor_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.kassa_id = " + kassa_id + "AND p.pol_dollor_summa > 0 AND p.hisob_id = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentPoluchitSummHisobIdKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentPoluchitSummHisobIdKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.poluchit_summa) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.kassa_id = " + kassa_id + " AND p.poluchit_summa > 0 AND p.hisob_id > 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

          // sotib olingan productlarni summasini yigish

            [HttpGet("getKassaCurrentInvoiceSotibOlingan")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentInvoiceSotibOlingan([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH' ";
            String change_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.summ) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE (p.status_inv_type_name = " + zaxira_status + " OR p.status_inv_type_name = " + change_status +" ) " + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }


        [HttpGet("getKassaCurrentZaxiraInvoiceSummKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentZaxiraInvoiceSummKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.summ) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND p.kassa_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentChangeInvoiceSummKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentChangeInvoiceSummKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String change_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.summ) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + change_status + " AND p.kassa_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }




        [HttpGet("getKassaCurrentZaxiraInvoiceSumm")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentZaxiraInvoiceSumm([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String zaxira_status = " 'INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.summ) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + zaxira_status + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getKassaCurrentChangeInvoiceSumm")]
        public async Task<ActionResult<IEnumerable<TegirmonMoneyInvoiceTemp>>> getKassaCurrentChangeInvoiceSumm([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String change_status = " 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH' ";
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMoneyInvoiceTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.summ) as summ   " +
                "FROM public.tegirmon_invoice p " + 
                "WHERE p.status_inv_type_name = " + change_status + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }


        [HttpPost("deleteNoTestedAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish")]
        public async Task<ActionResult<TegirmonInvoice>> deleteNoTestedAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(TegirmonInvoice tegirmonInvoice)
        {

            if (tegirmonInvoice.inv_accepted_status != true)
            {
                return NotFound("Accept qilinmagan invoice ");
            }

            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH;
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty - tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty - tegirmonInvoice.qty_real;

                    _context.TegirmonOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TegirmonOstatka ostatka = new TegirmonOstatka();
                    ostatka.active_status = true;
                    ostatka.qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.real_qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);

                    _context.TegirmonOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            }

            //nima sotib olsa oshani ostatkadan ayrib qoyadi
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                   .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                   .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty + item.qty;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty + item.qty;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty = item.qty;
                        ostatka.real_qty = item.qty;
                        ostatka.TegirmonProductid = item.TegirmonProductid;

                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }

                }

            }

            tegirmonInvoice.inv_accepted_status = false;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }




        /// <summary>
        /// Bunda bugdoyni klintni registratsiya qilmasdan almasnhtrib yuborish uchun keak api
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>

        [HttpPost("postInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH;
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty + tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty + tegirmonInvoice.qty_real;

                    _context.TegirmonOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TegirmonOstatka ostatka = new TegirmonOstatka();
                    ostatka.active_status = true;
                    ostatka.qty =  tegirmonInvoice.qty_real;
                    ostatka.real_qty =  tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);

                    _context.TegirmonOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            }

            //nima sotib olsa oshani ostatkadan ayrib qoyadi
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                   .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                   .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty - item.qty;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty - item.qty;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty = -1 * item.qty;
                        ostatka.real_qty = -1 * item.qty;
                        ostatka.TegirmonProductid = item.TegirmonProductid;

                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }

                }

            }

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpPost("addChangeBugdoyProductsWithoutRegistartion")]
        public async Task<ActionResult<TegirmonInvoice>> addChangeBugdoyProductsWithoutRegistartion(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH;
            // Agar ortiqcha tulovi 0 bulsa lekin oldin tulov qilgan bulsa puldi joyiga qaytarib check_id ni 0 qilib quyamiz
            if(tegirmonInvoice.zaxira_extra_amount == 0  && tegirmonInvoice.kassa_check_id > 0)
            {
                var check = await _context.TegirmonCheck
                    .FirstOrDefaultAsync(x => x.id == tegirmonInvoice.kassa_check_id);
                

                if (check != null)
                {
                    // Topib olamiz yuqoridagi kassa uchun bugungi (yoki oxirgi) kassa info
                    var kassalar = await _context.TegirmonKassaInfo
                        .Where(k => k.TegirmonKassaid == check.auth_user_updator_id
                                    && k.created_date_time >= DateTime.Now.Date)
                        .ToListAsync();

                    if (kassalar != null && kassalar.Count > 0)
                    {
                        // agar bugungi kunga yozuv bo'lsa
                        var todayKassaInfo = kassalar.Last();
                        todayKassaInfo.cash -= check.cash;  
                        todayKassaInfo.dollor -= check.real_sum;  
                        _context.TegirmonKassaInfo.Update(todayKassaInfo);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // bugungi kunga yozuv bo‘lmasa, oxirgi yozuvni topib, undan kelib yangi yozuv qo‘shish
                        var lastKassaInfo = await _context.TegirmonKassaInfo
                            .Where(k => k.TegirmonKassaid == check.auth_user_updator_id)
                            .OrderByDescending(k => k.created_date_time)
                            .FirstOrDefaultAsync();

                        double beforeCash = lastKassaInfo != null ? lastKassaInfo.cash : 0;
                        double beforeDollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0;

                        var newKassaInfo = new TegirmonKassaInfo()
                        {
                            TegirmonKassaid = check.auth_user_updator_id,
                            before_cash = beforeCash,
                            before_dollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0,
                            cash = beforeCash - check.cash,
                            dollor = beforeDollor - check.real_sum,
                            created_date_time = DateTime.Now,
                            active_status = true
                        };
                        _context.TegirmonKassaInfo.Add(newKassaInfo);
                        await _context.SaveChangesAsync();
                    }


                    // O'chiramiz
                    _context.TegirmonCheck.Remove(check);
                    await _context.SaveChangesAsync();
                }

                // Invoice dagi check id ni 0 ga tenglaymiz
                tegirmonInvoice.kassa_check_id = 0;
            }
            // srazi almashtirishda olgan productlarni skladda bor yuqligini tekshirish uchun
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item_check in tegirmonInvoice.item_list)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item_check.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                   .ToListAsync();
                    if (skladTovarList == null || skladTovarList.Count == 0)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";
                        return BadRequest($"{productName} skladda mavjud emas!");
                    }

                    var skladTovar = skladTovarList.First();

                    if (skladTovar.qty < item_check.qty)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";

                        return BadRequest($"{productName} yetarli emas! Omborda faqat {skladTovar.qty} dona bor.");
                    }
                }
            } // srazi almashtirishda olgan productlarni skladda bor yuqligini tekshirish uchun


            // skakladda mahsulot yetarli bulsa qushaveradi
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            //nima sotib olsa oshani ostatkadan ayrib qoyadi
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                   .ToListAsync();

                    // agar yetarli bo‘lsa kamaytirish
                    skladTovarList.First().qty = skladTovarList.First().qty - item.qty;
                    skladTovarList.First().real_qty = skladTovarList.First().real_qty - item.qty; // bu nimaga kerakligini rosti tushunmadim

                    _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                    await _context.SaveChangesAsync();


                    // bu sklad tarixiga yozib quyadi
                    TegirmonSkladHistory history = new TegirmonSkladHistory();
                    history.active_status = true;
                    history.qty = item.qty;
                    history.type = 2; // chiqim
                    // history.price = item.summa;
                    history.TegirmonProductId = item.TegirmonProductid;
                    history.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0; // bu sklad id si
                    history.note = tegirmonInvoice.note + " <<Srazi almashtirildi>>";
                    history.text = "Srazi";
                    history.userId = tegirmonInvoice.id; // bu agar uzgartirish kerak bulsa history ni uchirish uchun
                    history.user_name = tegirmonInvoice.user_name; // bu user_name
                    history.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid; // bu user auth is si

                    _context.TegirmonSkladHistory.Update(history);
                    await _context.SaveChangesAsync();

                }
            }
                Console.WriteLine(tegirmonInvoice.id);
            
        

            if (tegirmonInvoice.kassa_id != 0)
            {
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p => p.TegirmonKassaid == tegirmonInvoice.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                {
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == tegirmonInvoice.kassa_id)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                    if (tegirmonKassaInfoLast == null)
                    {
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = 0;
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else
                {
                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonInvoice.summ;
                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());

                    await _context.SaveChangesAsync();
                }

                // return tegirmonKassaInfo.Last();
            }

            return tegirmonInvoice;
        }
        [HttpGet("acceptChangeBugdoyProductsWithoutRegistartion")]
        public async Task<ActionResult<TegirmonInvoice>> acceptChangeBugdoyProductsWithoutRegistartion([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
             .Include(p => p.item_list)
             .Where(p => p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }

        [HttpGet("acceptTegirmonTortilganBugdoyRoyxatiGroupFromInvoice")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroup>> acceptTegirmonTortilganBugdoyRoyxatiGroupFromInvoice([FromQuery] long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroup = await _context.TegirmonTortilganBugdoyRoyxatiGroup.FindAsync(id);

            if (tegirmonTortilganBugdoyRoyxatiGroup == null)
            {
                return NotFound();
            }

            tegirmonTortilganBugdoyRoyxatiGroup.accepted_status = true;

            List<TegirmonTortilganBugdoyRoyxatiGroupDetail> detail_list = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p => p.royxati)
                .Include(p => p.invoice)
                .Where(p => p.TegirmonTortilganBugdoyRoyxatiGroupid == id)
                .ToListAsync();


            //details for accepted and invoice
            if (detail_list != null && detail_list.Count > 0)
            {

                foreach (TegirmonTortilganBugdoyRoyxatiGroupDetail item in detail_list)
                {
                    item.royxati.accepted_get_value = true;
                    await acceptChangeBugdoyProductsWithoutRegistartion(item.TegirmonInvoiceid);
                }

                //update
                _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.UpdateRange(detail_list);
                await _context.SaveChangesAsync();

            }

            //update
            _context.TegirmonTortilganBugdoyRoyxatiGroup.Update(tegirmonTortilganBugdoyRoyxatiGroup);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiGroup;
        }







        /// <summary>
        /// Bu zaxiraga olgandan keyin osha ostatkadan narsaga almashtrish uchun kerak boladi
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>
        [HttpPost("postInvoiceBugdoyniZaxiradanNarsalargaAlmashtrish")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyniZaxiradanNarsalargaAlmashtrish(TegirmonInvoice tegirmonInvoice)
        {
            // agar invoice yangilanayotgan bo‘lsa (id > 0)
            if (tegirmonInvoice.id > 0)
            {
                // eski invoice-ni bazadan topamiz
                var oldInvoice = await _context.TegirmonInvoice
                    .Where(x => x.id == tegirmonInvoice.id)
                    .Include(x => x.item_list)
                    .FirstOrDefaultAsync();
                
                if (oldInvoice != null)
                {
                    if (oldInvoice.summ > 0 && oldInvoice.kassa_id != null && oldInvoice.kassa_id != 0)
                    {
                        // Topib olamiz yuqoridagi kassa uchun bugungi (yoki oxirgi) kassa info
                        var kassalar = await _context.TegirmonKassaInfo
                            .Where(k => k.TegirmonKassaid == oldInvoice.kassa_id
                                        && k.created_date_time >= DateTime.Now.Date)
                            .ToListAsync();

                        if (kassalar != null && kassalar.Count > 0)
                        {
                            // agar bugungi kunga yozuv bo'lsa
                            var todayKassaInfo = kassalar.Last();
                            todayKassaInfo.cash += oldInvoice.summ;  
                            _context.TegirmonKassaInfo.Update(todayKassaInfo);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // bugungi kunga yozuv bo‘lmasa, oxirgi yozuvni topib, undan kelib yangi yozuv qo‘shish
                            var lastKassaInfo = await _context.TegirmonKassaInfo
                                .Where(k => k.TegirmonKassaid == oldInvoice.kassa_id)
                                .OrderByDescending(k => k.created_date_time)
                                .FirstOrDefaultAsync();

                            double beforeCash = lastKassaInfo != null ? lastKassaInfo.cash : 0;

                            var newKassaInfo = new TegirmonKassaInfo()
                            {
                                TegirmonKassaid = oldInvoice.kassa_id.Value,
                                before_cash = beforeCash,
                                before_dollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0,
                                cash = beforeCash + oldInvoice.summ,
                                dollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0,
                                created_date_time = DateTime.Now,
                                active_status = true
                            };
                            _context.TegirmonKassaInfo.Add(newKassaInfo);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Klientni ostatkasini joyiga qaytarish kerak eskisini o'zgartirganda

                    if (oldInvoice.TegirmonProductid != null)
                    {
                        List<TegirmonClientOstatka> ostatkaList = await _context.TegirmonClientOstatka
                            .Where(p => p.TegirmonProductid == oldInvoice.TegirmonProductid
                            && p.TegirmonClientid == oldInvoice.TegirmonClientid)
                            .ToListAsync();
                        if (ostatkaList != null && ostatkaList.Count > 0)
                        {
                            ostatkaList.First().qty = ostatkaList.First().qty + oldInvoice.qty_real;
                            ostatkaList.First().real_qty = ostatkaList.First().real_qty + oldInvoice.qty_real;

                            _context.TegirmonClientOstatka.UpdateRange(ostatkaList);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            TegirmonClientOstatka ostatka = new TegirmonClientOstatka();
                            ostatka.active_status = true;
                            ostatka.qty = 1 * oldInvoice.qty_real;
                            ostatka.real_qty = 1 * oldInvoice.qty_real;
                            ostatka.TegirmonProductid = oldInvoice.TegirmonProductid ?? default(long);
                            ostatka.TegirmonClientid = oldInvoice.TegirmonClientid ?? default(long);

                            _context.TegirmonClientOstatka.Update(ostatka);
                            await _context.SaveChangesAsync();
                        }
                    } // client klientni ostatkasidan ayrilmoqda



                    // Eski itemlarni o‘chirib yuboramiz
                    if (oldInvoice.item_list != null && oldInvoice.item_list.Count > 0)
                    {

                        foreach (var oldItem in oldInvoice.item_list)
                        {
                            // Ombordagi mahsulotni topamiz
                            var skladTovarList = await _context.TegirmonSkladTovar
                                .Where(p => p.TegirmonProductId == oldItem.TegirmonProductid
                                        && p.TegirmonSkladId == oldInvoice.TegirmonSkladid)
                                .ToListAsync();

                            if (skladTovarList != null && skladTovarList.Count > 0)
                            {
                                var skladItem = skladTovarList.First();

                                // oldingi minus bo’lgan qty ni qaytamiz
                                skladItem.qty += oldItem.qty;
                                skladItem.real_qty += oldItem.qty;

                                _context.TegirmonSkladTovar.Update(skladItem);
                                await _context.SaveChangesAsync();
                            }

                            // Yangi qty_log tablega qaytarilganini yozamiz
                            TegirmonSkladQtyLog qtyLog = new TegirmonSkladQtyLog();
                            qtyLog.active_status = true;
                            qtyLog.qty = oldItem.qty;
                            qtyLog.type = 1; // 1 = kirim (oldingi chiqimni qaytaryapmiz)
                            qtyLog.TegirmonProductId = oldItem.TegirmonProductid;
                            qtyLog.TegirmonSkladId = oldInvoice.TegirmonSkladid ?? 0;
                            qtyLog.note = "Invoice yangilanganda eski qty qaytarildi (Invoice ID: " + oldInvoice.id + ")";
                            qtyLog.text = "Invoice_update_qaytarish";
                            qtyLog.auth_user_creator_id = oldInvoice.TegirmonAuthid;
                            qtyLog.userId = oldInvoice.id;
                            qtyLog.user_name = oldInvoice.user_name;
                            qtyLog.operation_id = oldInvoice.id;
                            qtyLog.operation_type = "Invoice";
                            qtyLog.create_date = DateTime.Now;

                            _context.TegirmonSkladQtyLog.Add(qtyLog);
                            await _context.SaveChangesAsync();
                        }

                        // oxirida eski itemlarni o‘chiramiz
                        _context.TegirmonInvoiceItem.RemoveRange(oldInvoice.item_list);
                        await _context.SaveChangesAsync();
                    }
                    
                    
                    // Shu invoice ga bog'langan history larni topish
                    var histories = await _context.TegirmonSkladHistory
                        .Where(h => h.userId == tegirmonInvoice.id && (h.text == "Zaxira" || h.text == "Srazi"))
                        .ToListAsync();
                    // Avval history larni uchirish
                    _context.TegirmonSkladHistory.RemoveRange(histories);
                    await _context.SaveChangesAsync();


                    // Eski invoice-ni yangilashga tayyorlab qo'yamiz
                    _context.Entry(oldInvoice).State = EntityState.Detached;
                }

                if(tegirmonInvoice.zaxira_extra_amount == 0  && tegirmonInvoice.kassa_check_id > 0)
                {
                    var check = await _context.TegirmonCheck
                        .FirstOrDefaultAsync(x => x.id == tegirmonInvoice.kassa_check_id);
                    
                    if (check != null)
                    {
                        // Topib olamiz yuqoridagi kassa uchun bugungi (yoki oxirgi) kassa info
                        var kassalar = await _context.TegirmonKassaInfo
                            .Where(k => k.TegirmonKassaid == check.auth_user_updator_id
                                        && k.created_date_time >= DateTime.Now.Date)
                            .ToListAsync();

                        if (kassalar != null && kassalar.Count > 0)
                        {
                            // agar bugungi kunga yozuv bo'lsa
                            var todayKassaInfo = kassalar.Last();
                            todayKassaInfo.cash -= check.cash;  
                            todayKassaInfo.dollor -= check.real_sum;  
                            _context.TegirmonKassaInfo.Update(todayKassaInfo);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // bugungi kunga yozuv bo‘lmasa, oxirgi yozuvni topib, undan kelib yangi yozuv qo‘shish
                            var lastKassaInfo = await _context.TegirmonKassaInfo
                                .Where(k => k.TegirmonKassaid == check.auth_user_updator_id)
                                .OrderByDescending(k => k.created_date_time)
                                .FirstOrDefaultAsync();

                            double beforeCash = lastKassaInfo != null ? lastKassaInfo.cash : 0;
                            double beforeDollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0;

                            var newKassaInfo = new TegirmonKassaInfo()
                            {
                                TegirmonKassaid = check.auth_user_updator_id,
                                before_cash = beforeCash,
                                before_dollor = lastKassaInfo != null ? lastKassaInfo.dollor : 0,
                                cash = beforeCash - check.cash,
                                dollor = beforeDollor - check.real_sum,
                                created_date_time = DateTime.Now,
                                active_status = true
                            };
                            _context.TegirmonKassaInfo.Add(newKassaInfo);
                            await _context.SaveChangesAsync();
                        }


                        // O'chiramiz
                        _context.TegirmonCheck.Remove(check);
                        await _context.SaveChangesAsync();
                    }

                    // Invoice dagi check id ni 0 ga tenglaymiz
                    tegirmonInvoice.kassa_check_id = 0;
                }
            }
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH;
            if (tegirmonInvoice.TegirmonProductid == null || tegirmonInvoice.TegirmonProductid == null) {
                return NotFound("Client or product not found");
            }

            // zaxiradan olgan productlarni skladda bor yuqligini tekshirish uchun
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item_check in tegirmonInvoice.item_list)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item_check.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                   .ToListAsync();
                    if (skladTovarList == null || skladTovarList.Count == 0)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";
                        return BadRequest($"{productName} skladda mavjud emas!");
                    }

                    var skladTovar = skladTovarList.First();

                    if (skladTovar.qty < item_check.qty)
                    {
                        var product = await _context.TegirmonProduct
                            .FirstOrDefaultAsync(x => x.id == item_check.TegirmonProductid);

                        var productName = product != null ? product.name : "Noma'lum tovar";

                        return BadRequest($"{productName} yetarli emas! Omborda faqat {skladTovar.qty} dona bor.");
                    }
                }
            } // zaxiradan olgan productlarni skladda bor yuqligini tekshirish uchun

            // client klientni ostatkasidan ayrilmoqda
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                List<TegirmonClientOstatka> ostatkaList = await _context.TegirmonClientOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid
                    && p.TegirmonClientid == tegirmonInvoice.TegirmonClientid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty - tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty - tegirmonInvoice.qty_real;

                    _context.TegirmonClientOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TegirmonClientOstatka ostatka = new TegirmonClientOstatka();
                    ostatka.active_status = true;
                    ostatka.qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.real_qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);
                    ostatka.TegirmonClientid = tegirmonInvoice.TegirmonClientid ?? default(long);

                    _context.TegirmonClientOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            } // client klientni ostatkasidan ayrilmoqda

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            //nima sotib olsa oshani ostatkadan ayrib qoyadi
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                   .ToListAsync();

                    // agar yetarli bo‘lsa kamaytirish
                    skladTovarList.First().qty = skladTovarList.First().qty - item.qty;
                    skladTovarList.First().real_qty = skladTovarList.First().real_qty - item.qty; // bu nimaga kerakligini rosti tushunmadim

                    _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                    await _context.SaveChangesAsync();


                    // bu sklad tarixiga yozib quyadi
                    TegirmonSkladHistory history = new TegirmonSkladHistory();
                    history.active_status = true;
                    history.qty = item.qty;
                    history.type = 2; // chiqim
                    // history.price = item.summa;
                    history.TegirmonProductId = item.TegirmonProductid;
                    history.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0; // bu sklad id si
                    history.note =tegirmonInvoice.note + " <<Zaxiradan almashtirildi>>";
                    history.text = "Zaxira";
                    history.userId = tegirmonInvoice.id;
                    // history.user_name = tegirmonCheck.image_base_64; // bu user_name
                    history.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid; // bu user auth is si

                    _context.TegirmonSkladHistory.Update(history);
                    await _context.SaveChangesAsync();

                }






                // foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                // {
                //     List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                //    .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                //    .ToListAsync();
                //     if (ostatkaList != null && ostatkaList.Count > 0)
                //     {
                //         ostatkaList.First().qty = ostatkaList.First().qty - item.qty;
                //         ostatkaList.First().real_qty = ostatkaList.First().real_qty - item.qty;

                //         _context.TegirmonOstatka.UpdateRange(ostatkaList);
                //         await _context.SaveChangesAsync();
                //     }
                //     else
                //     {
                //         TegirmonOstatka ostatka = new TegirmonOstatka();
                //         ostatka.active_status = true;
                //         ostatka.qty = -1 * item.qty;
                //         ostatka.real_qty = -1 * item.qty;
                //         ostatka.TegirmonProductid = item.TegirmonProductid;

                //         _context.TegirmonOstatka.Update(ostatka);
                //         await _context.SaveChangesAsync();
                //     }

                // }

            }

            


            Console.WriteLine(tegirmonInvoice.kassa_id);
            
            if(tegirmonInvoice.kassa_id != 0){
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p =>p.TegirmonKassaid == tegirmonInvoice.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                
                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p =>p.TegirmonKassaid == tegirmonInvoice.kassa_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();

                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = 0;
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else{
                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonInvoice.summ;
                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());

                    await _context.SaveChangesAsync();
                }

                // return tegirmonKassaInfo.Last();
            }


            return tegirmonInvoice;
        }
        [HttpPost("addChangeBugdoyFomClientZaxiradanToProducts")]
        public async Task<ActionResult<TegirmonInvoice>> addChangeBugdoyFomClientZaxiradanToProducts(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpGet("acceptChangeBugdoyFomClientZaxiradanToProducts")]
        public async Task<ActionResult<TegirmonInvoice>> acceptChangeBugdoyFomClientZaxiradanToProducts([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
             .Include(p => p.item_list)
             .Where(p => p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyniZaxiradanNarsalargaAlmashtrish(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }






        /// <summary>
        /// Bu bugdoyni tegirmon qilishi uchun ishlatiladi 
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>
        [HttpPost("postInvoiceBugdoyniTegirmongaUnQilibTortish")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyniTegirmongaUnQilibTortish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH;
            if (tegirmonInvoice.TegirmonProductid != null)
            {
                List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                    .Where(p => p.TegirmonProductid == tegirmonInvoice.TegirmonProductid)
                    .ToListAsync();
                if (ostatkaList != null && ostatkaList.Count > 0)
                {
                    ostatkaList.First().qty = ostatkaList.First().qty - tegirmonInvoice.qty_real;
                    ostatkaList.First().real_qty = ostatkaList.First().real_qty - tegirmonInvoice.qty_real;

                    _context.TegirmonOstatka.UpdateRange(ostatkaList);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TegirmonOstatka ostatka = new TegirmonOstatka();
                    ostatka.active_status = true;
                    ostatka.qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.real_qty = -1 * tegirmonInvoice.qty_real;
                    ostatka.TegirmonProductid = tegirmonInvoice.TegirmonProductid ?? default(long);

                    _context.TegirmonOstatka.Update(ostatka);
                    await _context.SaveChangesAsync();
                }
            }

            //kepak un otxotlardi prixod qilish uchun
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0) {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list) {
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                   .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                   .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty + item.qty;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty + item.qty;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty =  item.qty;
                        ostatka.real_qty = item.qty;
                        ostatka.TegirmonProductid = item.TegirmonProductid;

                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }
                }
            
            }

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpPost("addBugdoyniUnQilish")]
        public async Task<ActionResult<TegirmonInvoice>> addBugdoyniUnQilish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        [HttpGet("acceptBugdoyniUnQilish")]
        public async Task<ActionResult<TegirmonInvoice>> acceptBugdoyniUnQilish([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                .Include(p => p.item_list)
                .Where(p => p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyniTegirmongaUnQilibTortish(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }






        /// <summary>
        /// bu tovarlardi prixod qilgani ozi uchun bozordan opkesa nimadr yog nilardir
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>
        [HttpPost("postInvoiceBugdoyTovarlarniPrixodQilishSkladga")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyTovarlarniPrixodQilishSkladga(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_PRIXOD_QILISH;
            // double sum = 0.0;
            // double dollor = 0.0;
            // if (tegirmonInvoice.TegirmonContragentid > 0)
            // {
            //     sum = tegirmonInvoice.summ - (double)tegirmonInvoice.poluchit_summa; // contragentga sum berilganda ayriladi
            //     dollor = (double)tegirmonInvoice.pol_all_sum - (double)tegirmonInvoice.pol_dollor_summa; // dollor berilganda ayriladi

            //     double val = 0.0;
            //     if (sum < 0 && dollor > 0) // sum kup bulsayu dollor kam bulsa yetmagan dollor uchun sumni dolorga aylantiramiz
            //     {
            //         val = (-1 * (sum / (double)tegirmonInvoice.pol_kurs_sum)) - dollor;
            //         if (val <= 0)
            //         {
            //             sum = 0;
            //             dollor = -1 * val;
            //         }
            //         else
            //         {
            //             dollor = 0;
            //             sum = -1 * (val * (double)tegirmonInvoice.pol_kurs_sum);
            //         }
            //     }
            //     else if (sum > 0 && dollor < 0) // sumga yetmay dollor kup bulganda
            //     {
            //         val = (-1 * (dollor * (double)tegirmonInvoice.pol_kurs_sum)) - sum;
            //         if (val <= 0)
            //         {
            //             dollor = 0;
            //             sum = -1 * val;
            //         }
            //         else
            //         {
            //             sum = 0;
            //             dollor = -1 * (val / (double)tegirmonInvoice.pol_kurs_sum);
            //         }
            //     }

            //     var tegirmonContragent = await _context.TegirmonContragent
            //     .FirstOrDefaultAsync(p => p.id == tegirmonInvoice.TegirmonContragentid);
            //     double summ = 0.0;
            //     double doll = 0.0;

            //     if (tegirmonContragent.sum <= 0 && tegirmonContragent.dollor <= 0) // biz ortiqcha summada bulsak oldindan pul berib quygan bulsak
            //     {
            //         if (dollor <= 0 && sum <= 0) // ortiqcha bergan bulsak
            //         {
            //             tegirmonContragent.sum += sum;
            //             tegirmonContragent.dollor += dollor;
            //         }
            //         else // 
            //         {
            //             summ = (double)tegirmonContragent.sum + sum;
            //             doll = (double)tegirmonContragent.dollor + dollor;

            //             if (summ < 0 && doll > 0) // sum manfiy biz foydadamiz lekin dollor musbat
            //             {
            //                 val = (doll * (double)tegirmonInvoice.pol_kurs_sum) + summ;
            //                 if (val <= 0)
            //                 {
            //                     doll = 0;
            //                     sum = val;
            //                 }
            //                 else
            //                 {
            //                     sum = 0;
            //                     doll = val / (double)tegirmonInvoice.pol_kurs_sum;
            //                 }
            //             }
            //             else if (summ > 0 && doll < 0) // dollor manfiyda biz foydada lekin summ musbatda biz manfiyda bulganda
            //             {
            //                 val = (summ / (double)tegirmonInvoice.pol_kurs_sum) + doll;
            //                 if (val <= 0)
            //                 {
            //                     summ = 0;
            //                     doll = val;
            //                 }
            //                 else
            //                 {
            //                     doll = 0;
            //                     summ = val * (double)tegirmonInvoice.pol_kurs_sum;
            //                 }
            //             }

            //             tegirmonContragent.sum = summ;
            //             tegirmonContragent.dollor = doll;

            //         }
            //     }
            //     else // hisobda biz qarzda bulsak 
            //     {
            //         if (dollor >= 0 && sum >= 0) // yana kam bergan bulsak qarzga shunday qushib quyaveradi
            //         {
            //             tegirmonContragent.sum += sum;
            //             tegirmonContragent.dollor += dollor;
            //         }
            //         else
            //         { // bunda yoki ikkalasi ham manfiy kelgan buladi yoki bitta manfiy bitta musbat buladi // bittasi manfiy kelgan buyicha kuramiz
            //             summ = (double)tegirmonContragent.sum + sum;
            //             doll = (double)tegirmonContragent.dollor + dollor;

            //             if (summ < 0 && doll > 0) // sum manfiy biz foydadamiz lekin dollor musbat biz qarzda
            //             {
            //                 val = (-1 * (summ / (double)tegirmonInvoice.pol_kurs_sum)) - doll;
            //                 if (val <= 0)
            //                 {
            //                     doll = -1 * val;
            //                     sum = 0;
            //                 }
            //                 else
            //                 {
            //                     doll = 0;
            //                     summ = -1 * (val * (double)tegirmonInvoice.pol_kurs_sum);
            //                 }
            //             }
            //             else if (summ > 0 && doll < 0) //dollor manfiy biz foydadamiz lekin sum musbat biz minusda
            //             {
            //                 val = (-1 * (doll * (double)tegirmonInvoice.pol_kurs_sum)) - summ;
            //                 if (val <= 0)
            //                 {
            //                     summ = -1 * val;
            //                     doll = 0;
            //                 }
            //                 else
            //                 {
            //                     summ = 0;
            //                     doll = -1 * (val / (double)tegirmonInvoice.pol_kurs_sum);
            //                 }
            //             }
            //             tegirmonContragent.sum = summ;
            //             tegirmonContragent.dollor = doll;
            //         }
            //     }







            // }


            //tovarlani prixod qilish ostatkalardi qoshish bolmasa yaratish uchun kerak

            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {

                if (tegirmonInvoice.prixod_status == 1) // 1 bulsa prixod buladi
                {
                    foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                    {
                        List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                    .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                    .ToListAsync();
                        if (skladTovarList != null && skladTovarList.Count > 0)
                        {
                            skladTovarList.First().qty = skladTovarList.First().qty + item.qty;
                            skladTovarList.First().real_qty = skladTovarList.First().real_qty + item.qty; // bu nimaga kerakligini rosti tushunmadim

                            _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            TegirmonSkladTovar skladTovar = new TegirmonSkladTovar();
                            skladTovar.active_status = true;
                            skladTovar.qty = item.qty;
                            skladTovar.real_qty = item.qty;
                            skladTovar.TegirmonProductId = item.TegirmonProductid;
                            skladTovar.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;

                            _context.TegirmonSkladTovar.Update(skladTovar);
                            await _context.SaveChangesAsync();

                        }

                        // Yangi qty_log tablega yozamiz (ehtimoliy qo'shilish uchun)
                        TegirmonSkladQtyLog qtyLog = new TegirmonSkladQtyLog();
                        qtyLog.active_status = true;
                        qtyLog.qty = item.qty;
                        qtyLog.type = 1; // 1 = kirim (prixod)
                        qtyLog.TegirmonProductId = item.TegirmonProductid;
                        qtyLog.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;
                        qtyLog.note = "Prixod qilish (Invoice ID: " + tegirmonInvoice.id + ") - ehtimoliy qo'shilish";
                        qtyLog.text = "Prixod_ehtimoliy";
                        qtyLog.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid;
                        qtyLog.userId = tegirmonInvoice.id;
                        qtyLog.user_name = tegirmonInvoice.user_name;
                        qtyLog.operation_id = tegirmonInvoice.id;
                        qtyLog.operation_type = "Invoice";
                        qtyLog.create_date = DateTime.Now;

                        _context.TegirmonSkladQtyLog.Add(qtyLog);
                        await _context.SaveChangesAsync();

                        TegirmonSkladHistory history = new TegirmonSkladHistory();
                        history.active_status = true;
                        history.qty = item.qty;
                        history.type = 1;
                        // history.real_qty = item.qty;
                        history.TegirmonProductId = item.TegirmonProductid;
                        history.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;
                        history.note = tegirmonInvoice.note + " <<Prixod bulimi>>";
                        history.text = "Prixod";
                        history.user_name = tegirmonInvoice.user_name;
                        history.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid;

                        _context.TegirmonSkladHistory.Update(history);
                        await _context.SaveChangesAsync();

                    }
                }
                else // 2 bulsa rasxod buladi
                {
                    foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                    {
                        List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                    .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                    .ToListAsync();
                        if (skladTovarList != null && skladTovarList.Count > 0)
                        {
                            skladTovarList.First().qty = skladTovarList.First().qty - item.qty;
                            skladTovarList.First().real_qty = skladTovarList.First().real_qty - item.qty; // bu nimaga kerakligini rosti tushunmadim

                            _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            TegirmonSkladTovar skladTovar = new TegirmonSkladTovar();
                            skladTovar.active_status = true;
                            skladTovar.qty = -1 * item.qty;
                            skladTovar.real_qty = -1 * item.qty;
                            skladTovar.TegirmonProductId = item.TegirmonProductid;
                            skladTovar.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;

                            _context.TegirmonSkladTovar.Update(skladTovar);
                            await _context.SaveChangesAsync();

                        }

                        TegirmonSkladHistory history = new TegirmonSkladHistory();
                        history.active_status = true;
                        history.qty = item.qty;
                        history.type = 2;
                        // history.real_qty = item.qty;
                        history.TegirmonProductId = item.TegirmonProductid;
                        history.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;
                        history.note = tegirmonInvoice.note + " <<Rasxod bulimi>>";
                        history.text = "Rasxod";
                        history.user_name = tegirmonInvoice.user_name;
                        history.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid;

                        _context.TegirmonSkladHistory.Update(history);
                        await _context.SaveChangesAsync();

                    }
                }

            }

            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        // tovarlarni skladga prixod qilish bulimi
                // List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                // .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                // .ToListAsync();
                // if (ostatkaList != null && ostatkaList.Count > 0)
                // {
                //     ostatkaList.First().qty = ostatkaList.First().qty + item.qty;
                //     ostatkaList.First().real_qty = ostatkaList.First().real_qty + item.qty;

                //     _context.TegirmonOstatka.UpdateRange(ostatkaList);
                //     await _context.SaveChangesAsync();
                // }
                // else
                // {
                //     TegirmonOstatka ostatka = new TegirmonOstatka();
                //     ostatka.active_status = true;
                //     ostatka.qty = item.qty;
                //     ostatka.real_qty = item.qty;
                //     ostatka.TegirmonProductid = item.TegirmonProductid;

                //     _context.TegirmonOstatka.Update(ostatka);
                //     await _context.SaveChangesAsync();
                // }


        [HttpPost("addTovarlardiPrixodQilish")]
        public async Task<ActionResult<TegirmonInvoice>> addTovarlardiPrixodQilish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_PRIXOD_QILISH;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        // tovarlarni skladga prixod qilish bulimi
        





        [HttpGet("acceptTovarlardiPrixodQilish")]
        public async Task<ActionResult<TegirmonInvoice>> acceptTovarlardiPrixodQilish([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                .Include(p => p.item_list)
                .Where(p =>p.id == id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }
            await postInvoiceBugdoyTovarlarniPrixodQilishSkladga(tegirmonInvoice.First());

            return tegirmonInvoice.First();
        }







        /// <summary>
        /// Bu srogi otgan tovarlardi spisaniya qilish uchun 
        /// </summary>
        /// <param name="tegirmonInvoice"></param>
        /// <returns></returns>
        [HttpPost("postInvoiceBugdoyEskirganTovarlardiSklatdanSpisaniyaQilish")]
        public async Task<ActionResult<TegirmonInvoice>> postInvoiceBugdoyEskirganTovarlardiSklatdanSpisaniyaQilish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR;
            //tovarlani prixod qilish ostatkalardi qoshish bolmasa yaratish uchun kerak
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonOstatka> ostatkaList = await _context.TegirmonOstatka
                   .Where(p => p.TegirmonProductid == item.TegirmonProductid)
                   .ToListAsync();
                    if (ostatkaList != null && ostatkaList.Count > 0)
                    {
                        ostatkaList.First().qty = ostatkaList.First().qty - item.qty;
                        ostatkaList.First().real_qty = ostatkaList.First().real_qty - item.qty;

                        _context.TegirmonOstatka.UpdateRange(ostatkaList);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TegirmonOstatka ostatka = new TegirmonOstatka();
                        ostatka.active_status = true;
                        ostatka.qty = -1 * item.qty;
                        ostatka.real_qty =-1 *  item.qty;
                        ostatka.TegirmonProductid = item.TegirmonProductid;

                        _context.TegirmonOstatka.Update(ostatka);
                        await _context.SaveChangesAsync();
                    }

                }

            }


            tegirmonInvoice.inv_accepted_status = true;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }
        [HttpPost("addSrogiOtganTovarlardiRasxodQilish")]
        public async Task<ActionResult<TegirmonInvoice>> addSrogiOtganTovarlardiRasxodQilish(TegirmonInvoice tegirmonInvoice)
        {
            tegirmonInvoice.status_inv_type_name = INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR;
            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();

            return tegirmonInvoice;
        }

        [HttpGet("acceptSrogiOtganTovarlardiRasxodQilish")]
        public async Task<ActionResult<TegirmonInvoice>> acceptSrogiOtganTovarlardiRasxodQilish([FromQuery] long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice.FindAsync(id);

            if (tegirmonInvoice == null)
            {
                return NotFound();
            }
            await postInvoiceBugdoyEskirganTovarlardiSklatdanSpisaniyaQilish(tegirmonInvoice);

            return tegirmonInvoice;
        }



        // [HttpGet("getPaginationBugdoyPrixodQilinganTovarlarListiVaqtsz")]
        // public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyPrixodQilinganTovarlarListiVaqtsz([FromQuery] int page, [FromQuery] int size)
        // {
        //     TexPaginationModel paginationModel = new TexPaginationModel();
        //     List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
        //         .Include(p => p.contragent)
        //         .Include(p => p.sklad)
        //         .Include(p => p.product)
        //         .Include(p => p.client)
        //         .Where(p => p.active_status == true

        //         && p.status_inv_type_name == INVOICE_BUGDOY_PRIXOD_QILISH)
        //         .OrderByDescending(p => p.id)
        //         .Skip(page * size).Take(size).ToListAsync();
        //     if (categoryList == null)
        //     {
        //         categoryList = new List<TegirmonInvoice>();
        //     }
        //     paginationModel.items_list = JArray.FromObject(categoryList);
        //     paginationModel.items_count = await _context.TegirmonInvoice
        //         .Where(p => p.active_status == true
        //         && p.status_inv_type_name == INVOICE_BUGDOY_PRIXOD_QILISH).CountAsync();
        //     paginationModel.current_item_count = categoryList.Count();
        //     paginationModel.current_page = page;
        //     return paginationModel;
        // }



        [HttpGet("getPaginationBugdoyPrixodQilinganTovarlarListiVaqtsz")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyPrixodQilinganTovarlarListiVaqtsz(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] DateTime date_from,
            [FromQuery] DateTime date_to,
            [FromQuery] int sklad_id
        )
        {
            TexPaginationModel paginationModel = new TexPaginationModel();

            var query = _context.TegirmonInvoice
                .Include(p => p.contragent)
                .Include(p => p.sklad)
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p =>
                    p.active_status == true &&
                    (p.status_inv_type_name == INVOICE_BUGDOY_PRIXOD_QILISH || p.status_inv_type_name == "SKLADGA_BOSHQA_SKLADDAN")
                );

            // ⭐ Sklad filtri: 0 bo‘lsa hammasi chiqadi
            if (sklad_id != 0)
            {
                query = query.Where(p => p.TegirmonSkladid == sklad_id);
            }

            // ⭐ Sana orasidan filterlash
            DateTime from = date_from.Date;
            DateTime to = date_to.Date.AddDays(1); // inclusive qilish uchun

            query = query.Where(p =>
                p.updated_date_time >= from &&
                p.updated_date_time < to
            );

            // --- Pagination ---
            List<TegirmonInvoice> categoryList = await query
                .OrderByDescending(p => p.id)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await query.CountAsync();
            paginationModel.current_item_count = categoryList.Count;
            paginationModel.current_page = page;

            return paginationModel;
        }



        [HttpGet("getPaginationByClientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByClientId([FromQuery] int page, [FromQuery] int size,[FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Include(p => p.contragent)
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p => p.active_status == true

                && p.TegirmonClientid == client_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && p.TegirmonClientid == client_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        [HttpGet("getPaginationBugdoyPrixodQilinganTovarlarListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyPrixodQIlinganTovarlarListi([FromQuery] int page, [FromQuery] int size,
    [FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Include(p => p.contragent)
                .Include(p =>p.product)
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_PRIXOD_QILISH)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_PRIXOD_QILISH).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBugdoyniPulgaAlmashishListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyniPulgaAlmashishListi([FromQuery] int page, [FromQuery] int size,
            [FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_SOTISH_PULGA)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            } 
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_SOTISH_PULGA).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBugdoyTortilganTegirmondanUnUchunListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyTortilganTegirmondanUnUchunListi([FromQuery] int page, [FromQuery] int size,
    [FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSpisatQilinganTovarlarListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSpisatQilinganTovarlarListi([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationZahiragaOlibQolinganTovarlarListilarListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationZahiragaOlibQolinganTovarlarListilarListi([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Include( p => p.client)
                .Include( p => p.product)
                .Include( p => p.contragent)
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBugdoyniRegistratsiyaQilmasdanNarsalargaALmashtrilganlarListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBugdoyniRegistratsiyaQilmasdanNarsalargaALmashtrilganlarListi([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Include(p => p.client)
                .Include(p => p.product)
                .Include(p => p.contragent)
                .Include(p => p.item_list)
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Include(p => p.item_list)
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationZahiradanNarsalargaAlmashganlardiListi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationZahiradanNarsalargaAlmashganlardiListi([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true
                && (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.status_inv_type_name == INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // Zaxira bo'limi uchun: status = INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH, kassa_id va sana bo'yicha, summ > 0
        [HttpGet("getZaxiraByDateAndKassa")]
        public async Task<IActionResult> GetZaxiraByDateAndKassa([FromQuery] string date, [FromQuery] long? kassaId)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto'g'ri. To'g'ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var list = await _context.TegirmonInvoice
                .AsNoTracking()
                .Include(p => p.product)
                .Include(p => p.client)
                .Include(p => p.auth)
                    .ThenInclude(a => a.user)
                .Where(p =>
                    p.active_status == true &&
                    p.created_date_time >= start &&
                    p.created_date_time < end &&
                    (kassaId == null || kassaId == 0 || p.kassa_id == kassaId) &&
                    p.summ > 0 &&
                    p.status_inv_type_name == INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH
                )
                .OrderByDescending(p => p.created_date_time)
                .ToListAsync();

            return Ok(list);
        }

        // Тароз список bo'limi uchun: status = INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH, kassa_id va sana bo'yicha, summ > 0
        [HttpGet("getTaroziByDateAndKassa")]
        public async Task<IActionResult> GetTaroziByDateAndKassa([FromQuery] string date, [FromQuery] long? kassaId)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto'g'ri. To'g'ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var list = await _context.TegirmonInvoice
                .AsNoTracking()
                .Include(p => p.product)
                .Include(p => p.client)
                .Include(p => p.auth)
                    .ThenInclude(a => a.user)
                .Where(p =>
                    p.active_status == true &&
                    p.created_date_time >= start &&
                    p.created_date_time < end &&
                    (kassaId == null || kassaId == 0 || p.kassa_id == kassaId) &&
                    p.summ > 0 &&
                    p.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH
                )
                .OrderByDescending(p => p.created_date_time)
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/TegirmonInvoice
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonInvoice>>> GetTegirmonInvoice()
        {
            return await _context.TegirmonInvoice.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoice> categoryList = await _context.TegirmonInvoice
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoice
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonInvoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonInvoice>> GetTegirmonInvoice(long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice.FindAsync(id);

            if (tegirmonInvoice == null)
            {
                return NotFound();
            }

            return tegirmonInvoice;
        }

        [HttpGet("deAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborishByInvoiecId")]
        public async Task<ActionResult<TegirmonInvoice>> deAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborishByInvoiecId([FromQuery]long invoice_id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                  .Include(p => p.product)
                  .Include(p => p.client)
                  .Include(p => p.contragent)
                  .Include(p => p.item_list)
                  .ThenInclude(p => p.product)
                  .Include(p => p.auth)
                  .ThenInclude(p => p.user)
                  .Where(p => p.id == invoice_id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }

            return await deAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(tegirmonInvoice.First());

            
        }

        [HttpGet("deleteNoTestedAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborishbyInvoiceId")]
        public async Task<ActionResult<TegirmonInvoice>> deleteNoTestedAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborishbyInvoiceId([FromQuery] long invoice_id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                  .Include(p => p.product)
                  .Include(p => p.client)
                  .Include(p => p.contragent)
                  .Include(p => p.item_list)
                  .ThenInclude(p => p.product)
                  .Include(p => p.auth)
                  .ThenInclude(p => p.user)
                  .Where(p => p.id == invoice_id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }

            return await deleteNoTestedAcceptpostInvoiceBugdoyniClientniRegistratsiyaQilmasdanNarsalargaAlmashtribYuborish(tegirmonInvoice.First());


        }

        // GET: api/TegirmonInvoice/5
        [HttpGet("getAnyInvoiceFullInfoById")]
        public async Task<ActionResult<TegirmonInvoice>> getAnyInvoiceFullInfoById([FromQuery]long invoice_id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
                .Include(p => p.product)
                .Include(p => p.client)
                .Include(p => p.contragent)
                .Include(p =>p.item_list)
                .ThenInclude(p =>p.product)
                .Include(p => p.auth)
                .ThenInclude(p => p.user)
                .Where(p =>p.id == invoice_id).ToListAsync();

            if (tegirmonInvoice == null || tegirmonInvoice.Count == 0)
            {
                return NotFound();
            }

            return tegirmonInvoice.First();
        }

        // PUT: api/TegirmonInvoice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonInvoice(long id, TegirmonInvoice tegirmonInvoice)
        {
            if (id != tegirmonInvoice.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonInvoiceExists(id))
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

        // POST: api/TegirmonInvoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonInvoice>> PostTegirmonInvoice(TegirmonInvoice tegirmonInvoice)
        {

            _context.TegirmonInvoice.Update(tegirmonInvoice);
            await _context.SaveChangesAsync();


            return tegirmonInvoice;
        }

        // DELETE: api/TegirmonInvoice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonInvoice>> DeleteTegirmonInvoice(long id)
        {
            var tegirmonInvoice = await _context.TegirmonInvoice
            .Include(i => i.item_list) // item_list ni ham olib keladi
            .FirstOrDefaultAsync(i => i.id == id);

            if (tegirmonInvoice == null)
            {
                return NotFound();
            }


            // bu invoice uchgan paytda uning itemslaridagi ayrilgan qty larni skladga qushib quyiladi 
            if (tegirmonInvoice.item_list != null && tegirmonInvoice.item_list.Count > 0)
            {
                foreach (TegirmonInvoiceItem item in tegirmonInvoice.item_list)
                {
                    List<TegirmonSkladTovar> skladTovarList = await _context.TegirmonSkladTovar
                   .Where(p => p.TegirmonProductId == item.TegirmonProductid && p.TegirmonSkladId == tegirmonInvoice.TegirmonSkladid)
                   .ToListAsync();

                    if (skladTovarList != null && skladTovarList.Count > 0)
                    {
                        // agar yetarli bo'lsa kamaytirish
                    skladTovarList.First().qty = skladTovarList.First().qty + item.qty;
                    skladTovarList.First().real_qty = skladTovarList.First().real_qty + item.qty; // bu nimaga kerakligini rosti tushunmadim

                    _context.TegirmonSkladTovar.UpdateRange(skladTovarList);
                    await _context.SaveChangesAsync();

                        // Yangi qty_log tablega yozamiz
                        TegirmonSkladQtyLog qtyLog = new TegirmonSkladQtyLog();
                        qtyLog.active_status = true;
                        qtyLog.qty = item.qty;
                        qtyLog.type = 1; // 1 = kirim (invoice o'chirilganda qty qaytariladi)
                        qtyLog.TegirmonProductId = item.TegirmonProductid;
                        qtyLog.TegirmonSkladId = tegirmonInvoice.TegirmonSkladid ?? 0;
                        qtyLog.note = "Invoice o'chirilganda qty qaytarildi (Invoice ID: " + tegirmonInvoice.id + ")";
                        qtyLog.text = "Invoice_delete_qaytarish";
                        qtyLog.auth_user_creator_id = tegirmonInvoice.TegirmonAuthid;
                        qtyLog.userId = tegirmonInvoice.id;
                        qtyLog.user_name = tegirmonInvoice.user_name;
                        qtyLog.operation_id = tegirmonInvoice.id;
                        qtyLog.operation_type = "Invoice";
                        qtyLog.create_date = DateTime.Now;

                        _context.TegirmonSkladQtyLog.Add(qtyLog);
                        await _context.SaveChangesAsync();
                    }
                }
            } // bu invoice uchgan paytda uning itemslaridagi ayrilgan qty larni skladga qushib quyiladi 


            // Shu invoice ga bog'langan history larni topish
            var histories = await _context.TegirmonSkladHistory
                .Where(h => h.userId == id && (h.text == "Zaxira" || h.text == "Srazi"))
                .ToListAsync();

            // Avval history larni uchirish
            _context.TegirmonSkladHistory.RemoveRange(histories);


            _context.TegirmonInvoice.Remove(tegirmonInvoice);
            await _context.SaveChangesAsync();



            if (tegirmonInvoice.kassa_id != 0)
            {
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p => p.TegirmonKassaid == tegirmonInvoice.kassa_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                {
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == tegirmonInvoice.kassa_id)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                    if (tegirmonKassaInfoLast == null)
                    {
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = 0;
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonInvoice.summ;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonInvoice.kassa_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else
                {
                    tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonInvoice.summ;
                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                    await _context.SaveChangesAsync();
                }

                // return tegirmonKassaInfo.Last();
                // "tegirmonSkladid": 1,
            }

            return tegirmonInvoice;
        }

        private bool TegirmonInvoiceExists(long id)
        {
            return _context.TegirmonInvoice.Any(e => e.id == id);
        }
    }
}
