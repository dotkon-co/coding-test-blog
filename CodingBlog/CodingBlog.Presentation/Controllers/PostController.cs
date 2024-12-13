namespace CodingBlog.Presentation.Controllers;

using Responses;
using Infrastructure.Extensions;
using Requests;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
     =>_postService = postService;

    [HttpGet]
    [ProducesResponseType(typeof(Post[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAll();
        return Ok(posts);
    }

    [Authorize(Roles = "Admin, Editor")]
    [HttpPost]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(PostRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
    {
        var post = request.MapTo<Post>();
        var createdPost = await _postService.Create(post);
        return Created("",createdPost.Value.MapTo<PostResponse>());
    }

    [Authorize(Roles = "Admin, Editor")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PostRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditPost([FromRoute] int id, [FromBody] PostRequest request)
    {
        var post = request.MapTo<Post>();
        var updatedPost = await _postService.Update(id, post);
        if (updatedPost.IsFailed)
            return NotFound();

        return Ok(updatedPost);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType( StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePost([FromRoute] int id)
    {
        var result = await _postService.Delete(id);
        if (result.IsFailed)
            return NotFound();

        return NoContent();
    }
}