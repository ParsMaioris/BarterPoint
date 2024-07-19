namespace BarterPoint.Application;

public interface IUserService
{
    Task<string> RegisterUserAsync(RegisterUserRequest request);
    Task<SignInResult> SignInUserAsync(SignInRequest request);
}