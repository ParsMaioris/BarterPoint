namespace BarterPoint.Domain;

public interface IBidRepository : IRepository<Bid, int, Bid>
{
    Task<int> AddAndReturnIdAsync(Bid bid);
}