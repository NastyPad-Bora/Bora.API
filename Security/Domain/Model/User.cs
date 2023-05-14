using System.ComponentModel.DataAnnotations.Schema;
using Bora.API.Security.Domain.Enums;
using Bora.API.Security.Domain.Model.Intermediate;

namespace Bora.API.Security.Domain.Model;

public class User
{
    public Guid? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? HashedPassword { get; set; }
    public long Cellphone { get; set; }
    [Column(TypeName = "varchar(30)")] public Sex Sex { get; set; }
    public DateTime DateExperience { get; set; }
    public DateTime? Birthday { get; set; }
    public String? Description { get; set; }
    public String? Career { get; set; }
    
    // From Minor Cardinality
    public IList<UserRole> UserRoles { get; set; }


    //
    // public long? CompanyId;
    
}