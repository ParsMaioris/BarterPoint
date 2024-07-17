using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly IRatingsService _ratingsService;

    public RatingsController(IRatingsService ratingService)
    {
        _ratingsService = ratingService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<UserRatingResult>> GetUserAverageRating(string userId)
    {
        var rating = await _ratingsService.GetUserAverageRating(userId);
        return Ok(rating);
    }

    [HttpPost]
    public async Task<ActionResult> AddRating([FromBody] RateUserRequest ratingRequest)
    {
        await _ratingsService.AddRating(ratingRequest);
        return Ok();
    }
}