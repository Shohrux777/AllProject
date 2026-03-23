using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonClientNewsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonClientNewsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonClientNews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonClientNew>>> GetTegirmonClientNew()
        {
            return await _context.TegirmonClientNew.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientNew> categoryList = await _context.TegirmonClientNew
                .Include(p => p.group)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientNew.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationByBornDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBornDate([FromQuery] int page, [FromQuery] int size, [FromQuery] String born_date_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientNew> categoryList = await _context.TegirmonClientNew
                .Include(p => p.group)
                .Where(p => p.active_status == true
                && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientNew>();
            }


            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientNew.Where(p => p.active_status == true
            && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber([FromQuery] int page, [FromQuery] int size, [FromQuery] String fio_or_serial_number)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientNew> categoryList = new List<TegirmonClientNew>();
            if (fio_or_serial_number.Trim().Length > 0)
            {
                categoryList = await _context.TegirmonClientNew
                .Include(p => p.group)
                .Where(p => p.active_status == true &&
                (p.fio.ToLower().Contains(fio_or_serial_number.ToLower())
                || p.passport_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.phone_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.home_phone_number.ToLower().Contains(fio_or_serial_number.ToLower())))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }
            else
            {
                categoryList = await _context.TegirmonClientNew
                .Include(p => p.group)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }


            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = categoryList.Count();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationClientListByGroupId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationClientListByGroupId([FromQuery] int page, [FromQuery] int size, [FromQuery] long group_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientNew> categoryList = await _context.TegirmonClientNew
                .Include(p => p.group)
                .Where(p => p.active_status == true && p.TegirmonClientGroupid == group_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientNew.Where(p => p.active_status == true && p.TegirmonClientGroupid == group_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonClientNews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonClientNew>> GetTegirmonClientNew(long id)
        {
            var tegirmonClientNew = await _context.TegirmonClientNew.FindAsync(id);

            if (tegirmonClientNew == null)
            {
                return NotFound();
            }

            return tegirmonClientNew;
        }

        // PUT: api/TegirmonClientNews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonClientNew(long id, TegirmonClientNew tegirmonClientNew)
        {
            if (id != tegirmonClientNew.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonClientNew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonClientNewExists(id))
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

        // POST: api/TegirmonClientNews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonClientNew>> PostTegirmonClientNew(TegirmonClientNew tegirmonClientNew)
        {
            _context.TegirmonClientNew.Update(tegirmonClientNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonClientNew", new { id = tegirmonClientNew.id }, tegirmonClientNew);
        }

        // DELETE: api/TegirmonClientNews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonClientNew>> DeleteTegirmonClientNew(long id)
        {
            var tegirmonClientNew = await _context.TegirmonClientNew.FindAsync(id);
            if (tegirmonClientNew == null)
            {
                return NotFound();
            }

            _context.TegirmonClientNew.Remove(tegirmonClientNew);
            await _context.SaveChangesAsync();

            return tegirmonClientNew;
        }

        private bool TegirmonClientNewExists(long id)
        {
            return _context.TegirmonClientNew.Any(e => e.id == id);
        }
    }
}
