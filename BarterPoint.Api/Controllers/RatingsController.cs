using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<UserRating>> GetUserAverageRating(string userId)
    {
        var rating = await _ratingService.GetUserAverageRating(userId);
        return Ok(rating);
    }

    [HttpPost]
    public async Task<ActionResult> AddRating([FromBody] RateUserRequest ratingRequest)
    {
        await _ratingService.AddRating(ratingRequest);
        return Ok();
    }
}