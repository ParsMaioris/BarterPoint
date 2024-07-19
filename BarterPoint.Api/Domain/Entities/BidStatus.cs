namespace BarterPoint.Domain;

public class BidStatus
{
    public int Id { get; private set; }
    public int BidId { get; private set; }
    public string Status { get; private set; }
    public DateTime DateUpdated { get; private set; }

    public BidStatus(int id, int bidId, string status, DateTime dateUpdated)
    {
        Id = id;
        BidId = bidId;
        Status = status;
        DateUpdated = dateUpdated;
    }
}