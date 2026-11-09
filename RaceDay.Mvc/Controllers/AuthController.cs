using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Contracts.Requests; // Adjust namespace to match your Contracts
using RaceDay.Mvc.Services;

namespace RaceDay.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly RaceDayApiClient _apiClient;

        public AuthController(RaceDayApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiClient.PostAsync("api/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                // Forward the auth cookie or redirect to dashboard
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}