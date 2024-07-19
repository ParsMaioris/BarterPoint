namespace BarterPoint.Domain;

public interface IUserRatingRepository : IRepository<UserRating, int, UserRating>
{
    IEnumerable<UserRating> GetRatingsByUserId(string userId);
}