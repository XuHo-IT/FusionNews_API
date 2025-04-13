//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System.Data;

//namespace Repositories.DataAccess
//{
//    public abstract class DapperBase
//    {
//        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> func)
//        {
//            await using var connection = new SqlConnection(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"));
//            await connection.OpenAsync().ConfigureAwait(false);
//            return await func(connection).ConfigureAwait(false);
//        }
//        protected async Task WithConnection(Func<IDbConnection, Task> func)
//        {
//            await using var connection = new SqlConnection(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"));
//            await connection.OpenAsync().ConfigureAwait(false);
//            await func(connection).ConfigureAwait(false);
//        }
//    }
//}
