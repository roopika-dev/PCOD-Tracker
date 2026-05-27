using Microsoft.AspNetCore.Mvc;
using PCODTracker.UI.Services;

namespace PCODTracker.UI.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApiService _api;

        public CalendarController(ApiService api)
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