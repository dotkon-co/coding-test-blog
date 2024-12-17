using CodingBlog.Application.Setups;
using CodingBlog.Infrastructure.Notification;
using CodingBlog.Infrastructure.Setups;
using CodingBlog.Presentation.Middlewares;
using FluentValidation.AspNetCore;

namespace CodingBlog.Presentation.Setups;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCors()
            .AddControllers();

        services.AddFluentValidationAutoValidation()
            .AddAuthentication()
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwagger()
            .AddValidators()
            .AddAutoMapper();

        //   services.AddHealthChecks(Configuration);


        services
            .AddApplication()
            .AddInfrastructure(Configuration);
    }

    public void Configure(WebApplication app)
    {
        app
            .UseRouting();

        app.UseWebSockets();

        app
            .UseCors("CorsPolicy")
            .UseMiddleware<ExceptionHandlerMiddleware>()
            .UseSwagger()
            .UseSwaggerUI()
            //    .UseHttpsRedirection()
            .UseAuthentication()
            //   .UseHealthChecks(Configuration)
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        app.MapHub<PostHub>("/posthub",
            options => { options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; });
    }
}