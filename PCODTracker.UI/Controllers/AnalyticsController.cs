using Microsoft.AspNetCore.Mvc;
using PCODTracker.UI.Services;

namespace PCODTracker.UI.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly ApiService _api;

        public AnalyticsController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var history =
                await _api.GetHistory(userId);

            return View(history);
        }
    }
}