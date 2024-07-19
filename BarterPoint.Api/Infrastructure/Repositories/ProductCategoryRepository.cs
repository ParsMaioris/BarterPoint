using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BarterPoint.Infrastructure;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly CacheSettings _cacheSettings;
    private const string CategoryCacheKey = "ProductCategories";

    public ProductCategoryRepository(DbConnectionFactoryDelegate dbConnectionFactory, IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _memoryCache = memoryCache;
        _cacheSettings = cacheSettings.Value;
    }

    private SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
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

    private async Task<List<T>> ExecuteReaderAsync<T>(SqlCommand command, Func<IDataReader, T> map)
    {
        var results = new List<T>();
        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }
        }
        return results;
    }

    private SqlCommand CreateCommand(SqlConnection connection, string storedProcedure)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedure;
        return command;
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