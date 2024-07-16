using System.Data.SqlClient;

public class BidService : IBidService
{
    private readonly IDatabaseService _databaseService;
    private readonly DatabaseHelper _databaseHelper;


    public BidService(IDatabaseService databaseService, DatabaseHelper databaseHelper)
    {
        _databaseService = databaseService;
        _databaseHelper = databaseHelper;
    }

    public async Task<IEnumerable<BidDTO>> GetBidsWithPendingStatusesAsync()
    {
        var allBids = await _databaseService.GetAllBidsAsync();
        var allStatuses = await _databaseService.GetAllBidStatusesAsync();

        var pendingStatuses = allStatuses
            .Where(s => s.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            .Select(s => s.BidId);

        var bidsWithPendingStatuses = allBids
            .Where(b => pendingStatuses.Contains(b.Id));

        return bidsWithPendingStatuses;
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        var bidId = await _databaseService.AddBidAsync(product1Id, product2Id);
        await _databaseService.AddBidStatusAsync(bidId, "Pending", DateTime.UtcNow);
        return bidId;
    }

    public async Task UpdateBidStatusToRejectedAsync(int bidId)
    {
        await _databaseService.UpdateBidStatusAsync(bidId, "Rejected", DateTime.UtcNow);
    }

    public void ApproveBid(int bidId)
    {
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var parameters = new[] { new SqlParameter("@BidId", bidId) };
                    var products = _databaseHelper.ExecuteReader(transaction, "GetProductsByBidId", reader => new
                    {
                        Product1Id = reader["product1Id"].ToString(),
                        Product2Id = reader["product2Id"].ToString()
                    }, parameters);

                    if (products.Count == 0)
                    {
                        throw new Exception("Invalid bid ID");
                    }

                    var product1Id = products[0].Product1Id;
                    var product2Id = products[0].Product2Id;

                    _databaseHelper.ExecuteNonQuery(transaction, "ApproveBidStatus", new SqlParameter("@BidId", bidId));

                    _databaseHelper.ExecuteNonQuery(transaction, "RejectOtherBids",
                        new SqlParameter("@Product1Id", product1Id),
                        new SqlParameter("@Product2Id", product2Id),
                        new SqlParameter("@BidId", bidId));

                    _databaseHelper.ExecuteNonQuery(transaction, "AddTransactionHistory",
                        new SqlParameter("@Product1Id", product1Id),
                        new SqlParameter("@Product2Id", product2Id));

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error approving bid: " + ex.Message);
                }
            }
        }
    }
}