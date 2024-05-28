using Microsoft.AspNetCore.Http;

namespace Blog.Core.Utilitarios.UsuarioHttpContext;

public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _acessor;

    public UsuarioLogado(IHttpContextAccessor acessor)
    {
        _acessor = acessor;
    }

    public string? ObterToken()
    {
        return _acessor.HttpContext.ObterBearerToken();
    }

    public Guid ObterId()
    {
        var id = _acessor.HttpContext.ObterUsuarioId();
        return id != null ? Guid.Parse(id) : Guid.Empty;
    }

    public string? ObterPerfil()
    {
        return _acessor.HttpContext.ObterPerfil();
    }
}
