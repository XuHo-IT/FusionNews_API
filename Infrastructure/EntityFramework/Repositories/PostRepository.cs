using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.Consts;
using Infrastructure.EntityFramework.DataAccess;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class PostRepository : DapperBase, IPostRepository
    {
        public async Task<Post> CreatePost(Post postModel)
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

        public async Task<List<Post>> GetAllPosts()
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

    }
}
