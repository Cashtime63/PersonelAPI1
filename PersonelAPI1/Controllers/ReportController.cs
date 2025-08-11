using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PersonelAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public ReportController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Report
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        // GET: api/Report/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        // POST: api/Report
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        }

        // PUT: api/Report/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reports.Any(e => e.Id == id))
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

        // DELETE: api/Report/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
