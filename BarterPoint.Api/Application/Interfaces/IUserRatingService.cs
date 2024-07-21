namespace BarterPoint.Application;

public interface IUserRatingService
{
    Task AddRatingAsync(RateUserRequest request);
    Task<double> GetUserAverageRatingAsync(string userId);
}