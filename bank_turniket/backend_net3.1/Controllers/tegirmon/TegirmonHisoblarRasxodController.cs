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
    public class TegirmonHisoblarRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonHisoblarRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonHisoblarRasxod>>> GetTegirmonHisoblarRasxod()
        {
            return await _context.TegirmonHisoblarRasxod.ToListAsync();
        }

        // GET: api/TegirmonWorker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonHisoblarRasxod>> GetTegirmonHisoblarRasxod(long id)
        {
            var tegirmonHisoblarRasxod = await _context.TegirmonHisoblarRasxod.FindAsync(id);

            if (tegirmonHisoblarRasxod == null)
            {
                return NotFound();
            }

            return tegirmonHisoblarRasxod;
        }


        [HttpGet("getHisoblarPrixodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonHisoblarTemp>>> getHisoblarPrixodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long hisob_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonHisoblarTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.perichsleniya) as perichsleniya, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_hisoblar_rasxod p " + 
                "WHERE p.status_rasxod = 1 AND p.\"TegirmonHisoblarid\" = " + hisob_id +" AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getHisoblarRasxodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonHisoblarTemp>>> getHisoblarRasxodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long hisob_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonHisoblarTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.perichsleniya) as perichsleniya, " +
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_hisoblar_rasxod p " + 
                "WHERE p.status_rasxod = 0 AND p.\"TegirmonHisoblarid\" = " + hisob_id +" AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }


      
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationWorkerListByWorkerId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByWorkerId([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date,[FromQuery] long worker_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationWorkerListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByNote")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByNote([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String note_str, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.note.ToLower().Contains(note_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }


            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.note.ToLower().Contains(note_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByWorker_name")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByWorker_name([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String worker_name_str, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.worker_name.ToLower().Contains(worker_name_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.worker_name.ToLower().Contains(worker_name_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByAll_summ")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByAll_summ([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String all_summ_string, [FromQuery] long hisob_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblarRasxod> categoryList = await _context.TegirmonHisoblarRasxod
                .Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblarRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblarRasxod.Where(p => p.active_status == true && p.TegirmonHisoblarid == hisob_id && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // PUT: api/TegirmonClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonHisoblarRasxod(long id, TegirmonHisoblarRasxod tegirmonHisoblarRasxod)
        {
            if (id != tegirmonHisoblarRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonHisoblarRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonHisoblarRasxodExists(id))
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
        public async Task<ActionResult<TegirmonHisoblarRasxod>> PostTegirmonHisoblarRasxod(TegirmonHisoblarRasxod tegirmonHisoblarRasxod)
        {
            _context.TegirmonHisoblarRasxod.Update(tegirmonHisoblarRasxod);
            await _context.SaveChangesAsync();
            

            if(tegirmonHisoblarRasxod.TegirmonHisoblarid != 0){
                var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                .Where(p =>p.TegirmonHisoblarid == tegirmonHisoblarRasxod.TegirmonHisoblarid && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                
                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                    .Where(p =>p.TegirmonHisoblarid == tegirmonHisoblarRasxod.TegirmonHisoblarid)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.dollor = 0;
                        if(tegirmonHisoblarRasxod.status_rasxod == 1){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonHisoblarRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonHisoblarRasxod.dollor;
                        }
                        else if(tegirmonHisoblarRasxod.status_rasxod == 0){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonHisoblarRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonHisoblarRasxod.dollor;
                        }
                        
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonHisoblarid = tegirmonHisoblarRasxod.TegirmonHisoblarid;
                        tegirmon_kassa_info.before_cash = 0;
                        tegirmon_kassa_info.before_dollor = 0;
                        _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();
                    }
                    else{
                        // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                        TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                        tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;

                        if(tegirmonHisoblarRasxod.status_rasxod == 1){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonHisoblarRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonHisoblarRasxod.dollor;
                        }
                        else if(tegirmonHisoblarRasxod.status_rasxod == 0){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonHisoblarRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonHisoblarRasxod.dollor;
                        }
                        
                        tegirmon_kassa_info.TegirmonHisoblarid = tegirmonHisoblarRasxod.TegirmonHisoblarid;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else{
                    if(tegirmonHisoblarRasxod.status_rasxod == 1){
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonHisoblarRasxod.prixod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonHisoblarRasxod.dollor;
                    }
                    else if(tegirmonHisoblarRasxod.status_rasxod == 0){
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonHisoblarRasxod.rasxod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonHisoblarRasxod.dollor;
                    }
                    

                    _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                    await _context.SaveChangesAsync();
                }

            
                // return tegirmonKassaInfo.Last();
            }

            return CreatedAtAction("GetTegirmonHisoblarRasxod", new { id = tegirmonHisoblarRasxod.id }, tegirmonHisoblarRasxod);
        }


        // DELETE: api/TegirmonClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonHisoblarRasxod>> DeleteTegirmonHisoblarRasxod(long id)
        {
            var tegirmonHisoblarRasxod = await _context.TegirmonHisoblarRasxod.FindAsync(id);
            if (tegirmonHisoblarRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonHisoblarRasxod.Remove(tegirmonHisoblarRasxod);
            await _context.SaveChangesAsync();

            return tegirmonHisoblarRasxod;
        }

        // PUT: api/TegirmonHisoblarRasxod/izminit/{id}
        // Bir kun ichida rasxod/prixod ni izminit qilish
        [HttpPut("izminit/{id}")]
        public async Task<IActionResult> IzminitTegirmonHisoblarRasxod(long id, TegirmonHisoblarRasxod updatedRasxod)
        {
            // 1️⃣ Eski rasxod/prixod ni topish
            var oldRasxod = await _context.TegirmonHisoblarRasxod.FindAsync(id);
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
            if (oldRasxod.TegirmonHisoblarid != 0)
            {
                var oldHisobInfo = await _context.TegirmonHisoblarInfo
                    .Where(p => p.TegirmonHisoblarid == oldRasxod.TegirmonHisoblarid && p.created_date_time >= DateTime.Now.Date)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                if (oldHisobInfo != null)
                {
                    // Eski ta'sirni bekor qilish
                    if (oldRasxod.status_rasxod == 1) // Prixod bo'lgan bo'lsa (hisobga qo'shilgan)
                    {
                        // Qaytarish: hisobdan ayirish
                        oldHisobInfo.cash = oldHisobInfo.cash - oldRasxod.prixod;
                        oldHisobInfo.dollor = oldHisobInfo.dollor - oldRasxod.dollor;
                    }
                    else if (oldRasxod.status_rasxod == 0) // Rasxod bo'lgan bo'lsa (hisobdan ayirilgan)
                    {
                        // Qaytarish: hisobga qo'shish
                        oldHisobInfo.cash = oldHisobInfo.cash + oldRasxod.rasxod;
                        oldHisobInfo.dollor = oldHisobInfo.dollor + oldRasxod.dollor;
                    }

                    _context.TegirmonHisoblarInfo.Update(oldHisobInfo);
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

            _context.TegirmonHisoblarRasxod.Update(oldRasxod);
            await _context.SaveChangesAsync();

            // 5️⃣ Yangi ma'lumotlar bilan hisobni yangilash
            if (oldRasxod.TegirmonHisoblarid != 0)
            {
                var newHisobInfo = await _context.TegirmonHisoblarInfo
                    .Where(p => p.TegirmonHisoblarid == oldRasxod.TegirmonHisoblarid && p.created_date_time >= DateTime.Now.Date)
                    .OrderByDescending(p => p.created_date_time)
                    .FirstOrDefaultAsync();

                if (newHisobInfo != null)
                {
                    // Yangi ta'sirni qo'shish
                    if (oldRasxod.status_rasxod == 1) // Prixod bo'lsa (hisobga qo'shish)
                    {
                        newHisobInfo.cash = newHisobInfo.cash + oldRasxod.prixod;
                        newHisobInfo.dollor = newHisobInfo.dollor + oldRasxod.dollor;
                    }
                    else if (oldRasxod.status_rasxod == 0) // Rasxod bo'lsa (hisobdan ayirish)
                    {
                        newHisobInfo.cash = newHisobInfo.cash - oldRasxod.rasxod;
                        newHisobInfo.dollor = newHisobInfo.dollor - oldRasxod.dollor;
                    }

                    _context.TegirmonHisoblarInfo.Update(newHisobInfo);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(oldRasxod);
        }

        private bool TegirmonHisoblarRasxodExists(long id)
        {
            return _context.TegirmonHisoblarRasxod.Any(e => e.id == id);
        }

    }
}
