using Application.Entities.Base;

namespace Application.Request.News
{
    public interface INewsService
    {
        Task<List<NewsArticle>> GetNewsAsync();
    }
}
