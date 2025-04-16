using Application.Entities.Base;
using Application.Reponse;

namespace Application.Interfaces.IServices
{
    public interface IPostService
    {
        Task<APIResponse> GetPostsAsync();
        Task<APIResponse> CreatePostAsync(Post postModel);
        Task<APIResponse> GetPostByIdAsync(int id);
        Task<APIResponse> UpdatePostAsync(Post postModel);
        Task<APIResponse> DeletePostAsync(int id);
    }
}
