using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class GameService : IGameService
    {
        IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> GetGame(int id)
        {
            return await _gameRepository.GetGame(id);
        }

        public async Task<bool> DidUserGuessWord(int userId, string message)
        {
            var turn = await _gameRepository.GetCurrentTurn(userId);

            return turn?.Word == message;
        }

        public async Task<bool> HasUserGuessedWord(int userId)
        {
            return await _gameRepository.HasUserGuessedWordForCurrentTurn(userId);
        }

        public void EndTurn()
        {
        }
    }
}