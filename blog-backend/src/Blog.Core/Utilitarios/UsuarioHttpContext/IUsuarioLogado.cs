namespace Blog.Core.Utilitarios.UsuarioHttpContext;

public interface IUsuarioLogado
{
    Guid ObterId();

    string? ObterToken();

    string? ObterPerfil();
}
