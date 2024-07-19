namespace BarterPoint.Domain;

public interface IProductCategoryRepository
{
    IEnumerable<ProductCategory> GetAll();
    string GetCategoryNameById(int id);
}