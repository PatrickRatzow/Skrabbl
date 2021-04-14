using Microsoft.AspNetCore.Mvc;
using Skrabbl.API.Services;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.Model.Dto;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Skrabbl.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService userService)
        {
            _messageService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto messageDto)
        {
            try
            {
                ChatMessage msg = await _messageService.CreateMessage(messageDto.Message, messageDto.GameId, messageDto.UserId);
                return Ok(msg);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
