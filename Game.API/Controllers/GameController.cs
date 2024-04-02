using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Interfaces.Services;
using Game.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new board configuration
        /// </summary>
        /// <param name="request">The new board configuration</param>
        /// <returns>A board response</returns>
        [HttpPost("Start")]
        [ProducesDefaultResponseType(typeof(BoardStatePostResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Start([FromBody] BoardStatePostRequest request)
        {
            var result = request.Validate();

            if (result.IsValid)
            {
                var response = _gameService.Start(_mapper.Map<Board>(request));
                return Ok(response);
            }
            else
                return BadRequest(result);
        }
    }
}
