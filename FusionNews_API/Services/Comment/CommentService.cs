using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Reponse;
using Infrastructure.EntityFramework.Repositories;
using System.Net;

namespace FusionNews_API.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<APIResponse> CreateCommentAsync(Comment CommentModel)
        {
            var response = new APIResponse();

            try
            {
                var comment = await _commentRepository.CreateCommentAsync(CommentModel);

                response.Result = comment;
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

        public async Task<APIResponse> DeleteCommentAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                await _commentRepository.DeleteCommentAsync(id);

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

        public async Task<APIResponse> GetCommentByIdAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                var comment = await _commentRepository.GetCommentByIdAsync(id);

                if (comment is null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.isSuccess = false;
                    response.ErrorMessages.Add("Comment not found");
                }
                else
                {
                    response.Result = comment;
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

        public async Task<APIResponse> GetCommentsAsync(int PostId)
        {
            var response = new APIResponse();

            try
            {
                var comment = await _commentRepository.GetCommentsAsync(PostId);

                response.Result = comment;
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

        public async Task<APIResponse> UpdateCommentAsync(Comment CommentModel)
        {
            var response = new APIResponse();

            try
            {
                var comment = await _commentRepository.UpdateCommentAsync(CommentModel);

                response.Result = comment;
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
