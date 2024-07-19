namespace BarterPoint.Domain;

public interface IBidRepository : IRepository<Bid, int, Bid>
{
    void ApproveBid(int bidId);
}