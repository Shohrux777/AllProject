using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.hospital.analiz;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudDevicesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudDevicesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudDevices>>> GetSkudDevices()
        {
            return await _context.SkudDevices.ToListAsync();
        }

        // GET: api/SkudDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudDevices>> GetSkudDevices(long id)
        {
            var skudDevices = await _context.SkudDevices.FindAsync(id);

            if (skudDevices == null)
            {
                return NotFound();
            }

            return skudDevices;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudDevices> itemList = await _context.SkudDevices
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudDevices>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudDevices.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudDevices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudDevices(long id, SkudDevices skudDevices)
        {
            if (id != skudDevices.id)
            {
                return BadRequest();
            }

            _context.Entry(skudDevices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudDevicesExists(id))
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

        // POST: api/SkudDevices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudDevices>> PostSkudDevices(SkudDevices skudDevices)
        {
            _context.SkudDevices.Update(skudDevices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudDevices", new { id = skudDevices.id }, skudDevices);
        }

        // DELETE: api/SkudDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudDevices>> DeleteSkudDevices(long id)
        {
            var skudDevices = await _context.SkudDevices.FindAsync(id);
            if (skudDevices == null)
            {
                return NotFound();
            }

            _context.SkudDevices.Remove(skudDevices);
            await _context.SaveChangesAsync();

            return skudDevices;
        }

        private bool SkudDevicesExists(long id)
        {
            return _context.SkudDevices.Any(e => e.id == id);
        }
    }
}
