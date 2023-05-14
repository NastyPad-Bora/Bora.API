namespace Bora.API.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JustAdminAttribute : Attribute
{
}