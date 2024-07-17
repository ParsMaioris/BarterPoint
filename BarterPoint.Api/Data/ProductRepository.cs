public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<ProductResult>> GetProductsByOwner(string ownerId)
    {
        var productList = new List<ProductResult>();

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetProductsByOwner"))
            {
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetProductsNotOwnedByUser"))
            {
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "AddProduct"))
            {
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

                var result = await command.ExecuteScalarAsync();
                return result.ToString();
            }
        }
    }

    public async Task RemoveProductAsync(string productId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "RemoveProduct"))
            {
                command.Parameters.AddWithValue("@id", productId);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}