using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Bora.API.Security.Exceptions;

namespace Bora.API.Security.Authorization.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            // We set the input and output format from the HttpContext to JSON. The httpContext.Response is what your api result from a request.
            var response = httpContext.Response; 
            response.ContentType = MediaTypeNames.Application.Json;
            Console.WriteLine(exception.Message);

            switch (exception)
            {
                case AppException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = $"ErrorHandler says: {exception.Message}" });
            await response.WriteAsync(result);
        }
    }
}