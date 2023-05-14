
using Bora.API.Security.Domain.Model;

namespace Bora.API.Security.Domain.Repository;

public interface IRoleRepository
{
    Task<Role> FindById(int id);
    Task<IEnumerable<Role>> ListAll();
}