using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model;

namespace ApiAll.Controllers.hospital
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class BotLanguagesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BotLanguagesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/BotLanguages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BotLanguage>>> GetBotLanguage()
        {
            return await _context.BotLanguage.ToListAsync();
        }

        // GET: api/BotLanguages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BotLanguage>> GetBotLanguage(long id)
        {
            var botLanguage = await _context.BotLanguage.FindAsync(id);

            if (botLanguage == null)
            {
                return NotFound();
            }

            return botLanguage;
        }

        // PUT: api/BotLanguages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBotLanguage(long id, BotLanguage botLanguage)
        {
            if (id != botLanguage.Id)
            {
                return BadRequest();
            }

            _context.Entry(botLanguage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotLanguageExists(id))
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

        // POST: api/BotLanguages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BotLanguage>> PostBotLanguage(BotLanguage botLanguage)
        {
            _context.BotLanguage.Add(botLanguage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBotLanguage", new { id = botLanguage.Id }, botLanguage);
        }

        // DELETE: api/BotLanguages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BotLanguage>> DeleteBotLanguage(long id)
        {
            var botLanguage = await _context.BotLanguage.FindAsync(id);
            if (botLanguage == null)
            {
                return NotFound();
            }

            _context.BotLanguage.Remove(botLanguage);
            await _context.SaveChangesAsync();

            return botLanguage;
        }

        private bool BotLanguageExists(long id)
        {
            return _context.BotLanguage.Any(e => e.Id == id);
        }
    }
}
