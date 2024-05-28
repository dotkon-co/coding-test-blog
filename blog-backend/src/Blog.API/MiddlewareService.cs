using Blog.Application.Handlers;
using Blog.Application.Hubs;
using Blog.Application.Queries.PostagemQueries;
using Blog.Application.Repositories;
using Blog.Application.Services;
using Blog.Core.Utilitarios.UsuarioHttpContext;
using Blog.Domain.Entidades.ControleAcesso;
using Blog.Infra.Data.Contexto;
using Blog.Infra.Data.Repositories;
using Blog.Infra.SignalRService.Hubs;
using Blog.Infra.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Text;

namespace Blog.API
{
    public static class MiddlewareService
    {
        public static void AdicionarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blog API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void AdicionarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetIsOriginAllowed(origin => true);
                });
            });
        }

        public static void AdicionarIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, Perfil>()
                .AddRoles<Perfil>()
                .AddEntityFrameworkStores<BlogContexto>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
            });
        }

        public static void AdicionarControllerComJsonConfig(this IServiceCollection services) =>
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        public static void AdicionarJwt(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConfiguracao.Segredo)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        public static void AdicionarInjecaoDependecia(this IServiceCollection services)
        {
            services.AddScoped<UsuarioCommandHandler, UsuarioCommandHandler>();
            services.AddScoped<PostagemCommandHandler, PostagemCommandHandler>();

            services.AddScoped<IPostagemRepository, PostagemRepository>();

            services.AddScoped<IPostagemQuery, PostagemQuery>();

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUsuarioLogado, UsuarioLogado>();

            services.AddTransient<IBlogHub, BlogHub>();
        }
    }
}
