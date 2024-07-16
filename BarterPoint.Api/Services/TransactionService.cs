public class TransactionService : ITransactionService
{
    private readonly IDatabaseService _databaseService;

    public TransactionService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public Task<List<TransactionHistory>> GetAllTransactionsAsync()
    {
        return _databaseService.GetAllTransactionHistoryAsync();
    }

    public Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId)
    {
        return _databaseService.GetUserTransactionsAsync(userId);
    }

    public Task RateUserAsync(RateUserRequest rating)
    {
        return _databaseService.RateUserAsync(rating);
    }
}