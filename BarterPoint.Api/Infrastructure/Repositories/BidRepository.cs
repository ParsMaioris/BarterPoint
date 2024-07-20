using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class BidRepository : BaseRepository, IBidRepository
{
    public BidRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public IEnumerable<Bid> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllBids"))
        {
            return ExecuteReaderAsync(command, MapBid).Result;
        }
    }

    public Bid GetById(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetBidById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var bids = ExecuteReaderAsync(command, MapBid).Result;
            return bids.FirstOrDefault();
        }
    }

    public void Add(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBid"))
        {
            AddBidParameters(command, bid);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public int AddAndReturnId(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBid"))
        {
            AddBidParameters(command, bid);
            var result = ExecuteScalarAsync(command).Result;
            return Convert.ToInt32(result);
        }
    }

    public void Update(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateBid"))
        {
            UpdateBidParameters(command, bid);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "RemoveBid"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
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