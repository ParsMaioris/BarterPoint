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

    public async Task<IEnumerable<BidStatus>> GetAllBidStatusesAsync()
    {
        var result = new List<BidStatus>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("GetAllBidStatuses", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var bidStatus = reader.MapTo<BidStatus>();
                        result.Add(bidStatus);
                    }
                }
            }
        }

        return result;
    }

    public async Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("AddBidStatus", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("UpdateBidStatus", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<TransactionHistory>> GetAllTransactionHistoryAsync()
    {
        var transactionHistories = new List<TransactionHistory>();

        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetAllTransactionHistory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        transactionHistories.Add(new TransactionHistory
                        {
                            Id = reader.GetInt32(0),
                            ProductId = reader.GetString(1),
                            BuyerId = reader.GetString(2),
                            SellerId = reader.GetString(3),
                            DateCompleted = reader.GetDateTime(4)
                        });
                    }
                }
            }
        }

        return transactionHistories;
    }

    public async Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId)
    {
        var transactions = new List<UserTransactionDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("GetUserTransactions", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new UserTransactionDto
                        {
                            TransactionId = reader.GetInt32(0),
                            ProductId = reader.GetString(1),
                            ProductName = reader.GetString(2),
                            BuyerId = reader.GetString(3),
                            BuyerUsername = reader.GetString(4),
                            SellerId = reader.GetString(5),
                            SellerUsername = reader.GetString(6),
                            DateCompleted = reader.GetDateTime(7)
                        });
                    }
                }
            }
        }
        return transactions;
    }

    public async Task RateUserAsync(RateUserRequest rating)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("RateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RaterId", rating.RaterId);
                command.Parameters.AddWithValue("@RateeId", rating.RateeId);
                command.Parameters.AddWithValue("@Rating", rating.Rating);
                command.Parameters.AddWithValue("@Review", rating.Review);
                command.Parameters.AddWithValue("@DateRated", rating.DateRated);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task InsertTransactionAsync(TransactionHistory transaction)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("InsertTransaction", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", transaction.ProductId);
                command.Parameters.AddWithValue("@BuyerId", transaction.BuyerId);
                command.Parameters.AddWithValue("@SellerId", transaction.SellerId);
                command.Parameters.AddWithValue("@DateCompleted", transaction.DateCompleted);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}