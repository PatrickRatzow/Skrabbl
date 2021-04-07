using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.Model;
using Skrabbl.API.Services;
using Skrabbl.DataAccess;

namespace Skrabbl.API.Controllers
{
      [ApiController]
      [Route("Api/[Controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
      public async Task<IActionResult> PostUser(string userName, string password, string email)
        {

            try
            {
               User user = await _userService.CreateUser(userName, password, email);
               return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
            
        }

    }
}
