using AutoFixture;
using CodingBlog.Presentation.Models.Requests;
using CodingBlog.Tests.Unit.Setup;
using FluentValidation.TestHelper;
using Xunit;

namespace CodingBlog.Tests.Unit.Presentation.Models.Requests;

public class RegisterRequestValidatorTests : IClassFixture<TestSetup>
{
    private readonly Fixture _fixture;
    private readonly RegisterRequestValidator _validator;

    public RegisterRequestValidatorTests(TestSetup setup)
    {
        _fixture = setup.Fixture;
        _validator = new RegisterRequestValidator();
    }

    [Fact]
    public void Given_email_and_password_valid_should_validate_successfully()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Username = _fixture.Create<string>(),
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
        var request = new RegisterRequest
        {
            Username = _fixture.Create<string>(),
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
        var request = new RegisterRequest
        {
            Username = _fixture.Create<string>(),
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
        var request = new RegisterRequest
        {
            Username = _fixture.Create<string>(),
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
    public void Given_username_is_empty_should_fail_validation()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Username = string.Empty,
            Email = _fixture.Create<string>() + "@example.com",
            Password = _fixture.Create<string>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Username)
            .WithErrorMessage("'Username' must not be empty.");
    }


    [Fact]
    public void Given_email_and_password_and_username_empty_should_fail_validation()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Username = string.Empty,
            Email = string.Empty,
            Password = string.Empty
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Username)
            .WithErrorMessage("'Username' must not be empty.");

        result.ShouldHaveValidationErrorFor(r => r.Email)
            .WithErrorMessage("'Email' must not be empty.");

        result.ShouldHaveValidationErrorFor(r => r.Password)
            .WithErrorMessage("'Password' must not be empty.");
    }
}