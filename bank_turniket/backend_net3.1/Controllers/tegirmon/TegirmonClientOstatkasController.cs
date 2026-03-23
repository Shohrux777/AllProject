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
using Telegram.Bot;
using ApiAll.Model;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonClientOstatkasController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly TelegramBotClient botClient = new TelegramBotClient("6241468616:AAHTPFcVWOkOpoUCp5mKvuYBj7FsHJMRAVY");


        public TegirmonClientOstatkasController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet("sendMessageToTegirmonClient")]
        public async Task<ActionResult<TegirmonClient>> sendMessageToTegirmonClient([FromQuery] String message_str, [FromQuery] long client_id)
        {
            TegirmonClient tegirmonClient = await _context.TegirmonClient.FindAsync(client_id);
            if (tegirmonClient != null && tegirmonClient.bot_id != null && tegirmonClient.bot_id > 0) {
                try
                {

                    await botClient.SendTextMessageAsync(tegirmonClient.bot_id, message_str);
                }
                catch (Exception) { }
            }
            return tegirmonClient;
        }

        // [HttpGet("getTegirmonClientOstatkaInfo")]
        // public async Task<ActionResult<IEnumerable<TegirmonClientOstatakaSumInfo>>> getTegirmonClientOstatkaInfo()
        // {

        //     return await _context.TegirmonClientOstatakaSumInfo.FromSqlRaw(
        //         " SELECT " +
        //         " tp.name as name, " +
        //         " sum(tco.qty) as qty, sum(tco.real_qty) as real_qty " +
        //         " FROM tegirmon_client_ostatka tco " +
        //         " LEFT JOIN tegirmon_product tp ON tp.id = tco.\"TegirmonProductid\" " +
        //         " GROUP BY tp.name").ToListAsync();
        // }

        [HttpGet("getTegirmonClientOstatkaInfo")]
        public async Task<ActionResult<IEnumerable<TegirmonClientOstatakaSumInfo>>> getTegirmonClientOstatkaInfo()
        {
            return await _context.TegirmonClientOstatakaSumInfo.FromSqlRaw(
                "SELECT " +
                " tp.id as TegirmonProductid, " +
                " tp.name as name, " +
                " SUM(tco.qty) as qty, " +
                " SUM(tco.real_qty) as real_qty " +
                " FROM tegirmon_client_ostatka tco " +
                " LEFT JOIN tegirmon_product tp ON tp.id = tco.\"TegirmonProductid\" " +
                " GROUP BY tp.id, tp.name " +
                " ORDER BY SUM(tco.qty) DESC"
            ).ToListAsync();
        }



        // GET: api/TegirmonClientOstatkas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonClientOstatka>>> GetTegirmonClientOstatka()
        {
            return await _context.TegirmonClientOstatka.ToListAsync();
        }

        // GET: api/TegirmonClientOstatkas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonClientOstatka>> GetTegirmonClientOstatka(long id)
        {
            var tegirmonClientOstatka = await _context.TegirmonClientOstatka.FindAsync(id);

            if (tegirmonClientOstatka == null)
            {
                return NotFound();
            }

            return tegirmonClientOstatka;
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientOstatka> categoryList = await _context.TegirmonClientOstatka
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p => p.active_status == true && p.real_qty > 0.3)
                .Skip(page * size).Take(size).OrderByDescending(p => p.qty).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientOstatka>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientOstatka
                .Where(p => p.active_status == true && p.real_qty != 0).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationProductId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationProductId([FromQuery] int page, [FromQuery] int size, [FromQuery] int product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientOstatka> categoryList = await _context.TegirmonClientOstatka
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p => p.active_status == true && p.TegirmonProductid == product_id && p.real_qty > 0.3)
                .Skip(page * size).Take(size).OrderByDescending(p => p.qty).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientOstatka>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientOstatka
                .Where(p => p.active_status == true && p.TegirmonProductid == product_id && p.real_qty > 0.3).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetByClientGroupIdList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByClientGroupIdList([FromQuery] int page, [FromQuery] int size,[FromQuery] long client_group_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientOstatka> categoryList = await _context.TegirmonClientOstatka
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p => p.active_status == true && p.real_qty > 0 && p.client.TegirmonClientGroupid == client_group_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientOstatka>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientOstatka
                .Include(p =>p.client)
                
                .Where(p => p.active_status == true && p.real_qty > 0 && p.client.TegirmonClientGroupid == client_group_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGetByClientClientIdList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGetByClientClientIdList([FromQuery] int page, [FromQuery] int size, [FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientOstatka> categoryList = new List<TegirmonClientOstatka>();
            TegirmonClient tegirmonClient = await _context.TegirmonClient.FindAsync(client_id);
            if (tegirmonClient == null) {
                return NotFound();
            }
            TegirmonClientGroup tegirmonClientGroup = await _context.TegirmonClientGroup.FindAsync(tegirmonClient.TegirmonClientGroupid);

            if (tegirmonClientGroup == null || tegirmonClientGroup.active_status == false) {
                 categoryList = await _context.TegirmonClientOstatka
                    .Include(p => p.product)
                    .ThenInclude(p => p.unitMeasurment)
                    .Include(p => p.client)
                    .Where(p => p.active_status == true && p.real_qty > 0
                    && p.client.id == tegirmonClient.id)
                    .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
                                if (categoryList == null)
                                {
                                    categoryList = new List<TegirmonClientOstatka>();
                                }
                                paginationModel.items_list = JArray.FromObject(categoryList);
                                paginationModel.items_count = await _context.TegirmonClientOstatka
                                    .Include(p => p.client)
                                    .Where(p => p.active_status == true && p.real_qty > 0
                                    && p.client.id == tegirmonClient.id).CountAsync();
                                paginationModel.current_item_count = categoryList.Count();
                                paginationModel.current_page = page;
                return paginationModel;
            }

            categoryList = await _context.TegirmonClientOstatka
                .Include(p => p.product)
                .ThenInclude(p =>p.unitMeasurment)
                .Include(p => p.client)
                .Where(p => p.active_status == true && p.real_qty > 0 
                && p.client.TegirmonClientGroupid == tegirmonClient.TegirmonClientGroupid)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientOstatka>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientOstatka
                .Include(p => p.client)

                .Where(p => p.active_status == true && p.real_qty > 0
                && p.client.TegirmonClientGroupid == tegirmonClient.TegirmonClientGroupid).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationClientOstatkaByClientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationClientOstatkaByClientId([FromQuery] int page, [FromQuery] int size,[FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonClientOstatka> categoryList = await _context.TegirmonClientOstatka
                .Include(p => p.product)
                .Include(p => p.client)
                .Where(p => p.active_status == true && p.TegirmonClientid == client_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonClientOstatka>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonClientOstatka
                .Where(p => p.active_status == true && p.TegirmonClientid == client_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonClientOstatkas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonClientOstatka(long id, TegirmonClientOstatka tegirmonClientOstatka)
        {
            if (id != tegirmonClientOstatka.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonClientOstatka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonClientOstatkaExists(id))
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

        // POST: api/TegirmonClientOstatkas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonClientOstatka>> PostTegirmonClientOstatka(TegirmonClientOstatka tegirmonClientOstatka)
        {
            _context.TegirmonClientOstatka.Update(tegirmonClientOstatka);
            await _context.SaveChangesAsync();

            return tegirmonClientOstatka;
        }

        // DELETE: api/TegirmonClientOstatkas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonClientOstatka>> DeleteTegirmonClientOstatka(long id)
        {
            var tegirmonClientOstatka = await _context.TegirmonClientOstatka.FindAsync(id);
            if (tegirmonClientOstatka == null)
            {
                return NotFound();
            }

            _context.TegirmonClientOstatka.Remove(tegirmonClientOstatka);
            await _context.SaveChangesAsync();

            return tegirmonClientOstatka;
        }

        private bool TegirmonClientOstatkaExists(long id)
        {
            return _context.TegirmonClientOstatka.Any(e => e.id == id);
        }
    }
}
