using PCODTracker.UI.Models;
using PCODTracker.Web.Models;
using System.Text;
using System.Text.Json;

namespace PCODTracker.UI.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        // REGISTER
        public async Task<string> Register(UserViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync(
                "https://localhost:7250/api/Auth/register",
                content);

            return await response.Content.ReadAsStringAsync();
        }

        // LOGIN
        public async Task<LoginResponseModel?> Login(
            UserViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync(
                "https://localhost:7250/api/Auth/login",
                content);

            // CHECK LOGIN FAILED
            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                throw new Exception(error);
            }
            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginResponseModel>(
                result,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        // SAVE DAILY HEALTH
        public async Task<string> SaveDailyHealth(
            DailyHealthViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync(
                "https://localhost:7250/api/DailyHealth",
                content);

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<List<DailyHistoryResponse>>
     GetHistory(string userId)
        {
            var response = await _http.GetAsync(
                $"https://localhost:7250/api/DailyHealth/history?userId={userId}");

            var json =
                await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            return JsonSerializer.Deserialize
                <List<DailyHistoryResponse>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<DailyHistoryResponse>();
        }
    }
}