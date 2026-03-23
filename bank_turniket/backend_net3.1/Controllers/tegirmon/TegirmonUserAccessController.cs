using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonUserAccessController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonUserAccessController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonUserAccess>>> GetTegirmonUserAccess()
        {
            return await _context.TegirmonUserAccess.ToListAsync();
        }

        // GET: api/TegirmonUserAccess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonUserAccess>> GetTegirmonUserAccess(long id)
        {
            var tegirmonUserAccess = await _context.TegirmonUserAccess.FindAsync(id);

            if (tegirmonUserAccess == null)
            {
                return NotFound();
            }

            return tegirmonUserAccess;
        }


        // PUT: api/TegirmonUserAccess/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonUserAccess(long id, TegirmonUserAccess tegirmonUserAccess)
        {
            if (id != tegirmonUserAccess.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonUserAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonUserAccessExists(id))
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

        // Get: api/getTegirmonUserAccessUserId
        // get user id
        [HttpGet("getTegirmonUserAccessUserId")]
        public async Task<ActionResult<TegirmonUserAccess>> getTegirmonUserAccessUserId([FromQuery]long user_id)
        {
            var tegirmonUserAccess = await _context.TegirmonUserAccess
                .Include(p => p.user)
                .Where(p =>p.TegirmonUserid == user_id).ToListAsync();

            if (tegirmonUserAccess == null || tegirmonUserAccess.Count == 0)
            {
                return NotFound();
            }

            return tegirmonUserAccess.First();
        }
        


        // POST: api/TegirmonAuth
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonUserAccess>> PostTegirmonUserAccess(TegirmonUserAccess tegirmonUserAccess)
        {
            _context.TegirmonUserAccess.Update(tegirmonUserAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonUserAccess", new { id = tegirmonUserAccess.id }, tegirmonUserAccess);
        }

        // DELETE: api/TegirmonAuth/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonUserAccess>> DeleteTegirmonUserAccess(long id)
        {
            var tegirmonUserAccess = await _context.TegirmonUserAccess.FindAsync(id);
            if (tegirmonUserAccess == null)
            {
                return NotFound();
            }

            _context.TegirmonUserAccess.Remove(tegirmonUserAccess);
            await _context.SaveChangesAsync();

            return tegirmonUserAccess;
        }

        private bool TegirmonUserAccessExists(long id)
        {
            return _context.TegirmonUserAccess.Any(e => e.id == id);
        }
    }
}
