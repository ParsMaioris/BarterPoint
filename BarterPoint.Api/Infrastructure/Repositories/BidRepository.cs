using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class BidRepository : BaseRepository, IBidRepository
{
    public BidRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<Bid>> GetAllAsync()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllBids"))
        {
            return await ExecuteReaderAsync(command, MapBid);
        }
    }

    public async Task<Bid> GetByIdAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetBidById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var bids = await ExecuteReaderAsync(command, MapBid);
            return bids.FirstOrDefault();
        }
    }

    public async Task AddAsync(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBid"))
        {
            AddBidParameters(command, bid);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task<int> AddAndReturnIdAsync(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBid"))
        {
            AddBidParameters(command, bid);
            var result = await ExecuteScalarAsync(command);
            return Convert.ToInt32(result);
        }
    }

    public async Task UpdateAsync(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateBid"))
        {
            UpdateBidParameters(command, bid);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "RemoveBid"))
        {
            command.Parameters.AddWithValue("@Id", id);
            await ExecuteNonQueryAsync(command);
        }
    }

    private void AddBidParameters(SqlCommand command, Bid bid)
    {
        command.Parameters.AddWithValue("@Product1Id", bid.Product1Id);
        command.Parameters.AddWithValue("@Product2Id", bid.Product2Id);
    }

    private void UpdateBidParameters(SqlCommand command, Bid bid)
    {
        command.Parameters.AddWithValue("@Id", bid.Id);
        command.Parameters.AddWithValue("@Product1Id", bid.Product1Id);
        command.Parameters.AddWithValue("@Product2Id", bid.Product2Id);
    }

    private Bid MapBid(IDataRecord record)
    {
        return new Bid
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            product1Id: record.GetString(record.GetOrdinal("product1Id")),
            product2Id: record.GetString(record.GetOrdinal("product2Id"))
        );
    }
}