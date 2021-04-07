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

namespace Skrabbl.API.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
      public async Task<IActionResult> Login([FromBody] LoginDto login )
        {
            try
            {
                User user = await _userService.GetUser(login.Username, login.Password);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }

    }
}
