namespace CodingBlog.Presentation.Controllers.Mappers;

using AutoMapper;
using Domain.Entities;
using Requests;
using Responses;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<PostRequest, Post>();
        CreateMap<Post, PostResponse>();
    }
}