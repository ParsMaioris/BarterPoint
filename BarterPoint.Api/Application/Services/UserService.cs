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

        return await Task.Run(() => _userDomainService.RegisterUser(user));
    }

    public async Task<SignInResult> SignInUserAsync(SignInRequest request)
    {
        var user = await Task.Run(() => _userDomainService.FindUserByUsernameAndPassword(request.Username, request.PasswordHash));

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