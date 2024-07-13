public interface IBidService
{
    Task<IEnumerable<BidDTO>> GetBidsWithPendingStatusesAsync();
}