using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Skrabbl.API.Hubs;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IHubContext<GameHub, IGameHub> _gameHub;

        public GameService(IGameRepository gameRepository, IHubContext<GameHub, IGameHub> gameHub)
        {
            _gameRepository = gameRepository;
            _gameHub = gameHub;
        }

        public async Task<Game> GetGame(int id)
        {
            return await _gameRepository.GetGame(id);
        }

        public async Task StartNextRound(int gameId)
        {
            var updatedRound = await _gameRepository.GoToNextRound(gameId);
            // Game is over, inform users & exit.
            if (!updatedRound)
            {
                await _gameHub.Clients.All.SendGameIsOver();

                return;
            }

            // Send round status overview
            // TODO: Add actual data
            await _gameHub.Clients.All.SendRoundStatus();
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
    }
}