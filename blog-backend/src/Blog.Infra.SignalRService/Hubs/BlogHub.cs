using Blog.Application.Hubs;
using Blog.Core.Mensagens.EventHubs;
using Microsoft.AspNetCore.SignalR;

namespace Blog.Infra.SignalRService.Hubs
{
    public class BlogHub : Hub, IBlogHub
    {
        private readonly IHubContext<BlogHub> _hubContext;

        public BlogHub(
            IHubContext<BlogHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task PostagemAdicionadaEvent(PostagemAdicionadaEventHub eventHub)
        {
            await _hubContext.Clients.All.SendAsync("PostagemAdicionadaEventHub", eventHub);
        }

        public async Task PostagemAlteradaEvent(PostagemAlteradaEventHub eventHub)
        {
            await _hubContext.Clients.All.SendAsync("PostagemAlteradaEventHub", eventHub);
        }

        public async Task PostagemExcluidaEvent(PostagemExcluidaEventHub eventHub)
        {
            await _hubContext.Clients.All.SendAsync("PostagemExcluidaEventHub", eventHub);
        }
    }
}
