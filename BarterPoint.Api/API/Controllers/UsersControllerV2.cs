using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersControllerV2 : ControllerBase
{
    private readonly IUserServiceV2 _userService;

    public UsersControllerV2(IUserServiceV2 databaseService)
    {
        _userService = databaseService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestV2 request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(request);
        if (result.Contains("Error"))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequestV2 request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.SignInUserAsync(request);
        if (result.ErrorMessage != null)
        {
            return Unauthorized(result.ErrorMessage);
        }

        return Ok(result);
    }
}