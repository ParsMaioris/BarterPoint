public interface IBidRepository
{
    Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task<IEnumerable<BidStatus>> GetAllBidStatusesAsync();
    Task<IEnumerable<BidDTO>> GetAllBidsAsync();
    Task RemoveBidAsync(int bidId);
    Task<int> AddBidAsync(string product1Id, string product2Id);
    void ApproveBid(int bidId);
}