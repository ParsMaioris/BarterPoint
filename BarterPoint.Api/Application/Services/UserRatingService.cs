using BarterPoint.Domain;

namespace BarterPoint.Application;

public class UserRatingService : IUserRatingService
{
    private readonly UserRatingDomainService _ratingDomainService;

    public UserRatingService(UserRatingDomainService ratingDomainService)
    {
        _ratingDomainService = ratingDomainService;
    }

    public async Task AddRating(RateUserRequest request)
    {
        var rating = new UserRating(0, request.RaterId, request.RateeId, request.Rating, request.Review, request.DateRated);
        await Task.Run(() => _ratingDomainService.AddRating(rating));
    }

    public async Task<double> GetUserAverageRating(string userId)
    {
        return await Task.Run(() => _ratingDomainService.GetUserAverageRating(userId));
    }
}