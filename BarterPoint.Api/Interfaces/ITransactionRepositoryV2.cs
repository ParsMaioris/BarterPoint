public interface ITransactionRepositoryV2
{
    Task<List<GetAllTransactionsResult>> GetAllTransactionHistoryAsync();
    Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId);
}