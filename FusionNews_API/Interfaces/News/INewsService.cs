using Core.Entities;

namespace FusionNews_API.Interfaces.News
{
    public interface INewsService
    {
        Task<List<NewsArticle>> GetNewsAsync();
    }
}
