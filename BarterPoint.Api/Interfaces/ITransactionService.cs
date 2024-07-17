public interface ITransactionService
{
    Task<List<TransactionHistory>> GetAllTransactionsAsync();
    public Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId);
}