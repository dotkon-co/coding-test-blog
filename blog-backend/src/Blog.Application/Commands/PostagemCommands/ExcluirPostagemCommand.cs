using Blog.Application.Commands.PostagemCommands.Contracts;
using Blog.Core.Mensagens.Commands;
using Flunt.Notifications;

namespace Blog.Application.Commands.PostagemCommands
{
    public class ExcluirPostagemCommand : Notifiable<Notification>, ICommand
    {
        public ExcluirPostagemCommand(Guid postId)
        {
            PostId = postId;

            AddNotifications(new ExcluirPostagemContract(this));
        }

        public Guid PostId { get; private set; }
    }
}
