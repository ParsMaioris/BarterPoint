using BarterPoint.Application;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoritesController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
    {
        await _favoriteService.AddFavoriteAsync(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserFavorites(string userId)
    {
        var favorites = await _favoriteService.GetUserFavoritesAsync(userId);
        return Ok(favorites);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFavorite([FromBody] RemoveFavoriteRequest request)
    {
        await _favoriteService.RemoveFavoriteAsync(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("isFavorite")]
    public async Task<IActionResult> IsFavorite([FromQuery] string userId, [FromQuery] string productId)
    {
        var isFavorite = await _favoriteService.IsFavoriteAsync(userId, productId);
        return Ok(new { IsFavorite = isFavorite });
    }
}