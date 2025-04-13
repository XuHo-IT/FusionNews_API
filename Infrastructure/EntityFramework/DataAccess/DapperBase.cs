using Npgsql;
using System.Data;

namespace Infrastructure.EntityFramework.DataAccess
{
    public abstract class DapperBase
    {
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> func)
        {
            await using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRE_CONNECTION_STRING"));
            await connection.OpenAsync().ConfigureAwait(false);
            return await func(connection).ConfigureAwait(false);
        }

        protected async Task WithConnection(Func<IDbConnection, Task> func)
        {
            await using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRE_CONNECTION_STRING"));
            await connection.OpenAsync().ConfigureAwait(false);
            await func(connection).ConfigureAwait(false);
        }
    }
}
