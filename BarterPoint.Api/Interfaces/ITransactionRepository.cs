public interface ITransactionRepository
{
    Task<List<TransactionHistory>> GetAllTransactionHistoryAsync();
    Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId);
}