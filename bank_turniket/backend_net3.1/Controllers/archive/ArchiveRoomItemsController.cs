using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.archive;

namespace ApiAll.Controllers.archive
{
    [ApiExplorerSettings(GroupName = "v6")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveRoomItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ArchiveRoomItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/ArchiveRoomItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArchiveRoomItem>>> GetArchiveRoomItem()
        {
            return await _context.ArchiveRoomItem.ToListAsync();
        }

        // GET: api/ArchiveRoomItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArchiveRoomItem>> GetArchiveRoomItem(long id)
        {
            var archiveRoomItem = await _context.ArchiveRoomItem.FindAsync(id);

            if (archiveRoomItem == null)
            {
                return NotFound();
            }

            return archiveRoomItem;
        }

        // PUT: api/ArchiveRoomItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchiveRoomItem(long id, ArchiveRoomItem archiveRoomItem)
        {
            if (id != archiveRoomItem.id)
            {
                return BadRequest();
            }

            _context.Entry(archiveRoomItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchiveRoomItemExists(id))
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

        // POST: api/ArchiveRoomItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ArchiveRoomItem>> PostArchiveRoomItem(ArchiveRoomItem archiveRoomItem)
        {
            _context.ArchiveRoomItem.Update(archiveRoomItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchiveRoomItem", new { id = archiveRoomItem.id }, archiveRoomItem);
        }

        // DELETE: api/ArchiveRoomItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArchiveRoomItem>> DeleteArchiveRoomItem(long id)
        {
            var archiveRoomItem = await _context.ArchiveRoomItem.FindAsync(id);
            if (archiveRoomItem == null)
            {
                return NotFound();
            }

            _context.ArchiveRoomItem.Remove(archiveRoomItem);
            await _context.SaveChangesAsync();

            return archiveRoomItem;
        }

        private bool ArchiveRoomItemExists(long id)
        {
            return _context.ArchiveRoomItem.Any(e => e.id == id);
        }
    }
}
