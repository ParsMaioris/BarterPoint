using BarterPoint.Domain;
using Moq;
using Xunit;

namespace BarterPoint.Application.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<ITransactionRepository> _mockTransactionRepository;
    private readonly Mock<IProductCategoryRepository> _mockProductCategoryRepository;
    private readonly ProductDomainService _productDomainService;
    private readonly TransactionDomainService _transactionDomainService;
    private readonly ProductCategoryDomainService _productCategoryDomainService;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockTransactionRepository = new Mock<ITransactionRepository>();
        _mockProductCategoryRepository = new Mock<IProductCategoryRepository>();

        _productDomainService = new ProductDomainService(_mockProductRepository.Object);
        _transactionDomainService = new TransactionDomainService(_mockTransactionRepository.Object);
        _productCategoryDomainService = new ProductCategoryDomainService(_mockProductCategoryRepository.Object);

        _productService = new ProductService(
            _productDomainService,
            _transactionDomainService,
            _productCategoryDomainService);
    }

    [Fact]
    public async Task GetAvailableProductsByOwnerAsync_ShouldReturnOnlyAvailableProducts()
    {
        // Arrange
        var ownerId = "owner123";
        var products = CreateSampleProducts(ownerId);
        var transactions = CreateSampleTransactions();

        _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        _mockTransactionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);
        _mockProductCategoryRepository.Setup(repo => repo.GetCategoryNameByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => $"Category{id}");

        // Act
        var result = await _productService.GetAvailableProductsByOwnerAsync(ownerId);

        // Assert
        Assert.Single(result);
        Assert.All(result, p => Assert.NotEqual("soldProduct", p.Id));
        Assert.All(result, p => Assert.Equal("Product1", p.Name));
    }

    [Fact]
    public async Task GetAvailableProductsNotOwnedByUserAsync_ShouldReturnOnlyAvailableProductsNotOwnedByUser()
    {
        // Arrange
        var ownerId = "owner123";
        var products = CreateSampleProducts(ownerId);
        var transactions = CreateSampleTransactions();

        _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        _mockTransactionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);
        _mockProductCategoryRepository.Setup(repo => repo.GetCategoryNameByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => $"Category{id}");

        // Act
        var result = await _productService.GetAvailableProductsNotOwnedByUserAsync(ownerId);

        // Assert
        Assert.Single(result);
        Assert.All(result, p => Assert.NotEqual(ownerId, p.OwnerId));
    }

    private IEnumerable<Product> CreateSampleProducts(string ownerId)
    {
        return new List<Product>
            {
                new Product("availableProduct", "Product1", null, null, null, 1, "New", null, ownerId, 0, 0, 0, 0, DateTime.UtcNow),
                new Product("soldProduct", "Product2", null, null, null, 2, "Used", null, ownerId, 0, 0, 0, 0, DateTime.UtcNow),
                new Product("anotherProduct", "Product3", null, null, null, 3, "Refurbished", null, "anotherOwner", 0, 0, 0, 0, DateTime.UtcNow)
            };
    }

    private IEnumerable<Transaction> CreateSampleTransactions()
    {
        return new List<Transaction>
            {
                new Transaction(1, "soldProduct", "buyer123", "owner123", DateTime.UtcNow)
            };
    }
}