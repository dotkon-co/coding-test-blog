using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entidades.ControleAcesso
{
    public class UsuarioPermissao : IdentityUserClaim<Guid>
    {
        public Usuario Usuario { get; private set; }
    }
}
