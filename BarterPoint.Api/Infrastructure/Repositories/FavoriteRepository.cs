using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class FavoriteRepository : BaseRepository, IFavoriteRepository
{
    public FavoriteRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public IEnumerable<Favorite> GetAll()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllFavorites"))
        {
            return ExecuteReaderAsync(command, MapFavorite).Result;
        }
    }

    public Favorite GetById(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetFavoriteById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var favorites = ExecuteReaderAsync(command, MapFavorite).Result;
            return favorites.FirstOrDefault();
        }
    }

    public void Add(Favorite favorite)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddFavorite"))
        {
            AddFavoriteParameters(command, favorite);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Update(Favorite favorite)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateFavorite"))
        {
            UpdateFavoriteParameters(command, favorite);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    public void Delete(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteFavorite"))
        {
            command.Parameters.AddWithValue("@Id", id);
            ExecuteNonQueryAsync(command).Wait();
        }
    }

    private void AddFavoriteParameters(SqlCommand command, Favorite favorite)
    {
        command.Parameters.AddWithValue("@UserId", favorite.UserId);
        command.Parameters.AddWithValue("@ProductId", favorite.ProductId);
        command.Parameters.AddWithValue("@DateAdded", favorite.DateAdded);
    }

    private void UpdateFavoriteParameters(SqlCommand command, Favorite favorite)
    {
        command.Parameters.AddWithValue("@Id", favorite.Id);
        command.Parameters.AddWithValue("@UserId", favorite.UserId);
        command.Parameters.AddWithValue("@ProductId", favorite.ProductId);
        command.Parameters.AddWithValue("@DateAdded", favorite.DateAdded);
    }

    private Favorite MapFavorite(IDataRecord record)
    {
        return new Favorite
        (
            id: record.GetInt32(record.GetOrdinal("id")),
            userId: record.GetString(record.GetOrdinal("userId")),
            productId: record.GetString(record.GetOrdinal("productId")),
            dateAdded: record.GetDateTime(record.GetOrdinal("dateAdded"))
        );
    }
}