namespace BarterPoint.Domain;

public interface IFavoriteRepository : IRepository<Favorite, int, Favorite>
{
    IEnumerable<Favorite> GetFavoritesByUserId(string userId);
    bool IsFavorite(string userId, string productId);
}