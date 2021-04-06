using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public class GameLobbyRepository : BaseRepository, IGameLobbyRepository
    {
        private readonly ICommandText _commandText;

        public GameLobbyRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public ValueTask<GameLobby> GetGameLobbyById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task AddGameLobby(GameLobby entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
