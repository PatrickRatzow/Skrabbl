using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Skrabbl.Model.Dto;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        public async Task SendDrawNode(CommandDto commandDto)
        {
            await Clients.All.ReceiveDrawNode(commandDto);
        }
    }
}