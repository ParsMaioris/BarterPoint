public interface IProductRepositoryV2
{
    Task RemoveProductAsync(string productId);
    Task<string> AddProductAsync(AddProductRequestV2 product);
    Task<IEnumerable<ProductResultV2>> GetProductsNotOwnedByUser(string ownerId);
    Task<IEnumerable<ProductResultV2>> GetProductsByOwner(string ownerId);
}