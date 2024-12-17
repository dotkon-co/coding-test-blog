using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodingBlog.Application.Services;
using CodingBlog.Domain.Entities;
using CodingBlog.Domain.Repositories;
using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CodingBlog.Tests.Unit.Application.Services;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly Mock<IUserRepository> _userRepository;

    public AuthServiceTests()
    {
        _userRepository = new Mock<IUserRepository>();

        var configValues = new Dictionary<string, string>
        {
            { "Jwt:SecretKey", "my_secret_key_have_32_characters" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" }
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

        _authService = new AuthService(_userRepository.Object, _configuration);
    }

    [Fact]
    public async Task Given_user_not_found_should_return_invalid_credentials()
    {
        // Arrange
        const string username = "notfound@example.com";
        const string password = "password123";

        var expectedResult = Result.Fail("Invalid credentials.");

        _userRepository
            .Setup(repo => repo.GetByEmail(username, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        var result = await _authService.Login(username, password, default);

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Given_login_wrong_password_should_return_invalid_credentials()
    {
        // Arrange
        const string username = "user@example.com";
        const string password = "wrongpassword";
        var user = new User
        {
            Id = 1, Username = username, Password = BCrypt.Net.BCrypt.HashPassword("correctpassword"), Role = "User"
        };

        _userRepository
            .Setup(repo => repo.GetByEmail(username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var expectedResult = Result.Fail("Invalid credentials.");

        // Act
        var result = await _authService.Login(username, password, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Given_login_successful_login_should_return_jwt_token()
    {
        // Arrange
        const string username = "user@example.com";
        const string password = "password123";
        var user = new User
            { Id = 1, Username = username, Password = BCrypt.Net.BCrypt.HashPassword(password), Role = "User" };

        _userRepository
            .Setup(repo => repo.GetByEmail(username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _authService.Login(username, password, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(result.Value);

        var expectedToken = new JwtSecurityToken(
            issuer: "TestIssuer",
            audience: "TestAudience",
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            },
            expires: DateTime.Now.AddHours(1)
        );

        jwtToken.Claims.Should().BeEquivalentTo(expectedToken.Claims, options => options
                .Excluding(c => c.Properties)
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1)))
                .WhenTypeIs<DateTime>() // Verifica proximidade de datas
        );
    }

    [Fact]
    public async Task Given_login_should_call_user_repository_get_by_email()
    {
        // Arrange
        const string username = "notfound@example.com";
        const string password = "password123";

        _userRepository
            .Setup(repo => repo.GetByEmail(username, default))
            .ReturnsAsync((User)null);

        await _authService.Login(username, password, default);

        _userRepository.Verify(u => u.GetByEmail(username, default), Times.Once);
    }

    [Fact]
    public async Task Given_existing_email_in_register_user_should_return_fail_result()
    {
        // Arrange
        const string username = "John Doe";
        const string email = "johndoe@example.com";
        const string password = "password123";

        var existingUser = new User(username, email, BCrypt.Net.BCrypt.HashPassword(password), "Editor");

        _userRepository
            .Setup(repo => repo.GetByEmail(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _authService.Register(username, email, password, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result.Fail("This user is already registered."));
    }

    [Fact]
    public async Task Given_non_existing_email_in_registering_user_should_return_success()
    {
        // Arrange
        const string username = "Jane Doe";
        const string email = "janedoe@example.com";
        const string password = "password123";

        _userRepository
            .Setup(repo => repo.GetByEmail(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        // Act
        var result = await _authService.Register(username, email, password, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result.Ok());
    }
}