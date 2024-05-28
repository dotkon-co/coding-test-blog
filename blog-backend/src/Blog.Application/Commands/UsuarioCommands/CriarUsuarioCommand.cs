using Blog.Application.Commands.UsuarioCommands.Contracts;
using Blog.Core.Mensagens.Commands;
using Flunt.Notifications;

namespace Blog.Application.Commands.UsuarioCommands
{
    public class CriarUsuarioCommand : Notifiable<Notification>, ICommand
    {
        public CriarUsuarioCommand(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            AddNotifications(new CriarUsuarioContract(this));
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}
