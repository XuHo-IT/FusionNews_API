using Application.Entities.Base;
using Application.Reponse;

namespace Application.Interfaces.IServices
{
    public interface IChatBotService
    {
        Task<APIResponse> GetAllQuetions();
        Task<APIResponse> CreateQuestion(ChatbotQuestion chatbotQuestion);
    }
}
