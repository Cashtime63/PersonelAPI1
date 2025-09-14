using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models;
using System.Security.Claims;

namespace PersonelAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        private readonly ILogger<AddressController> _logger;

        public AddressController(EmployeeDbContext context, ILogger<AddressController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            try
            {
                return await _context.Addresses.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving addresses");
                return StatusCode(500, "Bir hata oluştu");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(id);
                if (address == null)
                    return NotFound();

                if (User.IsInRole("Employee"))
                {
                    var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    if (address.EmployeeId != employeeId)
                        return Forbid();
                }

                return address;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving address");
                return StatusCode(500, "Bir hata oluştu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
                return BadRequest();

            if (User.IsInRole("Employee"))
            {
                var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var existingAddress = await _context.Addresses.FindAsync(id);

                if (existingAddress == null || existingAddress.EmployeeId != employeeId)
                    return Forbid();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}