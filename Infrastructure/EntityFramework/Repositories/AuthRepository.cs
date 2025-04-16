using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.DataAccess;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AuthRepository : DapperBase, IAuthRepository
    {
        public async Task AddUserAsync(User user)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(user);
                parameters.Add("@JInput", jInput, DbType.String);

                await connection.ExecuteAsync(
                    "SELECT usf_add_user(@JInput::jsonb)",
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
                var query = "SELECT * FROM usf_get_user_by_username(@Username)";
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
            });
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await WithConnection(async connection =>
            {

                var query = "SELECT usf_is_username_taken(@Username)";
                var result = await connection.ExecuteScalarAsync<int>(query, new { Username = username });
                return result > 0;
            });
        }
    }
}



