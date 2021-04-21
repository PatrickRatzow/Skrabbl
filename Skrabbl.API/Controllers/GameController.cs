using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using Skrabbl.Model.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Skrabbl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // POST api/<GameLobbyController>/userId
        [HttpPost("{gameId}/round/{roundId}")]
        public async Task<IActionResult> Post(int gameId, int roundId, [FromBody] EndTurnDto endTurnDto)
        {
            var game = await _gameService.GetGame(gameId);
            return null;
        }


    }
}
