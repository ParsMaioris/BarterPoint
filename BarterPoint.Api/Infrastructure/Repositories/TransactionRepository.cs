using System.Data.SqlClient;
using BarterPoint.Domain;
using BarterPoint.Application;
using System.Data;

namespace BarterPoint.Infrastructure;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public IEnumerable<Transaction> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllTransactions"))
        {
            return ExecuteReaderAsync(command, MapTransaction).Result;
        }
    }

    public Transaction GetById(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetTransactionById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var transactions = ExecuteReaderAsync(command, MapTransaction).Result;
            return transactions.FirstOrDefault();
        }
    }

    public void Add(AddTransactionRequest request)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddTransaction"))
        {
            AddTransactionParameters(command, request);
            ExecuteScalarAsync(command).Wait();
        }
    }

    public void Update(Transaction transaction)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateTransaction"))
        {
            command.Parameters.AddWithValue("@Id", transaction.Id);
            UpdateTransactionParameters(command, transaction);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteTransaction"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    private void AddTransactionParameters(SqlCommand command, AddTransactionRequest request)
    {
        command.Parameters.AddWithValue("@ProductId", request.ProductId);
        command.Parameters.AddWithValue("@BuyerId", request.BuyerId);
        command.Parameters.AddWithValue("@SellerId", request.SellerId);
        command.Parameters.AddWithValue("@DateCompleted", request.DateCompleted);
    }

    private void UpdateTransactionParameters(SqlCommand command, Transaction transaction)
    {
        command.Parameters.AddWithValue("@Id", transaction.Id);
        command.Parameters.AddWithValue("@ProductId", transaction.ProductId);
        command.Parameters.AddWithValue("@BuyerId", transaction.BuyerId);
        command.Parameters.AddWithValue("@SellerId", transaction.SellerId);
        command.Parameters.AddWithValue("@DateCompleted", transaction.DateCompleted);
    }

    private Transaction MapTransaction(IDataRecord record)
    {
        return new Transaction
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            productId: record.GetString(record.GetOrdinal("productId")),
            buyerId: record.GetString(record.GetOrdinal("buyerId")),
            sellerId: record.GetString(record.GetOrdinal("sellerId")),
            dateCompleted: record.GetDateTime(record.GetOrdinal("dateCompleted"))
        );
    }
}