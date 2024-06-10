using Blog.Core.EntidadeBase;
using Blog.Domain.Entidades.Postagem;
using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entidades.ControleAcesso
{
    public class Usuario : IdentityUser<Guid>, IAggregateRoot
    {
        protected Usuario() { }

        public Usuario(string nome, string email, string? telefone, Guid? perfilId)
        {
            Nome = nome;
            UserName = Email = email;
            PhoneNumber = telefone;
            AssociarPerfil(perfilId);
        }

        public string Nome { get; private set; }

        private readonly List<UsuarioPerfil> _usuarioPerfis = new();
        public IEnumerable<UsuarioPerfil> UsuarioPerfis => _usuarioPerfis;

        private readonly List<UsuarioPermissao> _usuarioPermissoes = new();
        public IEnumerable<UsuarioPermissao> UsuarioPermissoes => _usuarioPermissoes;

        private readonly List<Post> _usuarioPosts = new();
        public IEnumerable<Post> UsuarioPosts => _usuarioPosts;

        private void AssociarPerfil(Guid? perfilId)
        {
            if (!perfilId.HasValue || _usuarioPerfis.Count == 1) return;

            _usuarioPerfis.Add(new UsuarioPerfil(Id, (Guid)perfilId));
        }
    }
}
