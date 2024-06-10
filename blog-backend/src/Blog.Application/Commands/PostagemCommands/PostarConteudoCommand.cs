using Blog.Application.Commands.PostagemCommands.Contracts;
using Blog.Core.Mensagens.Commands;
using Flunt.Notifications;

namespace Blog.Application.Commands.PostagemCommands
{
    public class PostarConteudoCommand : Notifiable<Notification>, ICommand
    {
        public PostarConteudoCommand(string titulo, string conteudo)
        {
            Titulo = titulo;
            Conteudo = conteudo;

            AddNotifications(new PostarConteudoContract(this));
        }

        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
    }
}
