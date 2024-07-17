public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public Task<List<TransactionHistory>> GetAllTransactionsAsync()
    {
        return _transactionRepository.GetAllTransactionHistoryAsync();
    }

    public Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId)
    {
        return _transactionRepository.GetUserTransactionsAsync(userId);
    }
}