namespace BarterPoint.Domain;

public class UserRatingDomainService
{
    private readonly IUserRatingRepository _ratingRepository;

    public UserRatingDomainService(IUserRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task AddRatingAsync(UserRating rating)
    {
        await _ratingRepository.AddAsync(rating);
    }

    public async Task<IEnumerable<UserRating>> GetAllRatingsAsync()
    {
        return await _ratingRepository.GetAllAsync();
    }

    public async Task<UserRating> GetRatingByIdAsync(int id)
    {
        return await _ratingRepository.GetByIdAsync(id);
    }

    public async Task UpdateRatingAsync(UserRating rating)
    {
        await _ratingRepository.UpdateAsync(rating);
    }

    public async Task DeleteRatingAsync(int id)
    {
        await _ratingRepository.DeleteAsync(id);
    }

    public async Task<double> GetUserAverageRatingAsync(string userId)
    {
        var userRatings = (await _ratingRepository.GetAllAsync()).Where(r => r.RateeId == userId);
        if (!userRatings.Any())
        {
            return 0;
        }
        return userRatings.Average(r => r.Rating);
    }
}