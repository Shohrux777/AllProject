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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonKassaMainQoldController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonKassaMainQoldController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonKassaMainQold>>> GetTegirmonKassaMainQold()
        {
            return await _context.TegirmonKassaMainQold.ToListAsync();
        }

        // GET: api/TegirmonWorker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonKassaMainQold>> GetTegirmonKassaMainQold(long id)
        {
            var tegirmonKassaMainQold = await _context.TegirmonKassaMainQold.FindAsync(id);

            if (tegirmonKassaMainQold == null)
            {
                return NotFound();
            }

            return tegirmonKassaMainQold;
        }



        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaMainQold> categoryList = await _context.TegirmonKassaMainQold
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaMainQold>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaMainQold.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationWorkerListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaMainQold> categoryList = await _context.TegirmonKassaMainQold
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaMainQold>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaMainQold.Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonKassaMainQold(long id, TegirmonKassaMainQold tegirmonKassaMainQold)
        {
            if (id != tegirmonKassaMainQold.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonKassaMainQold).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonKassaMainQoldExists(id))
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

        // POST: api/TegirmonClient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonKassaMainQold>> PostTegirmonKassaMainQold(TegirmonKassaMainQold tegirmonKassaMainQold)
        {
            _context.TegirmonKassaMainQold.Update(tegirmonKassaMainQold);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonKassaMainQold", new { id = tegirmonKassaMainQold.id }, tegirmonKassaMainQold);
        }



        // DELETE: api/TegirmonClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonKassaMainQold>> DeleteTegirmonKassaMainQold(long id)
        {
            var tegirmonKassaMainQold = await _context.TegirmonKassaMainQold.FindAsync(id);
            if (tegirmonKassaMainQold == null)
            {
                return NotFound();
            }

            _context.TegirmonKassaMainQold.Remove(tegirmonKassaMainQold);
            await _context.SaveChangesAsync();

            return tegirmonKassaMainQold;
        }

        private bool TegirmonKassaMainQoldExists(long id)
        {
            return _context.TegirmonKassaMainQold.Any(e => e.id == id);
        }

    }
}
