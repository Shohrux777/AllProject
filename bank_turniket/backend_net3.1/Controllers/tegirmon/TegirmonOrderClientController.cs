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
    public class TegirmonOrderClientController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonOrderClientController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonOrderClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonOrderClient>>> GetTegirmonOrderClient()
        {
            return await _context.TegirmonOrderClient.ToListAsync();
        }
        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClient> categoryList = await _context.TegirmonOrderClient
                .Include(p => p.district)
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClient>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClient.Where(p => p.active_status == true).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber([FromQuery] int page, [FromQuery] int size,[FromQuery] String fio_or_serial_number)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClient> categoryList = new List<TegirmonOrderClient>();
            if (fio_or_serial_number.Trim().Length > 0)
            {
                categoryList = await _context.TegirmonOrderClient
                .Where(p => p.active_status == true &&
                (p.fio.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.passport_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.phone_number.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.phone_number1.ToLower().Contains(fio_or_serial_number.ToLower())
                 || p.phone_number2.ToLower().Contains(fio_or_serial_number.ToLower())
                 ))
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }
            else {
                categoryList = await _context.TegirmonOrderClient
                .Where(p => p.active_status == true)
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            }


            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClient>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = categoryList.Count();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationByNote")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByNote([FromQuery] int page, [FromQuery] int size, [FromQuery] String note_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClient> categoryList = await _context.TegirmonOrderClient
                .Where(p => p.active_status == true
                && p.note.ToLower().Contains(note_str.ToLower()))

                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClient>();
            }


            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClient.Where(p => p.active_status == true
            && p.note.ToLower().Contains(note_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


         [HttpGet("getPaginationByBornDate")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByBornDate([FromQuery] int page, [FromQuery] int size,[FromQuery] String born_date_str)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<TegirmonOrderClient> categoryList = await _context.TegirmonOrderClient
                .Where(p => p.active_status == true
                && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower()))
                
                .Skip(page * size).Take(size).OrderByDescending(p => p.id).ToListAsync();
            if (categoryList == null)
            {
                categoryList = new List<TegirmonOrderClient>();
            }

            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = await _context.TegirmonOrderClient.Where(p => p.active_status == true 
            && p.addiotionala_information.ToLower().Contains(born_date_str.ToLower())).CountAsync();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/TegirmonOrderClient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonOrderClient>> GetTegirmonOrderClient(long id)
        {
            var tegirmonOrderClient = await _context.TegirmonOrderClient.FindAsync(id);

            if (tegirmonOrderClient == null)
            {
                return NotFound();
            }

            return tegirmonOrderClient;
        }

        // PUT: api/TegirmonOrderClient/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonOrderClient(long id, TegirmonOrderClient tegirmonOrderClient)
        {
            if (id != tegirmonOrderClient.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonOrderClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonOrderClientExists(id))
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

        // POST: api/TegirmonOrderClient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonOrderClient>> PostTegirmonOrderClient(TegirmonOrderClient tegirmonOrderClient)
        {
            _context.TegirmonOrderClient.Update(tegirmonOrderClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonOrderClient", new { id = tegirmonOrderClient.id }, tegirmonOrderClient);
        }

        // DELETE: api/TegirmonOrderClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonOrderClient>> DeleteTegirmonOrderClient(long id)
        {
            var tegirmonOrderClient = await _context.TegirmonOrderClient.FindAsync(id);
            if (tegirmonOrderClient == null)
            {
                return NotFound();
            }

            _context.TegirmonOrderClient.Remove(tegirmonOrderClient);
            await _context.SaveChangesAsync();

            return tegirmonOrderClient;
        }

        // GET: api/TegirmonOrderClient/getClientsWithBalance
        // sum > 0 yoki dollor > 0 yoki product bor bo'lgan clientlarni va ularning productlarini qaytaradi
        [HttpGet("getClientsWithBalance")]
        public async Task<ActionResult<IEnumerable<object>>> GetClientsWithBalance()
        {
            try
            {
                // Barcha active clientlarni olish
                var allClients = await _context.TegirmonOrderClient
                    .Include(c => c.district)
                    .Where(c => c.active_status == true)
                    .ToListAsync();

                // Har bir client uchun products ni olish va filter qilish
                var result = new List<object>();
                
                foreach (var c in allClients)
                {
                    // Productlarni olish (faqat qty > 0 bo'lganlar)
                    var products = await _context.TegirmonOrderClientProduct
                        .Where(p => p.TegirmonOrderClientId == c.id && p.active_status == true && p.qty > 0)
                        .Include(p => p.product)
                        .ThenInclude(p => p.unitMeasurment)
                        .Select(p => new
                        {
                            id = p.id,
                            product_name = p.product != null ? p.product.name : "",
                            qty = p.qty,
                            real_qty = p.real_qty,
                            unit = p.product != null && p.product.unitMeasurment != null ? p.product.unitMeasurment.name : ""
                        })
                        .ToListAsync();

                    // Agar sum > 0 yoki dollor > 0 yoki product bor bo'lsa, qo'shish
                    if (c.sum > 0 || c.dollor > 0 || products.Count > 0)
                    {
                        result.Add(new
                        {
                            id = c.id,
                            name = c.name,
                            fio = c.fio,
                            adddress = c.adddress,
                            passport_number = c.passport_number,
                            phone_number = c.phone_number,
                            phone_number1 = c.phone_number1,
                            phone_number2 = c.phone_number2,
                            sum = c.sum,
                            dollor = c.dollor,
                            district = c.district,
                            products = products,
                            hasProducts = products.Count > 0, // Product borligini belgilash
                            totalBalance = c.sum + (c.dollor * 10000) // Umumiy balans
                        });
                    }
                }

                // Sort qilish:
                // 1. Avval product bor bo'lganlar (hasProducts = true)
                // 2. Keyin umumiy balans bo'yicha kamayish tartibida (ko'p puli borlar yuqorida)
                result = result.OrderByDescending(r => 
                {
                    var hasProducts = (bool)((dynamic)r).hasProducts;
                    return hasProducts ? 1 : 0; // Product bor bo'lsa 1, yo'q bo'lsa 0
                })
                .ThenByDescending(r => 
                {
                    var totalBalance = (double)((dynamic)r).totalBalance;
                    return totalBalance;
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        private bool TegirmonOrderClientExists(long id)
        {
            return _context.TegirmonOrderClient.Any(e => e.id == id);
        }
    }
}
