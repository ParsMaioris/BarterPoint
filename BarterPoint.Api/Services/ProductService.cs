public class ProductService : IProductService
{
    private readonly IDatabaseService _databaseService;

    public ProductService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<IEnumerable<ProductDTO>> GetAvailableProductsByOwnerAsync(string ownerId)
    {
        var allProducts = await _databaseService.GetProductsByOwner(ownerId);
        var allTransactionHistories = await _databaseService.GetAllTransactionHistoryAsync();

        var productIdsInTransactions = allTransactionHistories
            .Select(t => t.ProductId)
            .Distinct()
            .ToHashSet();

        var availableProducts = allProducts
            .Where(p => !productIdsInTransactions.Contains(p.Id))
            .ToList();

        return availableProducts;
    }

    public async Task<IEnumerable<ProductDTO>> GetAvailableProductsNotOwnedByUserAsync(string ownerId)
    {
        var allProducts = await _databaseService.GetProductsNotOwnedByUser(ownerId);
        var allTransactionHistories = await _databaseService.GetAllTransactionHistoryAsync();

        var productIdsInTransactions = allTransactionHistories
            .Select(t => t.ProductId)
            .Distinct()
            .ToHashSet();

        var availableProducts = allProducts
            .Where(p => !productIdsInTransactions.Contains(p.Id))
            .ToList();

        return availableProducts;
    }

    public async Task<string> AddProductAsync(AddProductRequest product)
    {
        return await _databaseService.AddProductAsync(product);
    }

    public async Task RemoveProductAsync(string productId)
    {
        await _databaseService.RemoveProductAsync(productId);
    }
}