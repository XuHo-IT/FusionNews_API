using Application.Entities.Base;

namespace FusionNews_API.DTOs.Post
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public int? NewsOfPostId { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
        public List<CommentOfPost>? Comments { get; set; }
        public NewsOfPost? NewsOfPost { get; set; }
    }
}
