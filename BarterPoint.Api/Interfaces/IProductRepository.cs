public interface IProductRepository
{
    Task RemoveProductAsync(string productId);
    Task<string> AddProductAsync(AddProductRequest product);
    Task<IEnumerable<ProductResult>> GetProductsNotOwnedByUser(string ownerId);
    Task<IEnumerable<ProductResult>> GetProductsByOwner(string ownerId);
}