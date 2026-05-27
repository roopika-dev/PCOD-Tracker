using Microsoft.AspNetCore.Mvc;

namespace PCODTracker.UI.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
