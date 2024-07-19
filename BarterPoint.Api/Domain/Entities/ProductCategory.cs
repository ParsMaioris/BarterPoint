namespace BarterPoint.Domain;

public class ProductCategory
{
    public int Id { get; private set; }
    public string Category { get; private set; }

    public ProductCategory(int id, string category)
    {
        Id = id;
        Category = category;
    }
}