using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudUserBonusController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudUserBonusController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudUserBonus>>> GetSkudUserBonus()
        {
            return await _context.SkudUserBonus.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkudUserBonus>> GetSkudUserBonus(long id)
        {
            var item = await _context.SkudUserBonus.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("getUserAddBonusUser_ID")]
        public async Task<ActionResult<IEnumerable<SkudUserBonus>>> getUserAddBonusUser_ID([FromQuery] long user_id)
        {
            List<SkudUserBonus> users_ls = await _context.SkudUserBonus
                .Where(p => p.userid == user_id)
                .OrderByDescending(p => p.id)
                .ToListAsync();
            return users_ls;
        }

        [HttpGet("getPaginationGetByDateTimeUser_bonus")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByDateTimeUser_bonus(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudUserBonus> itemList = await _context.SkudUserBonus
                .Where(p => p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();

            if (itemList == null)
            {
                itemList = new List<SkudUserBonus>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudUserBonus
                .Where(p => p.reg_date.Date >= begin_date.Date && p.reg_date.Date <= end_date.Date)
                .CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpPost]
        public async Task<ActionResult<SkudUserBonus>> PostSkudUserBonus(SkudUserBonus skudUserBonus)
        {
            _context.SkudUserBonus.Update(skudUserBonus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudUserBonus", new { id = skudUserBonus.id }, skudUserBonus);
        }
    }
}
