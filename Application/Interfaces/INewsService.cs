using Application.Entities.Base;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsArticle>> GetNewsAsync();
    }
}
