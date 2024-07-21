namespace BarterPoint.Domain;

public class ProductCategoryDomainService
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryDomainService(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<string> GetCategoryNameByIdAsync(int categoryId)
    {
        return await _productCategoryRepository.GetCategoryNameByIdAsync(categoryId);
    }
}