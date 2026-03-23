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
    public class TegirmonCloseController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonCloseController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClose
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonClose>>> GetTegirmonClose()
        {
            return await _context.TegirmonClose.ToListAsync();
        }

        // ---------------------------------------------
        //             SSE LISTENER  (REALTIME)
        // ---------------------------------------------
        [HttpGet("listen")]
        public async Task Listen()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            // Boshlang'ich statusni olish
            bool lastStatus = await _context.TegirmonClose
                .OrderByDescending(x => x.id)
                .Select(x => x.status)
                .FirstOrDefaultAsync();

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(1000);

                var row = await _context.TegirmonClose
                    .OrderByDescending(x => x.id)
                    .FirstOrDefaultAsync();

                if (row == null)
                    continue;

                // status o‘zgargan bo‘lsa — SSE yuboriladi
                if (lastStatus != row.status)
                {
                    lastStatus = row.status;

                    await Response.WriteAsync($"data: {row.status.ToString().ToLower()}\n\n");
                    await Response.Body.FlushAsync();
                }
            }
        }


        [HttpGet("getPaginationDistrict")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationDistrict([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClose> categoryList = await _context.TegirmonClose
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClose>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClose
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonClose/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonClose>> GetTegirmonClose(long id)
        {
            var tegirmonClose = await _context.TegirmonClose.FindAsync(id);

            if (tegirmonClose == null)
            {
                return NotFound();
            }

            return tegirmonClose;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClose> categoryList = await _context.TegirmonClose
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClose>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClose.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonClose/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonClose(long id, TegirmonClose tegirmonClose)
        {
            if (id != tegirmonClose.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonClose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonCloseExists(id))
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

        // POST: api/TegirmonClose
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonClose>> PostTegirmonClose(TegirmonClose tegirmonClose)
        {
            _context.TegirmonClose.Update(tegirmonClose);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonClose", new { id = tegirmonClose.id }, tegirmonClose);
        }

        // DELETE: api/TegirmonClose/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonClose>> DeleteTegirmonClose(long id)
        {
            var tegirmonClose = await _context.TegirmonClose.FindAsync(id);
            if (tegirmonClose == null)
            {
                return NotFound();
            }

            _context.TegirmonClose.Remove(tegirmonClose);
            await _context.SaveChangesAsync();

            return tegirmonClose;
        }

        private bool TegirmonCloseExists(long id)
        {
            return _context.TegirmonClose.Any(e => e.id == id);
        }
    }
}
