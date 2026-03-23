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
    public class TegirmonContragentRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonContragentRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonContragentRasxod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonContragentRasxod>>> GetTegirmonContragentRasxod()
        {
            return await _context.TegirmonContragentRasxod.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonContragentRasxod> categoryList = await _context.TegirmonContragentRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonContragentRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonContragentRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserId([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonContragentRasxod> categoryList = await _context.TegirmonContragentRasxod
                .Where(p => p.active_status == true && p.TegirmonContragentId == userid)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonContragentRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonContragentRasxod.Where(p => p.active_status == true && p.TegirmonContragentId == userid).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonContragentRasxod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonContragentRasxod>> GetTegirmonContragentRasxod(long id)
        {
            var tegirmonContragentRasxod = await _context.TegirmonContragentRasxod.FindAsync(id);

            if (tegirmonContragentRasxod == null)
            {
                return NotFound();
            }

            return tegirmonContragentRasxod;
        }

        // PUT: api/TegirmonContragentRasxod/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonContragentRasxod(long id, TegirmonContragentRasxod tegirmonContragentRasxod)
        {
            if (id != tegirmonContragentRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonContragentRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonContragentRasxodExists(id))
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

        // POST: api/TegirmonContragentRasxod
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonContragentRasxod>> PostTegirmonContragentRasxod(TegirmonContragentRasxod tegirmonContragentRasxod)
        {
            _context.TegirmonContragentRasxod.Update(tegirmonContragentRasxod);
            // await _context.SaveChangesAsync();
            
            // kassadan va hisobdan ayirishni uchirib quydim ishlatib tekshirib agar tugri ishlasa qushamiz
            
            // if (tegirmonContragentRasxod.bot_id > 0)
            // {
            //     TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
            //     tegirmon_hisob_prixod.status_rasxod = tegirmonContragentRasxod.status_rasxod;
            //     tegirmon_hisob_prixod.worker_name = tegirmonContragentRasxod.worker_name;
            //     tegirmon_hisob_prixod.phone_number = tegirmonContragentRasxod.phone_number;
            //     tegirmon_hisob_prixod.auth_user_creator_id = tegirmonContragentRasxod.auth_user_creator_id;
            //     tegirmon_hisob_prixod.note = tegirmonContragentRasxod.note;
            //     tegirmon_hisob_prixod.addiotionala_information = tegirmonContragentRasxod.addiotionala_information;// bunga kassirni nomini aniqlab yozish kerak;

            //     tegirmon_hisob_prixod.all_summ = (double)tegirmonContragentRasxod.all_summ;
            //     tegirmon_hisob_prixod.all_summ_string = tegirmonContragentRasxod.all_summ_string;
            //     tegirmon_hisob_prixod.dollor = (double)tegirmonContragentRasxod.dollor;
            //     tegirmon_hisob_prixod.dollor_string = tegirmonContragentRasxod.dollor_string;
            //     tegirmon_hisob_prixod.kurs = (double)tegirmonContragentRasxod.kurs;
            //     tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonContragentRasxod.bot_id;

            //     if (tegirmonContragentRasxod.status_rasxod == 0)
            //     {
            //         tegirmon_hisob_prixod.rasxod = (double)tegirmonContragentRasxod.sum;
            //         tegirmon_hisob_prixod.reserve = tegirmonContragentRasxod.sum_str;
            //     }
            //     else
            //     {
            //         tegirmon_hisob_prixod.prixod = (double)tegirmonContragentRasxod.sum;
            //         tegirmon_hisob_prixod.image_url = tegirmonContragentRasxod.sum_str;
            //     }

            //     _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
            //     await _context.SaveChangesAsync();


            //     if (tegirmonContragentRasxod.bot_id != 0)
            //     {
            //         var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
            //         .Where(p => p.TegirmonHisoblarid == tegirmonContragentRasxod.bot_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

            //         if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
            //         {
            //             // bu bugungi kunga dalni qushilmay qolgan bulsa
            //             var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
            //             .Where(p => p.TegirmonHisoblarid == tegirmonContragentRasxod.bot_id)
            //             .OrderByDescending(p => p.created_date_time)
            //             .FirstOrDefaultAsync();

            //             if (tegirmonKassaInfoLast == null)
            //             {
            //                 // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
            //                 TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
            //                 tegirmon_kassa_info.cash = 0;
            //                 tegirmon_kassa_info.dollor = 0;
            //                 if (tegirmonContragentRasxod.status_rasxod == 0)
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - (double)tegirmonContragentRasxod.dollor;
            //                 }
            //                 else
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonContragentRasxod.dollor;
            //                 }


            //                 // tegirmon_kassa_info.id = 0;
            //                 tegirmon_kassa_info.TegirmonHisoblarid = tegirmonContragentRasxod.bot_id;
            //                 tegirmon_kassa_info.before_cash = 0;
            //                 tegirmon_kassa_info.before_dollor = 0;
            //                 _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
            //                 await _context.SaveChangesAsync();
            //             }
            //             else
            //             {
            //                 // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
            //                 TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
            //                 tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
            //                 if (tegirmonContragentRasxod.status_rasxod == 0)
            //                 {
            //                     tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor - (double)tegirmonContragentRasxod.dollor;
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonContragentRasxod.sum;
            //                 }
            //                 else
            //                 {
            //                     tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonContragentRasxod.dollor;
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonContragentRasxod.sum;
            //                 }


            //                 tegirmon_kassa_info.TegirmonHisoblarid = tegirmonContragentRasxod.bot_id;
            //                 tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
            //                 tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
            //                 _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
            //                 await _context.SaveChangesAsync();

            //             }
            //         }
            //         else
            //         {
            //             if (tegirmonContragentRasxod.status_rasxod == 0)
            //             {
            //                 tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - (double)tegirmonContragentRasxod.sum;
            //                 tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - (double)tegirmonContragentRasxod.dollor;
            //             }
            //             else
            //             {
            //                 tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonContragentRasxod.sum;
            //                 tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonContragentRasxod.dollor;
            //             }


            //             _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
            //             await _context.SaveChangesAsync();

            //         }
            //     }
            // }
            // else
            // {
            //     // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==
            //     if (tegirmonContragentRasxod.auth_user_updator_id != 0)
            //     {
            //         var tegirmonKassaInfo = await _context.TegirmonKassaInfo
            //         .Where(p => p.TegirmonKassaid == tegirmonContragentRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

            //         if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
            //         {
            //             // bu bugungi kunga dalni qushilmay qolgan bulsa
            //             var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
            //             .Where(p => p.TegirmonKassaid == tegirmonContragentRasxod.auth_user_updator_id)
            //             .OrderByDescending(p => p.created_date_time)
            //             .FirstOrDefaultAsync();

            //             if (tegirmonKassaInfoLast == null)
            //             {
            //                 // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
            //                 TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
            //                 tegirmon_kassa_info.cash = 0;
            //                 tegirmon_kassa_info.dollor = 0;
            //                 if (tegirmonContragentRasxod.status_rasxod == 1)
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonContragentRasxod.dollor;
            //                 }
            //                 else
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonContragentRasxod.dollor;
            //                 }

            //                 // tegirmon_kassa_info.id = 0;
            //                 tegirmon_kassa_info.TegirmonKassaid = tegirmonContragentRasxod.auth_user_updator_id;
            //                 tegirmon_kassa_info.before_cash = 0;
            //                 tegirmon_kassa_info.before_dollor = 0;
            //                 _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
            //                 await _context.SaveChangesAsync();
            //             }
            //             else
            //             {
            //                 // bu kassani oxirgi malumoti bor bugungi kunga yangi malumot qushib eski malumotin before cashga yozish kerak
            //                 TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
            //                 tegirmon_kassa_info.cash = tegirmonKassaInfoLast.cash;
            //                 tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor;

            //                 if (tegirmonContragentRasxod.status_rasxod == 1)
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonContragentRasxod.dollor;
            //                 }
            //                 else
            //                 {
            //                     tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonContragentRasxod.sum;
            //                     tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonContragentRasxod.dollor;
            //                 }

            //                 tegirmon_kassa_info.TegirmonKassaid = tegirmonContragentRasxod.auth_user_updator_id;
            //                 tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
            //                 tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
            //                 _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
            //                 await _context.SaveChangesAsync();

            //             }
            //         }
            //         else
            //         {
            //             if (tegirmonContragentRasxod.status_rasxod == 1)
            //             {
            //                 tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonContragentRasxod.sum;
            //                 tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonContragentRasxod.dollor;
            //             }
            //             else
            //             {
            //                 tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonContragentRasxod.sum;
            //                 tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonContragentRasxod.dollor;
            //             }


            //             _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
            //             await _context.SaveChangesAsync();
            //         }
            //     }
            //     // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==


            //     // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==




            //     TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
            //     tegirmon_kassa_prixod.auth_user_updator_id = tegirmonContragentRasxod.auth_user_updator_id;
            //     tegirmon_kassa_prixod.auth_user_creator_id = tegirmonContragentRasxod.auth_user_creator_id;
            //     tegirmon_kassa_prixod.worker_name = tegirmonContragentRasxod.worker_name;
            //     tegirmon_kassa_prixod.phone_number = tegirmonContragentRasxod.phone_number;
            //     tegirmon_kassa_prixod.dollor = tegirmonContragentRasxod.dollor;
            //     tegirmon_kassa_prixod.dollor_string = tegirmonContragentRasxod.dollor_string;
            //     tegirmon_kassa_prixod.note = tegirmonContragentRasxod.note;
            //     tegirmon_kassa_prixod.addiotionala_information = tegirmonContragentRasxod.addiotionala_information;
            //     tegirmon_kassa_prixod.all_summ = tegirmonContragentRasxod.all_summ;
            //     tegirmon_kassa_prixod.all_summ_string = tegirmonContragentRasxod.all_summ_string;
            //     tegirmon_kassa_prixod.reserve_val_l = tegirmonContragentRasxod.TegirmonContragentId ?? 0;
            //     if (tegirmonContragentRasxod.status_rasxod == 1)
            //     {
            //         tegirmon_kassa_prixod.status_rasxod = 1;
            //         tegirmon_kassa_prixod.prixod = tegirmonContragentRasxod.sum;
            //         tegirmon_kassa_prixod.image_url = tegirmonContragentRasxod.sum_str;
            //         tegirmon_kassa_prixod.rasxod = 0;

            //     }
            //     else
            //     {
            //         tegirmon_kassa_prixod.status_rasxod = 0;
            //         tegirmon_kassa_prixod.rasxod = tegirmonContragentRasxod.sum;
            //         tegirmon_kassa_prixod.reserve = tegirmonContragentRasxod.sum_str;
            //         tegirmon_kassa_prixod.prixod = 0;
            //     }

            //     _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
            // }



            var tegirmonContragent = await _context.TegirmonContragent
            .FirstOrDefaultAsync(p => p.id == tegirmonContragentRasxod.TegirmonContragentId);

            if (tegirmonContragentRasxod.status_rasxod == 1) // prixod qilganimiz uchun
            {
                if (tegirmonContragent != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    // 1. biz foydada holimiz uchun kuramiz
                    if (tegirmonContragent.sum <= 0 && tegirmonContragent.dollor <= 0)
                    {
                        tegirmonContragent.sum -= tegirmonContragentRasxod.sum;
                        tegirmonContragent.dollor -= tegirmonContragentRasxod.dollor;
                    }
                    else
                    {
                        sum = (double)tegirmonContragent.sum - tegirmonContragentRasxod.sum;
                        dollor = (double)tegirmonContragent.dollor - tegirmonContragentRasxod.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (-1 * (sum / (double)tegirmonContragentRasxod.kurs)) - dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * val,0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = -1 * (val * (double)tegirmonContragentRasxod.kurs);
                            }
                        }
                        else if (sum > 0 && dollor < 0) // sumga yetmay dollor kup bulganda
                        {
                            val = (-1 * (dollor * (double)tegirmonContragentRasxod.kurs)) - sum;
                            if (val <= 0)
                            {
                                dollor = 0;
                                sum = -1 * val;
                            }
                            else
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * (val / (double)tegirmonContragentRasxod.kurs),0);
                            }
                        }

                        tegirmonContragent.sum = sum;
                        tegirmonContragent.dollor = dollor;
                        
                    }

                        tegirmonContragentRasxod.reserve_val_1 = (double)tegirmonContragent.sum;
                        tegirmonContragentRasxod.reserve_val_2 = (double)tegirmonContragent.dollor;


                    // tegirmonContragent.sum -= tegirmonContragentRasxod.sum; // bu qisimda xodim pul qaytarga qarzidan ayirish qismi
                    // tegirmonContragent.dollor -= tegirmonContragentRasxod.dollor;

                }

                await _context.SaveChangesAsync();
            }
            else // rasxod
            {
                
                if (tegirmonContragent != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    //biz minusda bulganimizda
                    if (tegirmonContragent.sum >= 0 && tegirmonContragent.dollor >= 0)
                    {
                        
                        tegirmonContragent.sum += tegirmonContragentRasxod.sum;
                        tegirmonContragent.dollor += tegirmonContragentRasxod.dollor;
                    }
                    else // biz ortiqcha pul berib quyganimizda
                    {
                        

                        sum = (double)tegirmonContragent.sum + tegirmonContragentRasxod.sum;
                        dollor = (double)tegirmonContragent.dollor + tegirmonContragentRasxod.dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (dollor * (double)tegirmonContragentRasxod.kurs) + sum;
                            if (val <= 0)
                            {
                                sum = val;
                                dollor = 0;
                            }
                            else
                            {
                                dollor = Math.Round(val / (double)tegirmonContragentRasxod.kurs, 0);
                                sum = 0;
                            }
                        }
                        else if (sum > 0 && dollor < 0)
                        {
                            val = (sum / (double)tegirmonContragentRasxod.kurs) + dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(val, 0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = val * (double)tegirmonContragentRasxod.kurs;
                            }
                        }

                        tegirmonContragent.sum = sum;
                        tegirmonContragent.dollor = dollor;
                        

                    }
                        tegirmonContragentRasxod.reserve_val_1 = (double)tegirmonContragent.sum;
                        tegirmonContragentRasxod.reserve_val_2 = (double)tegirmonContragent.dollor;
                    // tegirmonContragent.sum += tegirmonContragentRasxod.sum; // bu qisimda xodimga qarz berganda hisobiga yozib quyadi
                    // tegirmonContragent.dollor += tegirmonContragentRasxod.dollor;
                }

                await _context.SaveChangesAsync();
            }
            

            // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==



            return CreatedAtAction("GetTegirmonContragentRasxod", new { id = tegirmonContragentRasxod.id }, tegirmonContragentRasxod);
        }

        // DELETE: api/TegirmonContragentRasxod/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonContragentRasxod>> DeleteTegirmonContragentRasxod(long id)
        {
            var tegirmonContragentRasxod = await _context.TegirmonContragentRasxod.FindAsync(id);
            if (tegirmonContragentRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonContragentRasxod.Remove(tegirmonContragentRasxod);
            await _context.SaveChangesAsync();

            return tegirmonContragentRasxod;
        }

        private bool TegirmonContragentRasxodExists(long id)
        {
            return _context.TegirmonContragentRasxod.Any(e => e.id == id);
        }
    }
}
