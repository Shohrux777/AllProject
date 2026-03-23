using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudOyliksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudOyliksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudOyliks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudOylik>>> GetSkudOylik()
        {
            return await _context.SkudOylik.ToListAsync();
        }

        // GET: api/SkudOyliks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudOylik>> GetSkudOylik(long id)
        {
            var skudOylik = await _context.SkudOylik.FindAsync(id);

            if (skudOylik == null)
            {
                return NotFound();
            }

            return skudOylik;
        }


        [HttpGet("getPaginationUsersByOylikId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUsersByOylikId([FromQuery] int page,
            [FromQuery] int size, [FromQuery]long oylik_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.res_badgenumber == oylik_id)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.res_badgenumber == oylik_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUsersByOylikkaBriktrilmagan")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUsersByOylikkaBriktrilmagan([FromQuery] int page,
       [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.res_badgenumber == null)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.res_badgenumber == null).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("delAllByOylikFromUsers")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> delAllByOylikFromUsers([FromQuery] long oylik_id)
        {

            List<SkudMyUserinfo> users_ls = await _context.SkudMyUserinfo.Where(p => p.res_badgenumber == oylik_id).ToListAsync();

            foreach (SkudMyUserinfo it in users_ls)
            {
               
                if (it != null)
                {
                    it.res_badgenumber = null;
                    

                }

            }
            if (users_ls.Count > 0)
            {
                _context.SkudMyUserinfo.UpdateRange(users_ls);
                await _context.SaveChangesAsync();
            }

            return users_ls;
        }

        [HttpGet("delOylikFromUsers")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> delOylikFromUsers([FromQuery] List<long> user_id_list)
        {

            List<SkudMyUserinfo> users_ls = new List<SkudMyUserinfo>();

            foreach (long it in user_id_list)
            {
                var user_inf = await _context.SkudMyUserinfo.FindAsync(it);
                if (user_inf != null)
                {
                    user_inf.res_badgenumber = null;
                    users_ls.Add(user_inf);

                }

            }
            if (users_ls.Count > 0)
            {
                _context.SkudMyUserinfo.UpdateRange(users_ls);
                await _context.SaveChangesAsync();
            }

            return users_ls;
        }

        [HttpGet("addOylikToUsers")]
        public async Task<ActionResult<SkudOylik>> addOylikToUsers([FromQuery]long oylik_id, [FromQuery] List<long> user_id_list)
        {
            var skudOylik = await _context.SkudOylik.FindAsync(oylik_id);
            List<SkudMyUserinfo> users_ls = new List<SkudMyUserinfo>();


            if (skudOylik == null)
            {
                return NotFound();
            }
            foreach (long it in user_id_list) {
                var user_inf = await _context.SkudMyUserinfo.FindAsync(it);
                if (user_inf != null) {
                    user_inf.res_badgenumber = skudOylik.id;
                    users_ls.Add(user_inf);

                }
            
            }
            if (users_ls.Count > 0) {
                _context.SkudMyUserinfo.UpdateRange(users_ls);
                await _context.SaveChangesAsync();
            }




            return skudOylik;
        }

        // PUT: api/SkudOyliks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudOylik(long id, SkudOylik skudOylik)
        {
            if (id != skudOylik.id)
            {
                return BadRequest();
            }

            _context.Entry(skudOylik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudOylikExists(id))
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

        // POST: api/SkudOyliks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudOylik>> PostSkudOylik(SkudOylik skudOylik)
        {
            _context.SkudOylik.Update(skudOylik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudOylik", new { id = skudOylik.id }, skudOylik);
        }

        // DELETE: api/SkudOyliks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudOylik>> DeleteSkudOylik(long id)
        {
            var skudOylik = await _context.SkudOylik.FindAsync(id);
            if (skudOylik == null)
            {
                return NotFound();
            }

            _context.SkudOylik.Remove(skudOylik);
            await _context.SaveChangesAsync();

            return skudOylik;
        }

        private bool SkudOylikExists(long id)
        {
            return _context.SkudOylik.Any(e => e.id == id);
        }
    }
}
