public class UserTransactionDto
{
    public int TransactionId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public string ProductDescription { get; set; }
    public string BuyerId { get; set; }
    public string BuyerUsername { get; set; }
    public string SellerId { get; set; }
    public string SellerUsername { get; set; }
    public DateTime DateCompleted { get; set; }
}