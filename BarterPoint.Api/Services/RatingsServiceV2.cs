public class RatingsServiceV2 : IRatingsServiceV2
{
    private readonly IRatingsRepositoryV2 _ratingsRepository;

    public RatingsServiceV2(IRatingsRepositoryV2 databaseService)
    {
        _ratingsRepository = databaseService;
    }

    public async Task<UserRatingResultV2> GetUserAverageRating(string userId)
    {
        var averageRating = await _ratingsRepository.GetUserAverageRatingAsync(userId);
        return new UserRatingResultV2 { AverageRating = averageRating };
    }

    public async Task AddRating(RateUserRequestV2 ratingRequest)
    {
        await _ratingsRepository.RateUserAsync(ratingRequest);
    }
}