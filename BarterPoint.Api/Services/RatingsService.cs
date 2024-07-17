public class RatingsService : IRatingsService
{
    private readonly IRatingsRepository _ratingsRepository;

    public RatingsService(IRatingsRepository databaseService)
    {
        _ratingsRepository = databaseService;
    }

    public async Task<UserRating> GetUserAverageRating(string userId)
    {
        var averageRating = await _ratingsRepository.GetUserAverageRatingAsync(userId);
        return new UserRating { AverageRating = averageRating };
    }

    public async Task AddRating(RateUserRequest ratingRequest)
    {
        await _ratingsRepository.RateUserAsync(ratingRequest);
    }
}