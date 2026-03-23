using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.analiz;

namespace ApiAll.Controllers.hospital.analiz_unversal
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v4")]
    [ApiController]
    public class AnalizGroupInfoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnalizGroupInfoesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/AnalizGroupInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalizGroupInfo>>> GetAnalizGroupInfo()
        {
            return await _context.AnalizGroupInfo.ToListAsync();
        }

        // GET: api/AnalizGroupInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalizGroupInfo>> GetAnalizGroupInfo(long id)
        {
            var analizGroupInfo = await _context.AnalizGroupInfo.FindAsync(id);

            if (analizGroupInfo == null)
            {
                return NotFound();
            }

            return analizGroupInfo;
        }

        // PUT: api/AnalizGroupInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalizGroupInfo(long id, AnalizGroupInfo analizGroupInfo)
        {
            if (id != analizGroupInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(analizGroupInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalizGroupInfoExists(id))
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

        // POST: api/AnalizGroupInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AnalizGroupInfo>> PostAnalizGroupInfo(AnalizGroupInfo analizGroupInfo)
        {
            _context.AnalizGroupInfo.Update(analizGroupInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalizGroupInfo", new { id = analizGroupInfo.Id }, analizGroupInfo);
        }

        // DELETE: api/AnalizGroupInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnalizGroupInfo>> DeleteAnalizGroupInfo(long id)
        {
            var analizGroupInfo = await _context.AnalizGroupInfo.FindAsync(id);
            if (analizGroupInfo == null)
            {
                return NotFound();
            }

            _context.AnalizGroupInfo.Remove(analizGroupInfo);
            await _context.SaveChangesAsync();

            return analizGroupInfo;
        }

        private bool AnalizGroupInfoExists(long id)
        {
            return _context.AnalizGroupInfo.Any(e => e.Id == id);
        }
    }
}
