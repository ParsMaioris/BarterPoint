public class FavoriteResult
{
    [DbColumn("id")]
    public int FavoritesId { get; set; }

    [DbColumn("userId")]
    public string UserId { get; set; }

    [DbColumn("productId")]
    public string ProductId { get; set; }

    [DbColumn("dateAdded")]
    public DateTime DateAdded { get; set; }

    [DbColumn("name")]
    public string ProductName { get; set; }
}