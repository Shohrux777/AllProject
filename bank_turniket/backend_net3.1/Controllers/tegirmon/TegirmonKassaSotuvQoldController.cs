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
    public class TegirmonKassaSotuvQoldController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonKassaSotuvQoldController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonKassaSotuvQold>>> GetTegirmonKassaSotuvQold()
        {
            return await _context.TegirmonKassaSotuvQold.ToListAsync();
        }

        // GET: api/TegirmonWorker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonKassaSotuvQold>> GetTegirmonKassaSotuvQold(long id)
        {
            var tegirmonKassaSotuvQold = await _context.TegirmonKassaSotuvQold.FindAsync(id);

            if (tegirmonKassaSotuvQold == null)
            {
                return NotFound();
            }

            return tegirmonKassaSotuvQold;
        }



        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaSotuvQold> categoryList = await _context.TegirmonKassaSotuvQold
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaSotuvQold>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaSotuvQold.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationWorkerListByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationWorkerListByDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonKassaSotuvQold> categoryList = await _context.TegirmonKassaSotuvQold
                .Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonKassaSotuvQold>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonKassaSotuvQold.Where(p => p.active_status == true && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonKassaSotuvQold(long id, TegirmonKassaSotuvQold tegirmonKassaSotuvQold)
        {
            if (id != tegirmonKassaSotuvQold.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonKassaSotuvQold).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonKassaSotuvQoldExists(id))
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
        public async Task<ActionResult<TegirmonKassaSotuvQold>> PostTegirmonKassaSotuvQold(TegirmonKassaSotuvQold tegirmonKassaSotuvQold)
        {
            _context.TegirmonKassaSotuvQold.Update(tegirmonKassaSotuvQold);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonKassaSotuvQold", new { id = tegirmonKassaSotuvQold.id }, tegirmonKassaSotuvQold);
        }



        // DELETE: api/TegirmonClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonKassaSotuvQold>> DeleteTegirmonKassaSotuvQold(long id)
        {
            var tegirmonKassaSotuvQold = await _context.TegirmonKassaSotuvQold.FindAsync(id);
            if (tegirmonKassaSotuvQold == null)
            {
                return NotFound();
            }

            _context.TegirmonKassaSotuvQold.Remove(tegirmonKassaSotuvQold);
            await _context.SaveChangesAsync();

            return tegirmonKassaSotuvQold;
        }

        private bool TegirmonKassaSotuvQoldExists(long id)
        {
            return _context.TegirmonKassaSotuvQold.Any(e => e.id == id);
        }

    }
}
