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
    public class TegirmonOrderStatusController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderStatusController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderStatus>>> GetTegirmonOrderStatus()
        {
            return await _context.TegirmonOrderStatus.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderStatus> categoryList = await _context.TegirmonOrderStatus
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderStatus>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderStatus
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderStatus>> GetTegirmonOrderStatus(long id)
        {
            var tegirmonOrderStatus = await _context.TegirmonOrderStatus.FindAsync(id);

            if (tegirmonOrderStatus == null)
            {
                return NotFound();
            }

            return tegirmonOrderStatus;
        }

        // PUT: api/TegirmonOrderStatus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderStatus(long id, TegirmonOrderStatus tegirmonOrderStatus)
        {
            if (id != tegirmonOrderStatus.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderStatusExists(id))
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

        // POST: api/TegirmonOrderStatus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderStatus>> PostTegirmonOrderStatus(TegirmonOrderStatus tegirmonOrderStatus)
        {
            _context.TegirmonOrderStatus.Update(tegirmonOrderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderStatus", new { id = tegirmonOrderStatus.id }, tegirmonOrderStatus);
        }

        // DELETE: api/TegirmonOrderStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderStatus>> DeleteTegirmonOrderStatus(long id)
        {
            var tegirmonOrderStatus = await _context.TegirmonOrderStatus.FindAsync(id);
            if (tegirmonOrderStatus == null)
            {
                return NotFound();
            }
            _context.TegirmonOrderStatus.Remove(tegirmonOrderStatus);
            await _context.SaveChangesAsync();

            return tegirmonOrderStatus;
        }

        private bool TegirmonOrderStatusExists(long id)
        {
            return _context.TegirmonOrderStatus.Any(e => e.id == id);
        }
    }
}
