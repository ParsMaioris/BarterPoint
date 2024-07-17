using System.Data;
using System.Data.SqlClient;

public abstract class BaseRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    protected BaseRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    protected async Task<SqlConnection> OpenConnectionAsync()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        await connection.OpenAsync();
        return connection;
    }

    protected SqlCommand CreateCommand(SqlConnection connection, string storedProcedure)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedure;
        return command;
    }

    protected SqlCommand CreateCommandWithTransaction(SqlTransaction transaction, string storedProcedure, params SqlParameter[] parameters)
    {
        var command = CreateCommand(transaction.Connection, storedProcedure);
        command.Transaction = transaction;

        foreach (var parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }

        return command;
    }

    protected async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
    }

    protected async Task<T> ExecuteScalarAsync<T>(SqlCommand command)
    {
        var result = await command.ExecuteScalarAsync();
        return (T)result;
    }

    protected async Task<List<T>> ExecuteReaderAsync<T>(SqlCommand command, Func<IDataReader, T> map)
    {
        var results = new List<T>();
        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }
        }
        return results;
    }

    protected void ExecuteNonQuery(SqlTransaction transaction, SqlCommand command)
    {
        command.Transaction = transaction;
        command.ExecuteNonQuery();
    }

    protected T ExecuteScalar<T>(SqlTransaction transaction, SqlCommand command)
    {
        command.Transaction = transaction;
        return (T)command.ExecuteScalar();
    }

    protected List<T> ExecuteReader<T>(SqlTransaction transaction, SqlCommand command, Func<IDataReader, T> map)
    {
        var results = new List<T>();
        command.Transaction = transaction;
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                results.Add(map(reader));
            }
        }
        return results;
    }
}