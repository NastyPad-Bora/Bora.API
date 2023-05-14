namespace Bora.API.Security.Resources;

public class UserResource
{
    public string? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public long Cellphone { get; set; }
    public DateTime DateExperience { get; set; }
    public DateTime? Birthday { get; set; }
    public String? Description { get; set; }
    public String? Career { get; set; }
    public IList<String> UserRoles { get; set; }
}