namespace CodingBlog.Presentation.Controllers.Requests;

using FluentValidation;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}