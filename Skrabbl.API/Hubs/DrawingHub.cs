using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        public async Task SendDrawNode(string color, int size, int x1, int y1, int x2, int y2)
        {
            await Clients.Others.ReceiveDrawNode(color, size, x1, y1, x2, y2);
        }
    }
}