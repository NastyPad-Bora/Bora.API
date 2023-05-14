using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Repository;
using Bora.API.Shared.Persistence;
using Bora.API.Shared.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bora.API.Security.Repositories;

public class UserRepository: BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public bool Exist(string id)
    {
        throw new NotImplementedException();
    }

    public async Task SetRolesFromUser(Guid id)
    {
        var user = await base.FindById(id);
        user.UserRoles = await AppDbContext.UserRoles
            .Include(userRole => userRole.Role) // You set any Roles value with their respective id.
            .Where(userRole => userRole.UserId == id)
            .ToListAsync();
    }

    public async Task<bool> ExistByUsername(string username)
    {
        return await AppDbContext.Users.AnyAsync(user => user.Username == username);
    }

    public async Task<bool> ExistByEmail(string email)
    {
        return await AppDbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User> FindByUsernameOrEmail(string usernameOrEmail)
    {
        return await AppDbContext.Users.FirstOrDefaultAsync(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
    }
    
}
