
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
        var bids = await Task.Run(() => _bidDomainService.GetBidsWithPendingStatuses());
        return bids.Select(MapToBidResult);
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        var bidId = await Task.Run(() => _bidDomainService.AddBidandReturnId(product1Id, product2Id));
        _bidStatusDomainService.AddBidStatus(bidId, "Pending", DateTime.UtcNow);
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
                    var bid = _bidDomainService.GetBidById(bidId);
                    if (bid == null)
                    {
                        throw new Exception("Invalid bid ID");
                    }

                    var product1 = _productDomainService.GetProductById(bid.Product1Id);
                    var product2 = _productDomainService.GetProductById(bid.Product2Id);

                    if (product1 == null || product2 == null)
                    {
                        throw new Exception("Invalid product ID in bid");
                    }

                    _bidStatusDomainService.UpdateBidStatus(bid.Id, "Approved", DateTime.UtcNow);
                    _bidDomainService.RejectOtherBids(bid.Product1Id, bid.Product2Id, bidId);

                    var addTransactionRequest1 = new AddTransactionRequest
                    {
                        ProductId = bid.Product1Id,
                        BuyerId = product2.OwnerId,
                        SellerId = product1.OwnerId,
                        DateCompleted = DateTime.UtcNow
                    };

                    _transactionDomainService.AddTransaction(addTransactionRequest1);

                    var addTransactionRequest2 = new AddTransactionRequest
                    {
                        ProductId = bid.Product2Id,
                        BuyerId = product1.OwnerId,
                        SellerId = product2.OwnerId,
                        DateCompleted = DateTime.UtcNow
                    };

                    _transactionDomainService.AddTransaction(addTransactionRequest2);

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

    public async Task UpdateBidStatusToRejectedAsync(int bidId)
    {
        var bidStatusId = _bidStatusDomainService.GetAllBidStatuses().
            FirstOrDefault(bs => bs.BidId == bidId)?.Id;

        if (bidStatusId.HasValue)
        {
            await Task.Run(() => _bidDomainService.UpdateBidStatusToRejected(bidStatusId.Value));
        }
        else
        {
            throw new InvalidOperationException($"Bid status for bidId {bidId} not found.");
        }
    }

    private BidResult MapToBidResult(Bid bid)
    {
        var product1 = _productDomainService.GetProductById(bid.Product1Id);
        var product2 = _productDomainService.GetProductById(bid.Product2Id);

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