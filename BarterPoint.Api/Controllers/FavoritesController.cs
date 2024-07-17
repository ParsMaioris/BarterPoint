using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
    {
        await _favoritesService.AddFavoriteAsync(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserFavorites(string userId)
    {
        var favorites = await _favoritesService.GetUserFavoritesAsync(userId);
        return Ok(favorites);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFavorite([FromBody] RemoveFavoriteRequest request)
    {
        await _favoritesService.RemoveFavoriteAsync(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("isFavorite")]
    public async Task<IActionResult> IsFavorite([FromQuery] string userId, [FromQuery] string productId)
    {
        var isFavorite = await _favoritesService.IsFavoriteAsync(userId, productId);
        return Ok(new { IsFavorite = isFavorite });
    }
}