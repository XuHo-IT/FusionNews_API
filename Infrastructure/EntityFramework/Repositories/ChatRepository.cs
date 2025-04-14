using Application.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Infrastructure.EntityFramework.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly string _apiKey;
        private readonly HttpClient _http;

        public ChatRepository(IConfiguration config)
        {
            _apiKey = config["Gemini:ApiKey"];
            _http = new HttpClient();
        }

        public async Task<string> SendMessageToGeminiAsync(string userMessage)
        {
            var requestBody = new
            {
                contents = new[]
                {
                new {
                    role = "user",
                    parts = new[] { new { text = userMessage } }
                }
            }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}",
                content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Gemini API Error: " + responseString);
            }

            var json = JsonDocument.Parse(responseString);
            return json.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
        }
    }
}
