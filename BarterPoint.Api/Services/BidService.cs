public class BidService : IBidService
{
    private readonly IDatabaseService _databaseService;

    public BidService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<IEnumerable<BidDTO>> GetBidsWithPendingStatusesAsync()
    {
        var allBids = await _databaseService.GetAllBidsAsync();
        var allStatuses = await _databaseService.GetAllBidStatusesAsync();

        var pendingStatuses = allStatuses
            .Where(s => s.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            .Select(s => s.BidId);

        var bidsWithPendingStatuses = allBids
            .Where(b => pendingStatuses.Contains(b.Id));

        return bidsWithPendingStatuses;
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        var bidId = await _databaseService.AddBidAsync(product1Id, product2Id);
        await _databaseService.AddBidStatusAsync(bidId, "Pending", DateTime.UtcNow);
        return bidId;
    }

    public async Task UpdateBidStatusToRejectedAsync(int bidId)
    {
        await _databaseService.UpdateBidStatusAsync(bidId, "Rejected", DateTime.UtcNow);
    }
}