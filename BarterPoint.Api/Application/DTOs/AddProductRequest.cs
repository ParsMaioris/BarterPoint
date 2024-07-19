namespace BarterPoint.Application;

public class AddProductRequest
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string TradeFor { get; set; }
    public int CategoryId { get; set; }
    public string Condition { get; set; }
    public string Location { get; set; }
    public string OwnerId { get; set; }
    public float DimensionsWidth { get; set; }
    public float DimensionsHeight { get; set; }
    public float DimensionsDepth { get; set; }
    public float DimensionsWeight { get; set; }
    public DateTime DateListed { get; set; }
}