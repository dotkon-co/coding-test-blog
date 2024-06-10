namespace Blog.Core.Mensagens.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<CommandResult> Handler(T command);
    }
}
