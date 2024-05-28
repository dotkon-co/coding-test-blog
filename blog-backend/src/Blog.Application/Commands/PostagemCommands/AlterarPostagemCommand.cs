using Blog.Application.Commands.PostagemCommands.Contracts;
using Blog.Core.Mensagens.Commands;
using Flunt.Notifications;

namespace Blog.Application.Commands.PostagemCommands
{
    public class AlterarPostagemCommand : Notifiable<Notification>, ICommand
    {
        public AlterarPostagemCommand(Guid postId, string titulo, string conteudo)
        {
            PostId = postId;
            Titulo = titulo;
            Conteudo = conteudo;

            AddNotifications(new AlterarPostagemContract(this));
        }

        public Guid PostId { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
    }
}
