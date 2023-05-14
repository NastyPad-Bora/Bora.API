using AutoMapper;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Resources;

namespace Bora.API.Security.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        // User
        CreateMap<AuthRequest, User>();
        CreateMap<RegisterRequest, User>();
        CreateMap<AuthResource, User>();
    }
}