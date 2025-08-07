using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models;

namespace PersonelAPI1.Controllers
{
    public class BankaBilgisiController : ControllerBase
    {
        private readonly PersonelDbContext _context;
        public BankaBilgisiController(PersonelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankaBilgisi>>> GetBankaBilgileri()
        {
           return await  _context.BankaBilgileri.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BankaBilgisi>> GetBankaBilgisi(int id)
        {
            var info = await _context.BankaBilgileri.FindAsync(id);
            if(info == null)
            {
                return NotFound();
            }
            return info;
        }
        [HttpPost]
        public async Task<ActionResult<BankaBilgisi>> PostBankaBilgisi(BankaBilgisi bankaBilgisi)
        {
            _context.BankaBilgileri.Add(bankaBilgisi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankaBilgisi", new {id = bankaBilgisi.Id},bankaBilgisi);
        }

        private bool BankaBilgisiExists(int id)
        {
            return _context.BankaBilgileri.Any(b => b.Id == id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankaBilgisi(int id , BankaBilgisi bankaBilgisi)
        {
            if(id != bankaBilgisi.Id)
            {
                return BadRequest();
            }
            _context.Entry(bankaBilgisi).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankaBilgisiExists(id))
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
    }
}
