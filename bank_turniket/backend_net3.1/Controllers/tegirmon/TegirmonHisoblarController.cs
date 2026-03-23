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
    public class TegirmonHisoblarController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonHisoblarController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonHisoblar>>> GetTegirmonHisoblar()
        {
            return await _context.TegirmonHisoblar.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblar> categoryList = await _context.TegirmonHisoblar
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblar.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserId([FromQuery] int page, [FromQuery] int size, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonHisoblar> categoryList = await _context.TegirmonHisoblar
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonHisoblar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonHisoblar.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonHisoblar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonHisoblar>> GetTegirmonHisoblar(long id)
        {
            var tegirmonHisoblar = await _context.TegirmonHisoblar.FindAsync(id);

            if (tegirmonHisoblar == null)
            {
                return NotFound();
            }

            return tegirmonHisoblar;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonHisoblar(long id, TegirmonHisoblar tegirmonHisoblar)
        {
            if (id != tegirmonHisoblar.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonHisoblar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonHisoblarExists(id))
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
        [HttpPost]
        public async Task<ActionResult<TegirmonHisoblar>> PostTegirmonHisoblar(TegirmonHisoblar tegirmonHisoblar)
        {
            _context.TegirmonHisoblar.Update(tegirmonHisoblar);
            await _context.SaveChangesAsync();
            return tegirmonHisoblar;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonHisoblar>> DeleteTegirmonHisoblar(long id)
        {
            var tegirmonHisoblar = await _context.TegirmonHisoblar.FindAsync(id);
            if (tegirmonHisoblar == null)
            {
                return NotFound();
            }

            _context.TegirmonHisoblar.Remove(tegirmonHisoblar);
            await _context.SaveChangesAsync();

            return tegirmonHisoblar;
        }

        private bool TegirmonHisoblarExists(long id)
        {
            return _context.TegirmonHisoblar.Any(e => e.id == id);
        }
    }
}
