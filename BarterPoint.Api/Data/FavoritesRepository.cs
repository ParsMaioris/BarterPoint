public class FavoritesRepository : BaseRepository, IFavoritesRepositoryV2
{
    public FavoritesRepository(DbConnectionFactoryDelegate dbConnectionFactory)
        : base(dbConnectionFactory)
    {
    }

    public async Task AddFavoriteAsync(string userId, string productId)
    {
        if (!await IsFavoriteAsync(userId, productId))
        {
            using (var connection = await OpenConnectionAsync())
            {
                using (var command = CreateCommand(connection, "AddUserFavorite"))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@productId", productId);
                    await ExecuteNonQueryAsync(command);
                }
            }
        }
    }

    public async Task<List<FavoriteResultV2>> GetUserFavoritesAsync(string userId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "GetUserFavorites"))
            {
                command.Parameters.AddWithValue("@userId", userId);
                return await ExecuteReaderAsync(command, reader => reader.MapTo<FavoriteResultV2>());
            }
        }
    }

    public async Task RemoveFavoriteAsync(string userId, string productId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "RemoveUserFavorite"))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@productId", productId);
                await ExecuteNonQueryAsync(command);
            }
        }
    }

    public async Task<bool> IsFavoriteAsync(string userId, string productId)
    {
        using (var connection = await OpenConnectionAsync())
        {
            using (var command = CreateCommand(connection, "IsFavorite"))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@productId", productId);
                var result = await ExecuteScalarAsync<int>(command);
                return result == 1;
            }
        }
    }
}