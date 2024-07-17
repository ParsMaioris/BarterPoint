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
                        transactionHistories.Add(reader.MapTo<GetAllTransactionsResult>());
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
                        transactions.Add(reader.MapTo<GetUserTransactionsResult>());
                    }
                }
            }
        }
        return transactions;
    }
}