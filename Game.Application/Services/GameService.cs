using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Core;
using Game.Domain.Interfaces.Services;
using Game.Domain.Model;
using Game.Infra.Data.Core;

namespace Game.Application.Services
{
    internal class GameService : IGameService
    {
        private readonly IMapper _mapper;
        public GameService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CustomResult Start(Board board)
        {
            BaseGameOfLife _newGame = new GameOfLife();

            var currentBoard = _newGame.CurrentBoardGeneration;

            return CustomResult.Ok(new BoardStatePostResponse());
        }
    }
}
