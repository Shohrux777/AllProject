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
    public class PosUserPermisionWorkTypesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosUserPermisionWorkTypesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosUserPermisionWorkType> itemList = await _context.PosUserPermisionWorkType
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosUserPermisionWorkType>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosUserPermisionWorkType.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosUserPermisionWorkTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosUserPermisionWorkType>>> GetPosUserPermisionWorkType()
        {
            return await _context.PosUserPermisionWorkType.ToListAsync();
        }

        // GET: api/PosUserPermisionWorkTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosUserPermisionWorkType>> GetPosUserPermisionWorkType(long id)
        {
            var posUserPermisionWorkType = await _context.PosUserPermisionWorkType.FindAsync(id);

            if (posUserPermisionWorkType == null)
            {
                return NotFound();
            }

            return posUserPermisionWorkType;
        }

        // PUT: api/PosUserPermisionWorkTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosUserPermisionWorkType(long id, PosUserPermisionWorkType posUserPermisionWorkType)
        {
            if (id != posUserPermisionWorkType.id)
            {
                return BadRequest();
            }

            _context.Entry(posUserPermisionWorkType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosUserPermisionWorkTypeExists(id))
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

        // POST: api/PosUserPermisionWorkTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosUserPermisionWorkType>> PostPosUserPermisionWorkType(PosUserPermisionWorkType posUserPermisionWorkType)
        {
            _context.PosUserPermisionWorkType.Update(posUserPermisionWorkType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosUserPermisionWorkType", new { id = posUserPermisionWorkType.id }, posUserPermisionWorkType);
        }

        // DELETE: api/PosUserPermisionWorkTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosUserPermisionWorkType>> DeletePosUserPermisionWorkType(long id)
        {
            var posUserPermisionWorkType = await _context.PosUserPermisionWorkType.FindAsync(id);
            if (posUserPermisionWorkType == null)
            {
                return NotFound();
            }

            _context.PosUserPermisionWorkType.Remove(posUserPermisionWorkType);
            await _context.SaveChangesAsync();

            return posUserPermisionWorkType;
        }

        private bool PosUserPermisionWorkTypeExists(long id)
        {
            return _context.PosUserPermisionWorkType.Any(e => e.id == id);
        }
    }
}
