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
        await _favoriteDomainService.AddFavoriteAsync(favorite);
    }

    public async Task<IEnumerable<FavoriteResult>> GetUserFavoritesAsync(string userId)
    {
        var userFavorites = await _favoriteDomainService.GetUserFavoritesAsync(userId);
        var allTransactions = await _transactionDomainService.GetAllTransactionsAsync();
        var soldProductIds = allTransactions.Select(transaction => transaction.ProductId).ToHashSet();

        var unsoldFavoritesTasks = userFavorites
            .Where(favorite => !soldProductIds.Contains(favorite.ProductId))
            .Select(async favorite => new FavoriteResult
            {
                FavoritesId = favorite.Id,
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                DateAdded = favorite.DateAdded,
                ProductName = (await _productDomainService.GetProductByIdAsync(favorite.ProductId)).Name
            });

        var unsoldFavorites = await Task.WhenAll(unsoldFavoritesTasks);
        return unsoldFavorites;
    }

    public async Task RemoveFavoriteAsync(string userId, string productId)
    {
        var favorite = await _favoriteDomainService.GetFavoriteByUserAndProductAsync(userId, productId);
        if (favorite != null)
        {
            await _favoriteDomainService.RemoveFavoriteAsync(favorite.Id);
        }
    }

    public async Task<bool> IsFavoriteAsync(string userId, string productId)
    {
        var favorite = await _favoriteDomainService.GetFavoriteByUserAndProductAsync(userId, productId);
        return favorite != null;
    }
}