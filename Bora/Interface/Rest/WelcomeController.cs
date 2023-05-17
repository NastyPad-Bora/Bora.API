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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public WelcomeController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult SayHello()
    {
        string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Assets", "index.html");
        string htmlContent = System.IO.File.ReadAllText(filePath);
        return Content(htmlContent, "text/html");
    }
    
}