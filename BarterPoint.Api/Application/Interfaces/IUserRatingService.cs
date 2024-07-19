namespace BarterPoint.Application;

public interface IUserRatingService
{
    Task AddRating(RateUserRequest request);
    Task<double> GetUserAverageRating(string userId);
}