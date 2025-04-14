namespace Application.Interfaces.IRepositories
{
    public interface IChatRepository
    {
        Task<string> SendMessageToGeminiAsync(string userMessage);
    }
}
