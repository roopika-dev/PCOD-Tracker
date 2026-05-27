using Microsoft.AspNetCore.Mvc;
using PCODTracker.API.DTOs;
using PCODTracker.API.Services;

namespace PCODTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyHealthController : ControllerBase
    {
        private readonly DailyHealthService _service;

        public DailyHealthController(
            DailyHealthService service)
        {
            _service = service;
        }

        // SAVE DATA
        [HttpPost]
        public async Task<IActionResult> Save(
    DailyHealthDto dto)
        {
            var result =
     await _service.AddAsync(
         dto.UserId,
         dto);

            string message =
                result == "updated"
                ? "Today's record updated successfully ✅"
                : "Today's health saved successfully ✅";

            return Ok(new
            {
                success = true,
                message = message
            });
        }
        // GET HISTORY
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetHistory(
            string userId)
        {
            var result =
                await _service.GetLast7Days(userId);

            return Ok(result);
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryByQuery(
    [FromQuery] string userId)
        {
            var result =
                await _service.GetLast7Days(userId);

            return Ok(result);
        }
    }
}