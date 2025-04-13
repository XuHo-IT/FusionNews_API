

using Application.Entities.Base;
using Application.Interfaces;
using Application.Reponse;
using Application.Request.Application.Request;
using Newtonsoft.Json;
using System.Text;

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
        public async Task<APIResponse> GetNewsAsync()
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
                var httpRequest = new HttpRequestMessage(new HttpMethod(request.Method), request.Url);

                foreach (var header in request.Headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }

                if (request.Data != null && (request.Method == "POST" || request.Method == "PUT"))
                {
                    var jsonData = JsonConvert.SerializeObject(request.Data);
                    httpRequest.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.SendAsync(httpRequest);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var newsData = JsonConvert.DeserializeObject<NewsApiResponse>(jsonString);
                    apiResponse.Result = newsData.Results;
                    apiResponse.StatusCode = response.StatusCode;
                    apiResponse.isSuccess = true;
                }
                else
                {
                    apiResponse.StatusCode = response.StatusCode;
                    apiResponse.isSuccess = false;
                    apiResponse.ErrorMessages.Add(jsonString);
                }
            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                apiResponse.isSuccess = false;
                apiResponse.ErrorMessages.Add(ex.Message);
            }

            return apiResponse;
        }


    }
}
