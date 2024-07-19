public interface IBidServiceV2
{
    Task<IEnumerable<BidResultV2>> GetBidsWithPendingStatusesAsync();
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task UpdateBidStatusToRejectedAsync(int bidId);
    Task ApproveBidAsync(int bidId);
}