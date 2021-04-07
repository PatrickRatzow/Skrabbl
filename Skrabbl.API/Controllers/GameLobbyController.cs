using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Skrabbl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameLobbyController : ControllerBase
    {

        private readonly IGameLobbyService _gameLobbyService;

        public GameLobbyController(IGameLobbyService gameLobbyService)
        {
            _gameLobbyService = gameLobbyService;
        }

        // GET: api/<GameLobbyController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameLobby>>> Get()
        {
            try
            {
                var lobbies = await _gameLobbyService.GetAllGameLobbies();
                return Ok(lobbies);
            }
            catch
            {
                return NotFound();
            };
        }

        // GET api/<GameLobbyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameLobby>> Get(string id)
        {
            try
            {
                var lobby = await _gameLobbyService.GetGameLobbyById(id);
                return Ok(lobby);
            } catch
            {
                return NotFound();
            };
        }

        // POST api/<GameLobbyController>/userId
        [HttpPost("{userId}")]
        public async Task<ActionResult<GameLobby>> Post(int userId)
        {
            try
            {
                //Created($"api/resource/{object.ID}", object);
                var gameLobby = await _gameLobbyService.AddGameLobby(userId);
                return Created($"api/gamelobby/{gameLobby.GameCode}", gameLobby);
            } catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<GameLobbyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _gameLobbyService.RemoveGameLobby(id);
                return Ok();
            } catch
            {
                return NotFound();
            }
        }
    }
}
