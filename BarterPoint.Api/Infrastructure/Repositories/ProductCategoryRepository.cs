using System.Data;
using BarterPoint.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BarterPoint.Infrastructure;

public class ProductCategoryRepository : BaseRepository, IProductCategoryRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheSettings _cacheSettings;
    private const string CategoryCacheKey = "ProductCategories";

    public ProductCategoryRepository(DbConnectionFactoryDelegate dbConnectionFactory, IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
        : base(dbConnectionFactory)
    {
        _memoryCache = memoryCache;
        _cacheSettings = cacheSettings.Value;
    }

    public IEnumerable<ProductCategory> GetAll()
    {
        if (!_memoryCache.TryGetValue(CategoryCacheKey, out List<ProductCategory> categories))
        {
            using (var connection = OpenConnection())
            using (var command = CreateCommand(connection, "GetAllProductCategories"))
            {
                categories = ExecuteReaderAsync(command, MapProductCategory).Result;
                _memoryCache.Set(CategoryCacheKey, categories, TimeSpan.FromMinutes(_cacheSettings.ProductCategoryCacheDurationMinutes));
            }
        }
        return categories;
    }

    public string GetCategoryNameById(int id)
    {
        var categories = GetAll();
        var category = categories.FirstOrDefault(c => c.Id == id);
        return category?.Category;
    }

    private ProductCategory MapProductCategory(IDataRecord record)
    {
        return new ProductCategory
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            category: record.GetString(record.GetOrdinal("category"))
        );
    }
}