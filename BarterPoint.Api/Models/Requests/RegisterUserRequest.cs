public class RegisterUserRequest
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime DateJoined { get; set; }
}