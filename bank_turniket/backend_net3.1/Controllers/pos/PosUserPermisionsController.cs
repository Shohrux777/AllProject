using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.pos;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.pos
{
    [ApiExplorerSettings(GroupName = "v5")]
    [Route("api/[controller]")]
    [ApiController]
    public class PosUserPermisionsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosUserPermisionsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosUserPermisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosUserPermision>>> GetPosUserPermision()
        {
            return await _context.PosUserPermision.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosCompany> itemList = await _context.PosCompany
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosCompany>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosCompany.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosUserPermisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosUserPermision>> GetPosUserPermision(long id)
        {
            var posUserPermision = await _context.PosUserPermision.FindAsync(id);

            if (posUserPermision == null)
            {
                return NotFound();
            }

            return posUserPermision;
        }

        // PUT: api/PosUserPermisions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosUserPermision(long id, PosUserPermision posUserPermision)
        {
            if (id != posUserPermision.id)
            {
                return BadRequest();
            }

            _context.Entry(posUserPermision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosUserPermisionExists(id))
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

        // POST: api/PosUserPermisions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosUserPermision>> PostPosUserPermision(PosUserPermision posUserPermision)
        {
            _context.PosUserPermision.Update(posUserPermision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosUserPermision", new { id = posUserPermision.id }, posUserPermision);
        }

        // DELETE: api/PosUserPermisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosUserPermision>> DeletePosUserPermision(long id)
        {
            var posUserPermision = await _context.PosUserPermision.FindAsync(id);
            if (posUserPermision == null)
            {
                return NotFound();
            }

            _context.PosUserPermision.Remove(posUserPermision);
            await _context.SaveChangesAsync();

            return posUserPermision;
        }

        private bool PosUserPermisionExists(long id)
        {
            return _context.PosUserPermision.Any(e => e.id == id);
        }
    }
}
