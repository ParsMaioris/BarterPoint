using BarterPoint.Domain;

namespace BarterPoint.Application;

public class UserRatingService : IUserRatingService
{
    private readonly UserRatingDomainService _ratingDomainService;

    public UserRatingService(UserRatingDomainService ratingDomainService)
    {
        _ratingDomainService = ratingDomainService;
    }

    public async Task AddRatingAsync(RateUserRequest request)
    {
        var rating = new UserRating(0, request.RaterId, request.RateeId, request.Rating, request.Review, request.DateRated);
        await _ratingDomainService.AddRatingAsync(rating);
    }

    public async Task<double> GetUserAverageRatingAsync(string userId)
    {
        return await _ratingDomainService.GetUserAverageRatingAsync(userId);
    }
}