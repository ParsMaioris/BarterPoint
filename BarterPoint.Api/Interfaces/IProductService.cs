public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAvailableProductsByOwnerAsync(string ownerId);
    Task<IEnumerable<ProductDTO>> GetAvailableProductsNotOwnedByUserAsync(string ownerId);
    Task<string> AddProductAsync(AddProductRequest product);
    Task RemoveProductAsync(string productId);
}