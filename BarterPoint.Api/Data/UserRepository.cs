public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequest request)
    {
        var userId = Guid.NewGuid();

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "RegisterUser"))
            {
                command.Parameters.AddWithValue("@id", userId);
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);
                command.Parameters.AddWithValue("@email", request.Email);
                command.Parameters.AddWithValue("@name", request.Name);
                command.Parameters.AddWithValue("@location", request.Location);
                command.Parameters.AddWithValue("@dateJoined", request.DateJoined);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
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

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "SignInUser"))
            {
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
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