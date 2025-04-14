using Application.Entities.Base;

namespace Application.Interfaces.IRepositories
{
    public interface INewsRepository
    {
        Task<List<NewsArticle>> FetchNewsAsync();
    }
}
