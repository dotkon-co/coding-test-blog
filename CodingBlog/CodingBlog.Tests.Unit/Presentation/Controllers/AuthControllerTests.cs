using AutoFixture;
using CodingBlog.Application.Services;
using CodingBlog.Presentation.Controllers;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Presentation.Models.Requests;
using CodingBlog.Tests.Unit.Setup;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CodingBlog.Tests.Unit.Presentation.Controllers;

public class AuthControllerTests : IClassFixture<TestSetup>
{
    private readonly AuthController _authController;
    private readonly Mock<IAuthService> _authService;
    private readonly Fixture _fixture;
    private LoginRequest _loginRequest;
    private RegisterRequest _request;

    public AuthControllerTests(TestSetup setup)
    {
        _fixture = setup.Fixture;

        _authService = new Mock<IAuthService>();
        _authController = new AuthController(_authService.Object);

        Setup();
    }

    [Fact]
    public async Task Given_register_request_should_return_success()
    {
        // Act
        var response = await _authController.Register(_request, default);

        // Assert
        response.Should().BeOfType<CreatedResult>();
    }

    [Fact]
    public async Task Given_register_request_with_username_created_should_return_fail()
    {
        // Arrange

        _authService.Setup(a => a.Register(_request.Username, _request.Email, _request.Password, default))
            .ReturnsAsync(Result.Fail("Invalid"));

        // Act
        var response = await _authController.Register(_request, default);

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Given_register_request_should_call_auth_service_register()
    {
        // Act
        var response = await _authController.Register(_request, default);

        // Assert
        _authService.Verify(p => p.Register(_request.Username, _request.Email, _request.Password, default), Times.Once);
    }

    [Fact]
    public async Task Given_login_request_should_return_success()
    {
        // Act
        var response = await _authController.Login(_loginRequest, default);

        // Assert
        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Given_login_request_with_non_existent_user_should_return_fail()
    {
        // Arrange
        _authService.Setup(a => a.Login(_loginRequest.Email, _loginRequest.Password, default))
            .ReturnsAsync(Result.Fail("Invalid"));

        var expectedResult = new ProblemDetails
        {
            Type = "Validation",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status401Unauthorized,
            Detail = "Invalid",
        };

        // Act
        var response = await _authController.Login(_loginRequest, default);

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>().Which.Value.Should().BeEquivalentTo(expectedResult);
        ;
    }

    [Fact]
    public async Task Given_login_request_should_call_auth_login()
    {
        // Act
        await _authController.Login(_loginRequest, default);

        // Assert
        _authService.Verify(p => p.Login(_loginRequest.Email, _loginRequest.Password, default), Times.Once);
    }

    private void Setup()
    {
        _request = _fixture.Create<RegisterRequest>();
        _loginRequest = new LoginRequest { Email = "email@email.com", Password = "1234" };

        _authService.Setup(a => a.Register(_request.Username, _request.Email, _request.Password, default))
            .ReturnsAsync(Result.Ok());

        _authService.Setup(a => a.Login(_loginRequest.Email, _loginRequest.Password, default))
            .ReturnsAsync(Result.Ok());
    }
}