using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Skrabbl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameLobbyController : ControllerBase
    {
        private readonly IGameLobbyService _gameLobbyService;
        private readonly IUserService _userService;

        public GameLobbyController(IGameLobbyService gameLobbyService, IUserService userService)
        {
            _gameLobbyService = gameLobbyService;
            _userService = userService;
        }

        /*
        // GET: api/<GameLobbyController>
        [HttpGet]
        [Authorize]
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
            }

            ;
        }

        // GET api/<GameLobbyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameLobby>> Get(string id)
        {
            var lobby = await _gameLobbyService.GetGameLobbyById(id);

            if (lobby != null)
            {
                return Ok(lobby);
            }
            else
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
            catch (UserAlreadyHaveALobbyException e)
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
            if (lobby)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        */

        [HttpPost("join/{lobbyCode}")]
        [Authorize]
        public async Task<IActionResult> Join(string lobbyCode)
        {
            var claimUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if (claimUserId == null)
                return Unauthorized();

            var userId = int.Parse(claimUserId);
            var user = _userService.GetUser(userId);
            var gameLobby = _gameLobbyService.GetGameLobbyById(lobbyCode);

            await Task.WhenAll(user, gameLobby);

            if (gameLobby.Result == null)
                return NotFound();

            if (user.Result == null || user.Result.GameLobbyId != null)
                return Forbid();

            //Go to database and change the players connected to lobby + player connected lobby
            await _userService.AddToLobby(user.Result.Id, gameLobby.Result.GameCode);

            return Ok(gameLobby.Result);
        }
    }
}