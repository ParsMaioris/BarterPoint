public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public Task<List<GetAllTransactionsResult>> GetAllTransactionsAsync()
    {
        return _transactionRepository.GetAllTransactionHistoryAsync();
    }

    public Task<IEnumerable<GetUserTransactionsResult>> GetUserTransactionsAsync(string userId)
    {
        return _transactionRepository.GetUserTransactionsAsync(userId);
    }
}