namespace Bora.API.Security.Exceptions;

public class AppException : Exception
{
    public AppException(string? message) : base(message)
    {
    }
}