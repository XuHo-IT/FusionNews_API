using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class Tag
    {
        public string TagId { get; set; } = string.Empty;

        public string TagName { get; set; } = string.Empty;

        public List<Post>? Posts { get; set; }
    }
}
