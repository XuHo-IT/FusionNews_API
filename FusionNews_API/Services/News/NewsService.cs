

using Application.Entities.Base;
using Application.Interfaces;

namespace FusionNews_API.Services.News
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<List<NewsArticle>> GetNewsAsync()
        {
            var response = await _newsRepository.FetchNewsFromApiAsync();

            if (response.isSuccess && response.Result is List<NewsArticle> articles)
            {
                return articles;
            }

            return new List<NewsArticle>();
        }

    }
}
