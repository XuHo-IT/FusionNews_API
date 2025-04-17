namespace Infrastructure.EntityFramework.Const
{
    public static class StoredExecFunction
    {
        public const string CreatePost = "SELECT * FROM usf_create_post(@JInput::jsonb)"; // usf -> user stored function
        public const string GetAllPosts = "SELECT * FROM usf_get_all_post()";
        public const string FindPostById = "SELECT * FROM usf_find_post_by_id(@JInput::jsonb)";
        public const string UpdatePost = "SELECT * FROM usf_update_post(@JInput::jsonb)";
        public const string DeletePost = "SELECT * FROM usf_delete_post(@JInput::jsonb)";
        public const string CreateQuestion = "SELECT usf_create_question(@JInput::jsonb)";
        public const string GetAllQuestionsAndAnswer = "SELECT * FROM usf_get_all_questions()";
        public const string UpdateQuestion = "SELECT * FROM usf_update_question(@JInput::jsonb)";
        public const string DeleteQuestion = "SELECT usf_delete_question(@JInput::jsonb)";
        public const string GetQuestion = "SELECT * FROM usf_get_questions()";
        public const string GetAnswer = "SELECT usf_get_answer(@JInput::jsonb)";
        public const string AddUser = "SELECT usf_add_user(@JInput::jsonb)";
        public const string GetUserByUsername = "SELECT * FROM usf_get_user_by_username(@Username)";
        public const string IsUsernameTaken = "SELECT usf_is_username_taken(@Username)";




    }
}
