using System.Data;
using System.Data.SqlClient;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<GetAllTransactionsResult>> GetAllTransactionHistoryAsync()
    {
        var transactionHistories = new List<GetAllTransactionsResult>();

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
                        transactionHistories.Add(new GetAllTransactionsResult
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

    public async Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId)
    {
        var transactions = new List<GetUserTransactionsResult>();
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
                        transactions.Add(new GetUserTransactionsResult
                        {
                            TransactionId = reader.GetInt32(0),
                            ProductId = reader.GetString(1),
                            ProductName = reader.GetString(2),
                            ProductImage = reader.GetString(3),
                            ProductDescription = reader.GetString(4),
                            BuyerId = reader.GetString(5),
                            BuyerUsername = reader.GetString(6),
                            SellerId = reader.GetString(7),
                            SellerUsername = reader.GetString(8),
                            DateCompleted = reader.GetDateTime(9)
                        });
                    }
                }
            }
        }
        return transactions;
    }
}