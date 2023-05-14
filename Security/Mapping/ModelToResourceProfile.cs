using AutoMapper;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Resources;

namespace Bora.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>().ForMember(resource =>
            resource.UserRoles, expression =>
            expression.MapFrom(user =>
                user.UserRoles.Select(role =>
                    role.Role.RoleType.ToString()).ToArray()));
        CreateMap<User, AuthResource>();
    }
}