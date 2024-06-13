using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BlogSimples.Web.Service
{
    public class NotificationService
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();

        public void AddSocket(WebSocket socket)
        {
            _sockets.Add(socket);
        }

        public async Task NotifyAllAsync(string message)
        {
            var tasks = _sockets.Select(async socket =>
            {
                if (socket.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
