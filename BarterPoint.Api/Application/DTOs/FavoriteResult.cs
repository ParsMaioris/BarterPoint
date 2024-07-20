namespace BarterPoint.Application;

public class FavoriteResult
{
    public int FavoritesId { get; set; }

    public string UserId { get; set; }

    public string ProductId { get; set; }

    public DateTime DateAdded { get; set; }

    public string ProductName { get; set; }
}