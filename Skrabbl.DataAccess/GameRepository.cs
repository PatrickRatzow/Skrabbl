using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private readonly ICommandText _commandText;
        
        public GameRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task<Game> GetGame(int id)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Game>(_commandText.GetGameById, new { Id = id });
            });
        }

        public async Task AddGame()
        {
            //Needs to be implemented
        }
    }
}