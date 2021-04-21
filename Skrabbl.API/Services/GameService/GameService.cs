using System;
using System.Threading.Tasks;
using System.Timers;
using Skrabbl.API.Services.TimerService;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class GameService : IGameService
    {
        IGameRepository _gameRepository;
        TurnTimerService turnTimerService;

        public GameService(IGameRepository gameRepository, TurnTimerService turn)
        {
            _gameRepository = gameRepository;
            turnTimerService = turn;
        }

        public async Task<Game> GetGame(int id)
        {
            return await _gameRepository.GetGame(id);
        }

        public void EndTurn()
        {

        }

        public void CreateTimer(int gameId, int time)
        {
            //For now this timer is set to 60 seconds.
            //TurnTimer timer = new TurnTimer(60000);
            turnTimerService.CreateTimer(gameId, time);
        }

        public void StartTimer(int gameId)
        {
            turnTimerService.StartTimer(gameId);
        }

        public void StopTimer(int gameId)
        {
            turnTimerService.StopTimer(gameId);
        }
    }
}