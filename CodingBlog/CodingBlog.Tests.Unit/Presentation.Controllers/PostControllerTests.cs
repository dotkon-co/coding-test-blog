namespace CodingBlog.Tests.Unit.Presentation.Controllers;

using AutoFixture;
using Domain.Entities;
using Infrastructure.Extensions;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Presentation.Controllers.Responses;
using Setup;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using CodingBlog.Application.Services;
using CodingBlog.Presentation.Controllers;
using Moq;
using Xunit;

public class PostControllerTests : IClassFixture<TestSetup>
{
    private readonly PostController _postController;
    private readonly Mock<IPostService> _postService;

    private readonly Fixture _fixture;
    private PostRequest _postRequest;
    private Post _post;
    private PostResponse _postResponse;

    public PostControllerTests(TestSetup setup)
    {
        _fixture = setup.Fixture;


        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _postService = new Mock<IPostService>();

        _postController = new PostController(_postService.Object);

        Setup();
    }

    [Fact]
    public async Task Given_request_should_return_all_posts_response_success()
    {
        // Arrange
        var posts = _fixture.CreateMany<Post>().ToList();
        var expectedResult = posts.MapTo<List<PostResponse>>();

        _postService.Setup(a => a.GetAll(default))
            .ReturnsAsync(posts);

        // Act

        var response = await _postController.GetAllPosts(default);

        // Assert

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Given_get_all_request_should_call_post_service_get_all()
    {
        // Act
        await _postController.GetAllPosts(default);
        
        // Assert
        _postService.Verify( p => p.GetAll(default), Times.Once);
    }

    [Fact]
    public async Task Given_request_should_return_create_posts_response_success()
    {
   
        // Act

        var response = await _postController.CreatePost(_postRequest, default);

        // Assert

        response.Should().BeOfType<CreatedResult>().Which.Value.Should().BeEquivalentTo(_postResponse);
    }

    [Fact]
    public async Task Given_create_request_should_call_post_service_create()
    {
        // Arrange
        var postCallBack = default(Post);
        _postService.Setup(a => a.Create(It.IsAny<Post>(), default))
            .Callback((Post post, CancellationToken _) =>
            {
                postCallBack = post;
            })
            .ReturnsAsync(_post);
        
        // Act
        await _postController.CreatePost(_postRequest, default);
        
        // Assert
        _postService.Verify( p => p.Create(It.IsAny<Post>(), default), Times.Once);
        
        postCallBack.Should().BeEquivalentTo(_post, options =>
            options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation,TimeSpan.FromMilliseconds(1000))).WhenTypeIs<DateTime>()
        );
    }
    
    [Fact]
    public async Task Given_request_update_should_return_update_post_response_success()
    {
   
        // Act

        var response = await _postController.UpdatPost(1, _postRequest, default);

        // Assert

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(_postResponse);
    }
    
    [Fact]
    public async Task Given_request_update_should_fail_result_return_update_post_fail()
    {
        // Arrange
        
        _postService.Setup(a => a.Update(It.IsAny<int>(),It.IsAny<Post>(), default))
            .ReturnsAsync(Result.Fail("Fail"));
   
        // Act

        var response = await _postController.UpdatPost(1, _postRequest, default);

        // Assert

        response.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task Given_update_request_should_call_post_service_update()
    {
        // Arrange
        var postCallBack = default(Post);
        _postService.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Post>(), default))
            .Callback((int _, Post post, CancellationToken _) =>
            {
                postCallBack = post;
            })
            .ReturnsAsync(_post);
        
        // Act
        await _postController.UpdatPost(1, _postRequest, default);
        
        // Assert
        _postService.Verify( p => p.Update(1, It.IsAny<Post>(), default), Times.Once);
        
        postCallBack.Should().BeEquivalentTo(_post, options =>
            options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation,TimeSpan.FromMilliseconds(1000))).WhenTypeIs<DateTime>()
        );
    }
    
    [Fact]
    public async Task Given_request_delete_should_fail_result_return_delete_post_fail()
    {
        // Arrange
        
        _postService.Setup(a => a.Update(It.IsAny<int>(),It.IsAny<Post>(), default))
            .ReturnsAsync(Result.Fail("Fail"));
   
        // Act

        var response = await _postController.UpdatPost(1, _postRequest, default);

        // Assert

        response.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task Given_request_delete_should_return_response_success()
    {
   
        // Act

        var response = await _postController.DeletePost(1,  default);

        // Assert

        response.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task Given_update_request_should_call_post_service_delete()
    {

        // Act
        await _postController.DeletePost(1,  default);
        
        // Assert
        _postService.Verify( p => p.Delete(1,  default), Times.Once);
    
    }

    private void Setup()
    {
        _postRequest = _fixture.Create<PostRequest>();
        _post = _postRequest.MapTo<Post>();
        _postResponse = _post.MapTo<PostResponse>();
        
        _postService.Setup(a => a.Create(It.IsAny<Post>(), default))
            .ReturnsAsync(Result.Ok(_post));
        
        _postService.Setup(a => a.Update(It.IsAny<int>(),It.IsAny<Post>(), default))
            .ReturnsAsync(Result.Ok(_post));
        
        _postService.Setup(a => a.Delete(It.IsAny<int>(), default))
            .ReturnsAsync(Result.Ok());
    }
}