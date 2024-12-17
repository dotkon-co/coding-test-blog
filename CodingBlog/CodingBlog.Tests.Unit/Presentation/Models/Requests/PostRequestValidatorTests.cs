using AutoFixture;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Tests.Unit.Setup;
using FluentValidation.TestHelper;
using Xunit;

namespace CodingBlog.Tests.Unit.Presentation.Models.Requests;

public class PostRequestValidatorTests : IClassFixture<TestSetup>
{
    private readonly Fixture _fixture;
    private readonly PostRequestValidator _validator;

    public PostRequestValidatorTests(TestSetup setup)
    {
        _fixture = setup.Fixture;
        _validator = new PostRequestValidator();
    }

    [Fact]
    public void Given_email_and_password_valid_should_validate_successfully()
    {
        // Arrange
        var request = new PostRequest
        {
            Title = _fixture.Create<string>(),
            Content = _fixture.Create<string>(),
            UserId = _fixture.Create<int>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_content_empty_should_fail_validation()
    {
        // Arrange
        var request = new PostRequest
        {
            Title = _fixture.Create<string>(),
            Content = string.Empty,
            UserId = _fixture.Create<int>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Content)
            .WithErrorMessage("'Content' must not be empty.");
    }


    [Fact]
    public void Given_title_is_empty_should_fail_validation()
    {
        // Arrange
        var request = new PostRequest
        {
            Title = string.Empty,
            Content = _fixture.Create<string>(),
            UserId = _fixture.Create<int>()
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorMessage("'Title' must not be empty.");
    }

    [Fact]
    public void Given_user_id_is_empty_should_fail_validation()
    {
        // Arrange
        var request = new PostRequest
        {
            Title = _fixture.Create<string>(),
            Content = _fixture.Create<string>(),
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.UserId)
            .WithErrorMessage("'User Id' must not be empty.");
    }


    [Fact]
    public void Given_title_and_content_and_userid_empty_should_fail_validation()
    {
        // Arrange
        var request = new PostRequest
        {
            Title = string.Empty,
            Content = string.Empty,
            UserId = 0
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorMessage("'Title' must not be empty.");

        result.ShouldHaveValidationErrorFor(r => r.Content)
            .WithErrorMessage("'Content' must not be empty.");

        result.ShouldHaveValidationErrorFor(r => r.UserId)
            .WithErrorMessage("'User Id' must not be empty.");
    }
}