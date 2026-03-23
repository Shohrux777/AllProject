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
    public class SkudUserNoteController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUserNoteController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUserNote>>> GetSkudUserDayAdd()
        {
            return await _context.SkudUserNote.ToListAsync();
        }

        // GET: api/SkudCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUserNote>> GetSkudUserNote(long id)
        {
            var skudUserNote = await _context.SkudUserNote.FindAsync(id);

            if (skudUserNote == null)
            {
                return NotFound();
            }

            return skudUserNote;
        }

        [HttpGet("getUserAddDayUser_ID")]
        public async Task<ActionResult<IEnumerable<SkudUserNote>>> getUserAddDayUser_ID([FromQuery] long user_id)
        {

            List<SkudUserNote> users_ls = await _context.SkudUserNote.Where(p => p.userid == user_id).ToListAsync();
            
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
            List<SkudUserNote> itemList = await _context.SkudUserNote
                .Where(p =>p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)   
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserNote>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserNote.Where(p => p.reg_date >= begin_date.Date && p.reg_date <= end_date.Date).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserNote> itemList = await _context.SkudUserNote
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudUserNote>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserNote.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudUserNote(long id, SkudUserNote skudUserNote)
        {
            if (id != skudUserNote.id)
            {
                return BadRequest();
            }

            _context.Entry(skudUserNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudUserNoteExists(id))
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
        public async Task<ActionResult<SkudUserNote>> PostSkudUserNote(SkudUserNote skudUserNote)
        {
            _context.SkudUserNote.Update(skudUserNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUserNote", new { id = skudUserNote.id }, skudUserNote);
        }

        // DELETE: api/SkudCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudUserNote>> DeleteSkudUserNote(long id)
        {
            var skudUserNote = await _context.SkudUserNote.FindAsync(id);
            if (skudUserNote == null)
            {
                return NotFound();
            }

            _context.SkudUserNote.Remove(skudUserNote);
            await _context.SaveChangesAsync();

            return skudUserNote;
        }

        private bool SkudUserNoteExists(long id)
        {
            return _context.SkudUserNote.Any(e => e.id == id);
        }
    }
}
