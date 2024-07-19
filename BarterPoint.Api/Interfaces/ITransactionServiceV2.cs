public interface ITransactionServiceV2
{
    Task<List<GetAllTransactionsResult>> GetAllTransactionsAsync();
    public Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId);
}