namespace Blog.Core.Mensagens.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dado { get; set; }

        public CommandResult(bool sucesso, string mensagem, object dado)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dado = dado;
        }
    }
}
