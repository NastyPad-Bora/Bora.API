using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Service.Communication;
using Bora.API.Security.Resources;

namespace Bora.API.Security.Domain.Service;

public interface IUserService
{
    Task<IEnumerable<User>> ListAll();
    Task<UserResponse> FindByIdAsync(Guid id);
    Task<AuthResponse> Auth(AuthRequest authRequest);
    Task<UserResponse> Register(RegisterRequest registerRequest);
    Task<UserResponse> Update(User updatedUser);
}