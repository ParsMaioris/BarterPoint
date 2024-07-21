using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class BidStatusRepository : BaseRepository, IBidStatusRepository
{
    public BidStatusRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<BidStatus>> GetAllAsync()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllBidStatuses"))
        {
            return await ExecuteReaderAsync(command, MapBidStatus);
        }
    }

    public async Task<BidStatus> GetByIdAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetBidStatusById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var statuses = await ExecuteReaderAsync(command, MapBidStatus);
            return statuses.FirstOrDefault();
        }
    }

    public async Task AddAsync(BidStatus bidStatus)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddBidStatus"))
        {
            AddBidStatusParameters(command, bidStatus);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task UpdateAsync(BidStatus bidStatus)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateBidStatus"))
        {
            UpdateBidStatusParameters(command, bidStatus);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "RemoveBidStatus"))
        {
            command.Parameters.AddWithValue("@BidStatusId", id);
            await ExecuteNonQueryAsync(command);
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