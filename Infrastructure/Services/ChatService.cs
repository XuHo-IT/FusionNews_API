using Application.Interfaces.IRepositories;
using Application.Interfaces.Services;
using Application.Reponse;
using Application.Request;
using System.Net;

namespace Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;

        public ChatService(IChatRepository repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse> GetReplyAsync(ChatRequest request)
        {
            var response = new APIResponse();

            try
            {
                var reply = await _repo.SendMessageToGeminiAsync(request.Message);

                response.Result = new ChatResponse { Reply = reply };
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
