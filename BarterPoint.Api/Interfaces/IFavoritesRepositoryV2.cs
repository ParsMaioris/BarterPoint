public interface IFavoritesRepositoryV2
{
    Task AddFavoriteAsync(string userId, string productId);
    Task<List<FavoriteResultV2>> GetUserFavoritesAsync(string userId);
    Task RemoveFavoriteAsync(string userId, string productId);
    Task<bool> IsFavoriteAsync(string userId, string productId);
}