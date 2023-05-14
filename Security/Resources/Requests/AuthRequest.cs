using System.ComponentModel.DataAnnotations;
using Bora.API.Security.Domain.Enums;

namespace Bora.API.Security.Resources;

public class AuthRequest
{
    [Required] public string? UsernameOrEmail { get; set; }
    [Required] public string? Password { get; set; }
}