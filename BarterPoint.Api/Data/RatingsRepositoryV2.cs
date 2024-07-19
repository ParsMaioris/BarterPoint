public class RatingsRepositoryV2 : BaseRepository, IRatingsRepositoryV2
{
    public RatingsRepositoryV2(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task RateUserAsync(RateUserRequestV2 rating)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "RateUser"))
            {
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
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetUserRatings"))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                var result = await command.ExecuteScalarAsync();
                return (double)result;
            }
        }
    }
}