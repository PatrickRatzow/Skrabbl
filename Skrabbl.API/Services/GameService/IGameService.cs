using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public interface IGameService
    {
        Task<Game> GetGame(int id);
    }
}