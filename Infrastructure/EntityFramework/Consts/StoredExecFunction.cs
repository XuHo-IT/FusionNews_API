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
        public const string FindPostById = "SELECT * FROM usf_find_post_by_id(@JInput::jsonb)";
        public const string UpdatePost = "SELECT * FROM usf_update_post(@JInput::jsonb)";
        public const string DeletePost = "SELECT * FROM usf_delete_post(@JInput::jsonb)";
    }
}
