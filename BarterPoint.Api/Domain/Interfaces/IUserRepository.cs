namespace BarterPoint.Domain;

public interface IUserRepository : IRepository<User, int, User>
{
    User SignIn(string username, string password);
}