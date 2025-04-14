using Application.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FusionNews_API.DTOs.Post
{
    public class CreatePostDto
    {

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
