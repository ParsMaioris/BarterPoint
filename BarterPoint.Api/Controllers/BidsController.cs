using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BidsController : ControllerBase
{
    private readonly IBidService _bidService;

    public BidsController(IBidService bidService)
    {
        _bidService = bidService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BidDTO>>> GetAllBids()
    {
        var bids = await _bidService.GetBidsWithPendingStatusesAsync();
        return Ok(bids);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddBid([FromBody] AddBidRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Product1Id) || string.IsNullOrEmpty(request.Product2Id))
        {
            return BadRequest("Invalid bid request.");
        }

        var bidId = await _bidService.AddBidAsync(request.Product1Id, request.Product2Id);
        return Ok(bidId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveBid(int id)
    {
        await _bidService.UpdateBidStatusToRejectedAsync(id);
        return NoContent();
    }

    [HttpPost("approve/{bidId}")]
    public IActionResult ApproveBid(int bidId)
    {
        try
        {
            _bidService.ApproveBid(bidId);
            return Ok(new { message = "Bid approved successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while approving the bid.", error = ex.Message });
        }
    }
}