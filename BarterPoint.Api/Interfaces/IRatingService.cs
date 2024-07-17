public interface IRatingService
{
    Task<UserRating> GetUserAverageRating(string userId);
    Task AddRating(RateUserRequest ratingRequest);
}