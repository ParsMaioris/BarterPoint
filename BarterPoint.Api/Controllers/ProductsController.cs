using BarterPoint.Application;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("ByOwner/{ownerId}")]
    public async Task<ActionResult<IEnumerable<ProductResult>>> GetProductsByOwner(string ownerId)
    {
        var products = await _productService.GetAvailableProductsByOwnerAsync(ownerId);
        return Ok(products);
    }

    [HttpGet("NotOwnedByUser/{ownerId}")]
    public async Task<ActionResult<IEnumerable<ProductResult>>> GetProductsNotOwnedByUser(string ownerId)
    {
        var products = await _productService.GetAvailableProductsNotOwnedByUserAsync(ownerId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddProduct([FromBody] AddProductRequest product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }

        var newProductId = await _productService.AddProductAsync(product);
        return Ok(newProductId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveProduct(string id)
    {
        await _productService.RemoveProductAsync(id);
        return NoContent();
    }
}