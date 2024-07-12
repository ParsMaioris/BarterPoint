using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly BarterContext _context;
    private readonly IMemoryCache _cache;

    public CategoriesController(BarterContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
    {
        var cacheKey = "productCategories";
        var cachedCategories = await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(777);
            return await _context.ProductCategories
                                 .FromSqlRaw("EXEC GetAllProductCategories")
                                 .ToListAsync();
        });

        return Ok(cachedCategories);
    }
}