public interface IBidService
{
    Task<IEnumerable<BidResult>> GetBidsWithPendingStatusesAsync();
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task UpdateBidStatusToRejectedAsync(int bidId);
    void ApproveBid(int bidId);
}