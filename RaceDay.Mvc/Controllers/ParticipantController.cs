using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Services;

namespace RaceDay.Mvc.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly RaceDayApiClient _apiClient;

        public ParticipantController(RaceDayApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> BrowseEvents()
        {
            var events = await _apiClient.GetAsync<object>("api/events");
            return View(events);
        }

        public async Task<IActionResult> MyResults()
        {
            var results = await _apiClient.GetAsync<object>("api/enrolments/my-results");
            return View(results);
        }
    }
}
