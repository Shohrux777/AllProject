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
    public class SkudUserRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUserRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUserRasxod>>> GetSkudUserDayAdd()
        {
            return await _context.SkudUserRasxod.ToListAsync();
        }

        // GET: api/SkudCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUserRasxod>> GetSkudUserRasxod(long id)
        {
            var skudUserRasxod = await _context.SkudUserRasxod.FindAsync(id);

            if (skudUserRasxod == null)
            {
                return NotFound();
            }

            return skudUserRasxod;
        }

        [HttpGet("getUserAddDayUser_ID")]
        public async Task<ActionResult<IEnumerable<SkudUserRasxod>>> getUserAddDayUser_ID([FromQuery] long user_id)
        {

            List<SkudUserRasxod> users_ls = await _context.SkudUserRasxod.Where(p => p.userid == user_id).ToListAsync();
            
            if (users_ls == null)
            {
                return NotFound();
            }

            return users_ls;
        }

        [HttpGet("getPaginationGetByDateTimeUser_add_day")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByDateTimeUser_add_day([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserRasxod> itemList = await _context.SkudUserRasxod
                .Where(p =>p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)   
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserRasxod>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserRasxod.Where(p => p.reg_date >= begin_date.Date && p.reg_date <= end_date.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserRasxod> itemList = await _context.SkudUserRasxod
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserRasxod.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudUserRasxod(long id, SkudUserRasxod skudUserRasxod)
        {
            if (id != skudUserRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(skudUserRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudUserRasxodExists(id))
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

        // POST: api/SkudCompanies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudUserRasxod>> PostSkudUserRasxod(SkudUserRasxod skudUserRasxod)
        {
            _context.SkudUserRasxod.Update(skudUserRasxod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUserRasxod", new { id = skudUserRasxod.id }, skudUserRasxod);
        }

        // DELETE: api/SkudCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudUserRasxod>> DeleteSkudUserRasxod(long id)
        {
            var skudUserRasxod = await _context.SkudUserRasxod.FindAsync(id);
            if (skudUserRasxod == null)
            {
                return NotFound();
            }

            _context.SkudUserRasxod.Remove(skudUserRasxod);
            await _context.SaveChangesAsync();

            return skudUserRasxod;
        }

        private bool SkudUserRasxodExists(long id)
        {
            return _context.SkudUserRasxod.Any(e => e.id == id);
        }
    }
}
