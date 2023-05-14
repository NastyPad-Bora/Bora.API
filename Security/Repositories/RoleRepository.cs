using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Repository;
using Bora.API.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bora.API.Security.Repositories;

public class RoleRepository: IRoleRepository
{
    private readonly AppDbContext AppDbContext;
    
    public RoleRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public async Task<Role> FindById(int id)
    {
        return await AppDbContext.Roles.FindAsync(id);
    }

    public async Task<IEnumerable<Role>> ListAll()
    {
        return await AppDbContext.Roles.ToListAsync();
    }
}