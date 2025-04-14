using Application.Reponse;

namespace Application.Interfaces
{
    public interface INewsRepository
    {
        Task<APIResponse> FetchNewsFromApiAsync();
    }
}
