public class RatingsService : IRatingsService
{
    private readonly IRatingsRepository _ratingsRepository;

    public RatingsService(IRatingsRepository databaseService)
    {
        _ratingsRepository = databaseService;
    }

    public async Task<UserRatingResult> GetUserAverageRating(string userId)
    {
        var averageRating = await _ratingsRepository.GetUserAverageRatingAsync(userId);
        return new UserRatingResult { AverageRating = averageRating };
    }

    public async Task AddRating(RateUserRequest ratingRequest)
    {
        await _ratingsRepository.RateUserAsync(ratingRequest);
    }
}