using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entidades.ControleAcesso
{
    public class PerfilPermissao : IdentityRoleClaim<Guid>
    {
        public Perfil Perfil { get; private set; }
    }
}
