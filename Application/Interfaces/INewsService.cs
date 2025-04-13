using Application.Reponse;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<APIResponse> GetNewsAsync();
    }
}
