using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

public abstract class DapperBase
{
    private readonly string _connectionString;

    protected DapperBase(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }

    protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> func)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);
        return await func(connection).ConfigureAwait(false);
    }

    protected async Task WithConnection(Func<IDbConnection, Task> func)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);
        await func(connection).ConfigureAwait(false);
    }
}
