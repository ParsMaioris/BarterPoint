namespace BarterPoint.Domain;

public class ProductCategoryDomainService
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryDomainService(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public string GetCategoryNameById(int categoryId)
    {
        return _productCategoryRepository.GetCategoryNameById(categoryId);
    }
}