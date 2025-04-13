using Application.Entities.Base;
using Application.Interfaces;
using Application.Reponse;
using Application.Request.Application.Request;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.EntityFramework.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public NewsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<APIResponse> FetchNewsFromApiAsync()
        {
            var apiKey = _configuration["News:APIKey"];
            var endpoint = _configuration["News:APIEndPoint"];

            var request = new APIRequest
            {
                Url = $"{endpoint}{apiKey}",
                Method = "GET"
            };

            var apiResponse = new APIResponse();

            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, request.Url);

                foreach (var header in request.Headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }

                var response = await _httpClient.SendAsync(httpRequest);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var newsApi = JsonConvert.DeserializeObject<NewsApiResponse>(json);
                    apiResponse.Result = newsApi.Results;
                    apiResponse.StatusCode = response.StatusCode;
                    apiResponse.isSuccess = true;
                }
                else
                {
                    apiResponse.StatusCode = response.StatusCode;
                    apiResponse.isSuccess = false;
                    apiResponse.ErrorMessages.Add(json);
                }
            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.isSuccess = false;
                apiResponse.ErrorMessages.Add(ex.Message);
            }

            return apiResponse;
        }
    }
}
