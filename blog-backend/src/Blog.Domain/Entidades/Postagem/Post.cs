using Blog.Core.EntidadeBase;
using Blog.Domain.Entidades.ControleAcesso;

namespace Blog.Domain.Entidades.Postagem
{
    public class Post : EntidadeBase, IAggregateRoot
    {
        public Post(Guid autorId, string titulo, string conteudo)
        {
            PostarConteudo(autorId, titulo, conteudo);
        }

        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public Guid AutorId { get; private set; }
        public Usuario Autor { get; private set; }

        private void PostarConteudo(Guid autorId, string titulo, string conteudo)
        {
            AutorId = autorId;
            Titulo = titulo;
            Conteudo = conteudo;
            DataCriacao = DateTime.Now;
        }

        public void AlterarPostagem(string titulo, string conteudo)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            DataAtualizacao = DateTime.Now;
        }
    }
}
