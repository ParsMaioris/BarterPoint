namespace BarterPoint.Domain;

public class TransactionDomainService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionDomainService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> GetTransactionsByUserId(string userId)
    {
        var allTransactions = _transactionRepository.GetAll();
        return allTransactions.Where(t => t.BuyerId == userId || t.SellerId == userId);
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        return _transactionRepository.GetAll();
    }
}