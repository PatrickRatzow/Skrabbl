using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;
using Skrabbl.Model.Dto;

namespace Skrabbl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegistrationDto userDto)
        {
            var user = await _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _userService.GetUser(login.Username, login.Password);

            if (user == null)
                return Unauthorized();

            var userId = user.Id;

            var jwtToken = _jwtService.GenerateSecurityToken(user);
            var refreshToken = await _jwtService.GenerateRefreshToken(user);

            return Ok(new LoginResponseDto
            {
                Jwt = jwtToken,
                RefreshToken = refreshToken,
                UserId = userId
                // TODO: when jwt is implemented, this has to be removed
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto refreshDto)
        {
            var user = await _userService.GetUserByRefreshToken(refreshDto.Token);
            if (user == null)
                return NotFound();

            var refreshToken = await _jwtService.RefreshToken(user, refreshDto.Token);
            if (refreshToken == null)
                return NotFound();

            var jwt = _jwtService.GenerateSecurityToken(user);
            return Ok(new LoginResponseDto
            {
                Jwt = jwt,
                RefreshToken = refreshToken,
                UserId = user.Id
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshDto refreshDto)
        {
            await _jwtService.RemoveToken(refreshDto.Token);

            return Ok();
        }
    }
}