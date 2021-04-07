using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Skrabbl.API.Hubs
{
    public class DrawingHub : Hub
    {
        public async Task SendNode(string color, int size, int x1, int y1, int x2, int y2)
        {
            await Clients.Others.SendAsync("ReceiveNode", color, size, x1, y1, x2, y2);
        }
    }
}