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
    public class TegirmonTicketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonTicketController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonTicket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonTicket>>> GetTegirmonTicket()
        {
            return await _context.TegirmonTicket.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = await _context.TegirmonTicket
                .Where(t => t.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(t => t.id).ToListAsync();
            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = await _context.TegirmonTicket.Where(t => t.active_status == true).CountAsync();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchByTicketNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByTicketNumber([FromQuery] int page, [FromQuery] int size, [FromQuery] String ticket_number)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = new List<TegirmonTicket>();
            if (ticket_number != null && ticket_number.Trim().Length > 0)
            {
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true &&
                    (t.ticket_number != null && t.ticket_number.ToLower().Contains(ticket_number.ToLower())))
                    .Skip(page * size).Take(size).OrderByDescending(t => t.id).ToListAsync();
            }
            else
            {
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true)
                    .Skip(page * size).Take(size).OrderByDescending(t => t.id).ToListAsync();
            }

            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = ticketList.Count();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationOpenTickets")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationOpenTickets([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == false)
                .Skip(page * size).Take(size).OrderByDescending(t => t.id).ToListAsync();
            
            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == false)
                .CountAsync();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchOpenTickets")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchOpenTickets([FromQuery] int page, [FromQuery] int size, [FromQuery] String search)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = new List<TegirmonTicket>();
            
            if (search != null && search.Trim().Length > 0)
            {
                string searchLower = search.ToLower();
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true && t.is_closed == false &&
                    ((t.ticket_number != null && t.ticket_number.ToLower().Contains(searchLower)) ||
                     (t.organization_name != null && t.organization_name.ToLower().Contains(searchLower))))
                    .OrderByDescending(t => t.id)
                    .Skip(page * size).Take(size).ToListAsync();
            }
            else
            {
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true && t.is_closed == false)
                    .OrderByDescending(t => t.id)
                    .Skip(page * size).Take(size).ToListAsync();
            }

            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == false &&
                (search == null || search.Trim().Length == 0 ||
                 (t.ticket_number != null && t.ticket_number.ToLower().Contains(search.ToLower())) ||
                 (t.organization_name != null && t.organization_name.ToLower().Contains(search.ToLower()))))
                .CountAsync();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationClosedTickets")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationClosedTickets([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == true)
                .OrderByDescending(t => t.closed_date_time ?? t.updated_date_time)
                .Skip(page * size).Take(size).ToListAsync();
            
            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == true)
                .CountAsync();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchClosedTickets")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchClosedTickets([FromQuery] int page, [FromQuery] int size, [FromQuery] String search)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonTicket> ticketList = new List<TegirmonTicket>();
            
            if (search != null && search.Trim().Length > 0)
            {
                string searchLower = search.ToLower();
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true && t.is_closed == true &&
                    ((t.ticket_number != null && t.ticket_number.ToLower().Contains(searchLower)) ||
                     (t.organization_name != null && t.organization_name.ToLower().Contains(searchLower))))
                    .OrderByDescending(t => t.closed_date_time ?? t.updated_date_time)
                    .Skip(page * size).Take(size).ToListAsync();
            }
            else
            {
                ticketList = await _context.TegirmonTicket
                    .Where(t => t.active_status == true && t.is_closed == true)
                    .OrderByDescending(t => t.closed_date_time ?? t.updated_date_time)
                    .Skip(page * size).Take(size).ToListAsync();
            }

            if (ticketList == null)
            {
                ticketList = new List<TegirmonTicket>();
            }
            
            paginationModel.items_list = JArray.FromObject(ticketList);
            paginationModel.items_count = await _context.TegirmonTicket
                .Where(t => t.active_status == true && t.is_closed == true &&
                (search == null || search.Trim().Length == 0 ||
                 (t.ticket_number != null && t.ticket_number.ToLower().Contains(search.ToLower())) ||
                 (t.organization_name != null && t.organization_name.ToLower().Contains(search.ToLower()))))
                .CountAsync();
            paginationModel.current_item_count = ticketList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonTicket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonTicket>> GetTegirmonTicket(long id)
        {
            var tegirmonTicket = await _context.TegirmonTicket.FindAsync(id);

            if (tegirmonTicket == null)
            {
                return NotFound();
            }

            return tegirmonTicket;
        }

        // PUT: api/TegirmonTicket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonTicket(long id, TegirmonTicket tegirmonTicket)
        {
            if (id != tegirmonTicket.id)
            {
                return BadRequest();
            }

            tegirmonTicket.updated_date_time = DateTime.Now;
            _context.Entry(tegirmonTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonTicketExists(id))
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

        // POST: api/TegirmonTicket
        [HttpPost]
        public async Task<ActionResult<TegirmonTicket>> PostTegirmonTicket(TegirmonTicket tegirmonTicket)
        {
            tegirmonTicket.created_date_time = DateTime.Now;
            tegirmonTicket.updated_date_time = DateTime.Now;
            if (tegirmonTicket.tegirmonAuthId == null || tegirmonTicket.tegirmonAuthId == 0)
            {
                // Request header dan yoki boshqa joydan olish mumkin
                // Hozircha default qiymat
            }
            _context.TegirmonTicket.Add(tegirmonTicket);
            await _context.SaveChangesAsync();
            return tegirmonTicket;
        }

        // DELETE: api/TegirmonTicket/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonTicket>> DeleteTegirmonTicket(long id)
        {
            var tegirmonTicket = await _context.TegirmonTicket.FindAsync(id);
            if (tegirmonTicket == null)
            {
                return NotFound();
            }

            tegirmonTicket.active_status = false;
            tegirmonTicket.updated_date_time = DateTime.Now;
            await _context.SaveChangesAsync();

            return tegirmonTicket;
        }

        // Close Ticket: api/TegirmonTicket/close/5
        [HttpPut("close/{id}")]
        public async Task<ActionResult<TegirmonTicket>> CloseTegirmonTicket(long id)
        {
            var tegirmonTicket = await _context.TegirmonTicket.FindAsync(id);
            if (tegirmonTicket == null)
            {
                return NotFound();
            }

            tegirmonTicket.is_closed = true;
            tegirmonTicket.closed_date_time = DateTime.Now;
            tegirmonTicket.updated_date_time = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return tegirmonTicket;
        }

        private bool TegirmonTicketExists(long id)
        {
            return _context.TegirmonTicket.Any(e => e.id == id);
        }
    }
}

