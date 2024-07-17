using System.Data;
using System.Data.SqlClient;

public class RatingsRepository : IRatingsRepository
{
    private readonly string _connectionString;

    public RatingsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task RateUserAsync(RateUserRequest rating)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("RateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RaterId", rating.RaterId);
                command.Parameters.AddWithValue("@RateeId", rating.RateeId);
                command.Parameters.AddWithValue("@Rating", rating.Rating);
                command.Parameters.AddWithValue("@Review", rating.Review);
                command.Parameters.AddWithValue("@DateRated", rating.DateRated);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<double> GetUserAverageRatingAsync(string userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("GetUserRatings", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);

                var result = await command.ExecuteScalarAsync();
                return (double)result;
            }
        }
    }
}