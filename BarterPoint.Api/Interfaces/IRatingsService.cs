public interface IRatingsService
{
    Task<UserRating> GetUserAverageRating(string userId);
    Task AddRating(RateUserRequest ratingRequest);
}