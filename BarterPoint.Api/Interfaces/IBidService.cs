public interface IBidService
{
    Task<IEnumerable<BidDTO>> GetBidsWithPendingStatusesAsync();
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task UpdateBidStatusToRejectedAsync(int bidId);
}