using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.DTOS.CommentOfPost
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(300, ErrorMessage = "Title cannot be over 300 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
