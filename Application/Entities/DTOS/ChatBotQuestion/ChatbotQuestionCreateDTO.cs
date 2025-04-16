using System.ComponentModel.DataAnnotations;

namespace Application.Entities.DTOS.ChatBotQuestion
{
    public class ChatbotQuestionCreateDTO
    {
        [Required]
        public string Question { get; set; }
        [Required]

        public string Answer { get; set; }
    }
}
