namespace Application.Entities.Base
{
    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
