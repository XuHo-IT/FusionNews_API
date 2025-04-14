namespace Application.Entities.Base
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
