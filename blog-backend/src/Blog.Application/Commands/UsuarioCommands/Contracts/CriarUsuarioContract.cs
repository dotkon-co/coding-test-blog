using Flunt.Validations;

namespace Blog.Application.Commands.UsuarioCommands.Contracts
{
    public class CriarUsuarioContract : Contract<CriarUsuarioCommand>
    {
        public CriarUsuarioContract(CriarUsuarioCommand command) =>
            Requires()
                .IsEmail(command.Email, "Email", "Email é obrigatório")
                .IsNotNullOrEmpty(command.Nome, "Nome", "Nome é obrigatória")
                .IsNotNullOrEmpty(command.Senha, "Senha", "Senha é obrigatória")
                .IsGreaterOrEqualsThan(command.Senha.Length, 6, "Senha", "Senha deve conter 6 ou mais caractéres");
    }
}
