namespace BarterPoint.Domain;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<string> GetCategoryNameByIdAsync(int id);
}