using Blog.Infra.Data.Contexto;
using Blog.Infra.SignalRService.Hubs;
using Microsoft.EntityFrameworkCore;

namespace Blog.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContexto>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AdicionarCors();
            services.AdicionarIdentity();
            services.AdicionarJwt();
            services.AdicionarControllerComJsonConfig();
            services.AddEndpointsApiExplorer();
            services.AdicionarInjecaoDependecia();
            services.AdicionarSwagger();
            services.AddSignalR();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComandaGo API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapHub<BlogHub>("v1/blog-hub");
            });
        }
    }
}
