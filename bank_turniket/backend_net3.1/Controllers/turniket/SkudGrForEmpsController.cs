using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudGrForEmpsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudGrForEmpsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("addGrafikForEmplyees")]
        public async Task<ActionResult<IEnumerable<SkudGrForEmp>>> addGraficForEmplyees(List<SkudGrForEmp> skudGrForEmpList)
        {
            _context.SkudGrForEmp.UpdateRange(skudGrForEmpList);
            await _context.SaveChangesAsync();

            return skudGrForEmpList;
        }

        [HttpGet("addGrafikForEmplyeesGet")]
        public async Task<ActionResult<IEnumerable<SkudGrForEmp>>> addGrafikForEmplyeesGet(List<SkudGrForEmp> skudGrForEmpList)
        {
            _context.SkudGrForEmp.UpdateRange(skudGrForEmpList);
            await _context.SaveChangesAsync();

            return skudGrForEmpList;
        }     
        
        [HttpGet("addGrafikForEmplyeesGetRowSql")]
        public async Task<ActionResult<IEnumerable<SkudGrForEmp>>> addGrafikForEmplyeesGetRowSql([FromQuery]SkudGrForEmp skudGrForEmpRow)
        {
            String begin_date_str = skudGrForEmpRow._begin.Date.ToString("yyyy-MM-dd");
            String end_date_str = skudGrForEmpRow._end.Date.ToString("yyyy-MM-dd");
            string name_dep = skudGrForEmpRow.gr_nomi;
            List<SkudGrForEmp> skudGrForEmpList = await _context.SkudGrForEmp
                .FromSqlRaw(" BEGIN;" + 
                "\r\nDELETE FROM gr_for_emp WHERE id_emp = " + skudGrForEmpRow.id_emp.ToString() + ";" + " " +
                "\r\nCOMMIT;" + " " +
                "\r\nBEGIN;" + " " +
                "\r\nINSERT INTO gr_for_emp(" + " " +
                "\r\nid_emp, gr_nomi, _begin, _end)" + " " +
                "\r\n" + "VALUES ("+ skudGrForEmpRow.id_emp.ToString() + ", '"+ name_dep + "', '"+begin_date_str+"', '"+end_date_str+"');" +
                "\r\nCOMMIT;" + " " +
                "\r\nSELECT id_emp, gr_nomi, _begin, _end" + " " +
                "\r\n  FROM gr_for_emp WHERE id_emp = " + skudGrForEmpRow.id_emp.ToString() + ";").ToListAsync();

            return skudGrForEmpList;
        }

        [HttpGet("addGrafikForEmplyeesGetOnlyOne")]
        public async Task<ActionResult<SkudGrForEmp>> addGrafikForEmplyeesGetOnlyOne([FromQuery]SkudGrForEmp skudGrForEmpList)
        {
            _context.SkudGrForEmp.Update(skudGrForEmpList);
            await _context.SaveChangesAsync();

            return skudGrForEmpList;
        }

        // GET: api/SkudGrForEmps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudGrForEmp>>> GetSkudGrForEmp()
        {
            return await _context.SkudGrForEmp.ToListAsync();
        }

        // GET: api/SkudGrForEmps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudGrForEmp>> GetSkudGrForEmp(long id)
        {
            var skudGrForEmp = await _context.SkudGrForEmp.FindAsync(id);

            if (skudGrForEmp == null)
            {
                return NotFound();
            }

            return skudGrForEmp;
        }

        // PUT: api/SkudGrForEmps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudGrForEmp(long id, SkudGrForEmp skudGrForEmp)
        {
            if (id != skudGrForEmp.id_emp)
            {
                return BadRequest();
            }

            _context.Entry(skudGrForEmp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudGrForEmpExists(id))
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

        // POST: api/SkudGrForEmps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudGrForEmp>> PostSkudGrForEmp(SkudGrForEmp skudGrForEmp)
        {
            _context.SkudGrForEmp.Add(skudGrForEmp);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSkudGrForEmp", new { id = skudGrForEmp.id_emp }, skudGrForEmp);
        }

        [HttpPost("forUpdateUser")]
        public async Task<ActionResult<SkudGrForEmp>> forUpdateUser([FromQuery] SkudGrForEmp skudGrForEmp)
        {
            _context.SkudGrForEmp.Update(skudGrForEmp);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSkudGrForEmp", new { id = skudGrForEmp.id_emp }, skudGrForEmp);
        }

        // DELETE: api/SkudGrForEmps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudGrForEmp>> DeleteSkudGrForEmp(long id)
        {
            var skudGrForEmp = await _context.SkudGrForEmp.FindAsync(id);
            if (skudGrForEmp == null)
            {
                return NotFound();
            }

            _context.SkudGrForEmp.Remove(skudGrForEmp);
            await _context.SaveChangesAsync();

            return skudGrForEmp;
        }

        private bool SkudGrForEmpExists(long id)
        {
            return _context.SkudGrForEmp.Any(e => e.id_emp == id);
        }
    }
}
