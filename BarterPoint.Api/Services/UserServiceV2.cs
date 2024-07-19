public class UserServiceV2 : IUserServiceV2
{
    private readonly IUserRepositoryV2 _userRepository;

    public UserServiceV2(IUserRepositoryV2 userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequestV2 request)
    {
        return await _userRepository.SignInUserAsync(request);
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequestV2 request)
    {
        return await _userRepository.RegisterUserAsync(request);
    }
}