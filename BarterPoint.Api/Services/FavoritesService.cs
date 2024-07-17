public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoritesRepository;

    public FavoritesService(IFavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    public async Task AddFavoriteAsync(string userId, string productId)
    {
        await _favoritesRepository.AddFavoriteAsync(userId, productId);
    }

    public async Task<List<FavoriteResult>> GetUserFavoritesAsync(string userId)
    {
        return await _favoritesRepository.GetUserFavoritesAsync(userId);
    }

    public async Task RemoveFavoriteAsync(string userId, string productId)
    {
        await _favoritesRepository.RemoveFavoriteAsync(userId, productId);
    }

    public async Task<bool> IsFavoriteAsync(string userId, string productId)
    {
        return await _favoritesRepository.IsFavoriteAsync(userId, productId);
    }
}