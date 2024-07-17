using System.Data;
using System.Data.SqlClient;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<ProductResult>> GetProductsByOwner(string ownerId)
    {
        var productList = new List<ProductResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("GetProductsByOwner", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        productList.Add(reader.MapTo<ProductResult>());
                    }
                }
            }
        }

        return productList;
    }

    public async Task<IEnumerable<ProductResult>> GetProductsNotOwnedByUser(string ownerId)
    {
        var productList = new List<ProductResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("GetProductsNotOwnedByUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        productList.Add(reader.MapTo<ProductResult>());
                    }
                }
            }
        }

        return productList;
    }

    public async Task<string> AddProductAsync(AddProductRequest product)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("AddProduct", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@image", product.Image);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@tradeFor", product.TradeFor);
                command.Parameters.AddWithValue("@categoryId", product.Category);
                command.Parameters.AddWithValue("@condition", product.Condition);
                command.Parameters.AddWithValue("@location", product.Location);
                command.Parameters.AddWithValue("@ownerId", product.OwnerId);
                command.Parameters.AddWithValue("@dimensions_width", product.DimensionsWidth);
                command.Parameters.AddWithValue("@dimensions_height", product.DimensionsHeight);
                command.Parameters.AddWithValue("@dimensions_depth", product.DimensionsDepth);
                command.Parameters.AddWithValue("@dimensions_weight", product.DimensionsWeight);
                command.Parameters.AddWithValue("@dateListed", product.DateListed);

                connection.Open();
                var result = await command.ExecuteScalarAsync();
                return result.ToString();
            }
        }
    }

    public async Task RemoveProductAsync(string productId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("RemoveProduct", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", productId);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}