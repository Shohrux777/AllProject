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
    public class SkudUserAvtoRasxodController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUserAvtoRasxodController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUserAvtoRasxod>>> GetSkudUserDayAdd()
        {
            return await _context.SkudUserAvtoRasxod.ToListAsync();
        }

        // GET: api/SkudCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUserAvtoRasxod>> GetSkudUserAvtoRasxod(long id)
        {
            var skudUserAvtoRasxod = await _context.SkudUserAvtoRasxod.FindAsync(id);

            if (skudUserAvtoRasxod == null)
            {
                return NotFound();
            }

            return skudUserAvtoRasxod;
        }

        [HttpGet("getUserAddDayUser_ID")]
        public async Task<ActionResult<IEnumerable<SkudUserAvtoRasxod>>> getUserAddDayUser_ID([FromQuery] long user_id)
        {

            List<SkudUserAvtoRasxod> users_ls = await _context.SkudUserAvtoRasxod.Where(p => p.userid == user_id).ToListAsync();
            
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
            List<SkudUserAvtoRasxod> itemList = await _context.SkudUserAvtoRasxod
                .Where(p =>p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)   
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserAvtoRasxod>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserAvtoRasxod.Where(p => p.reg_date >= begin_date.Date && p.reg_date <= end_date.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserAvtoRasxod> itemList = await _context.SkudUserAvtoRasxod
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserAvtoRasxod>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserAvtoRasxod.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudUserAvtoRasxod(long id, SkudUserAvtoRasxod skudUserAvtoRasxod)
        {
            if (id != skudUserAvtoRasxod.id)
            {
                return BadRequest();
            }

            _context.Entry(skudUserAvtoRasxod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudUserAvtoRasxodExists(id))
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
        public async Task<ActionResult<SkudUserAvtoRasxod>> PostSkudUserAvtoRasxod(SkudUserAvtoRasxod skudUserAvtoRasxod)
        {
            _context.SkudUserAvtoRasxod.Update(skudUserAvtoRasxod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUserAvtoRasxod", new { id = skudUserAvtoRasxod.id }, skudUserAvtoRasxod);
        }

        // DELETE: api/SkudCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudUserAvtoRasxod>> DeleteSkudUserAvtoRasxod(long id)
        {
            var skudUserAvtoRasxod = await _context.SkudUserAvtoRasxod.FindAsync(id);
            if (skudUserAvtoRasxod == null)
            {
                return NotFound();
            }

            _context.SkudUserAvtoRasxod.Remove(skudUserAvtoRasxod);
            await _context.SaveChangesAsync();

            return skudUserAvtoRasxod;
        }

        private bool SkudUserAvtoRasxodExists(long id)
        {
            return _context.SkudUserAvtoRasxod.Any(e => e.id == id);
        }
    }
}
