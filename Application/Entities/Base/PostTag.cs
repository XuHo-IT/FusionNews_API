using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Base
{
    public class PostTag
    {
        public int PostId { get; set; }
        public required Post Post { get; set; } 
        public int TagId { get; set; }
        public required Tag Tag { get; set; }
    }

}
