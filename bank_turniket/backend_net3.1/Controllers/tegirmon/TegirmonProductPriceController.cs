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
    public class TegirmonProductPriceController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonProductPriceController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonProductPrice>>> GetTegirmonProductPrice()
        {
            return await _context.TegirmonProductPrice.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonProductPrice> categoryList = await _context.TegirmonProductPrice
                .Where(p => p.active_status == true )
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonProductPrice>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonProductPrice
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonProductPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonProductPrice>> GetTegirmonProductPrice(long id)
        {
            var tegirmonProductPrice = await _context.TegirmonProductPrice.FindAsync(id);

            if (tegirmonProductPrice == null)
            {
                return NotFound();
            }

            return tegirmonProductPrice;
        }

        // PUT: api/TegirmonProductPrice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonProductPrice(long id, TegirmonProductPrice tegirmonProductPrice)
        {
            if (id != tegirmonProductPrice.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonProductPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonProductPriceExists(id))
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

        // POST: api/TegirmonProductPrice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonProductPrice>> PostTegirmonProductPrice(TegirmonProductPrice tegirmonProductPrice)
        {
            _context.TegirmonProductPrice.Update(tegirmonProductPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonProductPrice", new { id = tegirmonProductPrice.id }, tegirmonProductPrice);
        }

        // DELETE: api/TegirmonProductPrice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonProductPrice>> DeleteTegirmonProductPrice(long id)
        {
            var tegirmonProductPrice = await _context.TegirmonProductPrice.FindAsync(id);
            if (tegirmonProductPrice == null)
            {
                return NotFound();
            }

            _context.TegirmonProductPrice.Remove(tegirmonProductPrice);
            await _context.SaveChangesAsync();

            return tegirmonProductPrice;
        }

        private bool TegirmonProductPriceExists(long id)
        {
            return _context.TegirmonProductPrice.Any(e => e.id == id);
        }
    }
}
