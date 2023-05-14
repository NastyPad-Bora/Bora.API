using Bora.API.Security.Domain.Enums;
using Bora.API.Security.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Bora.API.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class Authorize : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // For everyone
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        Console.WriteLine($"AllowAnonymouse: {allowAnonymous}");
        if (allowAnonymous)
             return;
        
        // Important Inputs from the HttpContext if is authenticated correctly
        var authorizedUser = (User)context.HttpContext.Items["User"]!; // The user
        var routeParamId = context.RouteData.Values["id"]!.ToString(); //  The RouteUserId
        var authenticatedId = authorizedUser.Id.ToString(); // The AuthenticatedUserId
        var authenticatedRoles = authorizedUser.UserRoles;
        Console.WriteLine($"Null or Empty: {authenticatedRoles.IsNullOrEmpty()}");
        
        
        // Just for admins
        var justAdmin = context.ActionDescriptor.EndpointMetadata.OfType<JustAdminAttribute>().Any();
        if (justAdmin)
        {
            var isAdmin = authenticatedRoles.ToList().Any(role => role.Role.RoleType == RoleType.Admin);
            if (isAdmin)
                return;
            context.Result = new JsonResult(new { message = "Just for administrators." })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        
        // Just for authorized Users
        
        if (authenticatedId == routeParamId)
            return;
        

        context.Result = new JsonResult(new { message = "You are not allowed" })
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}