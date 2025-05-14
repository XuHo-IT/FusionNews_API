using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Infrastructure.EntityFramework.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _endpoint;
        private static readonly ConcurrentDictionary<string, (NewsApiResponse Response, DateTime Timestamp)> _cache = new();
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

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

            // Create a unique cache key that includes search parameters
            var cacheKey = $"{searchQuery}_{yesterday}";

            // Check if we have a valid cached response
            if (_cache.TryGetValue(cacheKey, out var cachedData))
            {
                if (DateTime.Now - cachedData.Timestamp < _cacheDuration)
                {
                    Console.WriteLine($"Returning cached data for query: {searchQuery} with date: {yesterday}");
                    Console.WriteLine($"Cached articles count: {cachedData.Response.Articles?.Count ?? 0}");
                    if (cachedData.Response.Articles?.Any() == true)
                    {
                        Console.WriteLine($"First cached article title: {cachedData.Response.Articles.First().Title}");
                    }

                    // Create a new instance to avoid reference issues
                    var responseCache = new NewsApiResponse
                    {
                        Status = cachedData.Response.Status,
                        TotalResults = cachedData.Response.TotalResults > 100 ? 100 : cachedData.Response.TotalResults,
                        Articles = cachedData.Response.Articles?.Select(a => new NewsArticle
                        {
                            Source = a.Source != null ? new Source { Id = a.Source.Id, Name = a.Source.Name } : null,
                            Author = a.Author,
                            Title = a.Title,
                            Description = a.Description,
                            Url = a.Url,
                            UrlToImage = a.UrlToImage,
                            PublishedAt = a.PublishedAt,
                            Content = a.Content
                        }).ToList()
                    };

                    return responseCache;
                }
                // Remove expired cache
                _cache.TryRemove(cacheKey, out _);
            }

            Console.WriteLine($"Making API request for query: {searchQuery} with date: {yesterday}");
            
            // Make the API request
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw JSON response: {json}");

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("News API Error: " + json);
            }

            try
            {
                // Deserialize the JSON response to a NewsApiResponse object
                var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(json);
                
                if (newsResponse?.Articles == null)
                {
                    Console.WriteLine("Warning: Articles is null after deserialization");
                }
                else
                {
                    Console.WriteLine($"Successfully deserialized {newsResponse.Articles.Count} articles");
                }

                // Cache the response
                var cacheEntry = new NewsApiResponse
                {
                    Status = newsResponse.Status,
                    TotalResults = newsResponse.TotalResults > 100 ? 100 : newsResponse.TotalResults,
                    Articles = newsResponse.Articles?.Select(a => new NewsArticle
                    {
                        Source = a.Source != null ? new Source { Id = a.Source.Id, Name = a.Source.Name } : null,
                        Author = a.Author,
                        Title = a.Title,
                        Description = a.Description,
                        Url = a.Url,
                        UrlToImage = a.UrlToImage,
                        PublishedAt = a.PublishedAt,
                        Content = a.Content
                    }).ToList()
                };

                // Log before caching
                Console.WriteLine($"Before caching - Articles count: {cacheEntry.Articles?.Count ?? 0}");
                if (cacheEntry.Articles?.Any() == true)
                {
                    Console.WriteLine($"Before caching - First article title: {cacheEntry.Articles.First().Title}");
                }

                // Remove existing cache if any
                _cache.TryRemove(cacheKey, out _);
                
                // Add new cache entry
                if (!_cache.TryAdd(cacheKey, (cacheEntry, DateTime.Now)))
                {
                    Console.WriteLine("Warning: Failed to add to cache");
                }

                // Verify cache entry
                if (_cache.TryGetValue(cacheKey, out var verifyCache))
                {
                    Console.WriteLine($"After caching - Articles count: {verifyCache.Response.Articles?.Count ?? 0}");
                    if (verifyCache.Response.Articles?.Any() == true)
                    {
                        Console.WriteLine($"After caching - First article title: {verifyCache.Response.Articles.First().Title}");
                    }
                }

                Console.WriteLine($"Cached new data for query: {searchQuery} with date: {yesterday}");

                // Return a new instance to avoid reference issues
                return new NewsApiResponse
                {
                    Status = newsResponse.Status,
                    TotalResults = newsResponse.TotalResults > 100 ? 100 : newsResponse.TotalResults,
                    Articles = newsResponse.Articles?.Select(a => new NewsArticle
                    {
                        Source = a.Source != null ? new Source { Id = a.Source.Id, Name = a.Source.Name } : null,
                        Author = a.Author,
                        Title = a.Title,
                        Description = a.Description,
                        Url = a.Url,
                        UrlToImage = a.UrlToImage,
                        PublishedAt = a.PublishedAt,
                        Content = a.Content
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                throw;
            }
        }

        public void ClearCache()
        {
            _cache.Clear();
            Console.WriteLine("Cache cleared");
        }
    }
}
