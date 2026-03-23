using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.pos;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.pos
{
    [ApiExplorerSettings(GroupName = "v5")]
    [Route("api/[controller]")]
    [ApiController]
    public class PosClientDolgsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PosClientDolgsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PosClientDolgs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosClientDolg>>> GetPosClientDolg()
        {
            return await _context.PosClientDolg.ToListAsync();
        }

        [HttpGet("getClientsAllDolgSummList")]
        public async Task<ActionResult<TexPaginationModel>> getClientsAllDolgSummList([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolgAllTemp> itemList = await _context.PosClientDolgAllTemp.FromSqlRaw("SELECT " +
                " \r\nSUM(pcd.payed_summ) as payed_summ, " +
                " \r\nSUM(pcd.dolg_summ) as dolg_summ" +
                " \r\nFROM pos_client_dolg pcd \r\nLEFT JOIN pos_client pc ON pc.id  = pcd.\"PosClientid\" " +
                " \r\nWHERE pcd.dolg_summ > 0 \r\n")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolgAllTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }  
        
        [HttpGet("getClientDolgListAllSummByCleintList")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgListAllSummByCleintList([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolgTemp> itemList = await _context.PosClientDolgTemp.FromSqlRaw("SELECT  " +
                " \r\npc.fio as client_name," +
                " \r\npc.phone_number as clinet_phone_number, " +
                " \r\nSUM(pcd.payed_summ) as payed_summ, " +
                " \r\nSUM(pcd.dolg_summ) as dolg_summ," +
                " \r\npcd.\"PosClientid\" as client_id," +
                " pc.fio as user_name " +
                " \r\nFROM pos_client_dolg pcd " +
                " \r\nLEFT JOIN pos_client pc ON pc.id  = pcd.\"PosClientid\" " +
                " \r\nWHERE pcd.dolg_summ > 0\r\nGROUP BY pcd.\"PosClientid\",pc.fio,pc.phone_number")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolgTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getClientDolgListAllSummByCleintWithUserList")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgListAllSummByCleintWithUserList([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolgTemp> itemList = await _context.PosClientDolgTemp.FromSqlRaw("SELECT  " +
                " \r\npc.fio as client_name," +
                " \r\npc.phone_number as clinet_phone_number, " +
                " \r\nSUM(pcd.payed_summ) as payed_summ, " +
                " \r\nSUM(pcd.dolg_summ) as dolg_summ," +
                " \r\npcd.\"PosClientid\" as client_id," +
                " \r\npcd.reserved_2 as user_name" +
                " \r\nFROM pos_client_dolg pcd " +
                " \r\nLEFT JOIN pos_client pc ON pc.id  = pcd.\"PosClientid\" " +
                " \r\nWHERE pcd.dolg_summ > 0 \r\nGROUP BY pcd.\"PosClientid\",pc.fio,pc.phone_number,pcd.reserved_2" +
                " \r\nORDER BY pcd.reserved_2")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolgTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getClientDolgListAllSummByCleintListGetByClientId")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgListAllSummByCleintListGetByClientId([FromQuery] int page, [FromQuery] int size, [FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolgTemp> itemList = await _context.PosClientDolgTemp.FromSqlRaw("SELECT  " +
                " \r\npc.fio as client_name," +
                " \r\npc.phone_number as clinet_phone_number, " +
                " \r\nSUM(pcd.payed_summ) as payed_summ, " +
                " \r\nSUM(pcd.dolg_summ) as dolg_summ," +
                " \r\npcd.\"PosClientid\" as client_id," +
                " pc.fio as user_name" +
                " \r\nFROM pos_client_dolg pcd " +
                " \r\nLEFT JOIN pos_client pc ON pc.id  = pcd.\"PosClientid\" " +
                " \r\nWHERE pcd.dolg_summ > 0 AND  pcd.\"PosClientid\" = "+client_id+"" +
                " \r\nGROUP BY pcd.\"PosClientid\",pc.fio,pc.phone_number")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolgTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getClientDolgList")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgList([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolg> itemList = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.dolg_summ > 0)
                .Include(p => p.client)
                .Include(p => p.check)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolg>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.dolg_summ > 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getClientDolgByClientIdList")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgByClientIdList([FromQuery] int page, [FromQuery] int size, [FromQuery] long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolg> itemList = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.PosClientid == client_id && p.dolg_summ > 0)
                .Include(p => p.client)
                .Include(p => p.check)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolg>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.PosClientid == client_id && p.dolg_summ > 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        [HttpGet("getClientDolgListByLimitDaysOtibKetganlar")]
        public async Task<ActionResult<TexPaginationModel>> getClientDolgListByLimitDaysOtibKetganlar([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<PosClientDolg> itemList = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.limit_date_time <= DateTime.Now && p.dolg_summ > 0)
                .Include(p => p.client)
                .Include(p => p.check)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<PosClientDolg>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.PosClientDolg
                .Where(p => p.active_status == true && p.limit_date_time <= DateTime.Now && p.dolg_summ > 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/PosClientDolgs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosClientDolg>> GetPosClientDolg(long id)
        {
            var posClientDolg = await _context.PosClientDolg.FindAsync(id);

            if (posClientDolg == null)
            {
                return NotFound();
            }

            return posClientDolg;
        }

        [HttpGet("payClientDolg")]
        public async Task<ActionResult<PosClientDolg>> payClientDolg([FromQuery]long pos_client_dolg_id, [FromQuery] double payed_summ)
        {
            var posClientDolg = await _context.PosClientDolg.FindAsync(pos_client_dolg_id);

            if (posClientDolg == null)
            {
                return NotFound();
            }

            posClientDolg.payed_summ = posClientDolg.payed_summ + payed_summ;
            posClientDolg.dolg_summ = posClientDolg.dolg_summ - payed_summ;
            _context.PosClientDolg.Update(posClientDolg);
            await _context.SaveChangesAsync();
            return posClientDolg;
        }

        [HttpGet("payClientDolgWithHistory")]
        public async Task<ActionResult<PosClientDolg>> payClientDolgWithHistory([FromQuery] long pos_client_dolg_id, [FromQuery] double payed_summ)
        {
            var posClientDolg = await _context.PosClientDolg.FindAsync(pos_client_dolg_id);

            if (posClientDolg == null)
            {
                return NotFound();
            }

            posClientDolg.payed_summ = posClientDolg.payed_summ + payed_summ;
            posClientDolg.dolg_summ = posClientDolg.dolg_summ - payed_summ;
            _context.PosClientDolg.Update(posClientDolg);
            await _context.SaveChangesAsync();

            //add edd history
            PosClientDolgHistory dolgHistory = new PosClientDolgHistory();
            dolgHistory.payed_summ = payed_summ;
            dolgHistory.PosClientDolgid = pos_client_dolg_id;
            _context.PosClientDolgHistory.Update(dolgHistory);
            await _context.SaveChangesAsync();

            return posClientDolg;
        }


        [HttpGet("payClientDolgWithHistoryWithClietnId")]
        public async Task<ActionResult<IEnumerable<PosClientDolg>>> payClientDolgWithHistoryWithClietnId([FromQuery] long pos_client_id, [FromQuery] double payed_summ)
        {
            List<PosClientDolg> posClientDolgs = new List<PosClientDolg>();
            posClientDolgs = await _context.PosClientDolg
                .Where(p => p.PosClientid == pos_client_id && p.dolg_summ > 0).OrderBy(p =>p.id).ToListAsync();

            double left_payed_summ = payed_summ;
            bool finish_payed_summ = false;

            foreach (PosClientDolg posClientDolg in posClientDolgs) {
                if (left_payed_summ == 0.0) {
                    continue;
                }
                if (finish_payed_summ) {
                    continue;
                }

                    if (posClientDolg == null)
                    {
                        return NotFound();
                    }

                double need_payed_summm = left_payed_summ;
                if (need_payed_summm > posClientDolg.dolg_summ)
                {
                    need_payed_summm = posClientDolg.dolg_summ;
                    left_payed_summ = left_payed_summ - posClientDolg.dolg_summ;
                }
                else {
                    finish_payed_summ = true;
                
                }

                posClientDolg.payed_summ = posClientDolg.payed_summ + need_payed_summm;
                    posClientDolg.dolg_summ = posClientDolg.dolg_summ - need_payed_summm;
                    _context.PosClientDolg.Update(posClientDolg);
                    await _context.SaveChangesAsync();

                    //add edd history
                    PosClientDolgHistory dolgHistory = new PosClientDolgHistory();
                    dolgHistory.payed_summ = need_payed_summm;
                    dolgHistory.PosClientDolgid = posClientDolg.id;
                    _context.PosClientDolgHistory.Update(dolgHistory);
                    await _context.SaveChangesAsync();
                }
            
            return posClientDolgs;
        }

        // PUT: api/PosClientDolgs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosClientDolg(long id, PosClientDolg posClientDolg)
        {
            if (id != posClientDolg.id)
            {
                return BadRequest();
            }

            _context.Entry(posClientDolg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosClientDolgExists(id))
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

        // POST: api/PosClientDolgs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PosClientDolg>> PostPosClientDolg(PosClientDolg posClientDolg)
        {

            _context.PosClientDolg.Update(posClientDolg);
            if (posClientDolg.created_date_tm != null) {
                posClientDolg.limit_date_time = posClientDolg.created_date_tm.AddDays(posClientDolg.limit_days);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosClientDolg", new { id = posClientDolg.id }, posClientDolg);
        }

        // DELETE: api/PosClientDolgs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosClientDolg>> DeletePosClientDolg(long id)
        {
            var posClientDolg = await _context.PosClientDolg.FindAsync(id);
            if (posClientDolg == null)
            {
                return NotFound();
            }

            _context.PosClientDolg.Remove(posClientDolg);
            await _context.SaveChangesAsync();

            return posClientDolg;
        }

        private bool PosClientDolgExists(long id)
        {
            return _context.PosClientDolg.Any(e => e.id == id);
        }
    }
}
