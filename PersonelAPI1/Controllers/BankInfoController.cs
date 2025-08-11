using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PersonelAPI1.Data;

namespace PersonelAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInfoController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public BankInfoController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/BankInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankInfo>>> GetBankInfos()
        {
            return await _context.BankaBilgileri.ToListAsync();
        }

        // GET: api/BankInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankInfo>> GetBankInfo(int id)
        {
            var bankInfo = await _context.BankaBilgileri.FindAsync(id);
            if (bankInfo == null)
            {
                return NotFound();
            }
            return bankInfo;
        }

        // POST: api/BankInfo
        [HttpPost]
        public async Task<ActionResult<BankInfo>> PostBankInfo(BankInfo bankInfo)
        {
            _context.BankaBilgileri.Add(bankInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBankInfo), new { id = bankInfo.Id }, bankInfo);
        }

        // PUT: api/BankInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankInfo(int id, BankInfo bankInfo)
        {
            if (id != bankInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(bankInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankInfoExists(id))
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

        // DELETE: api/BankInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankInfo(int id)
        {
            var bankInfo = await _context.BankaBilgileri.FindAsync(id);
            if (bankInfo == null)
            {
                return NotFound();
            }

            _context.BankaBilgileri.Remove(bankInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankInfoExists(int id)
        {
            return _context.BankaBilgileri.Any(b => b.Id == id);
        }
    }
}
