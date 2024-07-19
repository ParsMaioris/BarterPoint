using BarterPoint.Application;

public interface IBidService
{
    Task<IEnumerable<BidResult>> GetBidsWithPendingStatusesAsync();
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task ApproveBidAsync(int bidId);
    Task UpdateBidStatusToRejectedAsync(int bidId);
}