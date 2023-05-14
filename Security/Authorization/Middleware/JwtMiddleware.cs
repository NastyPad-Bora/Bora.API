using Bora.API.Security.Authorization.Handlers.Interfaces;
using Bora.API.Security.Authorization.Settings;
using Bora.API.Security.Domain.Service;
using Microsoft.Extensions.Options;

namespace Bora.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly AppSettings _appSettings;
    private readonly RequestDelegate _next;

    public JwtMiddleware(IOptions<AppSettings> appSettings, RequestDelegate next)
    {
        _appSettings = appSettings.Value;
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IUserService userService, IJwtHandler jwtHandler)
    {
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); // From the authorization input (the icon with a 'locker')
        if (token != null)
        {
            var userId = jwtHandler.ValidateToken(token);
            if (userId != null)
            {
                httpContext.Items["User"] = (await userService.FindByIdAsync(userId.Value)).Resource;
            }
        }
        await _next(httpContext);
    }
}