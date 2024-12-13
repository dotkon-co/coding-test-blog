namespace CodingBlog.Presentation.Controllers;

using Requests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        var token = _authService.Register(request.Username, request.Password);
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        var token = _authService.Login(model.Username, model.Password);
        return Ok(new { Token = token });
    }
}