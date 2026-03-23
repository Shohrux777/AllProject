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
    public class SkudUserDayAddController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUserDayAddController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUserDayAdd>>> GetSkudUserDayAdd()
        {
            return await _context.SkudUserDayAdd.ToListAsync();
        }

        // GET: api/SkudCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUserDayAdd>> GetSkudUserDayAdd(long id)
        {
            var skudUserDayAdd = await _context.SkudUserDayAdd.FindAsync(id);

            if (skudUserDayAdd == null)
            {
                return NotFound();
            }

            return skudUserDayAdd;
        }

        [HttpGet("getUserAddDayUser_ID")]
        public async Task<ActionResult<IEnumerable<SkudUserDayAdd>>> getUserAddDayUser_ID([FromQuery] long user_id)
        {

            List<SkudUserDayAdd> users_ls = await _context.SkudUserDayAdd.Where(p => p.userid == user_id).ToListAsync();
            
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
            List<SkudUserDayAdd> itemList = await _context.SkudUserDayAdd
                .Where(p =>p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)   
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserDayAdd>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserDayAdd.Where(p => p.reg_date >= begin_date.Date && p.reg_date <= end_date.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserDayAdd> itemList = await _context.SkudUserDayAdd
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserDayAdd>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserDayAdd.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudUserDayAdd(long id, SkudUserDayAdd skudUserDayAdd)
        {
            if (id != skudUserDayAdd.id)
            {
                return BadRequest();
            }

            _context.Entry(skudUserDayAdd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudUserDayAddExists(id))
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
        public async Task<ActionResult<SkudUserDayAdd>> PostSkudUserDayAdd(SkudUserDayAdd skudUserDayAdd)
        {
            _context.SkudUserDayAdd.Update(skudUserDayAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUserDayAdd", new { id = skudUserDayAdd.id }, skudUserDayAdd);
        }

        // DELETE: api/SkudCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudUserDayAdd>> DeleteSkudUserDayAdd(long id)
        {
            var skudUserDayAdd = await _context.SkudUserDayAdd.FindAsync(id);
            if (skudUserDayAdd == null)
            {
                return NotFound();
            }

            _context.SkudUserDayAdd.Remove(skudUserDayAdd);
            await _context.SaveChangesAsync();

            return skudUserDayAdd;
        }

        private bool SkudUserDayAddExists(long id)
        {
            return _context.SkudUserDayAdd.Any(e => e.id == id);
        }
    }
}
