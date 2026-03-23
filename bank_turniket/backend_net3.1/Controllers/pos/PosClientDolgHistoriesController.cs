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
    public class PosClientDolgHistoriesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosClientDolgHistoriesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosClientDolgHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosClientDolgHistory>>> GetPosClientDolgHistory()
        {
            return await _context.PosClientDolgHistory.ToListAsync();
        }

        // GET: api/PosClientDolgHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosClientDolgHistory>> GetPosClientDolgHistory(long id)
        {
            var posClientDolgHistory = await _context.PosClientDolgHistory.FindAsync(id);

            if (posClientDolgHistory == null)
            {
                return NotFound();
            }

            return posClientDolgHistory;
        }

        // PUT: api/PosClientDolgHistories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosClientDolgHistory(long id, PosClientDolgHistory posClientDolgHistory)
        {
            if (id != posClientDolgHistory.id)
            {
                return BadRequest();
            }

            _context.Entry(posClientDolgHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosClientDolgHistoryExists(id))
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

        // POST: api/PosClientDolgHistories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosClientDolgHistory>> PostPosClientDolgHistory(PosClientDolgHistory posClientDolgHistory)
        {
            _context.PosClientDolgHistory.Update(posClientDolgHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosClientDolgHistory", new { id = posClientDolgHistory.id }, posClientDolgHistory);
        }

        // DELETE: api/PosClientDolgHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosClientDolgHistory>> DeletePosClientDolgHistory(long id)
        {
            var posClientDolgHistory = await _context.PosClientDolgHistory.FindAsync(id);
            if (posClientDolgHistory == null)
            {
                return NotFound();
            }

            _context.PosClientDolgHistory.Remove(posClientDolgHistory);
            await _context.SaveChangesAsync();

            return posClientDolgHistory;
        }

        private bool PosClientDolgHistoryExists(long id)
        {
            return _context.PosClientDolgHistory.Any(e => e.id == id);
        }
    }
}
