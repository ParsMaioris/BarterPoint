public class TransactionRepositoryV2 : BaseRepository, ITransactionRepositoryV2
{
    public TransactionRepositoryV2(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task<List<GetAllTransactionsResult>> GetAllTransactionHistoryAsync()
    {
        var transactionHistories = new List<GetAllTransactionsResult>();

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetAllTransactionHistory"))
            {
                using (var reader = await command.ExecuteReaderAsync())
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

        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetUserTransactions"))
            {
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