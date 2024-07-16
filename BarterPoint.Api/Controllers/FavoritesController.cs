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
    public IActionResult AddFavorite([FromBody] AddFavoriteRequest request)
    {
        _favoritesService.AddFavorite(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserFavorites(string userId)
    {
        var favorites = _favoritesService.GetUserFavorites(userId);
        return Ok(favorites);
    }

    [HttpDelete]
    public IActionResult RemoveFavorite([FromBody] RemoveFavoriteRequest request)
    {
        _favoritesService.RemoveFavorite(request.UserId, request.ProductId);
        return Ok();
    }

    [HttpGet("isFavorite")]
    public IActionResult IsFavorite([FromQuery] string userId, [FromQuery] string productId)
    {
        var isFavorite = _favoritesService.IsFavorite(userId, productId);
        return Ok(new { IsFavorite = isFavorite });
    }
}