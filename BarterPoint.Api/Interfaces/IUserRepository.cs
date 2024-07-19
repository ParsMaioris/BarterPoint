public interface IUserRepositoryV2
{
    Task<SignInResult> SignInUserAsync(SignInRequest request);
    Task<string> RegisterUserAsync(RegisterUserRequest request);
}