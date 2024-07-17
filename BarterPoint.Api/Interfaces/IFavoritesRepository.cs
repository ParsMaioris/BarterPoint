public interface IFavoritesRepository
{
    Task AddFavoriteAsync(string userId, string productId);
    Task<List<FavoriteResult>> GetUserFavoritesAsync(string userId);
    Task RemoveFavoriteAsync(string userId, string productId);
    Task<bool> IsFavoriteAsync(string userId, string productId);
}