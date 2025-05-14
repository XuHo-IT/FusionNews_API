using Application.Interfaces.IRepositories;
using Application.Interfaces.Services;
using Application.Reponse;
using Common.Constants;
using FusionNews_API.Helpers;
using System.Net;

namespace FusionNews_API.Services.News
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<APIResponse> GetNewsAsync( int pageNumber, int pageSize, string filterRequest = MyConstants.filterQuery)
        {
            var response = new APIResponse();

            try
            {
                var newsApiResponse = await _newsRepository.FetchNewsAsync(filterRequest);

                var newsResponse = new NewsResponse
                {
                    NewsApiResponse = newsApiResponse,
                    TotalPages = (int)Math.Ceiling((double)newsApiResponse.TotalResults / pageSize),
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                };
                newsResponse = Util.Pagination(pageNumber, pageSize, newsResponse);


                response.Result = newsResponse;
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }
    }
}
