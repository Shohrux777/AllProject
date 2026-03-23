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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonMainKassaRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonMainKassaRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaRasxod>>> GetTegirmonMainKassaRasxod()
        {
            return await _context.TegirmonMainKassaRasxod.ToListAsync();
        }

        // GET: api/TegirmonWorker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonMainKassaRasxod>> GetTegirmonMainKassaRasxod(long id)
        {
            var tegirmonMainKassaRasxod = await _context.TegirmonMainKassaRasxod.FindAsync(id);

            if (tegirmonMainKassaRasxod == null)
            {
                return NotFound();
            }

            return tegirmonMainKassaRasxod;
        }


        [HttpGet("getMainKassaPrixodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassaPrixodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.status_rasxod = 1 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getMainKassaRasxodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassaRasxodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.status_rasxod = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }



        [HttpGet("getMainKassaSavdoKassadanOtganSummaKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassaSavdoKassadanOtganSummaKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.main_kassa_status = true AND p.status_rasxod = 1 AND p.auth_user_updator_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getMainKassadanSavdoKassagaOtganSummaKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassadanSavdoKassagaOtganSummaKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.main_kassa_status = true AND p.status_rasxod = 0 AND p.auth_user_updator_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }



        [HttpGet("getMainKassaSavdoKassadanOtganSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassaSavdoKassadanOtganSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.main_kassa_status = true AND p.status_rasxod = 1 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getMainKassadanSavdoKassagaOtganSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonMainKassaTemp>>> getMainKassadanSavdoKassagaOtganSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonMainKassaTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_main_kassa_rasxod p " + 
                "WHERE p.main_kassa_status = true AND p.status_rasxod = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }




        [HttpGet("getPaginationWorkerListByWorkerId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByWorkerId([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date,[FromQuery] long worker_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationWorkerListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByNote")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByNote([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String note_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.note.ToLower().Contains(note_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }


            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.note.ToLower().Contains(note_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByWorker_name")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByWorker_name([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String worker_name_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.worker_name.ToLower().Contains(worker_name_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.worker_name.ToLower().Contains(worker_name_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByAll_summ")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByAll_summ([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String all_summ_string)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonMainKassaRasxod> categoryList = await _context.TegirmonMainKassaRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonMainKassaRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonMainKassaRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // PUT: api/TegirmonClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonMainKassaRasxod(long id, TegirmonMainKassaRasxod tegirmonMainKassaRasxod)
        {
            if (id != tegirmonMainKassaRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonMainKassaRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonMainKassaRasxodExists(id))
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

        // POST: api/TegirmonClient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonMainKassaRasxod>> PostTegirmonMainKassaRasxod(TegirmonMainKassaRasxod tegirmonMainKassaRasxod)
        {
            _context.TegirmonMainKassaRasxod.Update(tegirmonMainKassaRasxod);
            await _context.SaveChangesAsync();


            if(tegirmonMainKassaRasxod.auth_user_updator_id != 0){
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p =>p.TegirmonKassaid == tegirmonMainKassaRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                
                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p =>p.TegirmonKassaid == tegirmonMainKassaRasxod.auth_user_updator_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();

                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.dollor = 0;
                        if(tegirmonMainKassaRasxod.status_rasxod == 1 && tegirmonMainKassaRasxod.main_kassa_status == true){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonMainKassaRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonMainKassaRasxod.dollor;
                        }
                        else if(tegirmonMainKassaRasxod.status_rasxod == 0 && tegirmonMainKassaRasxod.main_kassa_status == true){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonMainKassaRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonMainKassaRasxod.dollor;
                        }
                        
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonMainKassaRasxod.auth_user_updator_id;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;

                        if(tegirmonMainKassaRasxod.status_rasxod == 1 && tegirmonMainKassaRasxod.main_kassa_status == true){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonMainKassaRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonMainKassaRasxod.dollor;
                        }
                        else if(tegirmonMainKassaRasxod.status_rasxod == 0 && tegirmonMainKassaRasxod.main_kassa_status == true){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonMainKassaRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonMainKassaRasxod.dollor;
                        }
                        
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonMainKassaRasxod.auth_user_updator_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else{
                    if(tegirmonMainKassaRasxod.status_rasxod == 1 && tegirmonMainKassaRasxod.main_kassa_status == true){
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonMainKassaRasxod.prixod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonMainKassaRasxod.dollor;
                    }
                    else if(tegirmonMainKassaRasxod.status_rasxod == 0 && tegirmonMainKassaRasxod.main_kassa_status == true){
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonMainKassaRasxod.rasxod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonMainKassaRasxod.dollor;
                    }
                    

                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                    await _context.SaveChangesAsync();
                }

            
                // return tegirmonKassaInfo.Last();
            }

            return CreatedAtAction("GetTegirmonMainKassaRasxod", new { id = tegirmonMainKassaRasxod.id }, tegirmonMainKassaRasxod);
        }



        // DELETE: api/TegirmonClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonMainKassaRasxod>> DeleteTegirmonMainKassaRasxod(long id)
        {
            var tegirmonMainKassaRasxod = await _context.TegirmonMainKassaRasxod.FindAsync(id);
            if (tegirmonMainKassaRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonMainKassaRasxod.Remove(tegirmonMainKassaRasxod);
            await _context.SaveChangesAsync();

            return tegirmonMainKassaRasxod;
        }

        private bool TegirmonMainKassaRasxodExists(long id)
        {
            return _context.TegirmonMainKassaRasxod.Any(e => e.id == id);
        }

        // PUT: api/TegirmonMainKassaRasxod/izminit/{id}
        // Bir kun ichida rasxod/prixod ni izminit qilish
        [HttpPut("izminit/{id}")]
        public async Task<IActionResult> IzminitTegirmonMainKassaRasxod(long id, TegirmonMainKassaRasxod updatedRasxod)
        {
            // 1️⃣ Eski rasxod/prixod ni topish
            var oldRasxod = await _context.TegirmonMainKassaRasxod.FindAsync(id);
            if (oldRasxod == null)
            {
                return NotFound("Rasxod/Prixod topilmadi");
            }

            // 2️⃣ Bir kun ichida izminit qilish tekshiruvi
            if (oldRasxod.created_date.Date != DateTime.Now.Date)
            {
                return BadRequest("Faqat bugungi kundagi rasxod/prixod ni izminit qilish mumkin");
            }

            // 3️⃣ Eski rasxod/prixod ning ta'sirini bekor qilish
            if (oldRasxod.auth_user_updator_id != 0 && oldRasxod.main_kassa_status == true)
            {
                var oldKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == oldRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                if (oldKassaInfo != null)
                {
                    // Eski ta'sirni bekor qilish
                    if (oldRasxod.status_rasxod == 1) // Prixod bo'lgan bo'lsa (kassadan ayirilgan)
                    {
                        // Qaytarish: kassaga qo'shish
                        oldKassaInfo.cash = oldKassaInfo.cash + oldRasxod.prixod;
                        oldKassaInfo.dollor = oldKassaInfo.dollor + oldRasxod.dollor;
                    }
                    else if (oldRasxod.status_rasxod == 0) // Rasxod bo'lgan bo'lsa (kassaga qo'shilgan)
                    {
                        // Qaytarish: kassadan ayirish
                        oldKassaInfo.cash = oldKassaInfo.cash - oldRasxod.rasxod;
                        oldKassaInfo.dollor = oldKassaInfo.dollor - oldRasxod.dollor;
                    }

                    _context.TegirmonKassaInfo.Update(oldKassaInfo);
                    await _context.SaveChangesAsync();
                }
            }

            // 4️⃣ Yangi ma'lumotlar bilan yangilash
            oldRasxod.worker_name = updatedRasxod.worker_name;
            oldRasxod.passport_number = updatedRasxod.passport_number;
            oldRasxod.phone_number = updatedRasxod.phone_number;
            oldRasxod.rasxod = updatedRasxod.rasxod;
            oldRasxod.prixod = updatedRasxod.prixod;
            oldRasxod.all_summ = updatedRasxod.all_summ;
            oldRasxod.all_summ_string = updatedRasxod.all_summ_string;
            oldRasxod.status_rasxod = updatedRasxod.status_rasxod;
            oldRasxod.main_kassa_status = updatedRasxod.main_kassa_status;
            oldRasxod.dollor = updatedRasxod.dollor;
            oldRasxod.dollor_string = updatedRasxod.dollor_string;
            oldRasxod.kurs = updatedRasxod.kurs;
            oldRasxod.note = updatedRasxod.note;
            oldRasxod.image_url = updatedRasxod.image_url;
            oldRasxod.addiotionala_information = updatedRasxod.addiotionala_information;
            oldRasxod.TegirmonWorkerid = updatedRasxod.TegirmonWorkerid;
            oldRasxod.reserve = updatedRasxod.reserve;
            oldRasxod.udate_date = DateTime.Now;

            _context.TegirmonMainKassaRasxod.Update(oldRasxod);
            await _context.SaveChangesAsync();

            // 5️⃣ Yangi ma'lumotlar bilan kassani yangilash
            if (oldRasxod.auth_user_updator_id != 0 && oldRasxod.main_kassa_status == true)
            {
                var newKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == oldRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                if (newKassaInfo != null)
                {
                    // Yangi ta'sirni qo'shish
                    if (oldRasxod.status_rasxod == 1) // Prixod bo'lsa (kassadan ayirish)
                    {
                        newKassaInfo.cash = newKassaInfo.cash - oldRasxod.prixod;
                        newKassaInfo.dollor = newKassaInfo.dollor - oldRasxod.dollor;
                    }
                    else if (oldRasxod.status_rasxod == 0) // Rasxod bo'lsa (kassaga qo'shish)
                    {
                        newKassaInfo.cash = newKassaInfo.cash + oldRasxod.rasxod;
                        newKassaInfo.dollor = newKassaInfo.dollor + oldRasxod.dollor;
                    }

                    _context.TegirmonKassaInfo.Update(newKassaInfo);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(oldRasxod);
        }

    }
}
