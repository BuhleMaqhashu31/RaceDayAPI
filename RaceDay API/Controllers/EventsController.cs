using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EventsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _context.Events
                .Include(e => e.Categories)
                .ToList();
            return Ok(events);
        }

        // POST: api/Events (Organiser Only)
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Organiser")
                return StatusCode(403, new { message = "Access denied. Only Organisers can manage events." });

            _context.Events.Add(newEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.EventId }, newEvent);
        }

        // GET: api/Events/{id}
        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            var ev = _context.Events
                .Include(e => e.Categories)
                .FirstOrDefault(e => e.EventId == id);

            if (ev == null) return NotFound();

            return Ok(ev);
        }
    }
}