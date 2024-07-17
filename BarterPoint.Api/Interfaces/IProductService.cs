public interface IProductService
{
    Task<IEnumerable<ProductResult>> GetAvailableProductsByOwnerAsync(string ownerId);
    Task<IEnumerable<ProductResult>> GetAvailableProductsNotOwnedByUserAsync(string ownerId);
    Task<string> AddProductAsync(AddProductRequest product);
    Task RemoveProductAsync(string productId);
}