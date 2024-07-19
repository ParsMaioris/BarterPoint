public class BidResult
{
    [DbField("bidId")]
    public int Id { get; set; }

    [DbField("product1Id")]
    public string Product1Id { get; set; }

    [DbField("product2Id")]
    public string Product2Id { get; set; }

    [DbField("product1Name")]
    public string Product1Name { get; set; }

    [DbField("product2Name")]
    public string Product2Name { get; set; }
}