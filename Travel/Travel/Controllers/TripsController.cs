using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Data;
using Travel.Models;
using Microsoft.EntityFrameworkCore;


namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly TravelDbContext _context;
        public TripsController(TravelDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripDetails(int id)
        {
            var trip = await _context.Tips
                .Include(t => t.Organizer)
                .FirstOrDefaultAsync(t => t.TripID == id);

            if (trip == null)
            {
                return NotFound();
            }

            var participants = await _context.TripParticipants
                .Where(tp => tp.TripId == id)
                .Include(tp => tp.User)
                .ToListAsync();

            var tripDetails = new
            {
                trip.TripID,
                trip.Destination,
                trip.StartDate,
                trip.EndDate,
                Organizer = new
                {
                    trip.Organizer.UserID,
                    trip.Organizer.FirstName,
                    trip.Organizer.LastName,
                    trip.Organizer.Email
                },
                Participants = participants.Select(p => new
                {
                    p.User.UserID,
                    p.User.FirstName,
                    p.User.LastName,
                    p.User.Email,
                })
            };
            return Ok(tripDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTripDetails), new { id = trip.TripID }, trip);
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinTrip(int id, [FromBody] int userId)
        {
            var trip = await _context.Tips.FindAsync(id);
            if (trip == null)
            {
                return NotFound("Trip not found.");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var existingParticipant = await _context.TripParticipants
                .FirstOrDefaultAsync(tp => tp.TripId == id && tp.UserId == userId);

            if (existingParticipant != null)
            {
                return BadRequest("User is already in the trip.");
            }

            var tripParticipant = new TripParticipant
            {
                TripId = id,
                UserId = userId,
            };

            _context.TripParticipants.Add(tripParticipant);
            await _context.SaveChangesAsync();

            return Ok("User has joined.");
        }
    }
}
