public interface IUserRepository
{
    Task<SignInResult> SignInUserAsync(SignInRequest request);
    Task<string> RegisterUserAsync(RegisterUserRequest request);
}