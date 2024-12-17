using CodingBlog.Presentation.Controllers.Requests;
using CodingBlog.Presentation.Models.Requests;
using FluentValidation;

namespace CodingBlog.Presentation.Setups;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PostRequest>, PostRequestValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

        return services;
    }
}