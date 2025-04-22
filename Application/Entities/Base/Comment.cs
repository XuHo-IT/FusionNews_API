using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class Comment
    {
        public int CommentId { get; set; }
        public required string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int PostId { get; set; }
        public required Post Post { get; set; }
        public required string UserId { get; set; }
        public required User User { get; set; }
    }
}
