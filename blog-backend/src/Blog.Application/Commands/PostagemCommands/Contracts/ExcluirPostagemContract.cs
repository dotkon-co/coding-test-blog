using Flunt.Validations;

namespace Blog.Application.Commands.PostagemCommands.Contracts
{
    public class ExcluirPostagemContract : Contract<ExcluirPostagemCommand>
    {
        public ExcluirPostagemContract(ExcluirPostagemCommand command) =>
            Requires()
                .IsNotEmpty(command.PostId, "PostId", "PostId é obrigatório");
    }
}
