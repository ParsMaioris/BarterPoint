public interface IBidRepository
{
    Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task<IEnumerable<BidStatusResult>> GetAllBidStatusesAsync();
    Task<IEnumerable<BidResult>> GetAllBidsAsync();
    Task RemoveBidAsync(int bidId);
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task ApproveBidAsync(int bidId);
}