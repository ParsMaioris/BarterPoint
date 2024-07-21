namespace BarterPoint.Domain;

public interface IRepository<T, TId, TEntity> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(TId id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TId id);
}