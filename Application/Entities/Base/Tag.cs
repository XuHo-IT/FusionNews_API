

namespace Application.Entities.Base
{
    public class Tag
    {
        public int TagId { get; set; } 

        public string TagName { get; set; } = string.Empty;

        public ICollection<PostTag>? PostTags { get; set; }
    }
}
