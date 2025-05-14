using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.Const;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class CommentRepository : DapperBase, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(commentModel);
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Comment>(
                    StoredExecFunction.CreateComment,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Comment creation failed.");
                }

                return result;
            });
        }

        public async Task DeleteCommentAsync(int id)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { id });
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.ExecuteAsync(
                    StoredExecFunction.DeleteComment,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == 0)
                {
                    throw new Exception("Delete comment failed.");
                }
            });
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { id });
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Comment>(
                    StoredExecFunction.FindCommentById,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Comment not found.");
                }

                return result;
            });
        }

        public async Task<ICollection<Comment>> GetCommentsAsync(int postId)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { postId });
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryAsync<Comment>(
                    StoredExecFunction.GetAllComments,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return result.ToList();
            });
        }

        public async Task<Comment> UpdateCommentAsync(Comment commentModel)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(commentModel);
                parameters.Add("@JInput", jInput, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<Comment>(
                    StoredExecFunction.UpdateComment,
                    param: parameters,
                    commandType: CommandType.Text
                );

                if (result == null)
                {
                    throw new Exception("Update comment failed.");
                }

                return result;
            });
        }
    }
}
