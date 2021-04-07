using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;
using Skrabbl.Model;
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
                return StatusCode(500);
            };
        }

        // GET api/<GameLobbyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameLobby>> Get(string id)
        {
            var lobby = await _gameLobbyService.GetGameLobbyById(id);
            
            if(lobby != null)
            {
                return Ok(lobby);
            } else
            {
                return NotFound();
            }

        }
        // GET /api/gamelobby/user/25
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<GameLobby>> Get(int userId)
        {
            var lobby = await _gameLobbyService.GetLobbyByOwnerId(userId);
            
            if (lobby != null)
            {
                return Ok(lobby);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<GameLobbyController>/userId
        [HttpPost("{userId}")]
        public async Task<ActionResult<GameLobby>> Post(int userId)
        {
                try
                {
                    var gameLobby = await _gameLobbyService.AddGameLobby(userId);
                    return Created($"api/gamelobby/{gameLobby.GameCode}", gameLobby);
                } 
                catch(UserAlreadyHaveALobbyException e)
                {
                    return Conflict();    
                }
                catch
                {
                    return BadRequest();
                }
        }

        // DELETE api/<GameLobbyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var lobby = await _gameLobbyService.RemoveGameLobby(id);
            if (lobby == true)
            {
                return Ok();
            } else
            {
                return NotFound();
            }

        }
    }
}
