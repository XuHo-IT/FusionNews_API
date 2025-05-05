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
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);

            _apiKey = configuration["News:APIKey"];
            _endpoint = configuration["News:APIEndPoint"];
        }

        public async Task<NewsApiResponse> FetchNewsAsync(string searchQuery)
        {
            // Calculate the date before today
            var yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            // Construct the API URL with the dynamic date and search query
            var url = $"{_endpoint}?q={searchQuery}&from={yesterday}&sortBy=publishedAt&apiKey={_apiKey}";

            // Make the API request
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("News API Error: " + json);
            }

            // Deserialize the JSON response to a NewsApiResponse object
            var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(json);

            // Return the list of news articles or an empty list if null
            return newsResponse;
        }

    }
}
