using Application.Entities.Base;
using Application.Entities.DTOS.Comment;
using Application.Entities.DTOS.Post;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.Const;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;


namespace Infrastructure.EntityFramework.Repositories
{
    public class PostRepository : DapperBase, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<List<Post>> GetPostsAsync()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<Post>(
                    StoredExecFunction.GetAllPosts,
                    commandType: CommandType.Text
                );

                return result.ToList();
            });
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { id });
                parameters.Add("@JInput", jInput, DbType.String);

                var post = await connection.QueryAsync(
                    StoredExecFunction.FindPostById,
                    param: parameters,
                    commandType: CommandType.Text
                );

                var postf = post.FirstOrDefault();

                if (postf == null)
                {
                    throw new Exception("Post not found.");
                }

                // Deserialize 'comments' field into List<CommentDto>
                var commentsJson = postf.comments.ToString();
                var comments = JsonConvert.DeserializeObject<ICollection<Comment>>(commentsJson);

                var postmodel = new Post
                {
                    PostId = postf.postid,
                    Title = postf.title,
                    Content = postf.content,
                    CreateAt = postf.createat,
                    UpdateAt = postf.updateat,
                    Comments = comments
                };

                return postmodel;
            });
        }

        public async Task<Post> CreatePostAsync(Post postModel)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(postModel);
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Post>(
                    StoredExecFunction.CreatePost,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Post creation failed.");
                }

                return result;
            });
        }

        public async Task<Post> UpdatePostAsync(Post postModel)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(postModel);
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Post>(
                    StoredExecFunction.UpdatePost,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Update post failed.");
                }

                return result;
            });
        }

        public async Task DeletePostAsync(int id)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { id });
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.ExecuteAsync(
                    StoredExecFunction.DeletePost,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == 0)
                {
                    throw new Exception("Delete post failed.");
                }
            });
        }
    }
}
