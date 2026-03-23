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
    public class TegirmonTortilganBugdoyRoyxatiGroupsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonTortilganBugdoyRoyxatiGroupsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonTortilganBugdoyRoyxatiGroup>>> GetTegirmonTortilganBugdoyRoyxatiGroup()
        {
            return await _context.TegirmonTortilganBugdoyRoyxatiGroup.ToListAsync();
        }

        // GET: api/TegirmonTortilganBugdoyRoyxatiGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroup>> GetTegirmonTortilganBugdoyRoyxatiGroup(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroup = await _context.TegirmonTortilganBugdoyRoyxatiGroup.FindAsync(id);

            if (tegirmonTortilganBugdoyRoyxatiGroup == null)
            {
                return NotFound();
            }

            return tegirmonTortilganBugdoyRoyxatiGroup;
        }

        [HttpGet("acceptTegirmonTortilganBugdoyRoyxatiGroup")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroup>> acceptTegirmonTortilganBugdoyRoyxatiGroup([FromQuery]long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroup = await _context.TegirmonTortilganBugdoyRoyxatiGroup.FindAsync(id);

            if (tegirmonTortilganBugdoyRoyxatiGroup == null)
            {
                return NotFound();
            }

            tegirmonTortilganBugdoyRoyxatiGroup.accepted_status = true;

            List<TegirmonTortilganBugdoyRoyxatiGroupDetail> detail_list = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                .Include(p =>p.royxati)
                .Include(p => p.invoice)
                .Where(p => p.TegirmonTortilganBugdoyRoyxatiGroupid == id)
                .ToListAsync();


            //details for accepted and invoice
            if (detail_list != null && detail_list.Count > 0) {

                foreach (TegirmonTortilganBugdoyRoyxatiGroupDetail item in detail_list) {
                    item.royxati.accepted_get_value = true;
                    item.invoice.inv_accepted_status = true;
                }

                //update
                _context.TegirmonTortilganBugdoyRoyxatiGroupDetail.UpdateRange(detail_list);
                await _context.SaveChangesAsync();

            }

          



            //update
            _context.TegirmonTortilganBugdoyRoyxatiGroup.Update(tegirmonTortilganBugdoyRoyxatiGroup);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiGroup;
        }

        [HttpGet("getPaginationOnlyNotAccepted")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyNotAccepted([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroup> categoryList = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true && p.accepted_status == false)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroup>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true && p.accepted_status == false).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOnlyAcceptedByDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyAcceptedByDate([FromQuery] int page, [FromQuery] int size,[FromQuery] DateTime begin_date,[FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroup> categoryList = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true
                && p.accepted_status == true
                && (p.date_time >= begin_date && p.date_time <= end_date))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroup>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true
                && p.accepted_status == true
                && (p.date_time >= begin_date && p.date_time <= end_date)).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        /// <summary>
        /// Filter bo'yicha accepted groups ro'yxatini qaytaradi
        /// filter_type: null yoki "all" - barcha, "changed_to_product" - mahsulotga almashtirilgan, "sold_for_money" - pulga sotilgan, "to_zaxira" - zaxiraga quyilgan
        /// </summary>
        [HttpGet("getPaginationOnlyAcceptedByDateWithFilter")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOnlyAcceptedByDateWithFilter(
            [FromQuery] int page, 
            [FromQuery] int size,
            [FromQuery] DateTime begin_date,
            [FromQuery] DateTime end_date,
            [FromQuery] string filter_type = null)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            
            // INVOICE TURLARI
            string INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH";
            string INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH";

            IQueryable<TegirmonTortilganBugdoyRoyxatiGroup> query = _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true
                && p.accepted_status == true
                && (p.date_time >= begin_date && p.date_time <= end_date));

            // Filter qo'llash
            if (!string.IsNullOrEmpty(filter_type) && filter_type != "all")
            {
                if (filter_type == "changed_to_product")
                {
                    // Mahsulotga almashtirilganlar - invoice status_inv_type_name == "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH"
                    var groupIdsWithChangedProduct = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                        .Include(p => p.bugdoyRoyxatiGroup)
                        .Include(p => p.invoice)
                        .Where(p => p.active_status == true
                            && p.bugdoyRoyxatiGroup != null
                            && p.bugdoyRoyxatiGroup.active_status == true
                            && p.bugdoyRoyxatiGroup.accepted_status == true
                            && p.bugdoyRoyxatiGroup.date_time >= begin_date
                            && p.bugdoyRoyxatiGroup.date_time <= end_date
                            && p.invoice != null
                            && p.invoice.active_status == true
                            && p.invoice.status_inv_type_name == INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH)
                        .Select(p => p.TegirmonTortilganBugdoyRoyxatiGroupid)
                        .Distinct()
                        .ToListAsync();

                    query = query.Where(p => groupIdsWithChangedProduct.Contains(p.id));
                }
                else if (filter_type == "sold_for_money")
                {
                    // Pulga sotilganlar - invoice note != null va summ > 0
                    var groupIdsSoldForMoney = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                        .Include(p => p.bugdoyRoyxatiGroup)
                        .Include(p => p.invoice)
                        .Where(p => p.active_status == true
                            && p.bugdoyRoyxatiGroup != null
                            && p.bugdoyRoyxatiGroup.active_status == true
                            && p.bugdoyRoyxatiGroup.accepted_status == true
                            && p.bugdoyRoyxatiGroup.date_time >= begin_date
                            && p.bugdoyRoyxatiGroup.date_time <= end_date
                            && p.invoice != null
                            && p.invoice.active_status == true
                            && p.invoice.note != null
                            && p.invoice.note != ""
                            && p.invoice.summ > 0)
                        .Select(p => p.TegirmonTortilganBugdoyRoyxatiGroupid)
                        .Distinct()
                        .ToListAsync();

                    query = query.Where(p => groupIdsSoldForMoney.Contains(p.id));
                }
                else if (filter_type == "to_zaxira")
                {
                    // Zaxiraga quyilganlar - name maydoni bo'sh bo'lmagan
                    var groupIdsToZaxira = await _context.TegirmonTortilganBugdoyRoyxatiGroupDetail
                        .Include(p => p.bugdoyRoyxatiGroup)
                        .Where(p => p.active_status == true
                            && p.bugdoyRoyxatiGroup != null
                            && p.bugdoyRoyxatiGroup.active_status == true
                            && p.bugdoyRoyxatiGroup.accepted_status == true
                            && p.bugdoyRoyxatiGroup.date_time >= begin_date
                            && p.bugdoyRoyxatiGroup.date_time <= end_date
                            && p.name != null
                            && p.name != "")
                        .Select(p => p.TegirmonTortilganBugdoyRoyxatiGroupid)
                        .Distinct()
                        .ToListAsync();

                    query = query.Where(p => groupIdsToZaxira.Contains(p.id));
                }
            }

            List<TegirmonTortilganBugdoyRoyxatiGroup> categoryList = await query
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();

            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroup>();
            }

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await query.CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationAll")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationAll([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroup> categoryList = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroup>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true ).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchByNameOrCarNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByNameOrCarNumber([FromQuery] int page, [FromQuery] int size,[FromQuery] String name)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTortilganBugdoyRoyxatiGroup> categoryList = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true 
                && (p.name.ToLower().Contains(name.ToLower()) 
                || p.car_number.ToLower().Contains(name.ToLower())))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonTortilganBugdoyRoyxatiGroup>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonTortilganBugdoyRoyxatiGroup
                .Where(p => p.active_status == true
                && (p.name.ToLower().Contains(name.ToLower())
                || p.car_number.ToLower().Contains(name.ToLower()))).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/TegirmonTortilganBugdoyRoyxatiGroups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonTortilganBugdoyRoyxatiGroup(long id, TegirmonTortilganBugdoyRoyxatiGroup tegirmonTortilganBugdoyRoyxatiGroup)
        {
            if (id != tegirmonTortilganBugdoyRoyxatiGroup.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonTortilganBugdoyRoyxatiGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonTortilganBugdoyRoyxatiGroupExists(id))
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

        // POST: api/TegirmonTortilganBugdoyRoyxatiGroups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroup>> PostTegirmonTortilganBugdoyRoyxatiGroup(TegirmonTortilganBugdoyRoyxatiGroup tegirmonTortilganBugdoyRoyxatiGroup)
        {
            _context.TegirmonTortilganBugdoyRoyxatiGroup.Update(tegirmonTortilganBugdoyRoyxatiGroup);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiGroup;
        }

        // DELETE: api/TegirmonTortilganBugdoyRoyxatiGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonTortilganBugdoyRoyxatiGroup>> DeleteTegirmonTortilganBugdoyRoyxatiGroup(long id)
        {
            var tegirmonTortilganBugdoyRoyxatiGroup = await _context.TegirmonTortilganBugdoyRoyxatiGroup.FindAsync(id);
            if (tegirmonTortilganBugdoyRoyxatiGroup == null)
            {
                return NotFound();
            }

            _context.TegirmonTortilganBugdoyRoyxatiGroup.Remove(tegirmonTortilganBugdoyRoyxatiGroup);
            await _context.SaveChangesAsync();

            return tegirmonTortilganBugdoyRoyxatiGroup;
        }

        private bool TegirmonTortilganBugdoyRoyxatiGroupExists(long id)
        {
            return _context.TegirmonTortilganBugdoyRoyxatiGroup.Any(e => e.id == id);
        }
    }
}
