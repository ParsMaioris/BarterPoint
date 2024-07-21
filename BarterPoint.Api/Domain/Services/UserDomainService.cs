namespace BarterPoint.Domain;

public class UserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<string> RegisterUserAsync(User user)
    {
        if (await FindByUsernameAsync(user.Username) != null)
        {
            return "Error: Username already exists";
        }

        if (await FindByEmailAsync(user.Email) != null)
        {
            return "Error: Email already exists";
        }

        await _userRepository.AddAsync(user);
        return "User registered successfully";
    }

    public async Task<User> FindUserByUsernameAndPasswordAsync(string username, string passwordHash)
    {
        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Username == username);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Email == email);
    }
}