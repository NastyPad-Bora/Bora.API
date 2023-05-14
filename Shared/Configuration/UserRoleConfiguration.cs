using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Model.Intermediate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bora.API.Shared.Configuration;

public class UserRoleConfiguration  : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
            new UserRole
            {
               RoleId = 122410,
               UserId = new Guid("ed159590-32de-4828-a20b-61c0b026b556"),
            },
            new UserRole
            {
                RoleId = 1,
                UserId = new Guid("e9651cd2-f4b5-4bd4-c4eb-08db52bd364f"),
            },
            new UserRole
            {
                RoleId = 2,
                UserId = new Guid("6dbc9cb6-8623-4f64-b984-4dc51fa2ed98"),
            });
    }
}