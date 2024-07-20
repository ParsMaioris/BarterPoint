using System.Data;
using System.Data.SqlClient;

namespace BarterPoint.Infrastructure;

public abstract class BaseRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    protected BaseRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    protected SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
    }

    protected SqlCommand CreateCommand(SqlConnection connection, string storedProcedure)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedure;
        return command;
    }

    protected async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
    }

    protected async Task<List<TResult>> ExecuteReaderAsync<TResult>(SqlCommand command, Func<IDataReader, TResult> map)
    {
        var results = new List<TResult>();
        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }
        }
        return results;
    }

    protected async Task<object> ExecuteScalarAsync(SqlCommand command)
    {
        return await command.ExecuteScalarAsync();
    }
}