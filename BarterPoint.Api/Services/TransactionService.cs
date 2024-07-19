public class TransactionService : ITransactionService
{
    private readonly ITransactionRepositoryV2 _transactionRepository;

    public TransactionService(ITransactionRepositoryV2 transactionRepository)
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