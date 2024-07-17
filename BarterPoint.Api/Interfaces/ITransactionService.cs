public interface ITransactionService
{
    Task<List<GetAllTransactionsResult>> GetAllTransactionsAsync();
    public Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId);
}