using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Application;

public class BidService : IBidService
{
    private readonly BidDomainService _bidDomainService;
    private readonly ProductDomainService _productDomainService;
    private readonly TransactionDomainService _transactionDomainService;
    private readonly BidStatusDomainService _bidStatusDomainService;
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    public BidService(
        BidDomainService bidDomainService,
        ProductDomainService productDomainService,
        TransactionDomainService transactionDomainService,
        BidStatusDomainService bidStatusDomainService,
        DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _bidDomainService = bidDomainService;
        _productDomainService = productDomainService;
        _transactionDomainService = transactionDomainService;
        _bidStatusDomainService = bidStatusDomainService;
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IEnumerable<BidResult>> GetBidsWithPendingStatusesAsync()
    {
        var bids = await _bidDomainService.GetBidsWithPendingStatusesAsync();
        var bidResults = await Task.WhenAll(bids.Select(MapToBidResultAsync));
        return bidResults;
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        var bidId = await _bidDomainService.AddBidAndReturnIdAsync(product1Id, product2Id);
        await _bidStatusDomainService.AddBidStatusAsync(bidId, "Pending", DateTime.UtcNow);
        return bidId;
    }

    public async Task ApproveBidAsync(int bidId)
    {
        using (var connection = (SqlConnection)_dbConnectionFactory())
        {
            await connection.OpenAsync();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var bid = await _bidDomainService.GetBidByIdAsync(bidId);
                    if (bid == null)
                    {
                        throw new Exception("Invalid bid ID");
                    }

                    var product1 = await _productDomainService.GetProductByIdAsync(bid.Product1Id);
                    var product2 = await _productDomainService.GetProductByIdAsync(bid.Product2Id);

                    if (product1 == null || product2 == null)
                    {
                        throw new Exception("Invalid product ID in bid");
                    }

                    await _bidStatusDomainService.UpdateBidStatusAsync(bid.Id, "Approved", DateTime.UtcNow);
                    await _bidDomainService.RejectOtherBidsAsync(bid.Product1Id, bid.Product2Id, bidId);

                    var addTransactionRequest1 = CreateAddTransactionRequest(bid.Product1Id, product2.OwnerId, product1.OwnerId);
                    var addTransactionRequest2 = CreateAddTransactionRequest(bid.Product2Id, product1.OwnerId, product2.OwnerId);

                    await _transactionDomainService.AddTransactionAsync(addTransactionRequest1);
                    await _transactionDomainService.AddTransactionAsync(addTransactionRequest2);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    private AddTransactionRequest CreateAddTransactionRequest(string productId, string buyerId, string sellerId)
    {
        return new AddTransactionRequest
        {
            ProductId = productId,
            BuyerId = buyerId,
            SellerId = sellerId,
            DateCompleted = DateTime.UtcNow
        };
    }

    public async Task UpdateBidStatusToRejectedAsync(int bidId)
    {
        var bidStatusId = (await _bidStatusDomainService.GetAllBidStatusesAsync())
            .FirstOrDefault(bs => bs.BidId == bidId)?.Id;

        if (bidStatusId.HasValue)
        {
            await _bidDomainService.UpdateBidStatusToRejectedAsync(bidStatusId.Value);
        }
        else
        {
            throw new InvalidOperationException($"Bid status for bidId {bidId} not found.");
        }
    }

    private async Task<BidResult> MapToBidResultAsync(Bid bid)
    {
        var product1 = await _productDomainService.GetProductByIdAsync(bid.Product1Id);
        var product2 = await _productDomainService.GetProductByIdAsync(bid.Product2Id);

        return new BidResult
        {
            Id = bid.Id,
            Product1Id = bid.Product1Id,
            Product2Id = bid.Product2Id,
            Product1Name = product1?.Name,
            Product2Name = product2?.Name
        };
    }
}