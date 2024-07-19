namespace BarterPoint.Domain;

public class UserRatingDomainService
{
    private readonly IUserRatingRepository _ratingRepository;

    public UserRatingDomainService(IUserRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public void AddRating(UserRating rating)
    {
        _ratingRepository.Add(rating);
    }

    public IEnumerable<UserRating> GetAllRatings()
    {
        return _ratingRepository.GetAll();
    }

    public UserRating GetRatingById(int id)
    {
        return _ratingRepository.GetById(id);
    }

    public void UpdateRating(UserRating rating)
    {
        _ratingRepository.Update(rating);
    }

    public void DeleteRating(int id)
    {
        _ratingRepository.Delete(id);
    }

    public double GetUserAverageRating(string userId)
    {
        var userRatings = _ratingRepository.GetAll().Where(r => r.RateeId == userId);
        if (!userRatings.Any())
        {
            return 0;
        }
        return userRatings.Average(r => r.Rating);
    }
}