using AutoMapper;
using Bora.API.Security.Authorization.Attributes;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Service;
using Bora.API.Security.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bora.API.Security.Interface.Rest;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[SwaggerTag("User registration, authentication and data update.")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> FindUserById(Guid id)
    {
        var result = await _userService.FindByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var mappedResult = _mapper.Map<User, UserResource>(result.Resource!);
        return Ok(mappedResult);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var result = await _userService.Register(registerRequest);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(new { message = "Successfully registered." });
    }
    
    [AllowAnonymous]
    [HttpPost("auth")]
    public async Task<IActionResult> Authentication(AuthRequest authRequest)
    {
        var result = await _userService.Auth(authRequest);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(new { message = "Successfully authenticated.", resource = result.Resource });
    }
}