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
    public class TegirmonInvoiceNewsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonInvoiceNewsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonInvoiceNews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonInvoiceNew>>> GetTegirmonInvoiceNew()
        {
            return await _context.TegirmonInvoiceNew.ToListAsync();
        }

        // GET: api/TegirmonInvoiceNews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonInvoiceNew>> GetTegirmonInvoiceNew(long id)
        {
            var tegirmonInvoiceNew = await _context.TegirmonInvoiceNew.FindAsync(id);

            if (tegirmonInvoiceNew == null)
            {
                return NotFound();
            }

            return tegirmonInvoiceNew;
        }

        // Mashina bilan tortilganlar listi
        [HttpGet("getPaginationMashinaBilanTortilganlarList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationMashinaBilanTortilganlarList([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Include(p => p.ticket)
                .Include(p => p.client)
                .Where(p => p.pull_status == true && p.rashshod_status == false && p.finish_status == false)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => p.pull_status == true && p.rashshod_status == false && p.finish_status == false).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // rasshod qilinmaganlar listi
        [HttpGet("getPaginationRashshodQilinmaganlarList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationRashshodQilinmaganlarList([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.pull_status == true && p.rashshod_status == true && p.finish_status == false)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.pull_status == true && p.rashshod_status == true && p.finish_status == false).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // rasshod qilinmaganlar listi
        [HttpGet("getPaginationRashshodBulganlarList")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationRashshodBulganlarList([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.finish_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => (p.created_date_time >= b_date && p.created_date_time <= e_date)
                && p.finish_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        

        // rasshod qilinmagan qilingan klient invoicelari
        [HttpGet("getPaginationInvoiceNewClientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationInvoiceNewClientId([FromQuery] int page, [FromQuery] int size,
[FromQuery]long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id && p.rashshod_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id && p.rashshod_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // rasshod qilinmaganlar listi klientlarniki
        [HttpGet("getPaginationInvoiceNewNotPaidClientId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationInvoiceNewNotPaidClientId([FromQuery] int page, [FromQuery] int size,
[FromQuery]long client_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id && p.rashshod_status == true && p.status_1 == false && p.status_2 == false)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id && p.rashshod_status == true && p.status_1 == false && p.status_2 == false).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // rasshod qilinmaganlar listi klientlarniki product_id bilan
        [HttpGet("getPaginationInvoiceNewNotPaidClientIdAndProductID")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationInvoiceNewNotPaidClientIdAndProductID([FromQuery] int page, [FromQuery] int size,
[FromQuery]long client_id, [FromQuery]long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id  && p.rashshod_status == true && 
                    p.status_1 == false && p.status_2 == false && p.TegirmonProductid == product_id)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonClientNewid == client_id && p.rashshod_status == true && 
                    p.status_1 == false && p.status_2 == false && p.TegirmonProductid == product_id).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // bir oylik oldi sottilar hisoboti
        [HttpGet("getPaginationInvoiceNewClientsReport")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationInvoiceNewClientsReport([FromQuery] int page, [FromQuery] int size,
[FromQuery] DateTime b_date, [FromQuery] DateTime e_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> categoryList = await _context.TegirmonInvoiceNew
                .Where(p => (p.date_second_pull >= b_date && p.date_second_pull <= e_date)
                && p.rashshod_status == true && p.TegirmonClientNewid > 0)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceNew>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => (p.date_second_pull >= b_date && p.date_second_pull <= e_date)
                && p.rashshod_status == true && p.TegirmonClientNewid > 0).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // full info invoice id
        // GET: api/TegirmonInvoiceNew/5
        [HttpGet("getAnyInvoiceNewFullInfoById")]
        public async Task<ActionResult<TegirmonInvoiceNew>> getAnyInvoiceNewFullInfoById([FromQuery]long invoice_id)
        {
            var TegirmonInvoiceNew = await _context.TegirmonInvoiceNew
                .Include(p => p.product)
                .Include(p => p.client)
                .Include(p => p.ticket)
                .Include(p =>p.item_list)
                .ThenInclude(p =>p.product)
                .Include(p => p.auth)
                .ThenInclude(p => p.user)
                .Where(p =>p.id == invoice_id).ToListAsync();

            if (TegirmonInvoiceNew == null || TegirmonInvoiceNew.Count == 0)
            {
                return NotFound();
            }

            return TegirmonInvoiceNew.First();
        }

        // GET: api/TegirmonInvoiceNews/getInvoicesByTicketId
        [HttpGet("getInvoicesByTicketId")]
        public async Task<ActionResult<TexPaginationModel>> getInvoicesByTicketId([FromQuery] long ticket_id, [FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceNew> invoiceList = await _context.TegirmonInvoiceNew
                .Include(p => p.product)
                .Include(p => p.client)
                .Include(p => p.ticket)
                .Where(p => p.TegirmonTicketid == ticket_id && p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();

            if (invoiceList == null)
            {
                invoiceList = new List<TegirmonInvoiceNew>();
            }

            paginationModel.items_list = JArray.FromObject(invoiceList);
            paginationModel.items_count = await _context.TegirmonInvoiceNew
                .Where(p => p.TegirmonTicketid == ticket_id && p.active_status == true)
                .CountAsync();
            paginationModel.current_item_count = invoiceList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // PUT: api/TegirmonInvoiceNews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonInvoiceNew(long id, TegirmonInvoiceNew tegirmonInvoiceNew)
        {
            if (id != tegirmonInvoiceNew.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonInvoiceNew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonInvoiceNewExists(id))
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

        // POST: api/TegirmonInvoiceNews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonInvoiceNew>> PostTegirmonInvoiceNew(TegirmonInvoiceNew tegirmonInvoiceNew)
        {
            _context.TegirmonInvoiceNew.Update(tegirmonInvoiceNew);
            await _context.SaveChangesAsync();

            // Ticket yangilash logikasi
            if (tegirmonInvoiceNew.TegirmonTicketid != null && tegirmonInvoiceNew.TegirmonTicketid > 0 && tegirmonInvoiceNew.car_measure > 0)
            {
                var ticket = await _context.TegirmonTicket.FindAsync(tegirmonInvoiceNew.TegirmonTicketid);
                if (ticket != null)
                {
                    // Barcha invoice lardagi product_measure larni yig'indisini olish
                    var totalInvoiceMassa = await _context.TegirmonInvoiceNew
                        .Where(i => i.TegirmonTicketid == tegirmonInvoiceNew.TegirmonTicketid && i.car_measure > 0)
                        .SumAsync(i => i.product_measure);

                    ticket.invoice_massa = totalInvoiceMassa;

                    // Percentage hisoblash: (invoice_massa / massa) * 100
                    if (ticket.massa > 0)
                    {
                        ticket.percentage = (ticket.invoice_massa / ticket.massa) * 100.0;
                    }
                    else
                    {
                        ticket.percentage = 0.0;
                    }

                    _context.TegirmonTicket.Update(ticket);
                    await _context.SaveChangesAsync();
                }
            }

            return CreatedAtAction("GetTegirmonInvoiceNew", new { id = tegirmonInvoiceNew.id }, tegirmonInvoiceNew);
        }

        // DELETE: api/TegirmonInvoiceNews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonInvoiceNew>> DeleteTegirmonInvoiceNew(long id)
        {
            var tegirmonInvoiceNew = await _context.TegirmonInvoiceNew.FindAsync(id);
            if (tegirmonInvoiceNew == null)
            {
                return NotFound();
            }

            _context.TegirmonInvoiceNew.Remove(tegirmonInvoiceNew);
            await _context.SaveChangesAsync();

            return tegirmonInvoiceNew;
        }

        private bool TegirmonInvoiceNewExists(long id)
        {
            return _context.TegirmonInvoiceNew.Any(e => e.id == id);
        }
    }
}
