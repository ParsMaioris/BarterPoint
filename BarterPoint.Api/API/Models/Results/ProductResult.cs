public class ProductResultV2
{
    [DbField("id")]
    public string Id { get; set; }

    [DbField("name")]
    public string Name { get; set; }

    [DbField("image")]
    public string Image { get; set; }

    [DbField("description")]
    public string Description { get; set; }

    [DbField("tradeFor")]
    public string TradeFor { get; set; }

    [DbField("category")]
    public string Category { get; set; }

    [DbField("condition")]
    public string Condition { get; set; }

    [DbField("location")]
    public string Location { get; set; }

    [DbField("ownerId")]
    public string OwnerId { get; set; }

    [DbField("dimensions_width")]
    public float DimensionsWidth { get; set; }

    [DbField("dimensions_height")]
    public float DimensionsHeight { get; set; }

    [DbField("dimensions_depth")]
    public float DimensionsDepth { get; set; }

    [DbField("dimensions_weight")]
    public float DimensionsWeight { get; set; }

    [DbField("dateListed")]
    public DateTime DateListed { get; set; }
}