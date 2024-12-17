using AutoMapper;
using CodingBlog.Domain.Entities;
using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Presentation.Controllers.Responses;

namespace CodingBlog.Presentation.Controllers.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<PostRequest, Post>();
        CreateMap<Post, PostResponse>();
    }
}