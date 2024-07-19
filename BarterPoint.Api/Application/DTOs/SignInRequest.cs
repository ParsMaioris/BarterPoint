namespace BarterPoint.Application;

public class SignInRequest
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}