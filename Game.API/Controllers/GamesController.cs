using AutoMapper;
using Game.API.Controllers;
using Game.Application.Contracts;
using Game.Domain.Entities;
using Game.Domain.Interfaces.Services;
using Game.Infra.Data.Core;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GamesController : CustomResultController
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
        [HttpGet("/boards/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetBoardsById([FromRoute(Name = "id")] Guid id, CancellationToken cancellationToken)
        {
            var response = await _gameService.GetByIdAsync(id, cancellationToken);
            return GenerateResponse(response);
        }

        /// <summary>
        /// Upload a new board configuration
        /// </summary>
        /// <param name="request">The new board configuration</param>
        /// <returns>The new id of the uploaded board</returns>
        [HttpPost("/boards")]
        [ProducesDefaultResponseType(typeof(BoardStatePostResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload([FromBody] BoardStatePostRequest request, CancellationToken cancellationToken)
        {
            var result = request.Validate();

            if (result.IsValid)
            {
                var board = _mapper.Map<BoardState>(request.Board);
                var response = await _gameService.UploadAsync(board.Grid, cancellationToken);
                
                return GenerateResponse(response);
            }
            else
                return BadRequest(CustomResult.Fail(result.Errors.Select(s=> s.ErrorMessage)));
        }

        /// <summary>
        /// Delete a board state by Id
        /// </summary>
        /// <param name="id">The Board's Id</param>
        /// <returns>The deleted board state</returns>
        [HttpDelete("/boards/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveBoardById([FromRoute(Name = "id")] Guid id, CancellationToken cancellationToken)
        {
            var response = await _gameService.RemoveByIdAsync(id, cancellationToken);
            return GenerateResponse(response);
        }

        /// <summary>
        /// Generate the next board state
        /// </summary>
        /// <returns>The next board state</returns>
        [HttpGet("/boards/states-next")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNextState(CancellationToken cancellationToken)
        {
            var response = await _gameService.GetNextStateAsync(cancellationToken);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Simulate the creation of N populations. 
        /// </summary>
        /// <param name="iterations">The number of iterations to simulate</param>
        /// <returns>Returns the simulations</returns>
        [HttpGet("/boards/states-away/{iterations:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Simulate(int iterations, CancellationToken cancellationToken)
        {
            var response = await _gameService.SimulateAsync(iterations, cancellationToken);
            return GenerateResponse(response);
        }

        /// <summary>
        /// Get the final board state of the game
        /// </summary>
        /// <param name="iterations">The number of iterations to simulate</param>
        /// <returns>The final board state of the game</returns>
        [HttpGet("/boards/states-final/{iterations:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFinalState(int iterations, CancellationToken cancellationToken)
        {
            var response = await _gameService.GetFinalStateAsync(iterations, cancellationToken);
            return GenerateResponse(response);
        }
    }
}
