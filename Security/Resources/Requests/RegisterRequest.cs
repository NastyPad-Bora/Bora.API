using System.ComponentModel.DataAnnotations;
using Bora.API.Security.Domain.Enums;

namespace Bora.API.Security.Resources;

public class RegisterRequest
{
    [Required]
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Password { get; set; }
    public long Cellphone { get; set; }
    public Sex Sex { get; set; }
    public DateTime DateExperience { get; set; }
    public DateTime? Birthday { get; set; }
    public String? Description { get; set; }
    public String? Career { get; set; }
}