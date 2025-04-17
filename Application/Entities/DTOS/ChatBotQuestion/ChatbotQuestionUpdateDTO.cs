using System.ComponentModel.DataAnnotations;

namespace Application.Entities.DTOS.ChatBotQuestion
{
    public class ChatbotQuestionUpdateDTO
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Question { get; set; }
        [Required]

        public string Answer { get; set; }
    }
}
