using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RatingsControllerV2 : ControllerBase
{
    private readonly IRatingsServiceV2 _ratingsService;

    public RatingsControllerV2(IRatingsServiceV2 ratingService)
    {
        _ratingsService = ratingService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<UserRatingResultV2>> GetUserAverageRating(string userId)
    {
        var rating = await _ratingsService.GetUserAverageRating(userId);
        return Ok(rating);
    }

    [HttpPost]
    public async Task<ActionResult> AddRating([FromBody] RateUserRequestV2 ratingRequest)
    {
        await _ratingsService.AddRating(ratingRequest);
        return Ok();
    }
}