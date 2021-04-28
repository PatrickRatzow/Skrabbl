using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IGameRepository
    {
        Task<Game> AddGame();
        Task<Game> GetGame(int id);
        Task<Turn> GetCurrentTurn(int userId);
        Task<bool> HasUserGuessedWordForCurrentTurn(int userId);
        Task<bool> GoToNextRound(int gameId);
    }
}