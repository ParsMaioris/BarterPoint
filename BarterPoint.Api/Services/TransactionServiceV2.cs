public class TransactionServiceV2 : ITransactionServiceV2
{
    private readonly ITransactionRepositoryV2 _transactionRepository;

    public TransactionServiceV2(ITransactionRepositoryV2 transactionRepository)
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