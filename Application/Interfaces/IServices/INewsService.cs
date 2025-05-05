using Application.Reponse;
using Common.Constants;

namespace Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<APIResponse> GetNewsAsync(string filterRequest = MyConstants.filterQuery, int pageNumber = MyConstants.pageNumber, int pageSize = MyConstants.pageSize);
    }
}
