using AutoFixture;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Tests.Unit.Setup;
using FluentValidation.TestHelper;
using Xunit;

namespace CodingBlog.Tests.Unit.Presentation.Models.Requests;

public class LoginRequestValidatorTests : IClassFixture<TestSetup>
{
    private readonly Fixture _fixture;
    private readonly LoginRequestValidator _validator;

    public LoginRequestValidatorTests(TestSetup setup)
    {
        _fixture = setup.Fixture;
        _validator = new LoginRequestValidator();
    }

    [Fact]
    public void Given_email_and_password_valid_should_validate_successfully()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = _fixture.Create<string>() + "@example.com",
            Password = _fixture.Create<string>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_email_empty_should_fail_validation()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = string.Empty,
            Password = _fixture.Create<string>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Email)
            .WithErrorMessage("'Email' must not be empty.");
    }

    [Fact]
    public void Given_email_is_invalid_should_fail_validation()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "email_invalido",
            Password = _fixture.Create<string>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Email)
            .WithErrorMessage("'Email' is not a valid email address.");
    }

    [Fact]
    public void Given_password_is_empty_should_fail_validation()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = _fixture.Create<string>() + "@example.com",
            Password = string.Empty
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Password)
            .WithErrorMessage("'Password' must not be empty.");
    }

    [Fact]
    public void Given_email_and_password_empty_should_fail_validation()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = string.Empty,
            Password = string.Empty
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Email)
            .WithErrorMessage("'Email' must not be empty.");

        result.ShouldHaveValidationErrorFor(r => r.Password)
            .WithErrorMessage("'Password' must not be empty.");
    }
}