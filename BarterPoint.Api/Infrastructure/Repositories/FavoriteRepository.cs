using System.Data;
using System.Data.SqlClient;
using BarterPoint.Domain;

namespace BarterPoint.Infrastructure;

public class FavoriteRepository : BaseRepository, IFavoriteRepository
{
    public FavoriteRepository(DbConnectionFactoryDelegate dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync()
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetAllFavorites"))
        {
            return await ExecuteReaderAsync(command, MapFavorite);
        }
    }

    public async Task<Favorite> GetByIdAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "GetFavoriteById"))
        {
            command.Parameters.AddWithValue("@Id", id);
            var favorites = await ExecuteReaderAsync(command, MapFavorite);
            return favorites.FirstOrDefault();
        }
    }

    public async Task AddAsync(Favorite favorite)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "AddFavorite"))
        {
            AddFavoriteParameters(command, favorite);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task UpdateAsync(Favorite favorite)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "UpdateFavorite"))
        {
            UpdateFavoriteParameters(command, favorite);
            await ExecuteNonQueryAsync(command);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = OpenConnection())
        using (var command = CreateCommand(connection, "DeleteFavorite"))
        {
            command.Parameters.AddWithValue("@Id", id);
            await ExecuteNonQueryAsync(command);
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