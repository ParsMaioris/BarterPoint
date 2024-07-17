using System.Data;
using System.Data.SqlClient;

public class BidRepository : IBidRepository
{
    private readonly string _connectionString;
    private readonly DatabaseHelper _databaseHelper;

    public BidRepository(IConfiguration configuration, DatabaseHelper databaseHelper)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _databaseHelper = databaseHelper;
    }

    public async Task AddBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("AddBidStatus", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateBidStatusAsync(int bidId, string status, DateTime dateUpdated)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("UpdateBidStatus", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BidId", bidId);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DateUpdated", dateUpdated);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<IEnumerable<BidStatus>> GetAllBidStatusesAsync()
    {
        var result = new List<BidStatus>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("GetAllBidStatuses", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var bidStatus = reader.MapTo<BidStatus>();
                        result.Add(bidStatus);
                    }
                }
            }
        }

        return result;
    }

    public async Task<IEnumerable<BidDTO>> GetAllBidsAsync()
    {
        var bidList = new List<BidDTO>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("GetAllBids", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        bidList.Add(reader.MapTo<BidDTO>());
                    }
                }
            }
        }

        return bidList;
    }

    public async Task RemoveBidAsync(int bidId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("RemoveBid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@bidId", bidId);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<int> AddBidAsync(string product1Id, string product2Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("AddBid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product1Id", product1Id);
                command.Parameters.AddWithValue("@product2Id", product2Id);

                connection.Open();
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
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