using Microsoft.AspNetCore.Mvc;

namespace PCODTracker.UI.Controllers
{
    public class SplashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}