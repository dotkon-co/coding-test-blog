using CodingBlog.Domain.Repositories;
using CodingBlog.Infrastructure.EntityFramework.Repositories;
using CodingBlog.Infrastructure.Notification;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CodingBlog.Infrastructure.Setups;

using EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

public static class InfrastructureDependencies
{
    public static IServiceCollection
        AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddEntityFrameWork(configuration)
            .AddNotificationServices(configuration)
            .ConfigureServiceLocator();

    private static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSignalR();
        
        services.AddScoped<IPostNotificationService, PostNotificationService>();

        return services;
    }

    private static IServiceCollection AddEntityFrameWork(this IServiceCollection services, IConfiguration configuration)
    {        
        var postgresDatabaseSettings = configuration.GetSection("Databases:Postgres");

        var connectionString = postgresDatabaseSettings["ConnectionString"];
        var retryCount = Convert.ToInt32(postgresDatabaseSettings["ConnectionRetryCount"]);
        var retryDelay = Convert.ToInt32(postgresDatabaseSettings["ConnectionRetryDelay"]);

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<PostgresDBContext>(opt =>
        {
            opt.UseNpgsql(dataSource, (npgSqlOptions) =>
            {
                npgSqlOptions.EnableRetryOnFailure(
                    maxRetryCount: retryCount,
                    maxRetryDelay: TimeSpan.FromSeconds(retryDelay),
                    errorCodesToAdd: null);
            });
            opt.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}