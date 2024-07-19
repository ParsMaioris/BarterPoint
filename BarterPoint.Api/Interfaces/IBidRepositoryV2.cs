public interface IBidRepositoryV2
{
    Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated);
    Task<IEnumerable<BidStatusResultV2>> GetAllBidStatusesAsync();
    Task<IEnumerable<BidResultV2>> GetAllBidsAsync();
    Task RemoveBidAsync(int bidId);
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task ApproveBidAsync(int bidId);
}