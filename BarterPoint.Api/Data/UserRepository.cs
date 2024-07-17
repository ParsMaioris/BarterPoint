using System.Data;
using System.Data.SqlClient;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequest request)
    {
        var userId = Guid.NewGuid();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("RegisterUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", userId);
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);
                command.Parameters.AddWithValue("@email", request.Email);
                command.Parameters.AddWithValue("@name", request.Name);
                command.Parameters.AddWithValue("@location", request.Location);
                command.Parameters.AddWithValue("@dateJoined", request.DateJoined);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        if (reader.FieldCount > 0)
                        {
                            var result = reader.GetName(0);
                            return reader[result].ToString();
                        }
                    }
                }
            }
        }

        return "Unknown error occurred during registration.";
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequest request)
    {
        var result = new SignInResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SignInUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        if (reader.FieldCount > 0)
                        {
                            var firstColumnName = reader.GetName(0);

                            if (firstColumnName == "Message")
                            {
                                result.Message = reader["Message"].ToString();
                                if (reader["UserId"] != DBNull.Value)
                                {
                                    result.UserId = reader["UserId"].ToString();
                                }
                            }
                            else if (firstColumnName == "ErrorMessage")
                            {
                                result.ErrorMessage = reader["ErrorMessage"].ToString();
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
}