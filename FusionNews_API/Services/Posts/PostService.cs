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

        public async Task<APIResponse> CreatePostAsync(Post postModel)
        {
            var response = new APIResponse();

            try
            {
                var articles = await _postRepository.CreatePostAsync(postModel);

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

        public async Task<APIResponse> GetPostByIdAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                var post = await _postRepository.GetPostByIdAsync(id);
                if (post is null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.isSuccess = false;
                    response.ErrorMessages.Add("Post not found");
                }
                else
                {
                    response.Result = post;
                    response.StatusCode = HttpStatusCode.OK;
                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }

        public async Task<APIResponse> GetPostsAsync()
        {
            var response = new APIResponse();

            try
            {
                var posts = await _postRepository.GetPostsAsync();

                response.Result = posts;
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

        public async Task<APIResponse> UpdatePostAsync(Post postModel)
        {
            var response = new APIResponse();

            try
            {
                var articles = await _postRepository.UpdatePostAsync(postModel);

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

        public async Task<APIResponse> DeletePostAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                var post = await _postRepository.DeletePostAsync(id);
                if (post is null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.isSuccess = false;
                    response.ErrorMessages.Add("Post not found");
                }
                else
                {
                    response.Result = post;
                    response.StatusCode = HttpStatusCode.OK;
                    response.isSuccess = true;
                }
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
