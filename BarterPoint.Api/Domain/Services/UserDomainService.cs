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
}