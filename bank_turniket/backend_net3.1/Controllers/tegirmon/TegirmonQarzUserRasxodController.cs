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
    public class TegirmonQarzUserRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonQarzUserRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonQarzUserRasxod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonQarzUserRasxod>>> GetTegirmonQarzUserRasxod()
        {
            return await _context.TegirmonQarzUserRasxod.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserRasxod> categoryList = await _context.TegirmonQarzUserRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserId([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserRasxod> categoryList = await _context.TegirmonQarzUserRasxod
                .Where(p => p.active_status == true && p.TegirmonQarzUserId == userid)
                .OrderByDescending(p => p.id)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserRasxod.Where(p => p.active_status == true && p.TegirmonQarzUserId == userid).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonQarzUserRasxod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonQarzUserRasxod>> GetTegirmonQarzUserRasxod(long id)
        {
            var tegirmonQarzUserRasxod = await _context.TegirmonQarzUserRasxod.FindAsync(id);

            if (tegirmonQarzUserRasxod == null)
            {
                return NotFound();
            }

            return tegirmonQarzUserRasxod;
        }

        // PUT: api/TegirmonQarzUserRasxod/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonQarzUserRasxod(long id, TegirmonQarzUserRasxod tegirmonQarzUserRasxod)
        {
            if (id != tegirmonQarzUserRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonQarzUserRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonQarzUserRasxodExists(id))
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

        // POST: api/TegirmonQarzUserRasxod
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonQarzUserRasxod>> PostTegirmonQarzUserRasxod(TegirmonQarzUserRasxod tegirmonQarzUserRasxod)
        {
            _context.TegirmonQarzUserRasxod.Update(tegirmonQarzUserRasxod);
            await _context.SaveChangesAsync();
            if (tegirmonQarzUserRasxod.bot_id > 0)
            {
                TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                tegirmon_hisob_prixod.status_rasxod = tegirmonQarzUserRasxod.status_rasxod;
                tegirmon_hisob_prixod.worker_name = tegirmonQarzUserRasxod.worker_name;
                tegirmon_hisob_prixod.phone_number = tegirmonQarzUserRasxod.phone_number;
                tegirmon_hisob_prixod.auth_user_creator_id = tegirmonQarzUserRasxod.auth_user_creator_id;
                tegirmon_hisob_prixod.note = tegirmonQarzUserRasxod.note;
                tegirmon_hisob_prixod.addiotionala_information = tegirmonQarzUserRasxod.addiotionala_information;// bunga kassirni nomini aniqlab yozish kerak;

                tegirmon_hisob_prixod.all_summ = (double)tegirmonQarzUserRasxod.all_summ;
                tegirmon_hisob_prixod.all_summ_string = tegirmonQarzUserRasxod.all_summ_string;
                tegirmon_hisob_prixod.dollor = (double)tegirmonQarzUserRasxod.dollor;
                tegirmon_hisob_prixod.dollor_string = tegirmonQarzUserRasxod.dollor_string;
                tegirmon_hisob_prixod.kurs = (double)tegirmonQarzUserRasxod.kurs;
                tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonQarzUserRasxod.bot_id;

                if (tegirmonQarzUserRasxod.status_rasxod == 0)
                {
                    tegirmon_hisob_prixod.rasxod = (double)tegirmonQarzUserRasxod.sum;
                    tegirmon_hisob_prixod.reserve = tegirmonQarzUserRasxod.sum_str;
                }
                else
                {
                    tegirmon_hisob_prixod.prixod = (double)tegirmonQarzUserRasxod.sum;
                    tegirmon_hisob_prixod.image_url = tegirmonQarzUserRasxod.sum_str;
                }

                _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                await _context.SaveChangesAsync();


                if (tegirmonQarzUserRasxod.bot_id != 0) // hisob tanlanganda hisobga pul tushadi
                {
                    var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                    .Where(p => p.TegirmonHisoblarid == tegirmonQarzUserRasxod.bot_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                        .Where(p => p.TegirmonHisoblarid == tegirmonQarzUserRasxod.bot_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonQarzUserRasxod.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - (double)tegirmonQarzUserRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonQarzUserRasxod.dollor;
                            }


                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonQarzUserRasxod.bot_id;
                            tegirmon_kassa_info.before_cash = 0;
                            tegirmon_kassa_info.before_dollor = 0;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
                            if (tegirmonQarzUserRasxod.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor - (double)tegirmonQarzUserRasxod.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonQarzUserRasxod.sum;
                            }
                            else
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonQarzUserRasxod.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonQarzUserRasxod.sum;
                            }


                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonQarzUserRasxod.bot_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonQarzUserRasxod.status_rasxod == 0)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - (double)tegirmonQarzUserRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - (double)tegirmonQarzUserRasxod.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonQarzUserRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonQarzUserRasxod.dollor;
                        }


                        _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();

                    }
                }
            }
            else
            {
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==
                if (tegirmonQarzUserRasxod.auth_user_updator_id != 0)
                {
                    var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == tegirmonQarzUserRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                        .Where(p => p.TegirmonKassaid == tegirmonQarzUserRasxod.auth_user_updator_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonQarzUserRasxod.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonQarzUserRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonQarzUserRasxod.dollor;
                            }

                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonKassaid = tegirmonQarzUserRasxod.auth_user_updator_id;
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
                            tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;

                            if (tegirmonQarzUserRasxod.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonQarzUserRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonQarzUserRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonQarzUserRasxod.dollor;
                            }

                            tegirmon_kassa_info.TegirmonKassaid = tegirmonQarzUserRasxod.auth_user_updator_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonQarzUserRasxod.status_rasxod == 1)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonQarzUserRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonQarzUserRasxod.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonQarzUserRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonQarzUserRasxod.dollor;
                        }


                        _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();
                    }
                }
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==


                // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==

                


                TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                tegirmon_kassa_prixod.auth_user_updator_id = tegirmonQarzUserRasxod.auth_user_updator_id;
                tegirmon_kassa_prixod.auth_user_creator_id = tegirmonQarzUserRasxod.auth_user_creator_id;
                tegirmon_kassa_prixod.worker_name = tegirmonQarzUserRasxod.worker_name;
                tegirmon_kassa_prixod.phone_number = tegirmonQarzUserRasxod.phone_number;
                tegirmon_kassa_prixod.dollor = tegirmonQarzUserRasxod.dollor;
                tegirmon_kassa_prixod.dollor_string = tegirmonQarzUserRasxod.dollor_string;
                tegirmon_kassa_prixod.note = tegirmonQarzUserRasxod.note;
                tegirmon_kassa_prixod.addiotionala_information = tegirmonQarzUserRasxod.addiotionala_information;
                tegirmon_kassa_prixod.all_summ = tegirmonQarzUserRasxod.all_summ;
                tegirmon_kassa_prixod.all_summ_string = tegirmonQarzUserRasxod.all_summ_string;
                tegirmon_kassa_prixod.reserve_val_l = tegirmonQarzUserRasxod.TegirmonQarzUserId ?? 0;
                if (tegirmonQarzUserRasxod.status_rasxod == 1)
                {
                    tegirmon_kassa_prixod.status_rasxod = 1;
                    tegirmon_kassa_prixod.prixod = tegirmonQarzUserRasxod.sum;
                    tegirmon_kassa_prixod.image_url = tegirmonQarzUserRasxod.sum_str;
                    tegirmon_kassa_prixod.rasxod = 0;

                }
                else
                {
                    tegirmon_kassa_prixod.status_rasxod = 0;
                    tegirmon_kassa_prixod.rasxod = tegirmonQarzUserRasxod.sum;
                    tegirmon_kassa_prixod.reserve = tegirmonQarzUserRasxod.sum_str;
                    tegirmon_kassa_prixod.prixod = 0;
                }

                _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
            }
            


            var tegirmonQarzUser = await _context.TegirmonQarzdorlikUser
            .FirstOrDefaultAsync(p => p.id == tegirmonQarzUserRasxod.TegirmonQarzUserId);

            if (tegirmonQarzUserRasxod.status_rasxod == 1)
            {
                if (tegirmonQarzUser != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    // 1. biz foydada holimiz uchun kuramiz
                    if (tegirmonQarzUser.sum <= 0 && tegirmonQarzUser.dollor <= 0)
                    {
                        tegirmonQarzUser.sum -= tegirmonQarzUserRasxod.sum;
                        tegirmonQarzUser.dollor -= tegirmonQarzUserRasxod.dollor;
                    }
                    else
                    {
                        sum = (double)tegirmonQarzUser.sum - tegirmonQarzUserRasxod.sum;
                        dollor = (double)tegirmonQarzUser.dollor - tegirmonQarzUserRasxod.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (-1 * (sum / (double)tegirmonQarzUserRasxod.kurs)) - dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * val,0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = -1 * (val * (double)tegirmonQarzUserRasxod.kurs);
                            }
                        }
                        else if (sum > 0 && dollor < 0) // sumga yetmay dollor kup bulganda
                        {
                            val = (-1 * (dollor * (double)tegirmonQarzUserRasxod.kurs)) - sum;
                            if (val <= 0)
                            {
                                dollor = 0;
                                sum = -1 * val;
                            }
                            else
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * (val / (double)tegirmonQarzUserRasxod.kurs),0);
                            }
                        }

                        tegirmonQarzUser.sum = sum;
                        tegirmonQarzUser.dollor = dollor;
                        tegirmonQarzUserRasxod.reserve_val_1 = sum;
                        tegirmonQarzUserRasxod.reserve_val_2 = dollor;
                    }

                    tegirmonQarzUserRasxod.reserve_val_1 = tegirmonQarzUser.sum;
                    tegirmonQarzUserRasxod.reserve_val_2 = tegirmonQarzUser.dollor;

                }

                await _context.SaveChangesAsync();
            }
            else
            {
                if (tegirmonQarzUser != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    //biz minusda bulganimizda
                    if (tegirmonQarzUser.sum >= 0 && tegirmonQarzUser.dollor >= 0)
                    {
                        tegirmonQarzUser.sum += tegirmonQarzUserRasxod.sum;
                        tegirmonQarzUser.dollor += tegirmonQarzUserRasxod.dollor;
                    }
                    else // biz ortiqcha pul berib quyganimizda
                    {


                        sum = (double)tegirmonQarzUser.sum + tegirmonQarzUserRasxod.sum;
                        dollor = (double)tegirmonQarzUser.dollor + tegirmonQarzUserRasxod.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (dollor * (double)tegirmonQarzUserRasxod.kurs) + sum;
                            if (val <= 0)
                            {
                                sum = val;
                                dollor = 0;
                            }
                            else
                            {
                                dollor = Math.Round(val / (double)tegirmonQarzUserRasxod.kurs, 0);
                                sum = 0;
                            }
                        }
                        else if (sum > 0 && dollor < 0)
                        {
                            val = (sum / (double)tegirmonQarzUserRasxod.kurs) + dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(val, 0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = val * (double)tegirmonQarzUserRasxod.kurs;
                            }
                        }

                        tegirmonQarzUser.sum = sum;
                        tegirmonQarzUser.dollor = dollor;


                    }
                    
                    tegirmonQarzUserRasxod.reserve_val_1 = tegirmonQarzUser.sum;
                    tegirmonQarzUserRasxod.reserve_val_2 = tegirmonQarzUser.dollor;
                
                }

                await _context.SaveChangesAsync();
            }
            

            // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==



            return CreatedAtAction("GetTegirmonQarzUserRasxod", new { id = tegirmonQarzUserRasxod.id }, tegirmonQarzUserRasxod);
        }

        // DELETE: api/TegirmonQarzUserRasxod/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonQarzUserRasxod>> DeleteTegirmonQarzUserRasxod(long id)
        {
            var tegirmonQarzUserRasxod = await _context.TegirmonQarzUserRasxod.FindAsync(id);
            if (tegirmonQarzUserRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonQarzUserRasxod.Remove(tegirmonQarzUserRasxod);
            await _context.SaveChangesAsync();

            return tegirmonQarzUserRasxod;
        }

        private bool TegirmonQarzUserRasxodExists(long id)
        {
            return _context.TegirmonQarzUserRasxod.Any(e => e.id == id);
        }
    }
}
