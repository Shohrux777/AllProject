using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.pos;

namespace ApiAll.Controllers.pos
{
    [ApiExplorerSettings(GroupName = "v5")]
    [Route("api/[controller]")]
    [ApiController]
    public class PosUserPermisionItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosUserPermisionItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosUserPermisionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosUserPermisionItem>>> GetPosUserPermisionItem()
        {
            return await _context.PosUserPermisionItem.ToListAsync();
        }

        // GET: api/PosUserPermisionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosUserPermisionItem>> GetPosUserPermisionItem(long id)
        {
            var posUserPermisionItem = await _context.PosUserPermisionItem.FindAsync(id);

            if (posUserPermisionItem == null)
            {
                return NotFound();
            }

            return posUserPermisionItem;
        }

        // PUT: api/PosUserPermisionItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosUserPermisionItem(long id, PosUserPermisionItem posUserPermisionItem)
        {
            if (id != posUserPermisionItem.id)
            {
                return BadRequest();
            }

            _context.Entry(posUserPermisionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosUserPermisionItemExists(id))
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

        // POST: api/PosUserPermisionItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosUserPermisionItem>> PostPosUserPermisionItem(PosUserPermisionItem posUserPermisionItem)
        {
            _context.PosUserPermisionItem.Update(posUserPermisionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosUserPermisionItem", new { id = posUserPermisionItem.id }, posUserPermisionItem);
        }

        // DELETE: api/PosUserPermisionItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosUserPermisionItem>> DeletePosUserPermisionItem(long id)
        {
            var posUserPermisionItem = await _context.PosUserPermisionItem.FindAsync(id);
            if (posUserPermisionItem == null)
            {
                return NotFound();
            }

            _context.PosUserPermisionItem.Remove(posUserPermisionItem);
            await _context.SaveChangesAsync();

            return posUserPermisionItem;
        }

        private bool PosUserPermisionItemExists(long id)
        {
            return _context.PosUserPermisionItem.Any(e => e.id == id);
        }
    }
}
