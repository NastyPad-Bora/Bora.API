using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bora.API.Bora.Interface.Rest;


[ApiController]
[Route("/api/v1")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Hello")]
public class WelcomeController : ControllerBase
{
    public WelcomeController()
    {
    }

    [HttpGet]
    public String SayHello()
    {
        return "Hello!";
    }
}