using Flunt.Validations;

namespace Blog.Application.Commands.UsuarioCommands.Contracts
{
    public class AutenticarUsuarioContract : Contract<AutenticarUsuarioCommand>
    {
        public AutenticarUsuarioContract(AutenticarUsuarioCommand command) =>
            Requires()
                .IsEmail(command.Email, "Email", "Email é obrigatório")
                .IsNotNullOrEmpty(command.Senha, "Senha", "Senha é obrigatória")
                .IsGreaterOrEqualsThan(command.Senha.Length, 6, "Senha", "Senha deve ter no mínimo 6 dígitos");
    }
}
