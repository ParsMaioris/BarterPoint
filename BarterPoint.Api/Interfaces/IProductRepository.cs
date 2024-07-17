public interface IProductRepository
{
    Task RemoveProductAsync(string productId);
    Task<string> AddProductAsync(AddProductRequest product);
    Task<IEnumerable<ProductDTO>> GetProductsNotOwnedByUser(string ownerId);
    Task<IEnumerable<ProductDTO>> GetProductsByOwner(string ownerId);
}