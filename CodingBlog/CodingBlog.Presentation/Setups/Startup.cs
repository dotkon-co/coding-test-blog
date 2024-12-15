using CodingBlog.Application.Setups;
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
            .AddControllers();

        services.AddFluentValidationAutoValidation()
            .AddAuthentication()
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwagger()
            .AddValidators()
            .AddAutoMapper();
        // services.AddHealthChecks(Configuration);

        services
            .AddApplication()
            .AddInfrastructure(Configuration);
    }

    public void Configure(IApplicationBuilder app)
    {
        app
            .UseRouting()
            // .UseHealthChecks(Configuration)
            .UseMiddleware<ExceptionHandlerMiddleware>()
            .UseSwagger()
            .UseSwaggerUI()
            .UseHttpsRedirection()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}