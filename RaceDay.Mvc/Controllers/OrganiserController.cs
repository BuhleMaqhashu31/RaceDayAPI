using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Services;

namespace RaceDay.Mvc.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly RaceDayApiClient _apiClient;

        public OrganiserController(RaceDayApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch events managed by the organiser from the API
            var events = await _apiClient.GetAsync<object>("api/events");
            return View(events);
        }

        [HttpGet]
        public IActionResult CreateEvent() => View();
    }
}
