using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
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
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IMemoryCache memoryCache;

        public WordController(IMemoryCache memoryCache, IWordService wordService)
        {
            _wordService = wordService;
            this.memoryCache = memoryCache;
        }

        // GET: api/<GameLobbyController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuessWord>>> Get()
        {
            try
            {
                var words = await _wordService.GetNewWords();
                List<GuessWord> wordList = words.ToList();

                return Ok(wordList);
            }
            catch
            {
                return StatusCode(500);
            };
        }
        // POST api/<GameLobbyController>/userId
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WordDto wordDto)
        {
            await _wordService.UsedWords(wordDto.Word);
            return Ok();

        }


    }
}
