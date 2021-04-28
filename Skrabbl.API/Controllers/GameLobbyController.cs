﻿using System.Linq;
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

        [HttpPost("create/{userId}")]
        //[Authorize]
        public async Task<IActionResult> Create(int userId)
        {
            var user = _userService.GetUser(userId);
            var gameLobby = _gameLobbyService.AddGameLobby(userId);

            return Ok();
        }
    }
}