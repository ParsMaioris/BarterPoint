using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class BidRepository : IBidRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    public BidRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
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
            command.Parameters.AddWithValue("@BidId", id);
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
            var result = ExecuteScalarAsync(command);
            return Convert.ToInt32(result);
        }
    }

    private object ExecuteScalarAsync(SqlCommand command)
    {
        return command.ExecuteScalarAsync().Result;
    }

    public void Update(Bid bid)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateBid"))
        {
            AddBidParameters(command, bid);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "RemoveBid"))
        {
            command.Parameters.AddWithValue("@BidId", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    private void AddBidParameters(SqlCommand command, Bid bid)
    {
        command.Parameters.AddWithValue("@Product1Id", bid.Product1Id);
        command.Parameters.AddWithValue("@Product2Id", bid.Product2Id);
    }

    private async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
    }

    private async Task<List<T>> ExecuteReaderAsync<T>(SqlCommand command, Func<IDataReader, T> map)
    {
        var results = new List<T>();
        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }
        }
        return results;
    }

    private SqlCommand CreateCommand(SqlConnection connection, string storedProcedure)
    {
        var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = storedProcedure;
        return command;
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