public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;

    public BidService(IBidRepository bidRepository)
    {
        _bidRepository = bidRepository;
    }

    public async Task<IEnumerable<BidResult>> GetBidsWithPendingStatusesAsync()
    {
        var allBids = await _bidRepository.GetAllBidsAsync();
        var allStatuses = await _bidRepository.GetAllBidStatusesAsync();

        var pendingStatuses = allStatuses
            .Where(s => s.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            .Select(s => s.BidId);

        var bidsWithPendingStatuses = allBids
            .Where(b => pendingStatuses.Contains(b.Id));

        return bidsWithPendingStatuses;
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        var bidId = await _bidRepository.AddBidAsync(product1Id, product2Id);
        await _bidRepository.AddBidStatusAsync(bidId, "Pending", DateTime.UtcNow);
        return bidId;
    }

    public async Task UpdateBidStatusToRejectedAsync(int bidId)
    {
        await _bidRepository.UpdateBidStatusAsync(bidId, "Rejected", DateTime.UtcNow);
    }

    public async Task ApproveBidAsync(int bidId)
    {
        await _bidRepository.ApproveBidAsync(bidId);
    }
}