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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personel>>> GetEmployees()
        {
            return await _context.Personeller
                .Include(p => p.BankInfo)
                .Include(p => p.AnnualLeaves)
                    .ThenInclude(y => y.UsedLeaveDays)
                .Include(p => p.UnpaidLeaves)
                .Include(p => p.Reports)
                .Include(p => p.Salaries)
                    .ThenInclude(m => m.ExtraPayments)
                .ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personel>> GetEmployee(int id)
        {
            var employee = await _context.Personeller
                .Include(p => p.BankInfo)
                .Include(p => p.AnnualLeaves)
                    .ThenInclude(y => y.UsedLeaveDays)
                .Include(p => p.UnpaidLeaves)
                .Include(p => p.Reports)
                .Include(p => p.Salaries)
                    .ThenInclude(m => m.ExtraPayments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Personel>> PostEmployee(Personel employee)
        {
            _context.Personeller.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Personel employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Personeller.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Personeller.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Employee/healthcheck
        [HttpGet("healthcheck")]
        public IActionResult HealthCheck()
        {
            return Ok("API is running.");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Personeller.Any(e => e.Id == id);
        }
    }
}
