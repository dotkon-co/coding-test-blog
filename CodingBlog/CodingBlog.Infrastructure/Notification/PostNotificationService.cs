using Microsoft.AspNetCore.SignalR;

namespace CodingBlog.Infrastructure.Notification;

public interface IPostNotificationService
{
    Task SendPostNotificationAsync(string message);
}

public class PostNotificationService : IPostNotificationService
{
    private readonly IHubContext<PostHub> _hubContext;

    public PostNotificationService(IHubContext<PostHub> hubContext)
    {
        _hubContext = hubContext;
        //
        // Task.Run(async () =>
        // {
        //     while (true)
        //     {
        //         await Task.Delay(TimeSpan.FromSeconds(5)); // A cada 10 segundos
        //         await SendPostNotificationAsync("Ping do servidor");
        //     }
        // });
    }

    public async Task SendPostNotificationAsync(string message)
        => await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
}