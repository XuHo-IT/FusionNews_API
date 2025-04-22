
namespace Application.Entities.Base
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? NewsOfPostId { get; set; } 
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public NewsOfPost? NewsOfPost { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
