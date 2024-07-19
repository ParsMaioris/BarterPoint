using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransactionsControllerV2 : ControllerBase
{
    private readonly ITransactionServiceV2 _transactionService;

    public TransactionsControllerV2(ITransactionServiceV2 transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTransactions()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        return Ok(transactions);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserTransactions(string userId)
    {
        var transactions = await _transactionService.GetUserTransactionsAsync(userId);
        return Ok(transactions);
    }
}