using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class UserRatingRepository : BaseRepository, IUserRatingRepository
{
    public UserRatingRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<UserRating>> GetAllAsync()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllUserRatings"))
        {
            return await ExecuteReaderAsync(command, MapUserRating);
        }
    }

    public async Task<UserRating> GetByIdAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetUserRatingById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var ratings = await ExecuteReaderAsync(command, MapUserRating);
            return ratings.FirstOrDefault();
        }
    }

    public async Task AddAsync(UserRating rating)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddUserRating"))
        {
            AddRatingParameters(command, rating);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task UpdateAsync(UserRating rating)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateUserRating"))
        {
            UpdateRatingParameters(command, rating);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteUserRating"))
        {
            command.Parameters.AddWithValue("@Id", id);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task<double> GetUserAverageRatingAsync(string userId)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetUserRatings"))
        {
            command.Parameters.AddWithValue("@UserId", userId);
            var rating = await ExecuteReaderAsync(command, r => r.GetDouble(0));
            return rating.Single();
        }
    }

    private void AddRatingParameters(SqlCommand command, UserRating rating)
    {
        command.Parameters.AddWithValue("@RaterId", rating.RaterId);
        command.Parameters.AddWithValue("@RateeId", rating.RateeId);
        command.Parameters.AddWithValue("@Rating", rating.Rating);
        command.Parameters.AddWithValue("@Review", rating.Review ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DateRated", rating.DateRated);
    }

    private void UpdateRatingParameters(SqlCommand command, UserRating rating)
    {
        command.Parameters.AddWithValue("@Id", rating.Id);
        command.Parameters.AddWithValue("@RaterId", rating.RaterId);
        command.Parameters.AddWithValue("@RateeId", rating.RateeId);
        command.Parameters.AddWithValue("@Rating", rating.Rating);
        command.Parameters.AddWithValue("@Review", rating.Review ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DateRated", rating.DateRated);
    }

    private UserRating MapUserRating(IDataRecord record)
    {
        return new UserRating
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            raterId: record.GetString(record.GetOrdinal("raterId")),
            rateeId: record.GetString(record.GetOrdinal("rateeId")),
            rating: record.GetInt32(record.GetOrdinal("rating")),
            review: record.IsDBNull(record.GetOrdinal("review")) ? null : record.GetString(record.GetOrdinal("review")),
            dateRated: record.GetDateTime(record.GetOrdinal("dateRated"))
        );
    }
}