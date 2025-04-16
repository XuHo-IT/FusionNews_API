using Application.Entities.Base;
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
                var result = await connection.QueryFirstOrDefaultAsync<Post>(
                    StoredExecFunction.FindPostById,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Post not found.");
                }

                return result;
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

        public async Task<Post> DeletePostAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { id });
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Post>(
                    StoredExecFunction.DeletePost,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Delete post failed.");
                }

                return result;
            });
        }
    }
}
