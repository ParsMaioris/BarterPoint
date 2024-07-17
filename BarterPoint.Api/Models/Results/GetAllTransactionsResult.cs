public class GetAllTransactionsResult
{
    [DbField("id")]
    public int Id { get; set; }
    [DbField("productId")]
    public string ProductId { get; set; }
    [DbField("buyerId")]
    public string BuyerId { get; set; }
    [DbField("sellerId")]
    public string SellerId { get; set; }
    [DbField("dateCompleted")]
    public DateTime DateCompleted { get; set; }
}