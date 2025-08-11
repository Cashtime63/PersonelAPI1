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
    public class UnpaidLeaveController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public UnpaidLeaveController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/UnpaidLeave
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnpaidLeave>>> GetUnpaidLeaves()
        {
            var list = await _context.UnpaidLeaves.Include(p => p.Personel).ToListAsync();
            return list;
        }

        // GET: api/UnpaidLeave/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnpaidLeave>> GetUnpaidLeave(int id)
        {
            var leave = await _context.UnpaidLeaves.Include(p => p.Personel).FirstOrDefaultAsync(l => l.Id == id);
            if (leave == null)
            {
                return NotFound();
            }
            return leave;
        }

        // POST: api/UnpaidLeave
        [HttpPost]
        public async Task<ActionResult<UnpaidLeave>> PostUnpaidLeave(UnpaidLeave leave)
        {
            await _context.UnpaidLeaves.AddAsync(leave);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnpaidLeave), new { id = leave.Id }, leave);
        }

        // PUT: api/UnpaidLeave/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnpaidLeave(int id, UnpaidLeave leave)
        {
            if (id != leave.Id)
            {
                return BadRequest();
            }

            _context.Entry(leave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UnpaidLeaves.Any(e => e.Id == id))
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

        // DELETE: api/UnpaidLeave/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnpaidLeave(int id)
        {
            var leave = await _context.UnpaidLeaves.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }

            _context.UnpaidLeaves.Remove(leave);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
