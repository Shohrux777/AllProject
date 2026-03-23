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
    public class TegirmonQarzUserRasxodTypeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonQarzUserRasxodTypeController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonQarzUserRasxodType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonQarzUserRasxodType>>> GetTegirmonQarzUserRasxodType()
        {
            return await _context.TegirmonQarzUserRasxodType.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserRasxodType> categoryList = await _context.TegirmonQarzUserRasxodType
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserRasxodType>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserRasxodType.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserTypeId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserTypeId([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserRasxodType> categoryList = await _context.TegirmonQarzUserRasxodType
                .Where(p => p.active_status == true && p.TegirmonQarzUserTypeId == userid)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserRasxodType>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserRasxodType.Where(p => p.active_status == true &&  p.TegirmonQarzUserTypeId == userid).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonQarzUserRasxodType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonQarzUserRasxodType>> GetTegirmonQarzUserRasxodType(long id)
        {
            var tegirmonQarzUserRasxodType = await _context.TegirmonQarzUserRasxodType.FindAsync(id);

            if (tegirmonQarzUserRasxodType == null)
            {
                return NotFound();
            }

            return tegirmonQarzUserRasxodType;
        }

        // PUT: api/TegirmonQarzUserRasxodType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonQarzUserRasxodType(long id, TegirmonQarzUserRasxodType tegirmonQarzUserRasxodType)
        {
            if (id != tegirmonQarzUserRasxodType.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonQarzUserRasxodType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonQarzUserRasxodTypeExists(id))
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

        // POST: api/TegirmonQarzUserRasxodType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonQarzUserRasxodType>> PostTegirmonQarzUserRasxodType(TegirmonQarzUserRasxodType tegirmonQarzUserRasxodType)
        {
            _context.TegirmonQarzUserRasxodType.Update(tegirmonQarzUserRasxodType);
            await _context.SaveChangesAsync();
            if (tegirmonQarzUserRasxodType.bot_id > 0)
            {
                TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                tegirmon_hisob_prixod.status_rasxod = tegirmonQarzUserRasxodType.status_rasxod;
                tegirmon_hisob_prixod.worker_name = tegirmonQarzUserRasxodType.worker_name;
                tegirmon_hisob_prixod.phone_number = tegirmonQarzUserRasxodType.phone_number;
                tegirmon_hisob_prixod.auth_user_creator_id = tegirmonQarzUserRasxodType.auth_user_creator_id;
                tegirmon_hisob_prixod.note = tegirmonQarzUserRasxodType.note;
                tegirmon_hisob_prixod.addiotionala_information = tegirmonQarzUserRasxodType.addiotionala_information;// bunga kassirni nomini aniqlab yozish kerak;

                tegirmon_hisob_prixod.all_summ = (double)tegirmonQarzUserRasxodType.all_summ;
                tegirmon_hisob_prixod.all_summ_string = tegirmonQarzUserRasxodType.all_summ_string;
                tegirmon_hisob_prixod.dollor = (double)tegirmonQarzUserRasxodType.dollor;
                tegirmon_hisob_prixod.dollor_string = tegirmonQarzUserRasxodType.dollor_string;
                tegirmon_hisob_prixod.kurs = (double)tegirmonQarzUserRasxodType.kurs;
                tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonQarzUserRasxodType.bot_id;

                if (tegirmonQarzUserRasxodType.status_rasxod == 0)
                {
                    tegirmon_hisob_prixod.rasxod = (double)tegirmonQarzUserRasxodType.sum;
                    tegirmon_hisob_prixod.reserve = tegirmonQarzUserRasxodType.sum_str;
                }
                else
                {
                    tegirmon_hisob_prixod.prixod = (double)tegirmonQarzUserRasxodType.sum;
                    tegirmon_hisob_prixod.image_url = tegirmonQarzUserRasxodType.sum_str;
                }

                _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                await _context.SaveChangesAsync();


                if (tegirmonQarzUserRasxodType.bot_id != 0) // hisob tanlanganda hisobga pul tushadi
                {
                    var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                    .Where(p => p.TegirmonHisoblarid == tegirmonQarzUserRasxodType.bot_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                        .Where(p => p.TegirmonHisoblarid == tegirmonQarzUserRasxodType.bot_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonQarzUserRasxodType.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - (double)tegirmonQarzUserRasxodType.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonQarzUserRasxodType.dollor;
                            }


                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonQarzUserRasxodType.bot_id;
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
                            if (tegirmonQarzUserRasxodType.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor - (double)tegirmonQarzUserRasxodType.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonQarzUserRasxodType.sum;
                            }
                            else
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonQarzUserRasxodType.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonQarzUserRasxodType.sum;
                            }


                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonQarzUserRasxodType.bot_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonQarzUserRasxodType.status_rasxod == 0)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - (double)tegirmonQarzUserRasxodType.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - (double)tegirmonQarzUserRasxodType.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonQarzUserRasxodType.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonQarzUserRasxodType.dollor;
                        }


                        _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();

                    }
                }
            }
            else
            {
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==
                if (tegirmonQarzUserRasxodType.auth_user_updator_id != 0)
                {
                    var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == tegirmonQarzUserRasxodType.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                        .Where(p => p.TegirmonKassaid == tegirmonQarzUserRasxodType.auth_user_updator_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonQarzUserRasxodType.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonQarzUserRasxodType.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonQarzUserRasxodType.dollor;
                            }

                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonKassaid = tegirmonQarzUserRasxodType.auth_user_updator_id;
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

                            if (tegirmonQarzUserRasxodType.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonQarzUserRasxodType.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonQarzUserRasxodType.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonQarzUserRasxodType.dollor;
                            }

                            tegirmon_kassa_info.TegirmonKassaid = tegirmonQarzUserRasxodType.auth_user_updator_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonQarzUserRasxodType.status_rasxod == 1)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonQarzUserRasxodType.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonQarzUserRasxodType.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonQarzUserRasxodType.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonQarzUserRasxodType.dollor;
                        }


                        _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();
                    }
                }
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==


                // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==

                


                TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                tegirmon_kassa_prixod.auth_user_updator_id = tegirmonQarzUserRasxodType.auth_user_updator_id;
                tegirmon_kassa_prixod.auth_user_creator_id = tegirmonQarzUserRasxodType.auth_user_creator_id;
                tegirmon_kassa_prixod.worker_name = tegirmonQarzUserRasxodType.worker_name;
                tegirmon_kassa_prixod.phone_number = tegirmonQarzUserRasxodType.phone_number;
                tegirmon_kassa_prixod.dollor = tegirmonQarzUserRasxodType.dollor;
                tegirmon_kassa_prixod.dollor_string = tegirmonQarzUserRasxodType.dollor_string;
                tegirmon_kassa_prixod.note = tegirmonQarzUserRasxodType.note;
                tegirmon_kassa_prixod.addiotionala_information = tegirmonQarzUserRasxodType.addiotionala_information;
                tegirmon_kassa_prixod.all_summ = tegirmonQarzUserRasxodType.all_summ;
                tegirmon_kassa_prixod.all_summ_string = tegirmonQarzUserRasxodType.all_summ_string;
                tegirmon_kassa_prixod.reserve_val_l = tegirmonQarzUserRasxodType.TegirmonQarzUserId ?? 0;
                if (tegirmonQarzUserRasxodType.status_rasxod == 1)
                {
                    tegirmon_kassa_prixod.status_rasxod = 1;
                    tegirmon_kassa_prixod.prixod = tegirmonQarzUserRasxodType.sum;
                    tegirmon_kassa_prixod.image_url = tegirmonQarzUserRasxodType.sum_str;
                    tegirmon_kassa_prixod.rasxod = 0;

                }
                else
                {
                    tegirmon_kassa_prixod.status_rasxod = 0;
                    tegirmon_kassa_prixod.rasxod = tegirmonQarzUserRasxodType.sum;
                    tegirmon_kassa_prixod.reserve = tegirmonQarzUserRasxodType.sum_str;
                    tegirmon_kassa_prixod.prixod = 0;
                }

                _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
            }
            


            var tegirmonQarzUser = await _context.TegirmonQarzUserType
            .FirstOrDefaultAsync(p => p.id == tegirmonQarzUserRasxodType.TegirmonQarzUserTypeId);

            if (tegirmonQarzUserRasxodType.status_rasxod == 1)
            {
                if (tegirmonQarzUser != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    // 1. biz foydada holimiz uchun kuramiz
                    if (tegirmonQarzUser.sum <= 0 && tegirmonQarzUser.dollor <= 0)
                    {
                        tegirmonQarzUser.sum -= tegirmonQarzUserRasxodType.sum;
                        tegirmonQarzUser.dollor -= tegirmonQarzUserRasxodType.dollor;
                    }
                    else
                    {
                        sum = (double)tegirmonQarzUser.sum - tegirmonQarzUserRasxodType.sum;
                        dollor = (double)tegirmonQarzUser.dollor - tegirmonQarzUserRasxodType.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (-1 * (sum / (double)tegirmonQarzUserRasxodType.kurs)) - dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * val,0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = -1 * (val * (double)tegirmonQarzUserRasxodType.kurs);
                            }
                        }
                        else if (sum > 0 && dollor < 0) // sumga yetmay dollor kup bulganda
                        {
                            val = (-1 * (dollor * (double)tegirmonQarzUserRasxodType.kurs)) - sum;
                            if (val <= 0)
                            {
                                dollor = 0;
                                sum = -1 * val;
                            }
                            else
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * (val / (double)tegirmonQarzUserRasxodType.kurs),0);
                            }
                        }

                        tegirmonQarzUser.sum = sum;
                        tegirmonQarzUser.dollor = dollor;
                        tegirmonQarzUserRasxodType.reserve_val_1 = sum;
                        tegirmonQarzUserRasxodType.reserve_val_2 = dollor;
                    }

                    tegirmonQarzUserRasxodType.reserve_val_1 = tegirmonQarzUser.sum;
                    tegirmonQarzUserRasxodType.reserve_val_2 = tegirmonQarzUser.dollor;

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
                        tegirmonQarzUser.sum += tegirmonQarzUserRasxodType.sum;
                        tegirmonQarzUser.dollor += tegirmonQarzUserRasxodType.dollor;
                    }
                    else // biz ortiqcha pul berib quyganimizda
                    {


                        sum = (double)tegirmonQarzUser.sum + tegirmonQarzUserRasxodType.sum;
                        dollor = (double)tegirmonQarzUser.dollor + tegirmonQarzUserRasxodType.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (dollor * (double)tegirmonQarzUserRasxodType.kurs) + sum;
                            if (val <= 0)
                            {
                                sum = val;
                                dollor = 0;
                            }
                            else
                            {
                                dollor = Math.Round(val / (double)tegirmonQarzUserRasxodType.kurs, 0);
                                sum = 0;
                            }
                        }
                        else if (sum > 0 && dollor < 0)
                        {
                            val = (sum / (double)tegirmonQarzUserRasxodType.kurs) + dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(val, 0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = val * (double)tegirmonQarzUserRasxodType.kurs;
                            }
                        }

                        tegirmonQarzUser.sum = sum;
                        tegirmonQarzUser.dollor = dollor;


                    }
                    
                    tegirmonQarzUserRasxodType.reserve_val_1 = tegirmonQarzUser.sum;
                    tegirmonQarzUserRasxodType.reserve_val_2 = tegirmonQarzUser.dollor;
                
                }

                await _context.SaveChangesAsync();
            }
            

            // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==



            return CreatedAtAction("GetTegirmonQarzUserRasxodType", new { id = tegirmonQarzUserRasxodType.id }, tegirmonQarzUserRasxodType);
        }

        // DELETE: api/TegirmonQarzUserRasxodType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonQarzUserRasxodType>> DeleteTegirmonQarzUserRasxodType(long id)
        {
            var tegirmonQarzUserRasxodType = await _context.TegirmonQarzUserRasxodType.FindAsync(id);
            if (tegirmonQarzUserRasxodType == null)
            {
                return NotFound();
            }

            _context.TegirmonQarzUserRasxodType.Remove(tegirmonQarzUserRasxodType);
            await _context.SaveChangesAsync();

            return tegirmonQarzUserRasxodType;
        }

        private bool TegirmonQarzUserRasxodTypeExists(long id)
        {
            return _context.TegirmonQarzUserRasxodType.Any(e => e.id == id);
        }
    }
}
