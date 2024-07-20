using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class BidStatusRepository : BaseRepository, IBidStatusRepository
{
    public BidStatusRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public IEnumerable<BidStatus> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllBidStatuses"))
        {
            return ExecuteReaderAsync(command, MapBidStatus).Result;
        }
    }

    public BidStatus GetById(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetBidStatusById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var statuses = ExecuteReaderAsync(command, MapBidStatus).Result;
            return statuses.FirstOrDefault();
        }
    }

    public void Add(BidStatus bidStatus)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBidStatus"))
        {
            AddBidStatusParameters(command, bidStatus);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(BidStatus bidStatus)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateBidStatus"))
        {
            UpdateBidStatusParameters(command, bidStatus);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "RemoveBidStatus"))
        {
            command.Parameters.AddWithValue("@BidStatusId", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    private void AddBidStatusParameters(SqlCommand command, BidStatus bidStatus)
    {
        command.Parameters.AddWithValue("@BidId", bidStatus.BidId);
        command.Parameters.AddWithValue("@Status", bidStatus.Status);
        command.Parameters.AddWithValue("@DateUpdated", bidStatus.DateUpdated);
    }

    private void UpdateBidStatusParameters(SqlCommand command, BidStatus bidStatus)
    {
        command.Parameters.AddWithValue("@Id", bidStatus.Id);
        command.Parameters.AddWithValue("@Status", bidStatus.Status);
        command.Parameters.AddWithValue("@DateUpdated", bidStatus.DateUpdated);
    }

    private BidStatus MapBidStatus(IDataRecord record)
    {
        return new BidStatus
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            bidId: record.GetInt32(record.GetOrdinal("bidId")),
            status: record.GetString(record.GetOrdinal("status")),
            dateUpdated: record.GetDateTime(record.GetOrdinal("dateUpdated"))
        );
    }
}