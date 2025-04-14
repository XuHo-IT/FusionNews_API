using Application.Reponse;
using Application.Request;

namespace Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<APIResponse> GetReplyAsync(ChatRequest request);
    }
}
