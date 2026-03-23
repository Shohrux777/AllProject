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
    public class TegirmonUserHisobAccessController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserHisobAccessController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserHisobAccess>>> GetTegirmonUserHisobAccess()
        {
            return await _context.TegirmonUserHisobAccess.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserHisobAccess> categoryList = await _context.TegirmonUserHisobAccess
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserHisobAccess>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserHisobAccess.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getTegirmonUserHisobAccessUserId")]
        public async Task<ActionResult<List<TegirmonUserHisobAccess>>> getTegirmonUserHisobAccessUserId([FromQuery] long user_id)
        {
            var tegirmonUserHisobAccess = await _context.TegirmonUserHisobAccess
                .Include(p => p.user)
                .Where(p => p.TegirmonUserid == user_id)
                .ToListAsync();

            if (tegirmonUserHisobAccess == null || tegirmonUserHisobAccess.Count == 0)
            {
                return NotFound();
            }

            return tegirmonUserHisobAccess; // ✅ barchasi qaytadi
        }

        
        [HttpGet("getHisoblarByUserId")]
        public async Task<ActionResult<List<TegirmonHisoblar>>> GetHisoblarByUserId([FromQuery] long user_id)
        {
            var hisob = await _context.TegirmonUserHisobAccess
                .Where(p => p.TegirmonUserid == user_id)
                .Select(p => p.hisob) // faqat Sklad obyektini tanlaymiz
                .ToListAsync();

            if (hisob == null || hisob.Count == 0)
            {
                return NotFound("Bu foydalanuvchiga berilgan sklad topilmadi.");
            }

            return hisob; // faqat Sklad ro'yxati qaytadi
        }




        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserHisobAccess>> GetTegirmonUserHisobAccess(long id)
        {
            var tegirmonUserHisobAccess = await _context.TegirmonUserHisobAccess.FindAsync(id);

            if (tegirmonUserHisobAccess == null)
            {
                return NotFound();
            }

            return tegirmonUserHisobAccess;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserHisobAccess(long id, TegirmonUserHisobAccess tegirmonUserHisobAccess)
        {
            if (id != tegirmonUserHisobAccess.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserHisobAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserHisobAccessExists(id))
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

        // POST: api/TegirmonUser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<TegirmonUserHisobAccess>> PostTegirmonUserHisobAccess(TegirmonUserHisobAccess tegirmonUserHisobAccess)
        // {
        //     _context.TegirmonUserHisobAccess.Update(tegirmonUserHisobAccess);
        //     await _context.SaveChangesAsync();
        //     return tegirmonUserHisobAccess;
        // }

        [HttpPost]
        public async Task<ActionResult<TegirmonUserHisobAccess>> PostTegirmonUserHisobAccess(TegirmonUserHisobAccess tegirmonUserHisobAccess)
        {
            // 1️⃣ Tekshirish: bunday User-Sklad juftligi allaqachon mavjudmi
            var exists = await _context.TegirmonUserHisobAccess
                .AnyAsync(x => x.TegirmonUserid == tegirmonUserHisobAccess.TegirmonUserid
                            && x.TegirmonHisoblarId == tegirmonUserHisobAccess.TegirmonHisoblarId);

            if (exists)
            {
                // Bunday ruxsat allaqachon bor
                return BadRequest("Bu foydalanuvchiga ushbu hisobga ruxsat allaqachon berilgan.");
            }

            // 2️⃣ Yangi ruxsatni saqlash
            _context.TegirmonUserHisobAccess.Add(tegirmonUserHisobAccess);
            await _context.SaveChangesAsync();

            return tegirmonUserHisobAccess;
        }

        [HttpDelete("deleteTegirmonUserHisobAccessByUserId")]
        public async Task<IActionResult> DeleteTegirmonUserHisobAccessByUserId([FromQuery] long user_id)
        {
            // 1️⃣ Ushbu user_id ga tegishli barcha ruxsatlarni olish
            var userAccessList = await _context.TegirmonUserHisobAccess
                .Where(x => x.TegirmonUserid == user_id)
                .ToListAsync();

            if (userAccessList == null || userAccessList.Count == 0)
            {
                return NotFound("Bu foydalanuvchiga berilgan sklad ruxsatlari topilmadi.");
            }

            // 2️⃣ O‘chirish
            _context.TegirmonUserHisobAccess.RemoveRange(userAccessList);
            await _context.SaveChangesAsync();

            return Ok($"Foydalanuvchiga berilgan {userAccessList.Count} ta sklad ruxsati o‘chirildi.");
        }

        




        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserHisobAccess>> DeleteTegirmonUserHisobAccess(long id)
        {
            var tegirmonUserHisobAccess = await _context.TegirmonUserHisobAccess.FindAsync(id);
            if (tegirmonUserHisobAccess == null)
            {
                return NotFound();
            }

            _context.TegirmonUserHisobAccess.Remove(tegirmonUserHisobAccess);
            await _context.SaveChangesAsync();

            return tegirmonUserHisobAccess;
        }

        private bool TegirmonUserHisobAccessExists(long id)
        {
            return _context.TegirmonUserHisobAccess.Any(e => e.id == id);
        }
    }
}
