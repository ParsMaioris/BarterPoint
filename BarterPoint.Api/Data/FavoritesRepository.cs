using System.Data.SqlClient;

public class FavoritesRepository : IFavoritesRepository
{
    private readonly DatabaseHelper _databaseHelper;

    public FavoritesRepository(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void AddFavorite(string userId, string productId)
    {
        if (!IsFavorite(userId, productId))
        {
            _databaseHelper.ExecuteNonQuery("AddUserFavorite",
                new SqlParameter("@userId", userId),
                new SqlParameter("@productId", productId));
        }
    }

    public List<FavoriteResult> GetUserFavorites(string userId)
    {
        return _databaseHelper.ExecuteReader("GetUserFavorites", reader => reader.MapTo<FavoriteResult>(),
            new SqlParameter("@userId", userId));
    }

    public void RemoveFavorite(string userId, string productId)
    {
        _databaseHelper.ExecuteNonQuery("RemoveUserFavorite",
            new SqlParameter("@userId", userId),
            new SqlParameter("@productId", productId));
    }

    public bool IsFavorite(string userId, string productId)
    {
        return _databaseHelper.ExecuteScalar<int>("IsFavorite",
            new SqlParameter("@userId", userId),
            new SqlParameter("@productId", productId)) == 1;
    }
}