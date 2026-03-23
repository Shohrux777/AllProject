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
    public class TegirmonKassaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonKassaController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonKassa>>> GetTegirmonKassa()
        {
            return await _context.TegirmonKassa.ToListAsync();
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassa> categoryList = await _context.TegirmonKassa
                // .Include(p => p.department)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassa>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassa.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationUserId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationUserId([FromQuery] int page, [FromQuery] int size, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassa> categoryList = await _context.TegirmonKassa
                // .Include(p => p.department)
                .Where(p => p.active_status == true && p.TegirmonUserid==user_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassa>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassa.Where(p => p.active_status == true && p.TegirmonUserid==user_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonKassa>> GetTegirmonKassa(long id)
        {
            var tegirmonKassa = await _context.TegirmonKassa.FindAsync(id);

            if (tegirmonKassa == null)
            {
                return NotFound();
            }

            return tegirmonKassa;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonKassa(long id, TegirmonKassa tegirmonKassa)
        {
            if (id != tegirmonKassa.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonKassa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonKassaExists(id))
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
        public async Task<ActionResult<TegirmonKassa>> PostTegirmonKassa(TegirmonKassa tegirmonKassa)
        {
            _context.TegirmonKassa.Update(tegirmonKassa);
            await _context.SaveChangesAsync();
            return tegirmonKassa;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonKassa>> DeleteTegirmonKassa(long id)
        {
            var tegirmonKassa = await _context.TegirmonKassa.FindAsync(id);
            if (tegirmonKassa == null)
            {
                return NotFound();
            }

            _context.TegirmonKassa.Remove(tegirmonKassa);
            await _context.SaveChangesAsync();

            return tegirmonKassa;
        }

        private bool TegirmonKassaExists(long id)
        {
            return _context.TegirmonKassa.Any(e => e.id == id);
        }
    }
}
