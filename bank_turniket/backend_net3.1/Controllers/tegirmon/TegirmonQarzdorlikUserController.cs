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
    public class TegirmonQarzdorlikUserController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonQarzdorlikUserController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonQarzdorlikUser/getById?id=1
        [HttpGet("getById")]
        public async Task<ActionResult<TegirmonQarzdorlikUser>> GetById([FromQuery] long id)
        {
            var user = await _context.TegirmonQarzdorlikUser
                .Where(u => u.id == id && u.active_status == true)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/TegirmonQarzdorlikUser/getByUserFaceId?user_face_id=1
        [HttpGet("getByUserFaceId")]
        public async Task<ActionResult<object>> GetByUserFaceId([FromQuery] int user_face_id)
        {
            var user = await _context.TegirmonQarzdorlikUser
                .Where(u => u.user_face_id == user_face_id && u.active_status == true)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            // TegirmonQarzUserType jadvalidan TegirmonQarzUserId orqali sum va dollor larni yig'ish
            var userTypes = await _context.TegirmonQarzUserType
                .Where(ut => ut.TegirmonQarzUserId.HasValue && ut.TegirmonQarzUserId.Value == user.id && (ut.sum > 0 || ut.dollor > 0))
                .ToListAsync();

            // TegirmonQarzUserType dan sum va dollor larni yig'ish
            var totalTypeSum = userTypes.Sum(ut => ut.sum);
            var totalTypeDollor = userTypes.Sum(ut => ut.dollor);

            // TegirmonQarzdorlikUser dagi sum va dollor ga qo'shish
            var totalSum = user.sum + totalTypeSum;
            var totalDollor = user.dollor + totalTypeDollor;

            return new
            {
                id = user.id,
                fio = user.fio,
                passport_number = user.passport_number,
                phone_number = user.phone_number,
                home_phone_number = user.home_phone_number,
                adddress = user.adddress,
                image_url = user.image_url,
                addiotionala_information = user.addiotionala_information,
                user_face_id = user.user_face_id,
                sum = totalSum,
                dollor = totalDollor,
                created_date = user.created_date,
                active_status = user.active_status
            };
        }

        // GET: api/TegirmonQarzdorlikUser/getMainDebtByUserFaceId?user_face_id=1
        [HttpGet("getMainDebtByUserFaceId")]
        public async Task<ActionResult<object>> GetMainDebtByUserFaceId([FromQuery] int user_face_id)
        {
            var user = await _context.TegirmonQarzdorlikUser
                .Where(u => u.user_face_id == user_face_id && u.active_status == true)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            // Faqat TegirmonQarzdorlikUser ning o'zidagi sum va dollor ni qaytarish (TegirmonQarzUserType dan qo'shilganlarini emas)
            return new
            {
                id = user.id,
                fio = user.fio,
                passport_number = user.passport_number,
                phone_number = user.phone_number,
                home_phone_number = user.home_phone_number,
                adddress = user.adddress,
                image_url = user.image_url,
                addiotionala_information = user.addiotionala_information,
                user_face_id = user.user_face_id,
                sum = user.sum,
                dollor = user.dollor,
                created_date = user.created_date,
                active_status = user.active_status
            };
        }

        // GET: api/TegirmonQarzdorlikUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonQarzdorlikUser>>> GetTegirmonQarzdorlikUser()
        {
            return await _context.TegirmonQarzdorlikUser.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzdorlikUser> categoryList = await _context.TegirmonQarzdorlikUser
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzdorlikUser>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzdorlikUser.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getAllSortedByDebt")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllSortedByDebt()
        {
            var users = await _context.TegirmonQarzdorlikUser
                .Where(p => p.active_status == true)
                .ToListAsync();

            // TegirmonQarzUserType dan har bir user uchun sum va dollor ni yig'ish
            var userTypes = await _context.TegirmonQarzUserType
                .Where(ut => ut.TegirmonQarzUserId.HasValue && (ut.sum > 0 || ut.dollor > 0))
                .GroupBy(ut => ut.TegirmonQarzUserId.Value)
                .Select(g => new
                {
                    TegirmonQarzUserId = g.Key,
                    TotalSum = g.Sum(x => x.sum),
                    TotalDollor = g.Sum(x => x.dollor)
                })
                .ToListAsync();

            var userTypeDict = userTypes.ToDictionary(x => x.TegirmonQarzUserId, x => x);

            // Dollar kursi
            const double dollarKurs = 12000;

            // Har bir user uchun tegirmon_qarz_user dan sum/dollor va tegirmon_qarz_user_type dan qo'shilgan sum/dollor ni birlashtirish
            var result = users
                .Select(user =>
                {
                    var userTypeTotal = userTypeDict.ContainsKey(user.id) 
                        ? userTypeDict[user.id] 
                        : null;

                    var totalSum = user.sum + (userTypeTotal?.TotalSum ?? 0);
                    var totalDollor = user.dollor + (userTypeTotal?.TotalDollor ?? 0);

                    // Faqat sum yoki dollor 0 dan katta bo'lgan userlarni qaytarish
                    if (totalSum <= 0 && totalDollor <= 0)
                        return null;

                    // Umumiy qarz (sum + dollar kursiga ko'paytirilgan)
                    var totalDebt = totalSum + (totalDollor * dollarKurs);

                    return new
                    {
                        id = user.id,
                        fio = user.fio,
                        passport_number = user.passport_number,
                        addiotionala_information = user.addiotionala_information,
                        phone_number = user.phone_number,
                        home_phone_number = user.home_phone_number,
                        adddress = user.adddress,
                        image_url = user.image_url,
                        user_face_id = user.user_face_id,
                        sum = totalSum,
                        dollor = totalDollor,
                        totalDebt = totalDebt, // Sortirovka uchun
                        created_date = user.created_date,
                        active_status = user.active_status
                    };
                })
                .Where(x => x != null)
                .OrderByDescending(p => p.totalDebt)  // Qarzi ko'p bo'lganlar birinchi (umumiy qarz bo'yicha)
                .ThenByDescending(p => p.id)          // Oxirida id bo'yicha
                .ToList();

            return Ok(result);
        }

        [HttpGet("getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber([FromQuery] int page, [FromQuery] int size,[FromQuery] String fio_or_serial_number)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzdorlikUser> categoryList = new List<TegirmonQarzdorlikUser>();
            if (fio_or_serial_number.Trim().Length > 0)
            {
                categoryList = await _context.TegirmonQarzdorlikUser
                .Where(p => p.active_status == true &&
                (p.fio.ToLower().Contains(fio_or_serial_number.ToLower())
                || p.passport_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.phone_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.home_phone_number.ToLower().Contains(fio_or_serial_number.ToLower())))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }
            else {
                categoryList = await _context.TegirmonQarzdorlikUser
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }


            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzdorlikUser>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = categoryList.Count();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


         [HttpGet("getPaginationByBornDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBornDate([FromQuery] int page, [FromQuery] int size,[FromQuery] String born_date_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzdorlikUser> categoryList = await _context.TegirmonQarzdorlikUser
                .Where(p => p.active_status == true
                && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower()))
                
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzdorlikUser>();
            }

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzdorlikUser.Where(p => p.active_status == true 
            && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination_user_vaqrOraligida")]
        public async Task<ActionResult<TexPaginationModel>> getPagination_user_vaqrOraligida([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzdorlikUser> categoryList = await _context.TegirmonQarzdorlikUser
                .Where(p => p.id == userid && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzdorlikUser>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzdorlikUser.Where(p => p.id == userid && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        // GET: api/TegirmonQarzdorlikUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonQarzdorlikUser>> GetTegirmonQarzdorlikUser(long id)
        {
            var tegirmonQarzdorlikUser = await _context.TegirmonQarzdorlikUser.FindAsync(id);

            if (tegirmonQarzdorlikUser == null)
            {
                return NotFound();
            }

            return tegirmonQarzdorlikUser;
        }

        // PUT: api/TegirmonQarzdorlikUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonQarzdorlikUser(long id, TegirmonQarzdorlikUser tegirmonQarzdorlikUser)
        {
            if (id != tegirmonQarzdorlikUser.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonQarzdorlikUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonQarzdorlikUserExists(id))
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

        [HttpPost]
        public async Task<ActionResult<TegirmonQarzdorlikUser>> PostTegirmonQarzdorlikUser(TegirmonQarzdorlikUser tegirmonQarzdorlikUser)
        {
            _context.TegirmonQarzdorlikUser.Update(tegirmonQarzdorlikUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonQarzdorlikUser", new { id = tegirmonQarzdorlikUser.id }, tegirmonQarzdorlikUser);
        }


        [HttpPost("transfer-from-skud")]
        public async Task<IActionResult> TransferFromSkud()
        {
            // SkudMyUserinfo dagi barcha foydalanuvchilarni olish
            var skudUsers = await _context.SkudMyUserinfo.ToListAsync();

            if (!skudUsers.Any())
            {
                return NotFound("SkudMyUserinfo jadvalida foydalanuvchi topilmadi.");
            }

            // Faqat yangi foydalanuvchilarni ajratib olish
            var newUsers = skudUsers
                .Where(s => !_context.TegirmonQarzdorlikUser
                    .Any(t => t.user_face_id == s.userid)) // userid bo‘yicha tekshirish
                .Select(s => new TegirmonQarzdorlikUser
                {
                    fio = s.ism,
                    phone_number = s.phone_number,
                    addiotionala_information = s.born_date,
                    passport_number = s.passport,
                    adddress = s.address,
                    image_url = s.image_url,
                    created_date = DateTime.Now,
                    sum = 0,
                    dollor = 0,
                    // addiotionala_information = s.addiotionala_information,
                    user_face_id = s.userid.HasValue ? Convert.ToInt32(s.userid.Value) : 0// bu yerda face_id sifatida yozib qo‘yamiz
                })
                .ToList();

            if (!newUsers.Any())
            {
                return Ok("Yangi foydalanuvchi topilmadi, hammasi allaqachon mavjud.");
            }

            // Bazaga qo‘shish
            await _context.TegirmonQarzdorlikUser.AddRangeAsync(newUsers);
            await _context.SaveChangesAsync();

            return Ok($"{newUsers.Count} ta yangi foydalanuvchi muvaffaqiyatli qo‘shildi.");
        }

        // POST: api/TegirmonUserIshlaganVaqt
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<TegirmonUserYuqlama>> PostTegirmonUserYuqlama(TegirmonUserYuqlama tegirmonUserYuqlama)
        // {
        //     _context.TegirmonUserYuqlama.Update(tegirmonUserYuqlama);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTegirmonUserYuqlama", new { id = tegirmonUserYuqlama.id }, tegirmonUserYuqlama);
        // }


        // DELETE: api/TegirmonUserIshlaganVaqt/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonQarzdorlikUser>> DeleteTegirmonQarzdorlikUser(long id)
        {
            var tegirmonQarzdorlikUser = await _context.TegirmonQarzdorlikUser.FindAsync(id);
            if (tegirmonQarzdorlikUser == null)
            {
                return NotFound();
            }

            _context.TegirmonQarzdorlikUser.Remove(tegirmonQarzdorlikUser);
            await _context.SaveChangesAsync();

            return tegirmonQarzdorlikUser;
        }

        private bool TegirmonQarzdorlikUserExists(long id)
        {
            return _context.TegirmonQarzdorlikUser.Any(e => e.id == id);
        }
    }
}
