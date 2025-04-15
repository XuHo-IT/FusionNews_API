using Application.Entities.Base;
using Application.Reponse;

namespace Application.Interfaces.IServices
{
    public interface IPostService
    {
        Task<APIResponse> GetAllPosts();
        Task<APIResponse> CreatePost(Post postModel);
    }
}
