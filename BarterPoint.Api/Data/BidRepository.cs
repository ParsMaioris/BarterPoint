using System.Data.SqlClient;

public class BidRepository : BaseRepository, IBidRepository
{
    public BidRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "AddBidStatus"))
            {
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await ExecuteNonQueryAsync(command);
            }
        }
    }

    public async Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "UpdateBidStatus"))
            {
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await ExecuteNonQueryAsync(command);
            }
        }
    }

    public async Task<IEnumerable<BidStatusResult>> GetAllBidStatusesAsync()
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetAllBidStatuses"))
            {
                return await ExecuteReaderAsync(command, reader => reader.MapTo<BidStatusResult>());
            }
        }
    }

    public async Task<IEnumerable<BidResult>> GetAllBidsAsync()
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetAllBids"))
            {
                return await ExecuteReaderAsync(command, reader => reader.MapTo<BidResult>());
            }
        }
    }

    public async Task RemoveBidAsync(int bidId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "RemoveBid"))
            {
                command.Parameters.AddWithValue("@bidId", bidId);
                await ExecuteNonQueryAsync(command);
            }
        }
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "AddBid"))
            {
                command.Parameters.AddWithValue("@product1Id", product1Id);
                command.Parameters.AddWithValue("@product2Id", product2Id);

                var result = await ExecuteScalarAsync<int>(command);
                return result;
            }
        }
    }

    public async Task ApproveBidAsync(int bidId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var products = await ExecuteReaderAsync(CreateCommandWithTransaction(transaction, "GetProductsByBidId", new SqlParameter("@BidId", bidId)), reader => new
                    {
                        Product1Id = reader["product1Id"].ToString(),
                        Product2Id = reader["product2Id"].ToString()
                    });

                    if (products.Count == 0)
                    {
                        throw new Exception("Invalid bid ID");
                    }

                    var product1Id = products[0].Product1Id;
                    var product2Id = products[0].Product2Id;

                    await ExecuteNonQueryAsync(CreateCommandWithTransaction(transaction, "ApproveBidStatus", new SqlParameter("@BidId", bidId)));
                    await ExecuteNonQueryAsync(CreateCommandWithTransaction(transaction, "RejectOtherBids",
                        new SqlParameter("@Product1Id", product1Id),
                        new SqlParameter("@Product2Id", product2Id),
                        new SqlParameter("@BidId", bidId)));
                    await ExecuteNonQueryAsync(CreateCommandWithTransaction(transaction, "AddTransactionHistory",
                        new SqlParameter("@Product1Id", product1Id),
                        new SqlParameter("@Product2Id", product2Id)));

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