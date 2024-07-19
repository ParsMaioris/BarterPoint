namespace BarterPoint.Application;

public interface IProductService
{
    Task<IEnumerable<ProductResult>> GetAvailableProductsByOwnerAsync(string ownerId);
    Task<IEnumerable<ProductResult>> GetAvailableProductsNotOwnedByUserAsync(string ownerId);
    Task<string> AddProductAsync(AddProductRequest request);
    Task RemoveProductAsync(string id);
}