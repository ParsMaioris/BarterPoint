public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoritesRepository;

    public FavoritesService(IFavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    public void AddFavorite(string userId, string productId)
    {
        _favoritesRepository.AddFavorite(userId, productId);
    }

    public List<Favorite> GetUserFavorites(string userId)
    {
        return _favoritesRepository.GetUserFavorites(userId);
    }

    public void RemoveFavorite(string userId, string productId)
    {
        _favoritesRepository.RemoveFavorite(userId, productId);
    }

    public bool IsFavorite(string userId, string productId)
    {
        return _favoritesRepository.IsFavorite(userId, productId);
    }
}