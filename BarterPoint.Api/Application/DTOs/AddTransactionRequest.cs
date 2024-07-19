namespace BarterPoint.Application;

public class AddTransactionRequest
{
    public string ProductId { get; set; }
    public string BuyerId { get; set; }
    public string SellerId { get; set; }
    public DateTime DateCompleted { get; set; }
}