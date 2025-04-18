
namespace Application.Entities.Base
{
    public class Post
    {
        public int PostId { get; set; }


        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? NewsOfPostId { get; set; } 

        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public NewsOfPost? NewsOfPost { get; set; }
    }
}
