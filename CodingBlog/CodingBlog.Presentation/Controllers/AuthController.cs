using System.Net;
using CodingBlog.Application.Services;
using CodingBlog.Infrastructure.Extensions;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Presentation.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CodingBlog.Presentation.Controllers;

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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RegisterRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.Register(request.Username, request.Email, request.Password, cancellationToken);

        if (result.IsFailed)
            return BadRequest(string.Join("; ", result.Errors.Select(e => e.Message)).CreateValidationError());

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model, CancellationToken cancellationToken)
    {
        var result = await _authService.Login(model.Email, model.Password, cancellationToken);
        if (result.IsFailed)
            return Unauthorized(string.Join("; ", result.Errors.Select(e => e.Message))
                .CreateValidationError(HttpStatusCode.Unauthorized));

        return Ok(new { Token = result.Value });
    }
}