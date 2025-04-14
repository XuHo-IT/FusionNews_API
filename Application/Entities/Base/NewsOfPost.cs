using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class NewsOfPost
    {
        public int NewsOfPostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public Post Post { get; set; }
    }
}
