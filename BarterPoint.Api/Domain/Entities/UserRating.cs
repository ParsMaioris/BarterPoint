namespace BarterPoint.Domain;

public class UserRating
{
    public int Id { get; private set; }
    public string RaterId { get; private set; }
    public string RateeId { get; private set; }
    public int Rating { get; private set; }
    public string Review { get; private set; }
    public DateTime DateRated { get; private set; }

    public UserRating(int id, string raterId, string rateeId, int rating, string review, DateTime dateRated)
    {
        Id = id;
        RaterId = raterId;
        RateeId = rateeId;
        Rating = rating;
        Review = review;
        DateRated = dateRated;
    }
}