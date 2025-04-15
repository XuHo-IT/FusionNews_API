using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Consts
{
    public static class StoredExecFunction
    {
        public const string CreatePost = "SELECT * FROM usf_create_post(@JInput::jsonb)"; // usf -> user stored function
        public const string GetAllPosts = "SELECT * FROM usf_get_all_post()";
    }
}
