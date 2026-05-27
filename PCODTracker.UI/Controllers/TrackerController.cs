using Microsoft.AspNetCore.Mvc;
using PCODTracker.UI.Models;
using PCODTracker.UI.Services;

namespace PCODTracker.UI.Controllers
{
    public class TrackerController : Controller
    {
        private readonly ApiService _api;

        public TrackerController(ApiService api)
        {
            _api = api;
        }

        public IActionResult Index()
        {
            // CHECK LOGIN
            var user =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(
    DailyHealthViewModel model)
        {
            var userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            model.UserId = userId;

            var result =
      await _api.SaveDailyHealth(model);

            if (result.Contains("updated"))
            {
                TempData["Success"] =
                    "Today's record updated successfully ✅";
            }
            else
            {
                TempData["Success"] =
                    "Daily health saved successfully ✅";
            }

            return RedirectToAction(
      "Index",
      "Dashboard",
      new { t = DateTime.Now.Ticks });
        }
    }
}