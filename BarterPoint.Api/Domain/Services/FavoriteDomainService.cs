namespace BarterPoint.Domain;

public class FavoriteDomainService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteDomainService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task AddFavoriteAsync(Favorite favorite)
    {
        await _favoriteRepository.AddAsync(favorite);
    }

    public async Task RemoveFavoriteAsync(int id)
    {
        await _favoriteRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(string userId)
    {
        var allFavorites = await _favoriteRepository.GetAllAsync();
        return allFavorites.Where(f => f.UserId == userId);
    }

    public async Task<Favorite> GetFavoriteByUserAndProductAsync(string userId, string productId)
    {
        var allFavorites = await _favoriteRepository.GetAllAsync();
        return allFavorites.FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);
    }
}