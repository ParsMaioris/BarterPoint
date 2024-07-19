namespace BarterPoint.Application;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
    Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId);
}