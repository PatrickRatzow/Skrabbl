using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IGameRepository
    {
        Task<Game> GetGame(int id);
    }
}