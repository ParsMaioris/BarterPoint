public interface IRatingsRepository
{
    Task RateUserAsync(RateUserRequest rating);
    Task<double> GetUserAverageRatingAsync(string userId);
}