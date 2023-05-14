using Bora.API.Shared.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bora.API.Shared.Persistence.Repository;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly AppDbContext AppDbContext;
    protected readonly DbSet<TEntity> DbSet;
    public BaseRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
        DbSet = AppDbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> ListAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity> FindById(TKey key)
    {
        return await DbSet.FindAsync(key);
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public bool Exist(TKey id)
    {
        return DbSet.Find(id) != null;
    }
}