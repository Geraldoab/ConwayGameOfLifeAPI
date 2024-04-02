using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Interfaces.Services;
using Game.Domain.Model;
using Game.Infra.Data.Core;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Uploads a new board configuration
        /// </summary>
        /// <param name="request">The new board configuration</param>
        /// <returns>The new id of the uploaded board</returns>
        [HttpPost("/boards/upload")]
        [ProducesDefaultResponseType(typeof(BoardStatePostResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Upload([FromBody] BoardStatePostRequest request)
        {
            var result = request.Validate();

            if (result.IsValid)
            {
                var board = _mapper.Map<BoardState>(request.Board);
                var response = _gameService.Upload(board.Grid);
                return Ok(response);
            }
            else
                return BadRequest(CustomResult.Fail(result.Errors.Select(s=> s.ErrorMessage)));
        }
    }
}
