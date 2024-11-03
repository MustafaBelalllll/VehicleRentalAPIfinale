using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly VehicleRentalContext _context;

    public RentalsController(VehicleRentalContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
    {
        return await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Vehicle)
            .ToListAsync();
    }

   
    [HttpGet("{id}")]
    public async Task<ActionResult<Rental>> GetRental(int id)
    {
        var rental = await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Vehicle)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rental == null) return NotFound();
        return rental;
    }

    
    [HttpPost]
    public async Task<ActionResult<Rental>> PostRental(Rental rental)
    {
      
        var customerExists = await _context.Customers.AnyAsync(c => c.Id == rental.CustomerId);
        var vehicleExists = await _context.Vehicles.AnyAsync(v => v.Id == rental.VehicleId);

        if (!customerExists || !vehicleExists)
            return BadRequest("Invalid Customer or Vehicle ID");

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRental(int id, Rental rental)
    {
        if (id != rental.Id) return BadRequest();
        _context.Entry(rental).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null) return NotFound();
        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
