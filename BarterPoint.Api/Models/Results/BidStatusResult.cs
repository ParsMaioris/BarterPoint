public class BidStatusResult
{
    [DbColumn("id")]
    public int Id { get; set; }

    [DbColumn("bidId")]
    public int BidId { get; set; }

    [DbColumn("status")]
    public string Status { get; set; }

    [DbColumn("dateUpdated")]
    public DateTime DateUpdated { get; set; }
}