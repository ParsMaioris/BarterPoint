
using BarterPoint.Domain;

namespace BarterPoint.Application;

public class FavoriteService : IFavoriteService
{
    private readonly FavoriteDomainService _favoriteDomainService;
    private readonly ProductDomainService _productDomainService;
    private readonly TransactionDomainService _transactionDomainService;

    public FavoriteService(FavoriteDomainService favoriteDomainService, ProductDomainService productDomainService,
        TransactionDomainService transactionDomainService)
    {
        _favoriteDomainService = favoriteDomainService;
        _productDomainService = productDomainService;
        _transactionDomainService = transactionDomainService;
    }

    public async Task AddFavoriteAsync(string userId, string productId)
    {
        var favorite = new Favorite(0, userId, productId, DateTime.UtcNow);
        await Task.Run(() => _favoriteDomainService.AddFavorite(favorite));
    }

    public async Task<IEnumerable<FavoriteResult>> GetUserFavoritesAsync(string userId)
    {
        var userFavorites = await Task.Run(() => _favoriteDomainService.GetUserFavorites(userId));
        var allTransactions = await Task.Run(() => _transactionDomainService.GetAllTransactions());
        var soldProductIds = allTransactions.Select(transaction => transaction.ProductId).ToHashSet();

        var unsoldFavorites = userFavorites
            .Where(favorite => !soldProductIds.Contains(favorite.ProductId))
            .Select(favorite => new FavoriteResult
            {
                FavoritesId = favorite.Id,
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                DateAdded = favorite.DateAdded,
                ProductName = _productDomainService.GetProductById(favorite.ProductId).Name
            });

        return unsoldFavorites;
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
