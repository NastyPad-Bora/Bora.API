using Bora.API.Security.Domain.Enums;
using Bora.API.Security.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bora.API.Shared.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = 1,
                RoleType = RoleType.Worker
            },
            new Role
            {
                Id = 2,
                RoleType = RoleType.Client
            },
            new Role
            {
                Id = 122410,
                RoleType = RoleType.Admin
            });
    }
}