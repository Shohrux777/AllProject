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
    public class SkudMyDepartmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudMyDepartmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudMyDepartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudMyDepartments>>> GetSkudMyDepartments()
        {
            return await _context.SkudMyDepartments.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyDepartments> itemList = await _context.SkudMyDepartments
                .OrderByDescending(p => p.deptid)
                .Include(p => p.skudCompany)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyDepartments>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyDepartments.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetById")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetById([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long company_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyDepartments> itemList = await _context.SkudMyDepartments
                .Include(p => p.skudCompany)
                .Where( p=>p.SkudCompanyid == company_id)
                .OrderByDescending(p => p.deptid)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyDepartments>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyDepartments.Where(p => p.SkudCompanyid == company_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/SkudMyDepartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudMyDepartments>> GetSkudMyDepartments(long id)
        {
            var skudMyDepartments = await _context.SkudMyDepartments.FindAsync(id);

            if (skudMyDepartments == null)
            {
                return NotFound();
            }

            return skudMyDepartments;
        }

        // PUT: api/SkudMyDepartments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudMyDepartments(long id, SkudMyDepartments skudMyDepartments)
        {
            if (id != skudMyDepartments.deptid)
            {
                return BadRequest();
            }

            _context.Entry(skudMyDepartments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudMyDepartmentsExists(id))
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

        // POST: api/SkudMyDepartments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudMyDepartments>> PostSkudMyDepartments(SkudMyDepartments skudMyDepartments)
        {
            _context.SkudMyDepartments.Update(skudMyDepartments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudMyDepartments", new { id = skudMyDepartments.deptid }, skudMyDepartments);
        }

        // DELETE: api/SkudMyDepartments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudMyDepartments>> DeleteSkudMyDepartments(long id)
        {
            var skudMyDepartments = await _context.SkudMyDepartments.FindAsync(id);
            if (skudMyDepartments == null)
            {
                return NotFound();
            }

            _context.SkudMyDepartments.Remove(skudMyDepartments);
            await _context.SaveChangesAsync();

            return skudMyDepartments;
        }

        private bool SkudMyDepartmentsExists(long id)
        {
            return _context.SkudMyDepartments.Any(e => e.deptid == id);
        }
    }
}
