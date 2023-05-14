namespace Bora.API.Security.Domain.Model.Intermediate;

public class UserRole
{
    // Foreign Key
    public Guid UserId { get; set; }
    public User User { get; set; }

    // Foreign Key
    public int RoleId { get; set; }
    public Role Role { get; set; }
}