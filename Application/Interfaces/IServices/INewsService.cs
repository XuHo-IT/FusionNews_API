using Application.Reponse;

namespace Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<APIResponse> GetNewsAsync();
    }
}
