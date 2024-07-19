namespace BarterPoint.Domain;

public class FavoriteDomainService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteDomainService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public void AddFavorite(Favorite favorite)
    {
        _favoriteRepository.Add(favorite);
    }

    public void RemoveFavorite(int id)
    {
        _favoriteRepository.Delete(id);
    }

    public IEnumerable<Favorite> GetUserFavorites(string userId)
    {
        var allFavorites = _favoriteRepository.GetAll();
        return allFavorites.Where(f => f.UserId == userId);
    }

    public Favorite GetFavoriteByUserAndProduct(string userId, string productId)
    {
        var allFavorites = _favoriteRepository.GetAll();
        return allFavorites.FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);
    }
}