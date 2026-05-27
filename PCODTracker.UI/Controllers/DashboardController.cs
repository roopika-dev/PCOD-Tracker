using Microsoft.AspNetCore.Mvc;
using PCODTracker.UI.Services;

namespace PCODTracker.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApiService _api;

        public DashboardController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            Response.Headers["Cache-Control"] =
                "no-cache, no-store, must-revalidate";

            Response.Headers["Pragma"] =
                "no-cache";

            Response.Headers["Expires"] =
                "0";

            // LOGIN CHECK
            var userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            // GET HISTORY
            var history =
            (await _api.GetHistory(userId))
            .OrderByDescending(x => x.Date)
            .ToList();
            var email =
    HttpContext.Session.GetString("Email");
            ViewBag.Email = email;

            return View(history);
        }
    }
}