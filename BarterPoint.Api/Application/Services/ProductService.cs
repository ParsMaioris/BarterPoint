using BarterPoint.Domain;

namespace BarterPoint.Application;

public class ProductService : IProductService
{
    private readonly ProductDomainService _productDomainService;
    private readonly TransactionDomainService _transactionDomainService;
    private readonly ProductCategoryDomainService _productCategoryDomainService;

    public ProductService(ProductDomainService productDomainService, TransactionDomainService transactionDomainService,
        ProductCategoryDomainService productCategoryDomainService)
    {
        _productDomainService = productDomainService;
        _transactionDomainService = transactionDomainService;
        _productCategoryDomainService = productCategoryDomainService;
    }

    public async Task<IEnumerable<ProductResult>> GetAvailableProductsByOwnerAsync(string ownerId)
    {
        var products = await Task.Run(() => _productDomainService.GetProductsByOwner(ownerId));
        var transactions = await Task.Run(() => _transactionDomainService.GetTransactionsByUserId(ownerId));
        var productIdsInTransactions = transactions.Select(t => t.ProductId).ToHashSet();

        var availableProducts = products.Where(p => !productIdsInTransactions.Contains(p.Id));
        return availableProducts.Select(MapToProductResult);
    }

    public async Task<IEnumerable<ProductResult>> GetAvailableProductsNotOwnedByUserAsync(string ownerId)
    {
        var products = await Task.Run(() => _productDomainService.GetProductsNotOwnedByUser(ownerId));
        var transactions = await Task.Run(() => _transactionDomainService.GetTransactionsByUserId(ownerId));
        var productIdsInTransactions = transactions.Select(t => t.ProductId).ToHashSet();

        var availableProducts = products.Where(p => !productIdsInTransactions.Contains(p.Id));
        return availableProducts.Select(MapToProductResult);
    }

    public async Task<string> AddProductAsync(AddProductRequest request)
    {
        var product = new Product(
            id: Guid.NewGuid().ToString(),
            name: request.Name,
            image: request.Image,
            description: request.Description,
            tradeFor: request.TradeFor,
            categoryId: request.CategoryId,
            condition: request.Condition,
            location: request.Location,
            ownerId: request.OwnerId,
            dimensionsWidth: request.DimensionsWidth,
            dimensionsHeight: request.DimensionsHeight,
            dimensionsDepth: request.DimensionsDepth,
            dimensionsWeight: request.DimensionsWeight,
            dateListed: DateTime.UtcNow
        );

        await Task.Run(() => _productDomainService.AddProduct(product));
        return product.Id;
    }

    public async Task RemoveProductAsync(string id)
    {
        await Task.Run(() => _productDomainService.DeleteProduct(id));
    }

    private ProductResult MapToProductResult(Product product)
    {
        return new ProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Image = product.Image,
            Description = product.Description,
            TradeFor = product.TradeFor,
            Category = _productCategoryDomainService.GetCategoryNameById(product.CategoryId),
            Condition = product.Condition,
            Location = product.Location,
            OwnerId = product.OwnerId,
            DimensionsWidth = product.DimensionsWidth,
            DimensionsHeight = product.DimensionsHeight,
            DimensionsDepth = product.DimensionsDepth,
            DimensionsWeight = product.DimensionsWeight,
            DateListed = product.DateListed
        };
    }
}