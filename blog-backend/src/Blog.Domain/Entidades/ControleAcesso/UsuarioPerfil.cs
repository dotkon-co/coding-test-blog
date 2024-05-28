using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entidades.ControleAcesso
{
    public class UsuarioPerfil : IdentityUserRole<Guid>
    {
        public UsuarioPerfil() { }

        public UsuarioPerfil(Guid usuarioId, Guid perfilId)
        {
            UserId = usuarioId;
            RoleId = perfilId;
        }

        public Perfil Perfil { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}
