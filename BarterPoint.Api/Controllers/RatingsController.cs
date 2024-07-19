using BarterPoint.Application;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly IUserRatingService _ratingService;

    public RatingsController(IUserRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<double>> GetUserAverageRating(string userId)
    {
        var rating = await _ratingService.GetUserAverageRating(userId);
        return Ok(rating);
    }

    [HttpPost]
    public async Task<IActionResult> AddRating([FromBody] RateUserRequest ratingRequest)
    {
        await _ratingService.AddRating(ratingRequest);
        return Ok();
    }
}