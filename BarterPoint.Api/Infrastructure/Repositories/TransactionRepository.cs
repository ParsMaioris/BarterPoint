using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;
using BarterPoint.Application;

namespace BarterPoint.Infrastructure;

public class TransactionRepository : ITransactionRepository
{
    private readonly DbConnectionFactoryDelegate _dbConnectionFactory;

    public TransactionRepository(DbConnectionFactoryDelegate dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private SqlConnection OpenConnection()
    {
        var connection = (SqlConnection)_dbConnectionFactory();
        connection.Open();
        return connection;
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
            var newId = ExecuteScalarAsync<decimal>(command).Result;
        }
    }

    public void Update(Transaction transaction)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateTransaction"))
        {
            command.Parameters.AddWithValue("@Id", transaction.Id);
            AddTransactionParameters(command, transaction);
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

    private void AddTransactionParameters(SqlCommand command, Transaction transaction)
    {
        command.Parameters.AddWithValue("@ProductId", transaction.ProductId);
        command.Parameters.AddWithValue("@BuyerId", transaction.BuyerId);
        command.Parameters.AddWithValue("@SellerId", transaction.SellerId);
        command.Parameters.AddWithValue("@DateCompleted", transaction.DateCompleted);
    }

    private async Task ExecuteNonQueryAsync(SqlCommand command)
    {
        await command.ExecuteNonQueryAsync();
    }

    private async Task<T> ExecuteScalarAsync<T>(SqlCommand command)
    {
        var result = await command.ExecuteScalarAsync();
        return (T)result;
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