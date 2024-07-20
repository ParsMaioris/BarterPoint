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

    public IEnumerable<Bid> GetBidsWithPendingStatuses()
    {
        var bids = _bidRepository.GetAll();
        var bidStatuses = _bidStatusRepository.GetAll();

        return bids.Where(bid => bidStatuses.Any(status => status.BidId == bid.Id && status.Status == "Pending"));
    }

    public int AddBidandReturnId(string product1Id, string product2Id)
    {
        var bid = new Bid(0, product1Id, product2Id);
        return _bidRepository.AddAndReturnId(bid);
    }

    public Bid GetBidById(int bidId)
    {
        return _bidRepository.GetById(bidId);
    }

    public void RejectOtherBids(string product1Id, string product2Id, int approvedBidId)
    {
        var bids = _bidRepository.GetAll();
        foreach (var bid in bids)
        {
            if ((bid.Product1Id == product1Id || bid.Product2Id == product2Id || bid.Product1Id == product2Id || bid.Product2Id == product1Id) && bid.Id != approvedBidId)
            {
                _bidStatusRepository.Update(new BidStatus(0, bid.Id, "Rejected", DateTime.UtcNow));
            }
        }
    }

    public void UpdateBidStatusToRejected(int bidStatusId)
    {
        _bidStatusRepository.Update(new BidStatus(bidStatusId, 0, "Rejected", DateTime.UtcNow));
    }
}