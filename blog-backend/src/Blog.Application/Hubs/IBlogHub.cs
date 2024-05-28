using Blog.Core.Mensagens.EventHubs;

namespace Blog.Application.Hubs
{
    public interface IBlogHub
    {
        Task PostagemAdicionadaEvent(PostagemAdicionadaEventHub eventHub);
        Task PostagemAlteradaEvent(PostagemAlteradaEventHub eventHub);
        Task PostagemExcluidaEvent(PostagemExcluidaEventHub eventHub);
    }
}
