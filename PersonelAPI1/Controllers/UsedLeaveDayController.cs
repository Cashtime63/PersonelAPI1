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
    public class UsedLeaveDayController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public UsedLeaveDayController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/UsedLeaveDay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsedLeaveDay>>> GetUsedLeaveDays()
        {
            return await _context.UsedLeaveDays.ToListAsync();
        }

        // GET: api/UsedLeaveDay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsedLeaveDay>> GetUsedLeaveDay(int id)
        {
            var leaveDay = await _context.UsedLeaveDays.FindAsync(id);
            if (leaveDay == null)
            {
                return NotFound();
            }
            return leaveDay;
        }

        // POST: api/UsedLeaveDay
        [HttpPost]
        public async Task<ActionResult<UsedLeaveDay>> PostUsedLeaveDay(UsedLeaveDay leaveDay)
        {
            await _context.UsedLeaveDays.AddAsync(leaveDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsedLeaveDay), new { id = leaveDay.Id }, leaveDay);
        }

        // PUT: api/UsedLeaveDay/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsedLeaveDay(int id, UsedLeaveDay leaveDay)
        {
            if (id != leaveDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UsedLeaveDays.Any(e => e.Id == id))
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

        // DELETE: api/UsedLeaveDay/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsedLeaveDay(int id)
        {
            var leaveDay = await _context.UsedLeaveDays.FindAsync(id);
            if (leaveDay == null)
            {
                return NotFound();
            }

            _context.UsedLeaveDays.Remove(leaveDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
