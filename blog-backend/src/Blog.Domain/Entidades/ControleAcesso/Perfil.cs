using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entidades.ControleAcesso
{
    public class Perfil : IdentityRole<Guid>
    {
        private readonly List<UsuarioPerfil> _usuarioPerfis = new();
        public IEnumerable<UsuarioPerfil> UsuarioPerfis => _usuarioPerfis;

        private readonly List<PerfilPermissao> _perfilPermissoes = new();
        public IEnumerable<PerfilPermissao>? PerfilPermissoes { get; set; }
    }
}
