using System.Data;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void ExecuteNonQuery(string procedureName, params SqlParameter[] parameters)
    {
        using (var connection = GetConnection())
        {
            var command = CreateCommand(connection, procedureName, parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public T ExecuteScalar<T>(string procedureName, params SqlParameter[] parameters)
    {
        using (var connection = GetConnection())
        {
            var command = CreateCommand(connection, procedureName, parameters);
            connection.Open();
            return (T)command.ExecuteScalar();
        }
    }

    public List<T> ExecuteReader<T>(string procedureName, Func<IDataReader, T> map, params SqlParameter[] parameters)
    {
        var results = new List<T>();

        using (var connection = GetConnection())
        {
            var command = CreateCommand(connection, procedureName, parameters);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(map(reader));
                }
            }
        }

        return results;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public SqlCommand CreateCommand(SqlConnection connection, string procedureName, params SqlParameter[] parameters)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = procedureName;

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }

    public void ExecuteNonQuery(SqlTransaction transaction, string procedureName, params SqlParameter[] parameters)
    {
        var command = CreateCommand(transaction.Connection, procedureName, parameters);
        command.Transaction = transaction;
        command.ExecuteNonQuery();
    }

    public T ExecuteScalar<T>(SqlTransaction transaction, string procedureName, params SqlParameter[] parameters)
    {
        var command = CreateCommand(transaction.Connection, procedureName, parameters);
        command.Transaction = transaction;
        return (T)command.ExecuteScalar();
    }

    public List<T> ExecuteReader<T>(SqlTransaction transaction, string procedureName, Func<IDataReader, T> map, params SqlParameter[] parameters)
    {
        var results = new List<T>();
        var command = CreateCommand(transaction.Connection, procedureName, parameters);
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