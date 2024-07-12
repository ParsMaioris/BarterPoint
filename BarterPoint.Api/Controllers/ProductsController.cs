using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public ProductsController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet("ByOwner/{ownerId}")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByOwner(string ownerId)
    {
        var products = await _databaseService.GetProductsByOwner(ownerId);
        return Ok(products);
    }

    [HttpGet("NotOwnedByUser/{ownerId}")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsNotOwnedByUser(string ownerId)
    {
        var products = await _databaseService.GetProductsNotOwnedByUser(ownerId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddProduct([FromBody] AddProductRequest product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }

        var newProductId = await _databaseService.AddProductAsync(product);
        return Ok(newProductId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveProduct(string id)
    {
        await _databaseService.RemoveProductAsync(id);
        return NoContent();
    }
}