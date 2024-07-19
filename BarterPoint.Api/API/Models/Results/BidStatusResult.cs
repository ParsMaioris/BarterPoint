public class BidStatusResult
{
    [DbField("id")]
    public int Id { get; set; }

    [DbField("bidId")]
    public int BidId { get; set; }

    [DbField("status")]
    public string Status { get; set; }

    [DbField("dateUpdated")]
    public DateTime DateUpdated { get; set; }
}