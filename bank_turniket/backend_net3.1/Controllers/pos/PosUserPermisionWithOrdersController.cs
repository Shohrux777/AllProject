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
    public class PosUserPermisionWithOrdersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosUserPermisionWithOrdersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosUserPermisionWithOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosUserPermisionWithOrder>>> GetPosUserPermisionWithOrder()
        {
            return await _context.PosUserPermisionWithOrder.ToListAsync();
        }

        // GET: api/PosUserPermisionWithOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosUserPermisionWithOrder>> GetPosUserPermisionWithOrder(long id)
        {
            var posUserPermisionWithOrder = await _context.PosUserPermisionWithOrder.FindAsync(id);

            if (posUserPermisionWithOrder == null)
            {
                return NotFound();
            }

            return posUserPermisionWithOrder;
        }

        // PUT: api/PosUserPermisionWithOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosUserPermisionWithOrder(long id, PosUserPermisionWithOrder posUserPermisionWithOrder)
        {
            if (id != posUserPermisionWithOrder.id)
            {
                return BadRequest();
            }

            _context.Entry(posUserPermisionWithOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosUserPermisionWithOrderExists(id))
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

        // POST: api/PosUserPermisionWithOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosUserPermisionWithOrder>> PostPosUserPermisionWithOrder(PosUserPermisionWithOrder posUserPermisionWithOrder)
        {
            _context.PosUserPermisionWithOrder.Update(posUserPermisionWithOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosUserPermisionWithOrder", new { id = posUserPermisionWithOrder.id }, posUserPermisionWithOrder);
        }

        // DELETE: api/PosUserPermisionWithOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosUserPermisionWithOrder>> DeletePosUserPermisionWithOrder(long id)
        {
            var posUserPermisionWithOrder = await _context.PosUserPermisionWithOrder.FindAsync(id);
            if (posUserPermisionWithOrder == null)
            {
                return NotFound();
            }

            _context.PosUserPermisionWithOrder.Remove(posUserPermisionWithOrder);
            await _context.SaveChangesAsync();

            return posUserPermisionWithOrder;
        }

        private bool PosUserPermisionWithOrderExists(long id)
        {
            return _context.PosUserPermisionWithOrder.Any(e => e.id == id);
        }
    }
}
