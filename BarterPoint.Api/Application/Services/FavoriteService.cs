
using BarterPoint.Domain;

namespace BarterPoint.Application;
public class FavoriteService : IFavoriteService
{
    private readonly FavoriteDomainService _favoriteDomainService;
    private readonly ProductDomainService _productDomainService;

    public FavoriteService(FavoriteDomainService favoriteDomainService, ProductDomainService productDomainService)
    {
        _favoriteDomainService = favoriteDomainService;
        _productDomainService = productDomainService;
    }

    public async Task AddFavoriteAsync(string userId, string productId)
    {
        var favorite = new Favorite(0, userId, productId, DateTime.UtcNow);
        await Task.Run(() => _favoriteDomainService.AddFavorite(favorite));
    }

    public async Task<IEnumerable<FavoriteResult>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await Task.Run(() => _favoriteDomainService.GetUserFavorites(userId));
        var favoriteResults = favorites.Select(f => new FavoriteResult
        {
            FavoritesId = f.Id,
            UserId = f.UserId,
            ProductId = f.ProductId,
            DateAdded = f.DateAdded,
            ProductName = _productDomainService.GetProductById(f.ProductId).Name
        });

        return favoriteResults;
    }

    public async Task RemoveFavoriteAsync(string userId, string productId)
    {
        var favorite = await Task.Run(() => _favoriteDomainService.GetFavoriteByUserAndProduct(userId, productId));
        if (favorite != null)
        {
            await Task.Run(() => _favoriteDomainService.RemoveFavorite(favorite.Id));
        }
    }

    public async Task<bool> IsFavoriteAsync(string userId, string productId)
    {
        var favorite = await Task.Run(() => _favoriteDomainService.GetFavoriteByUserAndProduct(userId, productId));
        return favorite != null;
    }
}
