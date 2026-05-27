using Microsoft.AspNetCore.Mvc;
using PCODTracker.UI.Models;
using PCODTracker.UI.Services;
using PCODTracker.Web.Models;

namespace PCODTracker.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _api;

        public AuthController(ApiService api)
        {
            _api = api;
        }

        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public async Task<IActionResult> Login(
    UserViewModel model)
        {
            try
            {
                var result = await _api.Login(model);

                if (result != null)
                {
                    // SAVE SESSION

                    HttpContext.Session.SetString(
                        "UserId",
                        result.localId);

                    HttpContext.Session.SetString(
                        "Email",
                        result.email);

                    HttpContext.Session.SetString(
                        "Token",
                        result.idToken);

                    return RedirectToAction(
                        "Index",
                        "Dashboard");
                }

                ViewBag.Result =
                    "Invalid email or password!";
            }
            catch
            {
                ViewBag.Result =
                    "Invalid email or password!";
            }

            return View();
        }

        // REGISTER PAGE
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            try
            {
                var result = await _api.Register(model);

                TempData["Success"] =
                    "Registration successful! Please login.";

                return RedirectToAction(
                    "Login",
                    "Auth");
            }
            catch
            {
                ViewBag.Result =
                    "User already exists!";
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Auth");
        }
    }
}