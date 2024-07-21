using BarterPoint.Domain;

namespace BarterPoint.Application;

public class UserService : IUserService
{
    private readonly UserDomainService _userDomainService;

    public UserService(UserDomainService userDomainService)
    {
        _userDomainService = userDomainService;
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequest request)
    {
        var user = new User
        (
            id: Guid.NewGuid().ToString(),
            username: request.Username,
            passwordHash: request.PasswordHash,
            email: request.Email,
            name: request.Name,
            location: request.Location,
            dateJoined: DateTime.UtcNow
        );

        return await _userDomainService.RegisterUserAsync(user);
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequest request)
    {
        var user = await _userDomainService.FindUserByUsernameAndPasswordAsync(request.Username, request.PasswordHash);

        if (user == null)
        {
            return new SignInResult
            {
                ErrorMessage = "Invalid username or password."
            };
        }

        return new SignInResult
        {
            Message = "Sign-in successful.",
            UserId = user.Id
        };
    }
}