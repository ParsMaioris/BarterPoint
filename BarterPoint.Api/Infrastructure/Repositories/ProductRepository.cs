using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllProducts"))
        {
            return await ExecuteReaderAsync(command, MapProduct);
        }
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetProductById"))
        {
            command.Parameters.AddWithValue("@ProductId", id);
            var products = await ExecuteReaderAsync(command, MapProduct);
            return products.FirstOrDefault();
        }
    }

    public async Task AddAsync(Product product)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddProduct"))
        {
            AddProductParameters(command, product);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task UpdateAsync(Product product)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateProduct"))
        {
            AddProductParameters(command, product);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task DeleteAsync(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteProduct"))
        {
            command.Parameters.AddWithValue("@Id", id);
            await ExecuteNonQueryAsync(command);
        }
    }

    private void AddProductParameters(SqlCommand command, Product product)
    {
        command.Parameters.AddWithValue("@Id", product.Id);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Image", product.Image ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@TradeFor", product.TradeFor ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
        command.Parameters.AddWithValue("@OwnerId", product.OwnerId);
        command.Parameters.AddWithValue("@Condition", product.Condition);
        command.Parameters.AddWithValue("@Location", product.Location ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DimensionsWidth", product.DimensionsWidth);
        command.Parameters.AddWithValue("@DimensionsHeight", product.DimensionsHeight);
        command.Parameters.AddWithValue("@DimensionsDepth", product.DimensionsDepth);
        command.Parameters.AddWithValue("@DimensionsWeight", product.DimensionsWeight);
        command.Parameters.AddWithValue("@DateListed", product.DateListed);
    }

    private Product MapProduct(IDataRecord record)
    {
        return new Product(
            id: record.GetString(record.GetOrdinal("id")),
            name: record.GetString(record.GetOrdinal("name")),
            image: record.IsDBNull(record.GetOrdinal("image")) ? null : record.GetString(record.GetOrdinal("image")),
            description: record.IsDBNull(record.GetOrdinal("description")) ? null : record.GetString(record.GetOrdinal("description")),
            tradeFor: record.IsDBNull(record.GetOrdinal("tradeFor")) ? null : record.GetString(record.GetOrdinal("tradeFor")),
            categoryId: record.GetInt32(record.GetOrdinal("categoryId")),
            condition: record.GetString(record.GetOrdinal("condition")),
            location: record.IsDBNull(record.GetOrdinal("location")) ? null : record.GetString(record.GetOrdinal("location")),
            ownerId: record.GetString(record.GetOrdinal("ownerId")),
            dimensionsWidth: (float)record.GetDouble(record.GetOrdinal("dimensions_width")),
            dimensionsHeight: (float)record.GetDouble(record.GetOrdinal("dimensions_height")),
            dimensionsDepth: (float)record.GetDouble(record.GetOrdinal("dimensions_depth")),
            dimensionsWeight: (float)record.GetDouble(record.GetOrdinal("dimensions_weight")),
            dateListed: record.GetDateTime(record.GetOrdinal("dateListed"))
        );
    }
}