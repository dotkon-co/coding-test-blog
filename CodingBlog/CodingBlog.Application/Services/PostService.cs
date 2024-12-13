namespace CodingBlog.Application.Services;


using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Notification;
using FluentResults;

public interface IPostService
{
    Task<List<Post>> GetAll();
    Task<Result<Post>> Create(Post post);
    Task<Result<Post>> Update(int id, Post post);
    Task<Result> Delete(int id);
}

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IPostNotificationService _postNotificationService;

    public PostService(IPostRepository postRepository, IPostNotificationService postNotificationService)
    {
        _postRepository = postRepository;
        _postNotificationService = postNotificationService;
    }

    public async Task<List<Post>> GetAll()
        => await _postRepository.GetAll();

    public async Task<Result<Post>> Create(Post post)
    {
        var createdPost = await _postRepository.Create(post);
        await _postNotificationService.SendPostNotificationAsync($"New post: {createdPost.Title}");
        return Result.Ok(createdPost);
    }

    public async Task<Result<Post>> Update(int id, Post post)
    {
        var result = await _postRepository.Update(id, post);

        return result is null
            ? Result.Fail("Not Update Post")
            : Result.Ok(result);
    }

    public async Task<Result> Delete(int id)
    {
        var result = await _postRepository.Delete(id);

        return result ? Result.Ok() : Result.Fail("Note Delete Post");
    }
}