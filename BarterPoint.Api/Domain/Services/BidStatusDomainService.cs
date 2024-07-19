namespace BarterPoint.Domain;

public class BidStatusDomainService
{
    private readonly IBidStatusRepository _bidStatusRepository;

    public BidStatusDomainService(IBidStatusRepository bidStatusRepository)
    {
        _bidStatusRepository = bidStatusRepository;
    }

    public IEnumerable<BidStatus> GetAllBidStatuses()
    {
        return _bidStatusRepository.GetAll();
    }

    public BidStatus GetBidStatusById(int bidStatusId)
    {
        return _bidStatusRepository.GetById(bidStatusId);
    }

    public void AddBidStatus(int bidId, string status, DateTime dateUpdated)
    {
        var bidStatus = new BidStatus(0, bidId, status, dateUpdated);
        _bidStatusRepository.Add(bidStatus);
    }

    public void UpdateBidStatus(int bidId, string status, DateTime dateUpdated)
    {
        var bidStatus = _bidStatusRepository.GetAll().FirstOrDefault(bs => bs.BidId == bidId);
        if (bidStatus != null)
        {
            bidStatus = new BidStatus(bidStatus.Id, bidStatus.BidId, status, dateUpdated);
            _bidStatusRepository.Update(bidStatus);
        }
        else
        {
            throw new Exception("Bid status not found.");
        }
    }

    public IEnumerable<BidStatus> GetBidStatuses()
    {
        return _bidStatusRepository.GetAll();
    }
}