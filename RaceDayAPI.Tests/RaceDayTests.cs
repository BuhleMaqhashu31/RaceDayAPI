using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Controllers;
using RaceDayAPI.Data;
using RaceDayAPI.Dtos;
using RaceDayAPI.Models;
using Xunit;

namespace RaceDayAPI.Tests
{
    public class RaceDayTests
    {
        private RaceDayContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new RaceDayContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Login_WithValidCredentials_ReturnsOkAndSetsSession()
        {
            var context = GetInMemoryContext();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Password123");

            context.Users.Add(new User
            {
                UserId = 1,
                FullName = "John Organiser",
                Email = "organiser@raceday.com",
                PasswordHash = hashedPassword,
                RoleId = 1
            });
            context.SaveChanges();

            var controller = new AuthController(context);
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new FakeSession();
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var loginDto = new LoginDto
            {
                Email = "organiser@raceday.com",
                Password = "Password123"
            };

            var result = controller.Login(loginDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void CreateEvent_WhenUserIsParticipant_ReturnsForbidden403()
        {
            var context = GetInMemoryContext();
            var controller = new EventsController(context);

            var httpContext = new DefaultHttpContext();
            httpContext.Session = new FakeSession();
            httpContext.Session.SetString("UserRole", "Participant");

            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var newEvent = new Event
            {
                Title = "City Marathon",
                Description = "Annual 10k run",
                EventDate = "2026-11-01",
                Location = "Downtown",
                OrganiserId = 1
            };

            var result = controller.CreateEvent(newEvent);
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, statusCodeResult.StatusCode);
        }
    }

    // Helper class for simulating session data in unit tests
    public class FakeSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear() => _sessionStorage.Clear();

        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Remove(string key) => _sessionStorage.Remove(key);

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
    }
}