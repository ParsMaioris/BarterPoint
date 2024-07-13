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
}