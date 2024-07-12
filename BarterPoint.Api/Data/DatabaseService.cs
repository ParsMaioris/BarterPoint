using System.Data;
using System.Data.SqlClient;

public class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByOwner(string ownerId)
    {
        var productList = new List<ProductDTO>();

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
                        productList.Add(reader.MapTo<ProductDTO>());
                    }
                }
            }
        }

        return productList;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsNotOwnedByUser(string ownerId)
    {
        var productList = new List<ProductDTO>();

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
                        productList.Add(reader.MapTo<ProductDTO>());
                    }
                }
            }
        }

        return productList;
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("AddBid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product1Id", product1Id);
                command.Parameters.AddWithValue("@product2Id", product2Id);

                connection.Open();
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }

    public async Task RemoveBidAsync(int bidId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("RemoveBid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@bidId", bidId);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<IEnumerable<BidDTO>> GetAllBidsAsync()
    {
        var bidList = new List<BidDTO>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("GetAllBids", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        bidList.Add(reader.MapTo<BidDTO>());
                    }
                }
            }
        }

        return bidList;
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

    public async Task<string> RegisterUserAsync(RegisterUserRequest request)
    {
        var userId = Guid.NewGuid();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("RegisterUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", userId);
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);
                command.Parameters.AddWithValue("@email", request.Email);
                command.Parameters.AddWithValue("@name", request.Name);
                command.Parameters.AddWithValue("@location", request.Location);
                command.Parameters.AddWithValue("@dateJoined", request.DateJoined);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        if (reader.FieldCount > 0)
                        {
                            var result = reader.GetName(0);
                            return reader[result].ToString();
                        }
                    }
                }
            }
        }

        return "Unknown error occurred during registration.";
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequest request)
    {
        var result = new SignInResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SignInUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", request.Username);
                command.Parameters.AddWithValue("@password_hash", request.PasswordHash);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        if (reader.FieldCount > 0)
                        {
                            var firstColumnName = reader.GetName(0);

                            if (firstColumnName == "Message")
                            {
                                result.Message = reader["Message"].ToString();
                                if (reader["UserId"] != DBNull.Value)
                                {
                                    result.UserId = reader["UserId"].ToString();
                                }
                            }
                            else if (firstColumnName == "ErrorMessage")
                            {
                                result.ErrorMessage = reader["ErrorMessage"].ToString();
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
}