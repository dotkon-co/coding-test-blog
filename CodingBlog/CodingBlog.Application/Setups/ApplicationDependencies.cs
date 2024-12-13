namespace CodingBlog.Application.Setups;

using Services;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}