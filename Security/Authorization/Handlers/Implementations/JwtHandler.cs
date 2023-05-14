using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bora.API.Security.Authorization.Handlers.Interfaces;
using Bora.API.Security.Authorization.Settings;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bora.API.Security.Authorization.Handlers.Implementations;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;

    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public Guid? ValidateToken(string token)
    {
        if (token.IsNullOrEmpty())
            return null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);

        // Validate Token
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration with no delay
                ClockSkew = TimeSpan.FromSeconds(5)
            }, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = new Guid(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            return userId;
        }
        catch (Exception e)
        {
            throw new AppException(e.Message);
        }
    }

    public string GenerateToken(User validatedUser)
    {
        var secret = _appSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret!);

        // RoleClaims
        List<Claim> roleClaims = new List<Claim>();
            
        // Set claims with user roles.
        validatedUser.UserRoles.ToList().ForEach(role =>
            roleClaims.Add(new Claim(ClaimTypes.Role, role.Role.RoleType.ToString()))
        );
        

        // This is the big deal.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, validatedUser.Id.ToString()!),
                new Claim(ClaimTypes.Name, validatedUser.Username!),
                new Claim(ClaimTypes.Email, validatedUser.Email!),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };
        tokenDescriptor.Subject.AddClaims(roleClaims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}