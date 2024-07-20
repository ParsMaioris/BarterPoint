using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
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
            UserParameters(command, user);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(User user)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateUser"))
        {
            UserParameters(command, user);
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

    private void UserParameters(SqlCommand command, User user)
    {
        command.Parameters.AddWithValue("@Id", user.Id);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@Location", user.Location);
        command.Parameters.AddWithValue("@DateJoined", user.DateJoined);
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