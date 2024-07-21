namespace BarterPoint.Domain;

public class BidDomainService
{
    private readonly IBidRepository _bidRepository;
    private readonly IBidStatusRepository _bidStatusRepository;

    public BidDomainService(IBidRepository bidRepository, IBidStatusRepository bidStatusRepository)
    {
        _bidRepository = bidRepository;
        _bidStatusRepository = bidStatusRepository;
    }

    public async Task<IEnumerable<Bid>> GetBidsWithPendingStatusesAsync()
    {
        var bids = await _bidRepository.GetAllAsync();
        var bidStatuses = await _bidStatusRepository.GetAllAsync();

        return bids.Where(bid => bidStatuses.Any(status => status.BidId == bid.Id && status.Status == "Pending"));
    }

    public async Task<int> AddBidAndReturnIdAsync(string product1Id, string product2Id)
    {
        var bid = new Bid(0, product1Id, product2Id);
        return await _bidRepository.AddAndReturnIdAsync(bid);
    }

    public async Task<Bid> GetBidByIdAsync(int bidId)
    {
        return await _bidRepository.GetByIdAsync(bidId);
    }

    public async Task RejectOtherBidsAsync(string product1Id, string product2Id, int approvedBidId)
    {
        var bids = await _bidRepository.GetAllAsync();
        foreach (var bid in bids)
        {
            if ((bid.Product1Id == product1Id || bid.Product2Id == product2Id || bid.Product1Id == product2Id || bid.Product2Id == product1Id) && bid.Id != approvedBidId)
            {
                await _bidStatusRepository.UpdateAsync(new BidStatus(0, bid.Id, "Rejected", DateTime.UtcNow));
            }
        }
    }

    public async Task UpdateBidStatusToRejectedAsync(int bidStatusId)
    {
        await _bidStatusRepository.UpdateAsync(new BidStatus(bidStatusId, 0, "Rejected", DateTime.UtcNow));
    }
}