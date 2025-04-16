using Application.Entities.Base;

namespace Application.Interfaces.IRepositories
{
    public interface IChatBotRepository
    {
        Task<List<ChatbotQuestion>> GetQuestion();
        Task CreateQuestion(ChatbotQuestion chatbotQuestionDTO);
        //Task<ChatbotQuestion> UpdateQuestion();
    }
}
