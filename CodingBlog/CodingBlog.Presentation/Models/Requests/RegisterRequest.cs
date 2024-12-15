using FluentValidation;

namespace CodingBlog.Presentation.Models.Requests;

public class RegisterRequest
{
    public string Username { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}