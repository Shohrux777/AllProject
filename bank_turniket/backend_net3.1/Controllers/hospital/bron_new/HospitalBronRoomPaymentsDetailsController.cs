using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.hospital.bron_new;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.hospital.bron_new
{
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalBronRoomPaymentsDetailsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HospitalBronRoomPaymentsDetailsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/HospitalBronRoomPaymentsDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalBronRoomPaymentsDetail>>> GetHospitalBronRoomPaymentsDetail()
        {
            return await _context.HospitalBronRoomPaymentsDetail.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsDetail> itemList = await _context.HospitalBronRoomPaymentsDetail
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsDetail>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsDetail.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBypaymentid")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBypaymentid([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long HospitalBronRoomPaymentsid)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<HospitalBronRoomPaymentsDetail> itemList = await _context.HospitalBronRoomPaymentsDetail
                .Where(p =>p.HospitalBronRoomPaymentsid == HospitalBronRoomPaymentsid)
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<HospitalBronRoomPaymentsDetail>();
            }



            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.HospitalBronRoomPaymentsDetail.Where(p => p.HospitalBronRoomPaymentsid == HospitalBronRoomPaymentsid).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/HospitalBronRoomPaymentsDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalBronRoomPaymentsDetail>> GetHospitalBronRoomPaymentsDetail(long id)
        {
            var hospitalBronRoomPaymentsDetail = await _context.HospitalBronRoomPaymentsDetail.FindAsync(id);

            if (hospitalBronRoomPaymentsDetail == null)
            {
                return NotFound();
            }

            return hospitalBronRoomPaymentsDetail;
        }

        // PUT: api/HospitalBronRoomPaymentsDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalBronRoomPaymentsDetail(long id, HospitalBronRoomPaymentsDetail hospitalBronRoomPaymentsDetail)
        {
            if (id != hospitalBronRoomPaymentsDetail.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalBronRoomPaymentsDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalBronRoomPaymentsDetailExists(id))
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

        // POST: api/HospitalBronRoomPaymentsDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalBronRoomPaymentsDetail>> PostHospitalBronRoomPaymentsDetail(HospitalBronRoomPaymentsDetail hospitalBronRoomPaymentsDetail)
        {
            _context.HospitalBronRoomPaymentsDetail.Update(hospitalBronRoomPaymentsDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalBronRoomPaymentsDetail", new { id = hospitalBronRoomPaymentsDetail.id }, hospitalBronRoomPaymentsDetail);
        }

        // DELETE: api/HospitalBronRoomPaymentsDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalBronRoomPaymentsDetail>> DeleteHospitalBronRoomPaymentsDetail(long id)
        {
            var hospitalBronRoomPaymentsDetail = await _context.HospitalBronRoomPaymentsDetail.FindAsync(id);
            if (hospitalBronRoomPaymentsDetail == null)
            {
                return NotFound();
            }

            _context.HospitalBronRoomPaymentsDetail.Remove(hospitalBronRoomPaymentsDetail);
            await _context.SaveChangesAsync();

            return hospitalBronRoomPaymentsDetail;
        }

        private bool HospitalBronRoomPaymentsDetailExists(long id)
        {
            return _context.HospitalBronRoomPaymentsDetail.Any(e => e.id == id);
        }
    }
}
