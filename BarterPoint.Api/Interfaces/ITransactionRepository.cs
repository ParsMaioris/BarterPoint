public interface ITransactionRepository
{
    Task<List<GetAllTransactionsResult>> GetAllTransactionHistoryAsync();
    Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId);
}