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

    [Fact]
    public async Task Given_get_all_should_return_list_Of_posts()
    {
        // Arrange
        var expectedPosts = _fixture
            .Build<Post>()
            .Without(p => p.User)
            .CreateMany(5)
            .ToList();

        _postRepository
            .Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPosts);

        // Act
        var result = await _postService.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedPosts);
        _postRepository.Verify(repo => repo.GetAll(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Given_get_all_should_return_empty_list_when_repository_returns_empty_list()
    {
        // Arrange
        var expectedPosts = new List<Post>();

        _postRepository
            .Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPosts);

        // Act
        var result = await _postService.GetAll(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Given_update_should_return_ok_result()
    {
        // Arrange
        var postId = _fixture.Create<int>();
        var postToUpdate = _fixture
            .Build<Post>()
            .Without(p => p.User)
            .Create();

        var updatedPost = _fixture
            .Build<Post>()
            .Without(p => p.User)
            .Create();

        var expectResult = Result.Ok(updatedPost);

        _postRepository
            .Setup(repo => repo.Update(postId, postToUpdate, default))
            .ReturnsAsync(updatedPost);

        // Act
        var result = await _postService.Update(postId, postToUpdate, default);

        // Assert
        result.Should().BeEquivalentTo(expectResult);
    }

    [Fact]
    public async Task Given_update_with_post_is_not_updated_should_return_fail_result()
    {
        // Arrange
        var postId = _fixture.Create<int>();
        var postToUpdate = _fixture
            .Build<Post>()
            .Without(p => p.User)
            .Create();

        var expectResult = Result.Fail("Not Update Post");

        _postRepository
            .Setup(repo => repo.Update(postId, postToUpdate, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Post)null);

        // Act
        var result = await _postService.Update(postId, postToUpdate, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectResult);
    }

    [Fact]
    public async Task Given_delete_should_return_ok_result()
    {
        // Arrange
        var postId = _fixture.Create<int>();

        _postRepository
            .Setup(repo => repo.Delete(postId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var expectedResult = Result.Ok();

        // Act
        var result = await _postService.Delete(postId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Given_delete_with_post_is_not_deleted_should_return_fail_result()
    {
        // Arrange
        var postId = _fixture.Create<int>();

        _postRepository
            .Setup(repo => repo.Delete(postId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var expectedResult = Result.Fail("Note Delete Post");

        // Act
        var result = await _postService.Delete(postId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
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