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

    public IEnumerable<UserRating> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllUserRatings"))
        {
            return ExecuteReaderAsync(command, MapUserRating).Result;
        }
    }

    public UserRating GetById(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetUserRatingById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var ratings = ExecuteReaderAsync(command, MapUserRating).Result;
            return ratings.FirstOrDefault();
        }
    }

    public void Add(UserRating rating)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddUserRating"))
        {
            AddRatingParameters(command, rating);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(UserRating rating)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateUserRating"))
        {
            UpdateRatingParameters(command, rating);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteUserRating"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public double GetUserAverageRating(string userId)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetUserRatings"))
        {
            command.Parameters.AddWithValue("@UserId", userId);
            var rating = ExecuteReaderAsync(command, r => r.GetDouble(0)).Result.Single();
            return rating;
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