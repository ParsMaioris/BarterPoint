using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public UsersController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _databaseService.RegisterUserAsync(request);
        if (result.Contains("Error"))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _databaseService.SignInUserAsync(request);
        if (result.ErrorMessage != null)
        {
            return Unauthorized(result.ErrorMessage);
        }

        return Ok(result);
    }
}