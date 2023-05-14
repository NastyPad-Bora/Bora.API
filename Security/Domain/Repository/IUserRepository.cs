using Bora.API.Security.Domain.Model;
using Bora.API.Shared.Domain.Repository;

namespace Bora.API.Security.Domain.Repository;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    Task SetRolesFromUser(Guid id);
    Task<bool> ExistByUsername(string username);
    Task<bool> ExistByEmail(string email);
    Task<User> FindByUsernameOrEmail(string usernameOrEmail);
}