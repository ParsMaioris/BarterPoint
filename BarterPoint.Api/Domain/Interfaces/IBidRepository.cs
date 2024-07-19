namespace BarterPoint.Domain;

public interface IBidRepository : IRepository<Bid, int, Bid>
{
    int AddAndReturnId(Bid bid);
}