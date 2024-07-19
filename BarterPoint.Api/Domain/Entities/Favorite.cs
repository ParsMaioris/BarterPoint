namespace BarterPoint.Domain;

public class Favorite
{
    public int Id { get; private set; }
    public string UserId { get; private set; }
    public string ProductId { get; private set; }
    public DateTime DateAdded { get; private set; }

    public Favorite(int id, string userId, string productId, DateTime dateAdded)
    {
        Id = id;
        UserId = userId;
        ProductId = productId;
        DateAdded = dateAdded;
    }
}