

using Application.Entities.Base;
using Application.Interfaces;
using Newtonsoft.Json;

namespace FusionNews_API.Services.News
{
    public class NewsService : INewsService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public NewsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();

        }
        public async Task<List<NewsArticle>> GetNewsAsync()
        {
            var apiKey = _configuration["News:APIKey"];
            var endpoint = _configuration["News:APIEndPoint"];
            var url = $"{endpoint}{apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(jsonString);

            var filteredResults = apiResponse.Results.ToList();

            return filteredResults;
        }


    }
}
