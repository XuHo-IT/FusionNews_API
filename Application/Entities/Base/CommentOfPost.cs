using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class CommentOfPost
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
