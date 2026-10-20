using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay_API.Dtos;
using RaceDayAPI.Data;
using RaceDayAPI.Dtos;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public AuthController(RaceDayContext context)
        {
            _context = context;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest(new { message = "Email is already registered." });

            // Hash the password securely using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                RoleId = dto.RoleId // 1 for Organiser, 2 for Participant (based on your roles table)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully." });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password." });

            // Maintain authenticated state and role using server-side session management
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserRole", user.Role?.RoleName ?? "Participant");

            return Ok(new { message = "Login successful", role = user.Role?.RoleName });
        }

        // GET: api/Auth/profile (View own profile for both Organisers and Participants)
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
                return StatusCode(401, "Please log in.");

            int userId = int.Parse(userIdString);
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null) return NotFound("User not found.");

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                Role = user.Role?.RoleName
            });
        }

        // PUT: api/Auth/profile (Update own profile information)
        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
                return StatusCode(401, "Please log in.");

            int userId = int.Parse(userIdString);
            var user = _context.Users.Find(userId);

            if (user == null) return NotFound("User not found.");

            // Ensure updated email isn't already taken by another account
            if (_context.Users.Any(u => u.Email == dto.Email && u.UserId != userId))
                return BadRequest("Email is already in use by another account.");

            user.FullName = dto.FullName;
            user.Email = dto.Email;

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
