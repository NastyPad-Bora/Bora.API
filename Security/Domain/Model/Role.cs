using System.ComponentModel.DataAnnotations.Schema;
using Bora.API.Security.Domain.Enums;
using Bora.API.Security.Domain.Model.Intermediate;

namespace Bora.API.Security.Domain.Model;

public class Role
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(30)")] public RoleType RoleType { get; set; }

    public IList<UserRole> UserRoles { get; set; }
}