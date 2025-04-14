using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class Post
    {
        public int PostId { get; set; }

        public string TagId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public NewsOfPost NewsOfPost { get; set; }  

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Tag? Tag { get; set; }
        public List<CommentOfPost>? Comments { get; set; }

    }
}
