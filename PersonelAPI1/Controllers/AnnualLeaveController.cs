using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PersonelAPI1.Data;

namespace PersonelAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnualLeaveController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public AnnualLeaveController(EmployeeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnualLeave>>> GetAnnualLeaves()
        {
            return await _context.AnnualLeaves
                .Include(y => y.UsedLeaveDays)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnualLeave>> GetAnnualLeave(int id)
        {
            var annualLeave = await _context.AnnualLeaves
                .Include(a => a.UsedLeaveDays)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (annualLeave == null)
            {
                return NotFound();
            }
            return annualLeave;
        }

        [HttpPost]
        public async Task<ActionResult<AnnualLeave>> PostAnnualLeave(AnnualLeave annualLeave)
        {
            _context.AnnualLeaves.Add(annualLeave);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnnualLeave), new { id = annualLeave.Id }, annualLeave);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnualLeave(int id, AnnualLeave annualLeave)
        {
            if (id != annualLeave.Id)
            {
                return BadRequest();
            }

            _context.Entry(annualLeave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnualLeaveExists(id))
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

        private bool AnnualLeaveExists(int id)
        {
            return _context.AnnualLeaves.Any(x => x.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnualLeave(int id)
        {
            var toDelete = await _context.AnnualLeaves.FindAsync(id);
            if (toDelete == null)
            {
                return NotFound();
            }

            _context.AnnualLeaves.Remove(toDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
