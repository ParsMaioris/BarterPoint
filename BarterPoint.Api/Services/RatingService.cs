public class RatingService : IRatingService
{
    private readonly IDatabaseService _databaseService;

    public RatingService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<UserRating> GetUserAverageRating(string userId)
    {
        var averageRating = await _databaseService.GetUserAverageRatingAsync(userId);
        return new UserRating { AverageRating = averageRating };
    }

    public async Task AddRating(RateUserRequest ratingRequest)
    {
        await _databaseService.RateUserAsync(ratingRequest);
    }
}