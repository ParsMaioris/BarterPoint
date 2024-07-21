using BarterPoint.Domain;

namespace BarterPoint.Application;

public class TransactionService : ITransactionService
{
    private readonly TransactionDomainService _transactionDomainService;
    private readonly ProductDomainService _productDomainService;
    private readonly UserDomainService _userDomainService;

    public TransactionService(TransactionDomainService transactionDomainService, ProductDomainService productDomainService, UserDomainService userDomainService)
    {
        _transactionDomainService = transactionDomainService;
        _productDomainService = productDomainService;
        _userDomainService = userDomainService;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        var transactions = await _transactionDomainService.GetAllTransactionsAsync();
        return transactions.Select(MapToTransactionDto);
    }

    public async Task<IEnumerable<UserTransactionDto>> GetUserTransactionsAsync(string userId)
    {
        var transactions = await _transactionDomainService.GetTransactionsByUserIdAsync(userId);
        var userTransactions = new List<UserTransactionDto>();

        foreach (var transaction in transactions)
        {
            var product = await _productDomainService.GetProductByIdAsync(transaction.ProductId);
            var buyer = await _userDomainService.GetUserByIdAsync(transaction.BuyerId);
            var seller = await _userDomainService.GetUserByIdAsync(transaction.SellerId);

            userTransactions.Add(new UserTransactionDto
            {
                TransactionId = transaction.Id,
                ProductId = transaction.ProductId,
                ProductName = product.Name,
                ProductImage = product.Image,
                ProductDescription = product.Description,
                BuyerId = transaction.BuyerId,
                BuyerUsername = buyer.Username,
                SellerId = transaction.SellerId,
                SellerUsername = seller.Username,
                DateCompleted = transaction.DateCompleted
            });
        }

        return userTransactions;
    }

    private TransactionDto MapToTransactionDto(Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            ProductId = transaction.ProductId,
            BuyerId = transaction.BuyerId,
            SellerId = transaction.SellerId,
            DateCompleted = transaction.DateCompleted
        };
    }
}