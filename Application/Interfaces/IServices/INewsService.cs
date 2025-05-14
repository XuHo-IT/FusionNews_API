using Application.Reponse;
using Common.Constants;

namespace Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<APIResponse> GetNewsAsync( int pageNumber, int pageSize, string filterRequest = MyConstants.filterQuery);
    }
}
