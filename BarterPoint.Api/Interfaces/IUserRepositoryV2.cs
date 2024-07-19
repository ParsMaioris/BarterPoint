public interface IUserRepositoryV2
{
    Task<SignInResult> SignInUserAsync(SignInRequestV2 request);
    Task<string> RegisterUserAsync(RegisterUserRequestV2 request);
}