public class ProductServiceV2 : IProductServiceV2
{
    private readonly IProductRepositoryV2 _productRepository;
    protected readonly ITransactionRepositoryV2 _transactionRepository;

    public ProductServiceV2(IProductRepositoryV2 productRepository, ITransactionRepositoryV2 transactionRepository)
    {
        _productRepository = productRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<ProductResultV2>> GetAvailableProductsByOwnerAsync(string ownerId)
    {
        var allProducts = await _productRepository.GetProductsByOwner(ownerId);
        var allTransactionHistories = await _transactionRepository.GetAllTransactionHistoryAsync();

        var productIdsInTransactions = allTransactionHistories
            .Select(t => t.ProductId)
            .Distinct()
            .ToHashSet();

        var availableProducts = allProducts
            .Where(p => !productIdsInTransactions.Contains(p.Id))
            .ToList();

        return availableProducts;
    }

    public async Task<IEnumerable<ProductResultV2>> GetAvailableProductsNotOwnedByUserAsync(string ownerId)
    {
        var allProducts = await _productRepository.GetProductsNotOwnedByUser(ownerId);
        var allTransactionHistories = await _transactionRepository.GetAllTransactionHistoryAsync();

        var productIdsInTransactions = allTransactionHistories
            .Select(t => t.ProductId)
            .Distinct()
            .ToHashSet();

        var availableProducts = allProducts
            .Where(p => !productIdsInTransactions.Contains(p.Id))
            .ToList();

        return availableProducts;
    }

    public async Task<string> AddProductAsync(AddProductRequestV2 product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task RemoveProductAsync(string productId)
    {
        await _productRepository.RemoveProductAsync(productId);
    }
}