using FluentValidation;

namespace CodingBlog.Presentation.Controllers.Requests;

public class PostRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
}

public class PostRequestValidator : AbstractValidator<PostRequest>
{
    public PostRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}