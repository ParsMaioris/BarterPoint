namespace BarterPoint.Application;

public class FavoriteResult
{
    [DbField("id")]
    public int FavoritesId { get; set; }

    [DbField("userId")]
    public string UserId { get; set; }

    [DbField("productId")]
    public string ProductId { get; set; }

    [DbField("dateAdded")]
    public DateTime DateAdded { get; set; }

    [DbField("name")]
    public string ProductName { get; set; }
}