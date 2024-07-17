public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequest request)
    {
        return await _userRepository.SignInUserAsync(request);
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequest request)
    {
        return await _userRepository.RegisterUserAsync(request);
    }
}