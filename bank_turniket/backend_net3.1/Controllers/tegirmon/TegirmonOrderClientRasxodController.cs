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
    public class TegirmonOrderClientRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderClientRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderClientRasxod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderClientRasxod>>> GetTegirmonOrderClientRasxod()
        {
            return await _context.TegirmonOrderClientRasxod.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClientRasxod> categoryList = await _context.TegirmonOrderClientRasxod
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClientRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClientRasxod.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserId([FromQuery] int page, [FromQuery] int size, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClientRasxod> categoryList = await _context.TegirmonOrderClientRasxod
                .Where(p => p.active_status == true && p.TegirmonOrderClientId == userid)
                .OrderByDescending(p => p.id)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClientRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClientRasxod.Where(p => p.active_status == true && p.TegirmonOrderClientId == userid).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchOrderRasxod([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("search parametri kerak.");

            // 1️⃣ Bazadan kerakli ma'lumotlarni olib kelamiz (faqat keraklilarni)
            var query = _context.TegirmonOrderClientRasxod
                .Where(x => EF.Functions.ILike(x.note, $"%{text}%") && x.active_status == true)
                .Include(x => x.orderClient);

            // 2️⃣ Endi bu yerda GroupBy ni EF emas, C# bajaradi
            var list = await query.OrderByDescending(p => p.id).ToListAsync();

            return Ok(list);
        }

        [HttpGet("getPrixodSummary")]
        public async Task<ActionResult<object>> GetPrixodSummary(
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            var start = begin_date.Date;
            var finish = end_date.Date.AddDays(1).AddTicks(-1);
            var result = await _context.TegirmonOrderClientRasxod
                .Where(x => x.status_rasxod == 1 && x.created_date >= start && x.created_date <= finish)
                .GroupBy(x => 1) // bitta umumiy natija olish uchun
                .Select(g => new
                {
                    summa_total = g.Sum(x => x.sum),
                    dollor_total = g.Sum(x => x.dollor)
                })
                .FirstOrDefaultAsync();

            // Agar hech narsa topilmasa 0 qaytarish uchun
            if (result == null)
            {
                result = new { summa_total = 0.0, dollor_total = 0.0 };
            }

            return Ok(result);
        }


        // GET: api/TegirmonOrderClientRasxod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderClientRasxod>> GetTegirmonOrderClientRasxod(long id)
        {
            var tegirmonOrderClientRasxod = await _context.TegirmonOrderClientRasxod.FindAsync(id);

            if (tegirmonOrderClientRasxod == null)
            {
                return NotFound();
            }
            return tegirmonOrderClientRasxod;
        }

        // PUT: api/TegirmonOrderClientRasxod/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderClientRasxod(long id, TegirmonOrderClientRasxod tegirmonOrderClientRasxod)
        {
            if (id != tegirmonOrderClientRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderClientRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderClientRasxodExists(id))
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

        // POST: api/TegirmonOrderClientRasxod
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderClientRasxod>> PostTegirmonOrderClientRasxod(TegirmonOrderClientRasxod tegirmonOrderClientRasxod)
        {
            if (tegirmonOrderClientRasxod.hisob_dollor == null)
            {
                tegirmonOrderClientRasxod.hisob_dollor = 0;
            }
            if (tegirmonOrderClientRasxod.hisob_sum == null)
            {
                tegirmonOrderClientRasxod.hisob_sum = 0;
            }
            
            _context.TegirmonOrderClientRasxod.Update(tegirmonOrderClientRasxod);
            // await _context.SaveChangesAsync();

            // bu bulimni klientlarni qushib oldin bergan pullarini qushib olsak keyin ochamiz
            var tegirmonOrderClient = await _context.TegirmonOrderClient
                .FindAsync(tegirmonOrderClientRasxod.TegirmonOrderClientId);
            String client_name = "";
            String phone_number = "";
            if (tegirmonOrderClient != null)
            {
                client_name = tegirmonOrderClient.fio;
                phone_number = tegirmonOrderClient.phone_number;
            }

            if (tegirmonOrderClientRasxod.bot_id > 0)
            {
                TegirmonHisoblarRasxod tegirmon_hisob_prixod = new TegirmonHisoblarRasxod();
                tegirmon_hisob_prixod.status_rasxod = tegirmonOrderClientRasxod.status_rasxod;
                tegirmon_hisob_prixod.worker_name = tegirmonOrderClientRasxod.client_name;
                tegirmon_hisob_prixod.phone_number = phone_number;
                tegirmon_hisob_prixod.auth_user_creator_id = tegirmonOrderClientRasxod.auth_user_creator_id;
                tegirmon_hisob_prixod.note = tegirmonOrderClientRasxod.note;
                tegirmon_hisob_prixod.addiotionala_information = tegirmonOrderClientRasxod.addiotionala_information; // bunga kassirni nomini aniqlab yozish kerak;

                tegirmon_hisob_prixod.all_summ = (double)tegirmonOrderClientRasxod.all_summ;
                tegirmon_hisob_prixod.all_summ_string = tegirmonOrderClientRasxod.all_summ_string;
                tegirmon_hisob_prixod.dollor = (double)tegirmonOrderClientRasxod.dollor;
                tegirmon_hisob_prixod.dollor_string = tegirmonOrderClientRasxod.dollor_string;
                tegirmon_hisob_prixod.kurs = (double)tegirmonOrderClientRasxod.kurs;
                tegirmon_hisob_prixod.TegirmonHisoblarid = (long)tegirmonOrderClientRasxod.bot_id;

                if (tegirmonOrderClientRasxod.status_rasxod == 0)
                {
                    tegirmon_hisob_prixod.rasxod = (double)tegirmonOrderClientRasxod.sum;
                    tegirmon_hisob_prixod.reserve = tegirmonOrderClientRasxod.sum_str;
                }
                else
                {
                    tegirmon_hisob_prixod.prixod = (double)tegirmonOrderClientRasxod.sum;
                    tegirmon_hisob_prixod.image_url = tegirmonOrderClientRasxod.sum_str;
                }

                _context.TegirmonHisoblarRasxod.Update(tegirmon_hisob_prixod);
                await _context.SaveChangesAsync();


                if (tegirmonOrderClientRasxod.bot_id != 0)
                {
                    var tegirmonKassaInfo = await _context.TegirmonHisoblarInfo
                    .Where(p => p.TegirmonHisoblarid == tegirmonOrderClientRasxod.bot_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonHisoblarInfo
                        .Where(p => p.TegirmonHisoblarid == tegirmonOrderClientRasxod.bot_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonHisoblarInfo tegirmon_kassa_info = new TegirmonHisoblarInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonOrderClientRasxod.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - (double)tegirmonOrderClientRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + (double)tegirmonOrderClientRasxod.dollor;
                            }


                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderClientRasxod.bot_id;
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
                            if (tegirmonOrderClientRasxod.status_rasxod == 0)
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor - (double)tegirmonOrderClientRasxod.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - (double)tegirmonOrderClientRasxod.sum;
                            }
                            else
                            {
                                tegirmon_kassa_info.dollor = tegirmonKassaInfoLast.dollor + (double)tegirmonOrderClientRasxod.dollor;
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + (double)tegirmonOrderClientRasxod.sum;
                            }


                            tegirmon_kassa_info.TegirmonHisoblarid = tegirmonOrderClientRasxod.bot_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonHisoblarInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonOrderClientRasxod.status_rasxod == 0)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - (double)tegirmonOrderClientRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - (double)tegirmonOrderClientRasxod.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + (double)tegirmonOrderClientRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + (double)tegirmonOrderClientRasxod.dollor;
                        }


                        _context.TegirmonHisoblarInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();

                    }
                }
            }
            else
            {
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==
                if (tegirmonOrderClientRasxod.auth_user_updator_id != 0)
                {
                    var tegirmonKassaInfo = await _context.TegirmonKassaInfo
                    .Where(p => p.TegirmonKassaid == tegirmonOrderClientRasxod.auth_user_updator_id && p.created_date_time >= DateTime.Now.Date).ToListAsync();

                    if (tegirmonKassaInfo == null || tegirmonKassaInfo.Count == 0)
                    {
                        // bu bugungi kunga dalni qushilmay qolgan bulsa
                        var tegirmonKassaInfoLast = await _context.TegirmonKassaInfo
                        .Where(p => p.TegirmonKassaid == tegirmonOrderClientRasxod.auth_user_updator_id)
                        .OrderByDescending(p => p.created_date_time)
                        .FirstOrDefaultAsync();

                        if (tegirmonKassaInfoLast == null)
                        {
                            // bu kassaga umuman malumot qushilmagan yangi yaratib yuborish kerak
                            TegirmonKassaInfo tegirmon_kassa_info = new TegirmonKassaInfo();
                            tegirmon_kassa_info.cash = 0;
                            tegirmon_kassa_info.dollor = 0;
                            if (tegirmonOrderClientRasxod.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderClientRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonOrderClientRasxod.dollor;
                            }

                            // tegirmon_kassa_info.id = 0;
                            tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderClientRasxod.auth_user_updator_id;
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

                            if (tegirmonOrderClientRasxod.status_rasxod == 1)
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash + tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor + tegirmonOrderClientRasxod.dollor;
                            }
                            else
                            {
                                tegirmon_kassa_info.cash = tegirmon_kassa_info.cash - tegirmonOrderClientRasxod.sum;
                                tegirmon_kassa_info.dollor = tegirmon_kassa_info.dollor - tegirmonOrderClientRasxod.dollor;
                            }

                            tegirmon_kassa_info.TegirmonKassaid = tegirmonOrderClientRasxod.auth_user_updator_id;
                            tegirmon_kassa_info.before_cash = tegirmonKassaInfoLast.cash;
                            tegirmon_kassa_info.before_dollor = tegirmonKassaInfoLast.dollor;
                            _context.TegirmonKassaInfo.Update(tegirmon_kassa_info);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (tegirmonOrderClientRasxod.status_rasxod == 1)
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash + tegirmonOrderClientRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor + tegirmonOrderClientRasxod.dollor;
                        }
                        else
                        {
                            tegirmonKassaInfo.Last().cash = tegirmonKassaInfo.Last().cash - tegirmonOrderClientRasxod.sum;
                            tegirmonKassaInfo.Last().dollor = tegirmonKassaInfo.Last().dollor - tegirmonOrderClientRasxod.dollor;
                        }
                        
                        _context.TegirmonKassaInfo.Update(tegirmonKassaInfo.Last());
                        await _context.SaveChangesAsync();
                    }
                }
                // ==> kassadan pul ayirish va qushish qismi buldi kassirni hisobidan xodimga pul berganda kassadan ayiradi olganda kassaga qushadi <==


                // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==




                TegirmonUserRasxod tegirmon_kassa_prixod = new TegirmonUserRasxod();
                tegirmon_kassa_prixod.auth_user_updator_id = tegirmonOrderClientRasxod.auth_user_updator_id;
                tegirmon_kassa_prixod.auth_user_creator_id = tegirmonOrderClientRasxod.auth_user_creator_id;
                tegirmon_kassa_prixod.worker_name = tegirmonOrderClientRasxod.client_name;
                tegirmon_kassa_prixod.phone_number = phone_number;
                tegirmon_kassa_prixod.dollor = tegirmonOrderClientRasxod.dollor;
                tegirmon_kassa_prixod.dollor_string = tegirmonOrderClientRasxod.dollor_string;
                tegirmon_kassa_prixod.note = tegirmonOrderClientRasxod.note;
                tegirmon_kassa_prixod.addiotionala_information = tegirmonOrderClientRasxod.addiotionala_information;
                tegirmon_kassa_prixod.all_summ = tegirmonOrderClientRasxod.all_summ;
                tegirmon_kassa_prixod.all_summ_string = tegirmonOrderClientRasxod.all_summ_string;
                tegirmon_kassa_prixod.reserve_val_l = tegirmonOrderClientRasxod.TegirmonOrderClientId;
                tegirmon_kassa_prixod.reserve_val = 3; // optom savdoni bilish uchun
                if (tegirmonOrderClientRasxod.status_rasxod == 1)
                {
                    tegirmon_kassa_prixod.status_rasxod = 1;
                    tegirmon_kassa_prixod.prixod = tegirmonOrderClientRasxod.sum;
                    tegirmon_kassa_prixod.image_url = tegirmonOrderClientRasxod.sum_str;
                    tegirmon_kassa_prixod.rasxod = 0;
                }
                else
                {
                    tegirmon_kassa_prixod.status_rasxod = 0;
                    tegirmon_kassa_prixod.rasxod = tegirmonOrderClientRasxod.sum;
                    tegirmon_kassa_prixod.reserve = tegirmonOrderClientRasxod.sum_str;
                    tegirmon_kassa_prixod.prixod = 0;
                }
                _context.TegirmonUserRasxod.Update(tegirmon_kassa_prixod);
            }

            // bu bulimni klientlarni qushib oldin bergan pullarini qushib olsak keyin ochamiz

            var tegirmonContragent = await _context.TegirmonOrderClient
            .FirstOrDefaultAsync(p => p.id == tegirmonOrderClientRasxod.TegirmonOrderClientId);

            if (tegirmonOrderClientRasxod.status_rasxod == 0) // prixod qilganimiz uchun
            {
                if (tegirmonContragent != null)
                {
                    double sum = 0.0;
                    double dollor = 0.0;
                    double val = 0.0;
                    // 1. biz foydada holimiz uchun kuramiz
                    if (tegirmonContragent.sum <= 0 && tegirmonContragent.dollor <= 0)
                    {
                        tegirmonContragent.sum -= (double)tegirmonOrderClientRasxod.hisob_sum;
                        tegirmonContragent.dollor -= (double)tegirmonOrderClientRasxod.hisob_dollor;
                    }
                    else
                    {
                        sum = (double)tegirmonContragent.sum - (double)tegirmonOrderClientRasxod.hisob_sum;
                        dollor = (double)tegirmonContragent.dollor - (double)tegirmonOrderClientRasxod.hisob_dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (-1 * (sum / (double)tegirmonOrderClientRasxod.kurs)) - dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * val,0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = -1 * (val * (double)tegirmonOrderClientRasxod.kurs);
                            }
                        }
                        else if (sum > 0 && dollor < 0) // sumga yetmay dollor kup bulganda
                        {
                            val = (-1 * (dollor * (double)tegirmonOrderClientRasxod.kurs)) - sum;
                            if (val <= 0)
                            {
                                dollor = 0;
                                sum = -1 * val;
                            }
                            else
                            {
                                sum = 0;
                                dollor = Math.Round(-1 * (val / (double)tegirmonOrderClientRasxod.kurs),0);
                            }
                        }

                        tegirmonContragent.sum = sum;
                        tegirmonContragent.dollor = dollor;
                        
                    }

                        tegirmonOrderClientRasxod.reserve_val_1 = (double)tegirmonContragent.sum;
                        tegirmonOrderClientRasxod.reserve_val_2 = (double)tegirmonContragent.dollor;


                    // tegirmonContragent.sum -= (double)tegirmonOrderClientRasxod.hisob_sum; // bu qisimda xodim pul qaytarga qarzidan ayirish qismi
                    // tegirmonContragent.dollor -= (double)tegirmonOrderClientRasxod.hisob_dollor;

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
                        
                        tegirmonContragent.sum += (double)tegirmonOrderClientRasxod.hisob_sum;
                        tegirmonContragent.dollor += (double)tegirmonOrderClientRasxod.hisob_dollor;
                    }
                    else // biz ortiqcha pul berib quyganimizda
                    {
                        

                        sum = (double)tegirmonContragent.sum + (double)tegirmonOrderClientRasxod.hisob_sum;
                        dollor = (double)tegirmonContragent.dollor + (double)tegirmonOrderClientRasxod.hisob_dollor;

                        if (sum < 0 && dollor > 0)
                        {
                            val = (dollor * (double)tegirmonOrderClientRasxod.kurs) + sum;
                            if (val <= 0)
                            {
                                sum = val;
                                dollor = 0;
                            }
                            else
                            {
                                dollor = Math.Round(val / (double)tegirmonOrderClientRasxod.kurs, 0);
                                sum = 0;
                            }
                        }
                        else if (sum > 0 && dollor < 0)
                        {
                            val = (sum / (double)tegirmonOrderClientRasxod.kurs) + dollor;
                            if (val <= 0)
                            {
                                sum = 0;
                                dollor = Math.Round(val, 0);
                            }
                            else
                            {
                                dollor = 0;
                                sum = val * (double)tegirmonOrderClientRasxod.kurs;
                            }
                        }

                        tegirmonContragent.sum = sum;
                        tegirmonContragent.dollor = dollor;
                        

                    }
                        tegirmonOrderClientRasxod.reserve_val_1 = (double)tegirmonContragent.sum;
                        tegirmonOrderClientRasxod.reserve_val_2 = (double)tegirmonContragent.dollor;
                    // tegirmonContragent.sum += (double)tegirmonOrderClientRasxod.hisob_sum; // bu qisimda xodimga qarz berganda hisobiga yozib quyadi
                    // tegirmonContragent.dollor += (double)tegirmonOrderClientRasxod.hisob_dollor;
                }

                await _context.SaveChangesAsync();
            }
            

            // ==> user Rasxodga qushib quyish ejidenniy ochotda chiqishi uchun <==



            return CreatedAtAction("GetTegirmonOrderClientRasxod", new { id = tegirmonOrderClientRasxod.id }, tegirmonOrderClientRasxod);
        }

        // DELETE: api/TegirmonOrderClientRasxod/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderClientRasxod>> DeleteTegirmonOrderClientRasxod(long id)
        {
            var tegirmonOrderClientRasxod = await _context.TegirmonOrderClientRasxod.FindAsync(id);
            if (tegirmonOrderClientRasxod == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderClientRasxod.Remove(tegirmonOrderClientRasxod);
            await _context.SaveChangesAsync();

            return tegirmonOrderClientRasxod;
        }

        private bool TegirmonOrderClientRasxodExists(long id)
        {
            return _context.TegirmonOrderClientRasxod.Any(e => e.id == id);
        }
    }
}
