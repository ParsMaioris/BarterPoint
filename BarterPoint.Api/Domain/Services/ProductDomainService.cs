namespace BarterPoint.Domain;

public class ProductDomainService
{
    private readonly IProductRepository _productRepository;

    public ProductDomainService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId)
    {
        var products = await _productRepository.GetAllAsync();
        return products.Where(p => p.OwnerId == ownerId);
    }

    public async Task<IEnumerable<Product>> GetProductsNotOwnedByUserAsync(string ownerId)
    {
        var products = await _productRepository.GetAllAsync();
        return products.Where(p => p.OwnerId != ownerId);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(string productId)
    {
        return await _productRepository.GetByIdAsync(productId);
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(string productId)
    {
        await _productRepository.DeleteAsync(productId);
    }
}