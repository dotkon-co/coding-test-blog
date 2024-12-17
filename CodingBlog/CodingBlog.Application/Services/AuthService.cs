using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodingBlog.Domain.Entities;
using CodingBlog.Domain.Repositories;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CodingBlog.Application.Services;

public interface IAuthService
{
    Task<Result> Register(string username, string email, string password, CancellationToken cancellationToken);
    Task<Result<string>> Login(string username, string password, CancellationToken cancellationToken);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<Result> Register(string username, string email, string password,
        CancellationToken cancellationToken)
    {
        var saveUser = await _userRepository.GetByEmail(email, cancellationToken);
        if (saveUser is not null)
            return Result.Fail("This user is already registered.");

        var user = new User(username, email, BCrypt.Net.BCrypt.HashPassword(password), "Editor");

        await _userRepository.Create(user, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<string>> Login(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(username, cancellationToken);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            return Result.Fail("Invalid credentials.");

        return Result.Ok(GenerateJwtToken(user));
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new("roles", user.Role),
            new("username", user.Username),
            new("userId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}