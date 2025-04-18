using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.DTOS.CommentOfPost
{
    public class UpdateComment
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public int PostId { get; set; }
    }
}
