using Flunt.Validations;

namespace Blog.Application.Commands.PostagemCommands.Contracts
{
    public class PostarConteudoContract : Contract<PostarConteudoCommand>
    {
        public PostarConteudoContract(PostarConteudoCommand command) =>
            Requires()
                .IsNotNullOrEmpty(command.Titulo, "Titulo", "Titulo é obrigatório")
                .IsNotNullOrEmpty(command.Conteudo, "Conteudo", "Conteudo é obrigatório")           
                .IsLowerOrEqualsThan(command.Titulo.Length, 50, "Titulo", "Titulo deve ter no máximo 50 caractéres")
                .IsLowerOrEqualsThan(command.Conteudo.Length, 300, "Conteudo", "Conteudo deve ter no máximo 300 caractéres");
    }
}
