public class FavoritesServiceV2 : IFavoritesServiceV2
{
    private readonly IFavoritesRepositoryV2 _favoritesRepository;

    public FavoritesServiceV2(IFavoritesRepositoryV2 favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    public async Task AddFavoriteAsync(string userId, string productId)
    {
        await _favoritesRepository.AddFavoriteAsync(userId, productId);
    }

    public async Task<List<FavoriteResultV2>> GetUserFavoritesAsync(string userId)
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