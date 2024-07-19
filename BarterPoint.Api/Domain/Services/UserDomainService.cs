namespace BarterPoint.Domain;

public class UserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserById(string userId)
    {
        return _userRepository.GetById(userId);
    }

    public string RegisterUser(User user)
    {
        if (FindByUsername(user.Username) != null)
        {
            return "Error: Username already exists";
        }

        if (FindByEmail(user.Email) != null)
        {
            return "Error: Email already exists";
        }

        _userRepository.Add(user);
        return "User registered successfully";
    }

    public User FindUserByUsernameAndPassword(string username, string passwordHash)
    {
        return _userRepository.GetAll().FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
    }

    public User FindByUsername(string username)
    {
        return _userRepository.GetAll().FirstOrDefault(u => u.Username == username);
    }

    public User FindByEmail(string email)
    {
        return _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
    }
}