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
    public class TegirmonOrderClientProductProductController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderClientProductProductController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderClientProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderClientProduct>>> GetTegirmonOrderClientProduct()
        {
            return await _context.TegirmonOrderClientProduct.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClientProduct> categoryList = await _context.TegirmonOrderClientProduct
                .Where(p => p.active_status == true)
                .Include(p => p.product)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClientProduct>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClientProduct.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getClientProducts")]
        public async Task<ActionResult<IEnumerable<TegirmonOrderClientProduct>>> GetClientProducts([FromQuery] long client_id)
        {
            if (client_id <= 0)
            {
                return BadRequest("client_id noto‘g‘ri yuborilgan");
            }

            // 🔹 client_id bo‘yicha faqat aktiv mahsulotlarni olish
            var products = await _context.TegirmonOrderClientProduct
                .Where(p => p.active_status == true && p.TegirmonOrderClientId == client_id)
                .Include(p => p.product)
                .ThenInclude(p => p.unitMeasurment)
                .OrderByDescending(p => p.id)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound($"Bu mijoz uchun mahsulot topilmadi (client_id: {client_id})");
            }

            return Ok(products);
        }



        // GET: api/TegirmonOrderClientProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderClientProduct>> GetTegirmonOrderClientProduct(long id)
        {
            var tegirmonOrderClientProduct = await _context.TegirmonOrderClientProduct.FindAsync(id);

            if (tegirmonOrderClientProduct == null)
            {
                return NotFound();
            }

            return tegirmonOrderClientProduct;
        }

        // PUT: api/TegirmonOrderClientProduct/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderClientProduct(long id, TegirmonOrderClientProduct tegirmonOrderClientProduct)
        {
            if (id != tegirmonOrderClientProduct.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderClientProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderClientProductExists(id))
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

        // POST: api/TegirmonOrderClientProduct
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderClientProduct>> PostTegirmonOrderClientProduct(TegirmonOrderClientProduct tegirmonOrderClientProduct)
        {
            _context.TegirmonOrderClientProduct.Update(tegirmonOrderClientProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderClientProduct", new { id = tegirmonOrderClientProduct.id }, tegirmonOrderClientProduct);
        }

        // DELETE: api/TegirmonOrderClientProduct/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderClientProduct>> DeleteTegirmonOrderClientProduct(long id)
        {
            var tegirmonOrderClientProduct = await _context.TegirmonOrderClientProduct.FindAsync(id);
            if (tegirmonOrderClientProduct == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderClientProduct.Remove(tegirmonOrderClientProduct);
            await _context.SaveChangesAsync();

            return tegirmonOrderClientProduct;
        }

        private bool TegirmonOrderClientProductExists(long id)
        {
            return _context.TegirmonOrderClientProduct.Any(e => e.id == id);
        }
    }
}
