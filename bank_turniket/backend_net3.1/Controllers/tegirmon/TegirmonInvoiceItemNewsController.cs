using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonInvoiceItemNewsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        //INVOICE TURLARI
        private String INVOICE_BUGDOY_SOTISH_PULGA = "INVOICE_BUGDOY_SOTISH_PULGA";
        private String INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH = "INVOICE_BUGDOY_CHIQARISH_TEGIRMONDAN_UN_QILISH";
        private String INVOICE_BUGDOY_PRIXOD_QILISH = "INVOICE_BUGDOY_PRIXOD_QILISH";
        private String INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR = "INVOICE_BUGDOY_RASXOD_BRAK_CHIQQANLAR";
        private String INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH";
        private String INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH = "INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH";
        private String INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH = "INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH";

        public TegirmonInvoiceItemNewsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonInvoiceItemNews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonInvoiceItemNew>>> GetTegirmonInvoiceItemNew()
        {
            return await _context.TegirmonInvoiceItemNew.ToListAsync();
        }



        // GET: api/TegirmonInvoiceItemNews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonInvoiceItemNew>> GetTegirmonInvoiceItemNew(long id)
        {
            var tegirmonInvoiceItemNew = await _context.TegirmonInvoiceItemNew.FindAsync(id);

            if (tegirmonInvoiceItemNew == null)
            {
                return NotFound();
            }

            return tegirmonInvoiceItemNew;
        }

        // PUT: api/TegirmonInvoiceItemNews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonInvoiceItemNew(long id, TegirmonInvoiceItemNew tegirmonInvoiceItemNew)
        {
            if (id != tegirmonInvoiceItemNew.id)
            {
                return BadRequest();
            }

            _context.Entry(tegirmonInvoiceItemNew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonInvoiceItemNewExists(id))
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

        // POST: api/TegirmonInvoiceItemNews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TegirmonInvoiceItemNew>> PostTegirmonInvoiceItemNew(TegirmonInvoiceItemNew tegirmonInvoiceItemNew)
        {
            _context.TegirmonInvoiceItemNew.Add(tegirmonInvoiceItemNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonInvoiceItemNew", new { id = tegirmonInvoiceItemNew.id }, tegirmonInvoiceItemNew);
        }

        // DELETE: api/TegirmonInvoiceItemNews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonInvoiceItemNew>> DeleteTegirmonInvoiceItemNew(long id)
        {
            var tegirmonInvoiceItemNew = await _context.TegirmonInvoiceItemNew.FindAsync(id);
            if (tegirmonInvoiceItemNew == null)
            {
                return NotFound();
            }

            _context.TegirmonInvoiceItemNew.Remove(tegirmonInvoiceItemNew);
            await _context.SaveChangesAsync();

            return tegirmonInvoiceItemNew;
        }

        private bool TegirmonInvoiceItemNewExists(long id)
        {
            return _context.TegirmonInvoiceItemNew.Any(e => e.id == id);
        }
    }
}
