public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    protected readonly ITransactionRepository _transactionRepository;

    public ProductService(IProductRepository productRepository, ITransactionRepository transactionRepository)
    {
        _productRepository = productRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<ProductResult>> GetAvailableProductsByOwnerAsync(string ownerId)
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

    public async Task<IEnumerable<ProductResult>> GetAvailableProductsNotOwnedByUserAsync(string ownerId)
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

    public async Task<string> AddProductAsync(AddProductRequest product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task RemoveProductAsync(string productId)
    {
        await _productRepository.RemoveProductAsync(productId);
    }
}