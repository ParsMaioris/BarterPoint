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

    private IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    private IDbCommand CreateCommand(IDbConnection connection, string procedureName, params SqlParameter[] parameters)
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
}