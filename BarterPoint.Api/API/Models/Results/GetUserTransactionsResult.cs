public class GetUserTransactionsResult
{
    [DbField("TransactionId")]
    public int TransactionId { get; set; }

    [DbField("productId")]
    public string ProductId { get; set; }

    [DbField("ProductName")]
    public string ProductName { get; set; }

    [DbField("ProductImage")]
    public string ProductImage { get; set; }

    [DbField("ProductDescription")]
    public string ProductDescription { get; set; }

    [DbField("buyerId")]
    public string BuyerId { get; set; }

    [DbField("BuyerUsername")]
    public string BuyerUsername { get; set; }

    [DbField("sellerId")]
    public string SellerId { get; set; }

    [DbField("SellerUsername")]
    public string SellerUsername { get; set; }

    [DbField("dateCompleted")]
    public DateTime DateCompleted { get; set; }
}