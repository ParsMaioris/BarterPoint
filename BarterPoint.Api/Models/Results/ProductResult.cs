public class ProductResult
{
    [DbColumn("id")]
    public string Id { get; set; }

    [DbColumn("name")]
    public string Name { get; set; }

    [DbColumn("image")]
    public string Image { get; set; }

    [DbColumn("description")]
    public string Description { get; set; }

    [DbColumn("tradeFor")]
    public string TradeFor { get; set; }

    [DbColumn("category")]
    public string Category { get; set; }

    [DbColumn("condition")]
    public string Condition { get; set; }

    [DbColumn("location")]
    public string Location { get; set; }

    [DbColumn("ownerId")]
    public string OwnerId { get; set; }

    [DbColumn("dimensions_width")]
    public float DimensionsWidth { get; set; }

    [DbColumn("dimensions_height")]
    public float DimensionsHeight { get; set; }

    [DbColumn("dimensions_depth")]
    public float DimensionsDepth { get; set; }

    [DbColumn("dimensions_weight")]
    public float DimensionsWeight { get; set; }

    [DbColumn("dateListed")]
    public DateTime DateListed { get; set; }
}