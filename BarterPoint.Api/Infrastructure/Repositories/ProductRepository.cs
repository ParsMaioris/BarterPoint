using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    public ProductRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
    }

    public IEnumerable<Product> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllProducts"))
        {
            return ExecuteReaderAsync(command, MapProduct).Result;
        }
    }

    public Product GetById(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetProductById"))
        {
            command.Parameters.AddWithValue("@ProductId", id);
            var products = ExecuteReaderAsync(command, MapProduct).Result;
            return products.FirstOrDefault();
        }
    }

    public void Add(Product product)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddProduct"))
        {
            AddProductParameters(command, product);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(Product product)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateProduct"))
        {
            AddProductParameters(command, product);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(string id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteProduct"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
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
        command.Parameters.AddWithValue("@Condition", product.Condition);
        command.Parameters.AddWithValue("@Location", product.Location ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DimensionsWidth", product.DimensionsWidth);
        command.Parameters.AddWithValue("@DimensionsHeight", product.DimensionsHeight);
        command.Parameters.AddWithValue("@DimensionsDepth", product.DimensionsDepth);
        command.Parameters.AddWithValue("@DimensionsWeight", product.DimensionsWeight);
        command.Parameters.AddWithValue("@DateListed", product.DateListed);
    }

    private async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
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
