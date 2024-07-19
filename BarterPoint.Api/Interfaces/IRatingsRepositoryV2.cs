public interface IRatingsRepositoryV2
{
    Task RateUserAsync(RateUserRequestV2 rating);
    Task<double> GetUserAverageRatingAsync(string userId);
}