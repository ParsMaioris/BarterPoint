public interface IDatabaseService
{
    Task<IEnumerable<ProductDTO>> GetProductsByOwner(string ownerId);
    Task<IEnumerable<ProductDTO>> GetProductsNotOwnedByUser(string ownerId);
    Task<IEnumerable<BidDTO>> GetAllBidsAsync();
    Task RemoveBidAsync(int bidId);
    Task<int> AddBidAsync(string product1Id, string product2Id);
    Task<string> AddProductAsync(AddProductRequest product);
    Task RemoveProductAsync(string productId);
    Task<string> RegisterUserAsync(RegisterUserRequest request);
    Task<SignInResult> SignInUserAsync(SignInRequest request);
    Task<IEnumerable<BidStatus>> GetAllBidStatusesAsync();
    Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated);
}