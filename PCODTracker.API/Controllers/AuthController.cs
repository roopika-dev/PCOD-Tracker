using Microsoft.AspNetCore.Mvc;
using PCODTracker.API.DTOs;
using PCODTracker.API.Services;

namespace PCODTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var uid = await _service.Register(dto);
            return Ok(new { userId = uid });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.Login(dto);
            return Ok(result);
        }
    }
}