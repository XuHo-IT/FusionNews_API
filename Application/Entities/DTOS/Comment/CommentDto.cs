using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.DTOS.Comment
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public required string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
