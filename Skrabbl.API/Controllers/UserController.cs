using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

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
    }
}