namespace BarterPoint.Domain;

public class Product
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Image { get; private set; }
    public string Description { get; private set; }
    public string TradeFor { get; private set; }
    public int CategoryId { get; private set; }
    public string Condition { get; private set; }
    public string Location { get; private set; }
    public string OwnerId { get; private set; }
    public float DimensionsWidth { get; private set; }
    public float DimensionsHeight { get; private set; }
    public float DimensionsDepth { get; private set; }
    public float DimensionsWeight { get; private set; }
    public DateTime DateListed { get; private set; }

    public Product(string id, string name, string image, string description, string tradeFor,
        int categoryId, string condition, string location, string ownerId, float dimensionsWidth,
        float dimensionsHeight, float dimensionsDepth, float dimensionsWeight, DateTime dateListed)
    {
        Id = id;
        Name = name;
        Image = image;
        Description = description;
        TradeFor = tradeFor;
        CategoryId = categoryId;
        Condition = condition;
        Location = location;
        OwnerId = ownerId;
        DimensionsWidth = dimensionsWidth;
        DimensionsHeight = dimensionsHeight;
        DimensionsDepth = dimensionsDepth;
        DimensionsWeight = dimensionsWeight;
        DateListed = dateListed;
    }
}
