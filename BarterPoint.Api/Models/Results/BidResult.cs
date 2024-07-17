public class BidResult
{
    [DbColumn("bidId")]
    public int Id { get; set; }

    [DbColumn("product1Id")]
    public string Product1Id { get; set; }

    [DbColumn("product2Id")]
    public string Product2Id { get; set; }

    [DbColumn("product1Name")]
    public string Product1Name { get; set; }

    [DbColumn("product2Name")]
    public string Product2Name { get; set; }
}