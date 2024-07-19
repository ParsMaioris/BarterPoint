public interface IRatingsServiceV2
{
    Task<UserRatingResultV2> GetUserAverageRating(string userId);
    Task AddRating(RateUserRequestV2 ratingRequest);
}