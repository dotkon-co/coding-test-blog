using Flunt.Validations;

namespace Blog.Application.Commands.PostagemCommands.Contracts
{
    public class AlterarPostagemContract : Contract<AlterarPostagemCommand>
    {
        public AlterarPostagemContract(AlterarPostagemCommand command) =>
            Requires()
                .IsNotEmpty(command.PostId, "PostId", "PostId é obrigatório")
                .IsNotNullOrEmpty(command.Titulo, "Titulo", "Titulo é obrigatório")
                .IsNotNullOrEmpty(command.Conteudo, "Conteudo", "Conteudo é obrigatório")
                .IsLowerOrEqualsThan(command.Titulo.Length, 50, "Titulo", "Titulo deve ter no máximo 50 caractéres")
                .IsLowerOrEqualsThan(command.Conteudo.Length, 300, "Conteudo", "Conteudo deve ter no máximo 300 caractéres");
    }
}
