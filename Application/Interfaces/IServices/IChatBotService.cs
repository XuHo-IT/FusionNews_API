using Application.Entities.Base;
using Application.Reponse;

namespace Application.Interfaces.IServices
{
    public interface IChatBotService
    {
        Task<APIResponse> GetQuestionAndAnswer();
        Task<APIResponse> CreateQuestion(ChatbotQuestion chatbotQuestion);
        Task<APIResponse> UpdateQuestion(ChatbotQuestion chatbotQuestion);
        Task<APIResponse> DeleteQuestion(int id);
        Task<APIResponse> GetQuestion();
        Task<APIResponse> GetAnswer(int id);

    }
}
