public interface IRatingsService
{
    Task<UserRatingResult> GetUserAverageRating(string userId);
    Task AddRating(RateUserRequest ratingRequest);
}