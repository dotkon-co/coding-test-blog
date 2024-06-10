namespace Blog.Application.Services
{
    public interface ITokenService
    {
        string GerarToken(Guid usuarioId, string email, IList<string> usuarioPerfil);
    }
}
