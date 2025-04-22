using Application.Entities.Base;
using Application.Entities.DTOS.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.DTOS.Post
{
    public class PostDto
    {
        public int PostId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int? NewsOfPostId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public ICollection<PostTag>? PostTags { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
        public NewsOfPost? NewsOfPost { get; set; }
    }
}
