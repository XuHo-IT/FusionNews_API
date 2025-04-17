using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.Const;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AuthRepository : DapperBase, IAuthRepository
    {
        public AuthRepository(IConfiguration configuration) : base(configuration) { }
        public async Task AddUserAsync(User user)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(user);
                parameters.Add("@JInput", jInput, DbType.String);

                await connection.ExecuteAsync(
                     StoredExecFunction.AddUser,
                    param: parameters,
                    commandType: CommandType.Text
                );
                return 0;
            });
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await WithConnection(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<User>(StoredExecFunction.GetUserByUsername, new { Username = username });
            });
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.ExecuteScalarAsync<int>(StoredExecFunction.IsUsernameTaken, new { Username = username });
                return result > 0;
            });
        }
    }
}



