using Bora.API.Security.Resources;
using Bora.API.Shared.Domain.Service;

namespace Bora.API.Security.Domain.Service.Communication;

public class AuthResponse : BaseResponse<AuthResource>
{
    public AuthResponse(AuthResource? resource) : base(resource)
    {
    }

    public AuthResponse(string message) : base(message)
    {
    }
}