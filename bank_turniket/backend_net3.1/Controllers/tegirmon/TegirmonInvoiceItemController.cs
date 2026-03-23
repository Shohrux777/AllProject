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
    public class TegirmonInvoiceItemController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonInvoiceItemController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonInvoiceItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonInvoiceItem>>> GetTegirmonInvoiceItem()
        {
            return await _context.TegirmonInvoiceItem.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceItem> categoryList = await _context.TegirmonInvoiceItem
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceItem>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceItem
                .Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationZaxiraProductIdBetweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationZaxiraProductIdBetweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceItem> categoryList = await _context.TegirmonInvoiceItem
                .Include(p => p.invoice)
                .Include(p => p.product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id && p.inv_accepted_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceItem>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceItem
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id && p.inv_accepted_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationChangeProductIdBetweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationChangeProductIdBetweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonInvoiceItem> categoryList = await _context.TegirmonInvoiceItem
                .Include(p => p.invoice)
                .Include(p => p.product)
                .ThenInclude(p => p.unitMeasurment)
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id && p.inv_accepted_status == false && p.auth_user_updator_id == 0
                && (p.invoice.status_inv_type_name == null || p.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH"))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonInvoiceItem>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonInvoiceItem
                .Include(p => p.invoice)
                .Where(p => p.active_status == true && (p.created_date_time >= begin_date && p.created_date_time <= end_date) 
                && p.TegirmonProductid == product_id && p.inv_accepted_status == false && p.auth_user_updator_id == 0
                && (p.invoice.status_inv_type_name == null || p.invoice.status_inv_type_name != "INVOICE_BUGDOY_PRIXOD_QILISH")).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getUsedInvoiceProductsQtyBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getUsedInvoiceProductsQtyBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_invoice inv ON inv.id = pit.\"TegirmonInvoiceid\" "+
                " WHERE pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND pit.active_status = true AND (inv.status_inv_type_name IS NULL OR inv.status_inv_type_name != 'INVOICE_BUGDOY_PRIXOD_QILISH') "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_invoice inv ON inv.id = pit.\"TegirmonInvoiceid\" "+
                " WHERE pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (inv.status_inv_type_name IS NULL OR inv.status_inv_type_name != 'INVOICE_BUGDOY_PRIXOD_QILISH') "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getUsedInvoiceProductsQtyZaxiraBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getUsedInvoiceProductsQtyZaxiraBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.inv_accepted_status = true AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.inv_accepted_status = true AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getUsedInvoiceProductsQtyZaxiraProductIdBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getUsedInvoiceProductsQtyZaxiraProductIdBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE p.id= " + product_id + " AND pit.inv_accepted_status = true AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE p.id= " + product_id + " AND pit.inv_accepted_status = true AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getUsedInvoiceProductsQtyFastChangeBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getUsedInvoiceProductsQtyFastChangeBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.inv_accepted_status = false AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.inv_accepted_status = false AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getUsedInvoiceProductsQtyFastChangeProductIdBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getUsedInvoiceProductsQtyFastChangeProductIdBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long product_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_invoice inv ON inv.id = pit.\"TegirmonInvoiceid\" "+
                " WHERE p.id= " + product_id + " AND pit.inv_accepted_status = false AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (inv.status_inv_type_name IS NULL OR inv.status_inv_type_name != 'INVOICE_BUGDOY_PRIXOD_QILISH') "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " LEFT JOIN tegirmon_invoice inv ON inv.id = pit.\"TegirmonInvoiceid\" "+
                " WHERE p.id= " + product_id + " AND pit.inv_accepted_status = false AND pit.active_status = true AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " AND (inv.status_inv_type_name IS NULL OR inv.status_inv_type_name != 'INVOICE_BUGDOY_PRIXOD_QILISH') "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPrixodInvoiceItemProductBeatweenDate")]
        public async Task<ActionResult<TexPaginationModel>> getPrixodInvoiceItemProductBeatweenDate([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String begin_date_str = begin_date.Date.ToString("yyyy-MM-dd");
            String end_date_str = end_date.Date.ToString("yyyy-MM-dd");
            List<TegirmonInvoiceItemProductTemp> itemList = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.auth_user_updator_id = 1 AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<TegirmonInvoiceItemProductTemp>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.TegirmonInvoiceItemProductTemp.FromSqlRaw("" +
                " SELECT p.name as product_name,  "+
                " p.id as product_id, "+
                " sum(pit.qty) as total_qty, "+
                " sum(pit.real_qty) as total_real_qty,  "+
                " sum(pit.sum) as total_sum,  "+
                " sum(pit.real_sum) as total_real_sum " +
                " FROM tegirmon_invoice_item pit  "+
                " LEFT JOIN tegirmon_product p ON p.id = pit.\"TegirmonProductid\" "+
                " WHERE pit.auth_user_updator_id = 1 AND pit.created_date_time between '"+ begin_date_str + " 00:00:00' and '"+ end_date_str + " 23:59:59' "+
                " GROUP BY p.name, p.id ORDER BY  total_real_qty DESC ").CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        // GET: api/TegirmonInvoiceItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonInvoiceItem>> GetTegirmonInvoiceItem(long id)
        {
            var tegirmonInvoiceItem = await _context.TegirmonInvoiceItem.FindAsync(id);

            if (tegirmonInvoiceItem == null)
            {
                return NotFound();
            }

            return tegirmonInvoiceItem;
        }

        // PUT: api/TegirmonInvoiceItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonInvoiceItem(long id, TegirmonInvoiceItem tegirmonInvoiceItem)
        {
            if (id != tegirmonInvoiceItem.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonInvoiceItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonInvoiceItemExists(id))
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

        // POST: api/TegirmonInvoiceItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonInvoiceItem>> PostTegirmonInvoiceItem(TegirmonInvoiceItem tegirmonInvoiceItem)
        {
            _context.TegirmonInvoiceItem.Update(tegirmonInvoiceItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonInvoiceItem", new { id = tegirmonInvoiceItem.id }, tegirmonInvoiceItem);
        }

        // DELETE: api/TegirmonInvoiceItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonInvoiceItem>> DeleteTegirmonInvoiceItem(long id)
        {
            var tegirmonInvoiceItem = await _context.TegirmonInvoiceItem.FindAsync(id);
            if (tegirmonInvoiceItem == null)
            {
                return NotFound();
            }

            _context.TegirmonInvoiceItem.Remove(tegirmonInvoiceItem);
            await _context.SaveChangesAsync();

            return tegirmonInvoiceItem;
        }

        private bool TegirmonInvoiceItemExists(long id)
        {
            return _context.TegirmonInvoiceItem.Any(e => e.id == id);
        }
    }
}
