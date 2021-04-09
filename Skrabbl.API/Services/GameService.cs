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
    }
}