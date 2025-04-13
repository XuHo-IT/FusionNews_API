using Application.Entities.Base;

namespace Application.Interfaces
{
    public interface IChatRepository
    {
        void SaveMessage(ChatMessage message);
        List<ChatMessage> GetConversation();
    }
}
