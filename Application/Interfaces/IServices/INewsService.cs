using Application.Reponse;
using Common.Constants;

namespace Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<APIResponse> GetNewsAsync(string? filterOn = null, string? filterRequest = null, int pageNumber = MyConstants.pageNumber, int pageSize = MyConstants.pageSize);
    }
}
