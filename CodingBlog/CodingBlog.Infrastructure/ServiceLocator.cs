namespace CodingBlog.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceLocator
{
    static IServiceProvider? ServiceProvider { get; set; }

    public static IServiceCollection ConfigureServiceLocator(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();

        return services;
    }

    public static TService? GetService<TService>()
    {
        if (ServiceProvider is null)
            throw new NullReferenceException("Service provider should be initialized prior to locating services!");

        return ServiceProvider.GetService<TService>();
    }
}