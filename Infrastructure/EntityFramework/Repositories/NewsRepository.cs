using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.EntityFramework.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _endpoint;

        public NewsRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["News:APIKey"];
            _endpoint = configuration["News:APIEndPoint"];
        }

        public async Task<List<NewsArticle>> FetchNewsAsync()
        {
            var url = $"{_endpoint}{_apiKey}";
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("News API Error: " + json);
            }

            var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(json);
            return newsResponse.Results ?? new List<NewsArticle>();
        }
    }
}
