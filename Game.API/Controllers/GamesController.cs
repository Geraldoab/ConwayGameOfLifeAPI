using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Interfaces.Services;
using Game.Domain.Model;
using Game.Infra.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        /// Get a board state by Id
        /// </summary>
        /// <param name="id">The Board's Id</param>
        /// <returns>The selected board state</returns>
        [HttpGet("/boards/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBoardsById([FromRoute(Name = "id")] int id)
        {
            var response = _gameService.GetById(id);

            if (response.Success)
                return Ok(response);
            else
            {
                if (response.CurrentHttpStatusCode != null && response.CurrentHttpStatusCode.Value == HttpStatusCode.NotFound)
                    return NotFound(response);
                else
                    return BadRequest(response);
            }
        }

        /// <summary>
        /// Uploads a new board configuration
        /// </summary>
        /// <param name="request">The new board configuration</param>
        /// <returns>The new id of the uploaded board</returns>
        [HttpPost("/boards")]
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
                
                return response.Success ? Ok(response) : BadRequest(response);
            }
            else
                return BadRequest(CustomResult.Fail(result.Errors.Select(s=> s.ErrorMessage)));
        }

        /// <summary>
        /// Delete a board state by Id
        /// </summary>
        /// <param name="id">The Board's Id</param>
        /// <returns>The deleted board state</returns>
        [HttpDelete("/boards/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveBoardById([FromRoute(Name = "id")] int id)
        {
            var response = _gameService.RemoveById(id);

            if (response.Success)
                return Ok(response);
            else
            {
                if (response.CurrentHttpStatusCode != null && response.CurrentHttpStatusCode.Value == HttpStatusCode.NotFound)
                    return NotFound(response);
                else
                    return BadRequest(response);
            }
        }

        /// <summary>
        /// Generate the next board state
        /// </summary>
        /// <returns>The next board state</returns>
        [HttpGet("/boards/next")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNextState()
        {
            var response = _gameService.GetNextState();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Simulate the creation of X populations
        /// </summary>
        /// <param name="iterations">The number of iterations to simulate</param>
        /// <returns>Returns the simulations</returns>
        [HttpGet("/boards/simulate/{iterations}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Simulate(int iterations)
        {
            var response = _gameService.Simulate(iterations);
            
            if(response.Success)
                return Ok(response);
            else
            {
                if(response.CurrentHttpStatusCode != null && response.CurrentHttpStatusCode.Value == HttpStatusCode.NotFound)
                    return NotFound(response);
                else
                    return BadRequest(response);
            }
        }

        /// <summary>
        /// Get the final board state of the game
        /// </summary>
        /// <param name="iterations">The number of iterations to simulate</param>
        /// <returns>The final board state of the game</returns>
        [HttpGet("/boards/states/final/{iterations}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFinalState(int iterations)
        {
            var response = _gameService.GetFinalState(iterations);

            if (response.Success)
                return Ok(response);
            else
            {
                if (response.CurrentHttpStatusCode != null && response.CurrentHttpStatusCode.Value == HttpStatusCode.NotFound)
                    return NotFound(response);
                else
                    return BadRequest(response);
            }
        }
    }
}