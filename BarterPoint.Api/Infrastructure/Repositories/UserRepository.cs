using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    public UserRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
    }

    public IEnumerable<User> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllUsers"))
        {
            return ExecuteReaderAsync(command, MapUser).Result;
        }
    }

    public User GetById(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetUserById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var users = ExecuteReaderAsync(command, MapUser).Result;
            return users.FirstOrDefault();
        }
    }

    public void Add(User user)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddUser"))
        {
            AddUserParameters(command, user);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(User user)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateUser"))
        {
            AddUserParameters(command, user);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteUser"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    private void AddUserParameters(SqlCommand command, User user)
    {
        command.Parameters.AddWithValue("@Id", user.Id);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@Location", user.Location);
        command.Parameters.AddWithValue("@DateJoined", user.DateJoined);
    }

    private async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
    }

    private async Task<List<T>> ExecuteReaderAsync<T>(SqlCommand command, Func<IDataReader, T> map)
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

    private SqlCommand CreateCommand(SqlConnection connection, string storedProcedure)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedure;
        return command;
    }

    private User MapUser(IDataRecord record)
    {
        return new User
        (
            id: record.GetString(record.GetOrdinal("id")),
            username: record.GetString(record.GetOrdinal("username")),
            passwordHash: record.GetString(record.GetOrdinal("password_hash")),
            email: record.GetString(record.GetOrdinal("email")),
            name: record.GetString(record.GetOrdinal("name")),
            location: record.IsDBNull(record.GetOrdinal("location")) ? null : record.GetString(record.GetOrdinal("location")),
            dateJoined: record.GetDateTime(record.GetOrdinal("dateJoined"))
        );
    }
}