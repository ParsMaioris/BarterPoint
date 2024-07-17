public interface IFavoritesRepository
{
    void AddFavorite(string userId, string productId);
    List<FavoriteResult> GetUserFavorites(string userId);
    void RemoveFavorite(string userId, string productId);
    bool IsFavorite(string userId, string productId);
}