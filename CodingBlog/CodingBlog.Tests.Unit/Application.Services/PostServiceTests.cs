using AutoFixture;
using CodingBlog.Application.Services;
using CodingBlog.Domain.Entities;
using CodingBlog.Domain.Repositories;
using CodingBlog.Infrastructure.Notification;
using CodingBlog.Tests.Unit.Setup;
using FluentAssertions;
using FluentResults;
using Moq;
using Xunit;

namespace CodingBlog.Tests.Unit.Application.Services;

public class PostServiceTests : IClassFixture<TestSetup>
{
    private readonly Fixture _fixture;
    private readonly Mock<IPostNotificationService> _postNotificationService;
    private readonly Mock<IPostRepository> _postRepository;
    private readonly PostService _postService;
    private Post _post;

    public PostServiceTests(TestSetup setup)
    {
        _fixture = setup.Fixture;

        _postRepository = new Mock<IPostRepository>();
        _postNotificationService = new Mock<IPostNotificationService>();
        _postService = new PostService(_postRepository.Object, _postNotificationService.Object);

        Setup();
    }

    [Fact]
    public async Task Given_valid_post_should_return_success()
    {
        // Act
        var result = await _postService.Create(_post, default);

        // Assert
        result.Should().BeEquivalentTo(Result.Ok(_post));
    }

    [Fact]
    public async Task Given_valid_post_should_call_post_repository_create()
    {
        // Act
        await _postService.Create(_post, default);

        // Assert
        _postRepository.Verify(p => p.Create(_post, default), Times.Once);
    }

    [Fact]
    public async Task Given_valid_post_should_call_notification_service()
    {
        // Act
        await _postService.Create(_post, default);

        // Assert
        _postNotificationService.Verify(p => p.SendPostNotificationAsync($"New post: {_post.Title}"), Times.Once);
    }

    private void Setup()
    {
        _post = _fixture.Build<Post>()
            .Without(p => p.User)
            .Create();

        _postRepository
            .Setup(repo => repo.Create(It.IsAny<Post>(), default))
            .ReturnsAsync(_post);
    }
}