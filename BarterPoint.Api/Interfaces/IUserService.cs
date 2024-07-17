public interface IUserService
{
    Task<SignInResult> SignInUserAsync(SignInRequest request);
    Task<string> RegisterUserAsync(RegisterUserRequest request);
}