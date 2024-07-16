public class TransactionHistory
{
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string BuyerId { get; set; }
    public string SellerId { get; set; }
    public DateTime DateCompleted { get; set; }
}