using Moq;
using Xunit;

namespace BarterPoint.Domain.Tests;

public class ProductDomainServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductDomainService _productDomainService;

    public ProductDomainServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productDomainService = new ProductDomainService(_productRepositoryMock.Object);
    }

    [Fact]
    public void GetProductsByOwner_ShouldReturnOnlyProductsOwnedBySpecifiedOwner()
    {
        // Arrange
        var ownerId = "owner123";
        var products = CreateSampleProducts();
        _productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _productDomainService.GetProductsByOwner(ownerId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, product => Assert.Equal(ownerId, product.OwnerId));
    }

    [Fact]
    public void GetProductsNotOwnedByUser_ShouldReturnOnlyProductsNotOwnedBySpecifiedOwner()
    {
        // Arrange
        var ownerId = "owner123";
        var products = CreateSampleProducts();
        _productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _productDomainService.GetProductsNotOwnedByUser(ownerId);

        // Assert
        Assert.Single(result);
        Assert.All(result, product => Assert.NotEqual(ownerId, product.OwnerId));
    }

    [Fact]
    public void GetProductById_ShouldReturnProductWithSpecifiedId()
    {
        // Arrange
        var productId = "1";
        var product = CreateSampleProduct(productId);
        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(product);

        // Act
        var result = _productDomainService.GetProductById(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
    }

    private IEnumerable<Product> CreateSampleProducts()
    {
        return new List<Product>
            {
                new Product("1", "Product1", null, null, null, 1, "New", null, "owner123", 0, 0, 0, 0, DateTime.UtcNow),
                new Product("2", "Product2", null, null, null, 2, "Used", null, "anotherOwner", 0, 0, 0, 0, DateTime.UtcNow),
                new Product("3", "Product3", null, null, null, 3, "Refurbished", null, "owner123", 0, 0, 0, 0, DateTime.UtcNow)
            };
    }

    private Product CreateSampleProduct(string productId)
    {
        return new Product(productId, "Product1", null, null, null, 1, "New", null, "owner123", 0, 0, 0, 0, DateTime.UtcNow);
    }
}
