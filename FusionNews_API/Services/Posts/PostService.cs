using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Reponse;
using System.Net;

namespace FusionNews_API.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<APIResponse> CreatePost(Post postModel)
        {
            var response = new APIResponse();

            try
            {
                var articles = await _postRepository.CreatePost(postModel);

                response.Result = articles;
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }


        public async Task<APIResponse> GetAllPosts()
        {
            var response = new APIResponse();

            try
            {
                var articles = await _postRepository.GetAllPosts();

                response.Result = articles;
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }
    }
}
