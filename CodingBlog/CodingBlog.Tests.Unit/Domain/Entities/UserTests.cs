using AutoFixture;
using CodingBlog.Domain.Entities;
using CodingBlog.Tests.Unit.Setup;
using FluentAssertions;
using Xunit;

namespace CodingBlog.Tests.Unit.Domain.Entities;

public class UserTests : IClassFixture<TestSetup>
{
    private readonly Fixture _fixture;

    public UserTests(TestSetup setup)
    {
        _fixture = setup.Fixture;
    }

    [Fact]
    public void Given_call_constructor_should_create_user()
    {
        // Arrange
        var username = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var password = _fixture.Create<string>();
        var role = _fixture.Create<string>();

        // Act
        var user = new User(username, email, password, role);

        // Assert
        user.Username.Should().Be(username);
        user.Email.Should().Be(email);
        user.Password.Should().Be(password);
        user.Role.Should().Be(role);
        user.Posts.Should().BeEmpty();
    }
}