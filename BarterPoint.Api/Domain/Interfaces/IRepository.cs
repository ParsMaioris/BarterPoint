namespace BarterPoint.Domain;

public interface IRepository<T, TId, TEntity> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(TId id);
    void Add(TEntity entity);
    void Update(T entity);
    void Delete(TId id);
}