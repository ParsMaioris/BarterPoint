using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BidsController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public BidsController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BidDTO>>> GetAllBids()
    {
        var bids = await _databaseService.GetAllBidsAsync();
        return Ok(bids);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddBid([FromBody] AddBidRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Product1Id) || string.IsNullOrEmpty(request.Product2Id))
        {
            return BadRequest("Invalid bid request.");
        }

        var bidId = await _databaseService.AddBidAsync(request.Product1Id, request.Product2Id);
        return Ok(bidId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveBid(int id)
    {
        await _databaseService.RemoveBidAsync(id);
        return NoContent();
    }
}