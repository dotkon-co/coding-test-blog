using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Blog.Core.Utilitarios.UsuarioHttpContext;

public static class HttpContextExtensions
{
    public static bool UsuarioEstaAutenticado(this HttpContext httpContext)
    {
        return httpContext.User.Identity is { IsAuthenticated: true };
    }

    public static string? ObterUsuarioId(this HttpContext? httpContext)
    {
        var claim = httpContext?.User?.FindFirst(ClaimTypes.Name);
        return claim?.Value;
    }

    public static string? ObterBearerToken(this HttpContext httpContext)
    {
        if (!httpContext.UsuarioEstaAutenticado())
            return null;

        var token = httpContext.Request.Headers[HeaderNames.Authorization].ToString();
        return token.Replace("Bearer", string.Empty).Trim();
    }

    public static string? ObterPerfil(this HttpContext httpContext)
    {
        if (!httpContext.UsuarioEstaAutenticado())
            return null;

        var claim = httpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        return claim?.Value;
    }
}
