using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using RaceDayAPI.Dtos;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EnrolmentsController(RaceDayContext context)
        {
            _context = context;
        }

        // POST: api/Enrolments (Participants enroll in an event category)
        [HttpPost]
        public IActionResult Enroll([FromBody] EnrolmentDto enrolmentDto)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
                return StatusCode(401, "Please log in.");

            var enrolment = new Enrolment
            {
                ParticipantId = int.Parse(userIdString),
                EventId = enrolmentDto.EventId,
                CategoryId = enrolmentDto.CategoryId,
                EnrolmentDate = DateTime.UtcNow
            };

            _context.Enrolments.Add(enrolment);
            _context.SaveChanges();

            return Ok(new { message = "Successfully enrolled in event." });
        }

        // GET: api/Enrolments/my-enrolments
        [HttpGet("my-enrolments")]
        public IActionResult GetMyEnrolments()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
                return StatusCode(401, "Please log in.");

            int userId = int.Parse(userIdString);
            var enrolments = _context.Enrolments
                .Include(e => e.Event)
                .Include(e => e.Category)
                .Where(e => e.ParticipantId == userId)
                .ToList();

            return Ok(enrolments);
        }

        // GET: api/Enrolments/my-results
        [HttpGet("my-results")]
        public IActionResult GetMyResults()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
                return StatusCode(401, "Please log in.");

            int userId = int.Parse(userIdString);
            var results = _context.Results
                .Include(r => r.Enrolment)
                    .ThenInclude(e => e.Category)
                        .ThenInclude(c => c.Event)
                .Where(r => r.Enrolment.ParticipantId == userId)
                .ToList();

            return Ok(results);
        }
    }
}
