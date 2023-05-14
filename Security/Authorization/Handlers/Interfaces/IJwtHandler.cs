using Bora.API.Security.Domain.Model;

namespace Bora.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public Guid? ValidateToken(string token);
    public string GenerateToken(User validatedUser);
}