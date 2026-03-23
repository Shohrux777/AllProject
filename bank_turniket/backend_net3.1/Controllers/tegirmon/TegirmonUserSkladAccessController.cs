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
    public class TegirmonUserSkladAccessController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserSkladAccessController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserSkladAccess>>> GetTegirmonUserSkladAccess()
        {
            return await _context.TegirmonUserSkladAccess.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonUserSkladAccess> categoryList = await _context.TegirmonUserSkladAccess
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonUserSkladAccess>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonUserSkladAccess.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getTegirmonUserSkladAccessUserId")]
        public async Task<ActionResult<List<TegirmonUserSkladAccess>>> getTegirmonUserSkladAccessUserId([FromQuery] long user_id)
        {
            var tegirmonUserSkladAccess = await _context.TegirmonUserSkladAccess
                .Include(p => p.user)
                .Where(p => p.TegirmonUserid == user_id)
                .ToListAsync();

            if (tegirmonUserSkladAccess == null || tegirmonUserSkladAccess.Count == 0)
            {
                return NotFound();
            }

            return tegirmonUserSkladAccess; // ✅ barchasi qaytadi
        }

        
        [HttpGet("getSkladsByUserId")]
        public async Task<ActionResult<List<TegirmonSklad>>> GetSkladsByUserId([FromQuery] long user_id)
        {
            var sklads = await _context.TegirmonUserSkladAccess
                .Where(p => p.TegirmonUserid == user_id)
                .Select(p => p.sklad) // faqat Sklad obyektini tanlaymiz
                .ToListAsync();

            if (sklads == null || sklads.Count == 0)
            {
                return NotFound("Bu foydalanuvchiga berilgan sklad topilmadi.");
            }

            return sklads; // faqat Sklad ro'yxati qaytadi
        }




        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserSkladAccess>> GetTegirmonUserSkladAccess(long id)
        {
            var tegirmonUserSkladAccess = await _context.TegirmonUserSkladAccess.FindAsync(id);

            if (tegirmonUserSkladAccess == null)
            {
                return NotFound();
            }

            return tegirmonUserSkladAccess;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserSkladAccess(long id, TegirmonUserSkladAccess tegirmonUserSkladAccess)
        {
            if (id != tegirmonUserSkladAccess.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserSkladAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserSkladAccessExists(id))
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
        // public async Task<ActionResult<TegirmonUserSkladAccess>> PostTegirmonUserSkladAccess(TegirmonUserSkladAccess tegirmonUserSkladAccess)
        // {
        //     _context.TegirmonUserSkladAccess.Update(tegirmonUserSkladAccess);
        //     await _context.SaveChangesAsync();
        //     return tegirmonUserSkladAccess;
        // }

        [HttpPost]
        public async Task<ActionResult<TegirmonUserSkladAccess>> PostTegirmonUserSkladAccess(TegirmonUserSkladAccess tegirmonUserSkladAccess)
        {
            // 1️⃣ Tekshirish: bunday User-Sklad juftligi allaqachon mavjudmi
            var exists = await _context.TegirmonUserSkladAccess
                .AnyAsync(x => x.TegirmonUserid == tegirmonUserSkladAccess.TegirmonUserid
                            && x.TegirmonSkladId == tegirmonUserSkladAccess.TegirmonSkladId);

            if (exists)
            {
                // Bunday ruxsat allaqachon bor
                return BadRequest("Bu foydalanuvchiga ushbu skladga ruxsat allaqachon berilgan.");
            }

            // 2️⃣ Yangi ruxsatni saqlash
            _context.TegirmonUserSkladAccess.Add(tegirmonUserSkladAccess);
            await _context.SaveChangesAsync();

            return tegirmonUserSkladAccess;
        }

        [HttpDelete("deleteTegirmonUserSkladAccessByUserId")]
        public async Task<IActionResult> DeleteTegirmonUserSkladAccessByUserId([FromQuery] long user_id)
        {
            // 1️⃣ Ushbu user_id ga tegishli barcha ruxsatlarni olish
            var userAccessList = await _context.TegirmonUserSkladAccess
                .Where(x => x.TegirmonUserid == user_id)
                .ToListAsync();

            if (userAccessList == null || userAccessList.Count == 0)
            {
                return NotFound("Bu foydalanuvchiga berilgan sklad ruxsatlari topilmadi.");
            }

            // 2️⃣ O‘chirish
            _context.TegirmonUserSkladAccess.RemoveRange(userAccessList);
            await _context.SaveChangesAsync();

            return Ok($"Foydalanuvchiga berilgan {userAccessList.Count} ta sklad ruxsati o‘chirildi.");
        }

        




        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserSkladAccess>> DeleteTegirmonUserSkladAccess(long id)
        {
            var tegirmonUserSkladAccess = await _context.TegirmonUserSkladAccess.FindAsync(id);
            if (tegirmonUserSkladAccess == null)
            {
                return NotFound();
            }

            _context.TegirmonUserSkladAccess.Remove(tegirmonUserSkladAccess);
            await _context.SaveChangesAsync();

            return tegirmonUserSkladAccess;
        }

        private bool TegirmonUserSkladAccessExists(long id)
        {
            return _context.TegirmonUserSkladAccess.Any(e => e.id == id);
        }
    }
}
