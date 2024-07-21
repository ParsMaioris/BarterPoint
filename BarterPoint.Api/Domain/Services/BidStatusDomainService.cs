namespace BarterPoint.Domain;

public class BidStatusDomainService
{
    private readonly IBidStatusRepository _bidStatusRepository;

    public BidStatusDomainService(IBidStatusRepository bidStatusRepository)
    {
        _bidStatusRepository = bidStatusRepository;
    }

    public async Task<IEnumerable<BidStatus>> GetAllBidStatusesAsync()
    {
        return await _bidStatusRepository.GetAllAsync();
    }

    public async Task<BidStatus> GetBidStatusByIdAsync(int bidStatusId)
    {
        return await _bidStatusRepository.GetByIdAsync(bidStatusId);
    }

    public async Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        var bidStatus = new BidStatus(0, bidId, status, dateUpdated);
        await _bidStatusRepository.AddAsync(bidStatus);
    }

    public async Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        var bidStatus = (await _bidStatusRepository.GetAllAsync()).FirstOrDefault(bs => bs.BidId == bidId);
        if (bidStatus != null)
        {
            bidStatus = new BidStatus(bidStatus.Id, bidStatus.BidId, status, dateUpdated);
            await _bidStatusRepository.UpdateAsync(bidStatus);
        }
        else
        {
            throw new Exception("Bid status not found.");
        }
    }

    public async Task<IEnumerable<BidStatus>> GetBidStatusesAsync()
    {
        return await _bidStatusRepository.GetAllAsync();
    }
}