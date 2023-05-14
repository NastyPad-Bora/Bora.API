using Bora.API.Shared.Domain.Repository;

namespace Bora.API.Shared.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext AppDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public async Task CompleteAsync()
    {
        await AppDbContext.SaveChangesAsync();
    }
}