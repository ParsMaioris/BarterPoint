using BarterPoint.Application;

namespace BarterPoint.Domain;

public class TransactionDomainService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionDomainService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(string userId)
    {
        var allTransactions = await _transactionRepository.GetAllAsync();
        return allTransactions.Where(t => t.BuyerId == userId || t.SellerId == userId);
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }

    public async Task AddTransactionAsync(AddTransactionRequest transactionRequest)
    {
        await _transactionRepository.AddAsync(transactionRequest);
    }
}