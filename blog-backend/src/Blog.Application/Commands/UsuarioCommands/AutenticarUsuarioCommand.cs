using Blog.Application.Commands.UsuarioCommands.Contracts;
using Blog.Core.Mensagens.Commands;
using Flunt.Notifications;

namespace Blog.Application.Commands.UsuarioCommands
{
    public class AutenticarUsuarioCommand : Notifiable<Notification>, ICommand
    {
        public AutenticarUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;

            AddNotifications(new AutenticarUsuarioContract(this));
        }

        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}
