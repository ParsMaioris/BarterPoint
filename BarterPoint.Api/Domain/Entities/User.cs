namespace BarterPoint.Domain;

public class User
{
    public string Id { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public DateTime DateJoined { get; private set; }

    public User(string id, string username, string passwordHash, string email, string name, string location, DateTime dateJoined)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        Name = name;
        Location = location;
        DateJoined = dateJoined;
    }
}
