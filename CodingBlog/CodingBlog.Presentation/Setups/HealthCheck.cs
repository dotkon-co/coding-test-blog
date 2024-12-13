namespace CodingBlog.Presentation.Setups;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Serilog;

public static class HealthCheck
{
    private static readonly string[] tags = new[] { "healthcheck", "database" };

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection("Databases:Postgres");
        var npgsqlConnectionString = databaseSettings["ConnectionString"];


        services
            .AddHealthChecks()
            .AddNpgSql(
                connectionString: npgsqlConnectionString,
                name: "blog-db",
                tags: tags,
                failureStatus: HealthStatus.Unhealthy);

        return services;
    }

    public static IApplicationBuilder UseHealthChecks(
        this IApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.UseHealthChecks(
            configuration["Api:Routes:Heartbeat"],
            new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = HealthResponseWriter,
            });

        // builder.UseHealthChecks(
        //     configuration["Api:Routes:HealthCheck"],
        //     new HealthCheckOptions
        //     {
        //         Predicate = _ => true,
        //         ResponseWriter = HealthResponseWriter,
        //     });

        return builder;
    }

    private static Task HealthResponseWriter(HttpContext httpContext, HealthReport result) =>
        WriteResponseFor(httpContext, result);

    private static Task WriteResponseFor(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "application/json";

        if (result.Status != HealthStatus.Healthy)
            Log.Warning("Application is not healthy");

        return httpContext.Response.WriteAsync(
            JsonConvert.SerializeObject(result, Formatting.Indented));
    }
}