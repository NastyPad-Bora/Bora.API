namespace Bora.API.Shared.Domain.Repository;

public interface IBaseRepository<TEntity, TKey>
{
    Task<IEnumerable<TEntity>> ListAll();
    Task<TEntity> FindById(TKey key);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    bool Exist(TKey id);
}