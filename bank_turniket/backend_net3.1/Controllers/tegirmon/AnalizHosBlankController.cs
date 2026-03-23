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
    public class AnalizHosBlankController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnalizHosBlankController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonUserAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalizHosBlank>>> GetAnalizHosBlank()
        {
            return await _context.AnalizHosBlank.ToListAsync();
        }

        // GET: api/TegirmonUserAccess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalizHosBlank>> GetAnalizHosBlank(long id)
        {
            var analizHosBlank = await _context.AnalizHosBlank.FindAsync(id);

            if (analizHosBlank == null)
            {
                return NotFound();
            }

            return analizHosBlank;
        }


        // PUT: api/TegirmonUserAccess/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalizHosBlank(long id, AnalizHosBlank analizHosBlank)
        {
            if (id != analizHosBlank.id)
            {
                return BadRequest();
            }

            _context.Entry(analizHosBlank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalizHosBlankExists(id))
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
        // [HttpGet("getTegirmonUserAccessUserId")]
        // public async Task<ActionResult<TegirmonUserAccess>> getTegirmonUserAccessUserId([FromQuery]long user_id)
        // {
        //     var tegirmonUserAccess = await _context.TegirmonUserAccess
        //         .Include(p => p.user)
        //         .Where(p =>p.TegirmonUserid == user_id).ToListAsync();

        //     if (tegirmonUserAccess == null || tegirmonUserAccess.Count == 0)
        //     {
        //         return NotFound();
        //     }

        //     return tegirmonUserAccess.First();
        // }
        


        // POST: api/TegirmonAuth
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AnalizHosBlank>> PostAnalizHosBlank(AnalizHosBlank analizHosBlank)
        {
            _context.AnalizHosBlank.Update(analizHosBlank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalizHosBlank", new { id = analizHosBlank.id }, analizHosBlank);
        }

        // DELETE: api/TegirmonAuth/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnalizHosBlank>> DeleteAnalizHosBlank(long id)
        {
            var analizHosBlank = await _context.AnalizHosBlank.FindAsync(id);
            if (analizHosBlank == null)
            {
                return NotFound();
            }

            _context.AnalizHosBlank.Remove(analizHosBlank);
            await _context.SaveChangesAsync();

            return analizHosBlank;
        }

        private bool AnalizHosBlankExists(long id)
        {
            return _context.AnalizHosBlank.Any(e => e.id == id);
        }
    }
}
