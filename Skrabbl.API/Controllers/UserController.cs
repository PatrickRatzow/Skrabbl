using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.Model;
using Skrabbl.API.Services;
using Skrabbl.DataAccess;
using Skrabbl.Model.Dto;
using Microsoft.Extensions.Configuration;

namespace Skrabbl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGameLobbyService _gameLobbyService;
        private IConfiguration _config;

        public UserController(IUserService userService, IGameLobbyService gameLobbyService, IConfiguration config)
        {
            _userService = userService;
            _gameLobbyService = gameLobbyService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegistrationDto userDto)
        {
            try
            {
                User user = await _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email);
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                User user = await _userService.GetUser(login.Username, login.Password);
                var jwt = new JwtService(_config);
                var token = jwt.GenerateSecurityToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody] JoinLobbyDto join)
        {
            try
            {
                User user = await _userService.GetUser(join.UserId);

                GameLobby gameLobby = await _gameLobbyService.GetGameLobbyById(join.LobbyCode);

                if (user == null || user.GameLobbyId != null || gameLobby == null)
                    return BadRequest();

                //Go to database and change the players connected to lobby + player connected lobby
                await _userService.AddToLobby(user.Id, gameLobby.GameCode);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
