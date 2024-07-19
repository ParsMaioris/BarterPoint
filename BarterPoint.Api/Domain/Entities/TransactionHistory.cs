namespace BarterPoint.Domain;

public class Transaction
{
    public int Id { get; private set; }
    public string ProductId { get; private set; }
    public string BuyerId { get; private set; }
    public string SellerId { get; private set; }
    public DateTime DateCompleted { get; private set; }

    public Transaction(int id, string productId, string buyerId, string sellerId, DateTime dateCompleted)
    {
        Id = id;
        ProductId = productId;
        BuyerId = buyerId;
        SellerId = sellerId;
        DateCompleted = dateCompleted;
    }
}