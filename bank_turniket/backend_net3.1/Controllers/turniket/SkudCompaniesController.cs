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
    public class SkudCompaniesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudCompaniesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudCompany>>> GetSkudCompany()
        {
            return await _context.SkudCompany.ToListAsync();
        }

        // GET: api/SkudCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudCompany>> GetSkudCompany(long id)
        {
            var skudCompany = await _context.SkudCompany.FindAsync(id);

            if (skudCompany == null)
            {
                return NotFound();
            }

            return skudCompany;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudCompany> itemList = await _context.SkudCompany
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudCompany>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudCompany.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudCompany(long id, SkudCompany skudCompany)
        {
            if (id != skudCompany.id)
            {
                return BadRequest();
            }

            _context.Entry(skudCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudCompanyExists(id))
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
        public async Task<ActionResult<SkudCompany>> PostSkudCompany(SkudCompany skudCompany)
        {
            _context.SkudCompany.Update(skudCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudCompany", new { id = skudCompany.id }, skudCompany);
        }

        // DELETE: api/SkudCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudCompany>> DeleteSkudCompany(long id)
        {
            var skudCompany = await _context.SkudCompany.FindAsync(id);
            if (skudCompany == null)
            {
                return NotFound();
            }

            _context.SkudCompany.Remove(skudCompany);
            await _context.SaveChangesAsync();

            return skudCompany;
        }

        private bool SkudCompanyExists(long id)
        {
            return _context.SkudCompany.Any(e => e.id == id);
        }
    }
}
