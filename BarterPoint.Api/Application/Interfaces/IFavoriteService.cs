
namespace BarterPoint.Application;

public interface IFavoriteService
{
    Task AddFavoriteAsync(string userId, string productId);
    Task<IEnumerable<FavoriteResult>> GetUserFavoritesAsync(string userId);
    Task RemoveFavoriteAsync(string userId, string productId);
    Task<bool> IsFavoriteAsync(string userId, string productId);
}