using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.Services;
using Application.Reponse;
using Common.Constants;
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

        public async Task<APIResponse> GetNewsAsync(string? filterOn = null, string? filterRequest = null, int pageNumber = MyConstants.pageNumber, int pageSize = MyConstants.pageSize)
        {
            var response = new APIResponse();

            try
            {
                var articles = await _newsRepository.FetchNewsAsync();

                // Filtering
                if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterRequest))
                {
                    if (filterOn.Equals("Country", StringComparison.OrdinalIgnoreCase))
                    {
                        articles = articles.Where(a => a.Country.Any(c => filterRequest.ToUpper().Contains(c.ToUpper()))).ToList();
                    }
                    if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                    {
                        articles = articles.Where(a => a.Category.Any(c => filterRequest.ToUpper().Contains(c.ToUpper()))).ToList();
                    }
                }

                //Pagination
                var skip = (pageNumber - 1) * pageSize;
                articles = articles.Skip(skip).Take(pageSize).ToList();

                response.Result = articles;
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
