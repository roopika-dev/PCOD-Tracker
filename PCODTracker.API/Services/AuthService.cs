using FirebaseAdmin.Auth;
using System.Text;
using System.Text.Json;
using PCODTracker.API.DTOs;

namespace PCODTracker.API.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        // ✅ REGISTER USER
        public async Task<string> Register(RegisterDto dto)
        {
            var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs()
            {
                Email = dto.Email,
                Password = dto.Password
            });

            return user.Uid;
        }

        // ✅ LOGIN USER
        public async Task<object> Login(LoginDto dto)
        {
            var apiKey = _config["Firebase:ApiKey"];

            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";

            var payload = new
            {
                email = dto.Email,
                password = dto.Password,
                returnSecureToken = true
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {responseString}");
            }

            return JsonSerializer.Deserialize<object>(responseString);
        }
    }
}