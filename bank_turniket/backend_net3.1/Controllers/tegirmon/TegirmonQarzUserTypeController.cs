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
    public class TegirmonQarzUserTypeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonQarzUserTypeController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonQarzUserType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonQarzUserType>>> GetTegirmonQarzUserType()
        {
            return await _context.TegirmonQarzUserType.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserType> categoryList = await _context.TegirmonQarzUserType
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserType>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserType.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationQarzType")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationQarzType([FromQuery] int page, [FromQuery] int size, [FromQuery] int user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserType> categoryList = await _context.TegirmonQarzUserType
                .Where(p => p.active_status == true && p.TegirmonQarzUserId == user_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserType>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserType.Where(p => p.active_status == true && p.TegirmonQarzUserId == user_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        
         [HttpGet("getPaginationByBornDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBornDate([FromQuery] int page, [FromQuery] int size,[FromQuery] String born_date_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserType> categoryList = await _context.TegirmonQarzUserType
                .Where(p => p.active_status == true
                && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower()))
                
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserType>();
            }

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserType.Where(p => p.active_status == true 
            && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPagination_user_vaqrOraligida")]
        public async Task<ActionResult<TexPaginationModel>> getPagination_user_vaqrOraligida([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date,[FromQuery] DateTime end_date, [FromQuery] int userid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonQarzUserType> categoryList = await _context.TegirmonQarzUserType
                .Where(p => p.id == userid && (p.created_date >= begin_date && p.created_date <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonQarzUserType>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonQarzUserType.Where(p => p.id == userid && (p.created_date >= begin_date && p.created_date <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        // GET: api/TegirmonQarzUserType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonQarzUserType>> GetTegirmonQarzUserType(long id)
        {
            var tegirmonQarzUserType = await _context.TegirmonQarzUserType.FindAsync(id);

            if (tegirmonQarzUserType == null)
            {
                return NotFound();
            }

            return tegirmonQarzUserType;
        }

        // PUT: api/TegirmonQarzUserType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonQarzUserType(long id, TegirmonQarzUserType tegirmonQarzUserType)
        {
            if (id != tegirmonQarzUserType.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonQarzUserType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonQarzUserTypeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<TegirmonQarzUserType>> PostTegirmonQarzUserType(TegirmonQarzUserType tegirmonQarzUserType)
        {
            _context.TegirmonQarzUserType.Update(tegirmonQarzUserType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonQarzUserType", new { id = tegirmonQarzUserType.id }, tegirmonQarzUserType);
        }


        // DELETE: api/TegirmonUserIshlaganVaqt/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonQarzUserType>> DeleteTegirmonQarzUserType(long id)
        {
            var tegirmonQarzUserType = await _context.TegirmonQarzUserType.FindAsync(id);
            if (tegirmonQarzUserType == null)
            {
                return NotFound();
            }

            _context.TegirmonQarzUserType.Remove(tegirmonQarzUserType);
            await _context.SaveChangesAsync();

            return tegirmonQarzUserType;
        }

        private bool TegirmonQarzUserTypeExists(long id)
        {
            return _context.TegirmonQarzUserType.Any(e => e.id == id);
        }
    }
}
