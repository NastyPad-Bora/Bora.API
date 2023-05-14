using Bora.API.Security.Domain.Model;
using Bora.API.Shared.Domain.Service;

namespace Bora.API.Security.Domain.Service.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(User? resource) : base(resource)
    {
    }

    public UserResponse(string message) : base(message)
    {
    }
}