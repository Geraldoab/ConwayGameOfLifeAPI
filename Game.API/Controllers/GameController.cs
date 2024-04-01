using Game.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("Start")]
        public IActionResult Start()
        {
            _gameService.Start();
            return Ok();
        }
    }
}
