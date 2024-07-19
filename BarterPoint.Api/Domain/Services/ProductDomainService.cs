namespace BarterPoint.Domain;

public class ProductDomainService
{
    private readonly IProductRepository _productRepository;

    public ProductDomainService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetProductsByOwner(string ownerId)
    {
        return _productRepository.GetAll().Where(p => p.OwnerId == ownerId);
    }

    public IEnumerable<Product> GetProductsNotOwnedByUser(string ownerId)
    {
        return _productRepository.GetAll().Where(p => p.OwnerId != ownerId);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public Product GetProductById(string productId)
    {
        return _productRepository.GetById(productId);
    }

    public void AddProduct(Product product)
    {
        _productRepository.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        _productRepository.Update(product);
    }

    public void DeleteProduct(string productId)
    {
        _productRepository.Delete(productId);
    }
}