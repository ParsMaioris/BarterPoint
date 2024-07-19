public interface IProductServiceV2
{
    Task<IEnumerable<ProductResultV2>> GetAvailableProductsByOwnerAsync(string ownerId);
    Task<IEnumerable<ProductResultV2>> GetAvailableProductsNotOwnedByUserAsync(string ownerId);
    Task<string> AddProductAsync(AddProductRequestV2 product);
    Task RemoveProductAsync(string productId);
}