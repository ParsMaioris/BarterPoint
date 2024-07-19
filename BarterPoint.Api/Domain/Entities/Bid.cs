namespace BarterPoint.Domain;

public class Bid
{
    public int Id { get; private set; }
    public string Product1Id { get; private set; }
    public string Product2Id { get; private set; }

    public Bid(int id, string product1Id, string product2Id)
    {
        Id = id;
        Product1Id = product1Id;
        Product2Id = product2Id;
    }
}