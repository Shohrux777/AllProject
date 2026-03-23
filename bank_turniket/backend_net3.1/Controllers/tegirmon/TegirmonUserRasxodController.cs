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
    public class TegirmonUserRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserRasxod>>> GetTegirmonUserRasxod()
        {
            return await _context.TegirmonUserRasxod.ToListAsync();
        }

        // GET: api/TegirmonWorker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserRasxod>> GetTegirmonUserRasxod(long id)
        {
            var tegirmonUserRasxod = await _context.TegirmonUserRasxod.FindAsync(id);

            if (tegirmonUserRasxod == null)
            {
                return NotFound();
            }

            return tegirmonUserRasxod;
        }



        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getUserPrixodSummaKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonUserRasxodTemp>>> getUserPrixodSummaKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonUserRasxodTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_user_rasxod p " + 
                "WHERE p.status_rasxod = 1 AND p.auth_user_updator_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getUserRasxodSummaKassaId")]
        public async Task<ActionResult<IEnumerable<TegirmonUserRasxodTemp>>> getUserRasxodSummaKassaId([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long kassa_id)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonUserRasxodTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_user_rasxod p " + 
                "WHERE p.status_rasxod = 0 AND p.auth_user_updator_id = " + kassa_id + " AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }


         [HttpGet("getUserPrixodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonUserRasxodTemp>>> getUserPrixodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonUserRasxodTemp.FromSqlRaw("" +
                " SELECT   "+
                " sum(p.prixod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_user_rasxod p " + 
                "WHERE p.status_rasxod = 1 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }

        [HttpGet("getUserRasxodSumma")]
        public async Task<ActionResult<IEnumerable<TegirmonUserRasxodTemp>>> getUserRasxodSumma([FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            String beginDateStr = begin_date.ToString("yyyy-MM-dd hh:mm:ss");
            String endDateStr = end_date.ToString("yyyy-MM-dd hh:mm:ss");
            return await _context.TegirmonUserRasxodTemp.FromSqlRaw("" +
                " SELECT   "+ 
                " sum(p.rasxod) as cash, " + 
                " sum(p.dollor) as dollor, " + 
                " sum(p.all_summ) as all_summ  " + 
                "FROM public.tegirmon_user_rasxod p " + 
                "WHERE p.status_rasxod = 0 AND created_date_time BETWEEN '"+ beginDateStr + "' AND '"+endDateStr+"'").ToListAsync();
        }




        [HttpGet("getPaginationWorkerListByWorkerId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByWorkerId([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date,[FromQuery] long worker_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true && p.TegirmonWorkerid == worker_id && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationWorkerListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByNote")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByNote([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String note_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.note.ToLower().Contains(note_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }


            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.note.ToLower().Contains(note_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByWorker_name")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByWorker_name([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String worker_name_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.worker_name.ToLower().Contains(worker_name_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.worker_name.ToLower().Contains(worker_name_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByAll_summ")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByAll_summ([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] String all_summ_string)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserRasxod> categoryList = await _context.TegirmonUserRasxod
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date) 
                && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserRasxod.Where(p => p.active_status == true  && (p.created_date >= begin_date && p.created_date <= end_date) 
            && p.all_summ_string.ToLower().Contains(all_summ_string.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // Kun va kassa (auth_user_updator_id) bo'yicha barcha user_rasxod yozuvlarini olish
        [HttpGet("getByDateAndKassa")]
        public async Task<IActionResult> GetByDateAndKassa([FromQuery] string date, [FromQuery] long? kassa_id)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { error = "date parametri majburiy. Masalan: 2025-10-31" });

            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return BadRequest(new { error = "date formati noto'g'ri. To'g'ri format: YYYY-MM-DD" });

            var start = parsedDate.Date;
            var end = start.AddDays(1);

            var list = await _context.TegirmonUserRasxod
                .AsNoTracking()
                .Where(p =>
                    p.active_status == true &&
                    p.created_date >= start &&
                    p.created_date < end &&
                    (kassa_id == null || kassa_id == 0 || p.auth_user_updator_id == kassa_id) &&
                    (p.note == null || !p.note.Contains("Optom savdo bo'limi"))
                )
                .OrderByDescending(p => p.created_date)
                .ToListAsync();

            return Ok(list);
        }
        // PUT: api/TegirmonClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserRasxod(long id, TegirmonUserRasxod tegirmonUserRasxod)
        {
            if (id != tegirmonUserRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserRasxodExists(id))
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
        public async Task<ActionResult<TegirmonUserRasxod>> PostTegirmonUserRasxod(TegirmonUserRasxod tegirmonUserRasxod)
        {
            _context.TegirmonUserRasxod.Update(tegirmonUserRasxod);
            await _context.SaveChangesAsync();

            if(tegirmonUserRasxod.auth_user_updator_id != 0){
                var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                .Where(p =>p.TegirmonKassaid == tegirmonUserRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                
                if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                    // bu bugungi kunga dalni qushilmay qolgan bulsa
                    var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                    .Where(p =>p.TegirmonKassaid == tegirmonUserRasxod.auth_user_updator_id)
                    .OrderByDescending(p => p.created_date_time) 
                    .FirstOrDefaultAsync();

                    if(tegirmonKassaInfoLast == null){
                        // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                        TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                        tegirmon_kassa_info.cash = 0;
                        tegirmon_kassa_info.dollor = 0;
                        if(tegirmonUserRasxod.status_rasxod == 1){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonUserRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonUserRasxod.dollor;
                        }
                        else{
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonUserRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonUserRasxod.dollor;
                        }
                        
                        // tegirmon_kassa_info.id = 0;
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonUserRasxod.auth_user_updator_id;
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

                        if(tegirmonUserRasxod.status_rasxod == 1){
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonUserRasxod.prixod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonUserRasxod.dollor;
                        }
                        else{
                            tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonUserRasxod.rasxod;
                            tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonUserRasxod.dollor;
                        }
                        
                        tegirmon_kassa_info.TegirmonKassaid = tegirmonUserRasxod.auth_user_updator_id;
                        tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                        tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                        _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                        await _context.SaveChangesAsync();

                    }
                }
                else{
                    if(tegirmonUserRasxod.status_rasxod == 1){
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonUserRasxod.prixod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonUserRasxod.dollor;
                    }
                    else{
                        tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonUserRasxod.rasxod;
                        tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonUserRasxod.dollor;
                    }
                    

                    _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                    await _context.SaveChangesAsync();
                }

            
                // return tegirmonKassaInfo.Last();
            }
            if(tegirmonUserRasxod.bot_id != null){
                TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                tegirmon_kassa_prixod.auth_user_updator_id = tegirmonUserRasxod.bot_id; // kassa id
                tegirmon_kassa_prixod.prixod = tegirmonUserRasxod.rasxod;
                tegirmon_kassa_prixod.image_url = tegirmonUserRasxod.reserve;
                tegirmon_kassa_prixod.status_rasxod = 1;
                tegirmon_kassa_prixod.auth_user_creator_id = tegirmonUserRasxod.reserve_val_l;
                tegirmon_kassa_prixod.rasxod = 0;
                tegirmon_kassa_prixod.worker_name = tegirmonUserRasxod.worker_name;
                tegirmon_kassa_prixod.phone_number = tegirmonUserRasxod.phone_number;
                tegirmon_kassa_prixod.dollor = tegirmonUserRasxod.dollor;
                tegirmon_kassa_prixod.dollor_string = tegirmonUserRasxod.dollor_string;
                tegirmon_kassa_prixod.note = tegirmonUserRasxod.note;
                tegirmon_kassa_prixod.addiotionala_information = tegirmonUserRasxod.addiotionala_information;

                _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
                await _context.SaveChangesAsync();


                if(tegirmon_kassa_prixod.auth_user_updator_id != 0){
                    var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p =>p.TegirmonKassaid == tegirmon_kassa_prixod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();
                    
                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0){
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                        .Where(p =>p.TegirmonKassaid == tegirmon_kassa_prixod.auth_user_updator_id)
                        .OrderByDescending(p => p.created_date_time) 
                        .FirstOrDefaultAsync();

                        if(tegirmonKassaInfoLast == null){
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if(tegirmon_kassa_prixod.status_rasxod == 1){
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmon_kassa_prixod.prixod;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmon_kassa_prixod.dollor;
                            }
                            else{
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmon_kassa_prixod.rasxod;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmon_kassa_prixod.dollor;
                            }
                            
                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonKassaid = tegirmon_kassa_prixod.auth_user_updator_id;
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

                            if(tegirmon_kassa_prixod.status_rasxod == 1){
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmon_kassa_prixod.prixod;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmon_kassa_prixod.dollor;
                            }
                            else{
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmon_kassa_prixod.rasxod;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmon_kassa_prixod.dollor;
                            }
                            
                            tegirmon_kassa_info.TegirmonKassaid = tegirmon_kassa_prixod.auth_user_updator_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else{
                        if(tegirmon_kassa_prixod.status_rasxod == 1){
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmon_kassa_prixod.prixod;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmon_kassa_prixod.dollor;
                        }
                        else{
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmon_kassa_prixod.rasxod;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmon_kassa_prixod.dollor;
                        }
                        

                        _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();
                    }

                
                    // return tegirmonKassaInfo.Last();
                }

            }

            return CreatedAtAction("GetTegirmonUserRasxod", new { id = tegirmonUserRasxod.id }, tegirmonUserRasxod);
        }



        // DELETE: api/TegirmonClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserRasxod>> DeleteTegirmonUserRasxod(long id)
        {
            var tegirmonUserRasxod = await _context.TegirmonUserRasxod.FindAsync(id);
            if (tegirmonUserRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonUserRasxod.Remove(tegirmonUserRasxod);
            await _context.SaveChangesAsync();

            return tegirmonUserRasxod;
        }

        private bool TegirmonUserRasxodExists(long id)
        {
            return _context.TegirmonUserRasxod.Any(e => e.id == id);
        }

    }
}
