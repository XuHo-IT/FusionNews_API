
namespace Application.Entities.Base
{
    public class Post
    {
        public int PostId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int? NewsOfPostId { get; set; } 
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public NewsOfPost? NewsOfPost { get; set; }
        public required string UserId { get; set; }
        public required User User { get; set; }
    }
}
