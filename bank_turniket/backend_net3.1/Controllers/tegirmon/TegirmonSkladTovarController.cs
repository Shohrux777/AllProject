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
    public class TegirmonSkladTovarController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonSkladTovarController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonSkladTovar>>> GetTegirmonSkladTovar()
        {
            return await _context.TegirmonSkladTovar.ToListAsync();
        }

        [HttpGet("getProductsBySklad/{skladId}")]
        public async Task<ActionResult<IEnumerable<TegirmonProduct>>> GetProductsBySklad(long skladId)
        {
            var products = await _context.TegirmonSkladTovar
                .Where(x => x.TegirmonSkladId == skladId)
                .Include(x => x.Product)                // Productni yuklash
                    .ThenInclude(p => p.unitMeasurment) // Product ichidagi UnitMeasurment
                .Include(x => x.Product)
                    .ThenInclude(p => p.ProductPrices)  // Product ichidagi ProductPrices
                .Select(x => x.Product)              // Endi faqat Productni olib chiqamiz
                .OrderBy(p => p.auth_user_updator_id)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound("Bu skladda mahsulot yo‘q.");
            }

            return Ok(products);
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size, [FromQuery] int sklad_id )
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladTovar> categoryList = await _context.TegirmonSkladTovar
                // .Include(p => p.department)
                .Include(p => p.Product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true && p.TegirmonSkladId == sklad_id)
                .OrderBy(p => p.Product.auth_user_updator_id)
                .Skip(page * size).Take(size).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladTovar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSkladTovar.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOstatkaProduct_Id")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOstatkaProduct_Id([FromQuery] int page, [FromQuery] int size, [FromQuery] long product_id,  [FromQuery] long sklad_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladTovar> categoryList = await _context.TegirmonSkladTovar
                // .Include(p => p.department)
                .Include(p => p.Product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true && p.TegirmonProductId == product_id && p.TegirmonSkladId == sklad_id)
                .OrderBy(p => p.Product.auth_user_updator_id)
                .Skip(page * size).Take(size).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladTovar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonSkladTovar.Where(p => p.active_status == true && p.TegirmonProductId == product_id && p.TegirmonSkladId == sklad_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationSearchProductByNameOrCodeOrShtrixCode")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchProductByNameOrCodeOrShtrixCode([FromQuery] int page, [FromQuery] int size,[FromQuery] String name_o_code_or_shtrix, [FromQuery] long sklad_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonSkladTovar> categoryList = new List<TegirmonSkladTovar>();
            if (name_o_code_or_shtrix.Trim().Length > 0) {
                categoryList = await _context.TegirmonSkladTovar
               .Include(p => p.Product)
               .Where(p => p.active_status == true && p.real_qty > 0 && p.TegirmonSkladId == sklad_id
               && (p.Product.name.ToLower().Contains(name_o_code_or_shtrix.ToLower())
               || p.Product.code.ToLower().Contains(name_o_code_or_shtrix.ToLower())
               || p.Product.shitrix_code.ToLower().Contains(name_o_code_or_shtrix.ToLower())))
               .OrderBy(p => p.Product.auth_user_updator_id)
               .Skip(page * size).Take(size).ToListAsync();
            }
            else {
                categoryList = await _context.TegirmonSkladTovar
                .Include(p => p.Product)
                .Where(p => p.active_status == true && p.real_qty > 0 && p.TegirmonSkladId == sklad_id)
                .OrderBy(p => p.Product.auth_user_updator_id)
                .Skip(page * size).Take(size).ToListAsync();
            }


            if (categoryList == null)
            {
                categoryList = new List<TegirmonSkladTovar>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = categoryList.Count();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonKassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonSkladTovar>> GetTegirmonSkladTovar(long id)
        {
            var tegirmonSkladTovar = await _context.TegirmonSkladTovar.FindAsync(id);

            if (tegirmonSkladTovar == null)
            {
                return NotFound();
            }

            return tegirmonSkladTovar;
        }

        // PUT: api/TegirmonUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonSkladTovar(long id, TegirmonSkladTovar tegirmonSkladTovar)
        {
            if (id != tegirmonSkladTovar.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonSkladTovar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonSkladTovarExists(id))
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
        public async Task<ActionResult<TegirmonSkladTovar>> PostTegirmonSkladTovar(TegirmonSkladTovar tegirmonSkladTovar)
        {
            _context.TegirmonSkladTovar.Update(tegirmonSkladTovar);
            await _context.SaveChangesAsync();
            return tegirmonSkladTovar;
        }

        // DELETE: api/TegirmonUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonSkladTovar>> DeleteTegirmonSkladTovar(long id)
        {
            var tegirmonSkladTovar = await _context.TegirmonSkladTovar.FindAsync(id);
            if (tegirmonSkladTovar == null)
            {
                return NotFound();
            }

            _context.TegirmonSkladTovar.Remove(tegirmonSkladTovar);
            await _context.SaveChangesAsync();

            return tegirmonSkladTovar;
        }

        private bool TegirmonSkladTovarExists(long id)
        {
            return _context.TegirmonSkladTovar.Any(e => e.id == id);
        }
    }
}
