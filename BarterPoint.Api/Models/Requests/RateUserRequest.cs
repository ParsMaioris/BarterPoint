public class RateUserRequest
{
    public string RaterId { get; set; }
    public string RateeId { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
    public DateTime DateRated { get; set; }
}