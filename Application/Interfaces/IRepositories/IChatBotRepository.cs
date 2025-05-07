using Application.Entities.Base;
using Application.Reponse.Chatbot;
using Application.Request.Chatbot;

namespace Application.Interfaces.IRepositories
{
    public interface IChatBotRepository
    {
        Task<List<ChatbotQuestion>> GetQuestionAndAnswer();
        Task CreateQuestion(ChatbotQuestion chatbotQuestion);
        Task UpdateQuestion(ChatbotQuestion chatbotQuestion);
        Task DeleteQuestion(int id);
        Task<List<ChatbotQuestionRequest>> GetQuestion();
        Task<ChatbotAnswerResponse> GetAnswer(int id);


    }
}
