using AutoMapper;
using Game.Application.Contracts;
using Game.Application.Contracts.Core;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Interfaces.Services;
using Game.Domain.Entities;
using Game.Domain.Resources;
using Game.Infra.Data.Core;
using System.Net;

namespace Game.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public CustomResult GetById(int id)
        {
            if (id < 1)
                return CustomResult.Fail($"{Messages.INVALID_BOARD_ID} {id}");

            var result = _gameRepository.GetById(id);
            if (result == null)
                return CustomResult.Fail($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public CustomResult GetFinalState(int iterations)
        {
            if (iterations < 1)
                return CustomResult.Fail(Messages.INVALID_ITERATIONS);

            var result = _gameRepository.GetFinalState(iterations);
            if (result == null)
                return CustomResult.Fail(Messages.INVALID_FINAL_STATE);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public CustomResult GetNextState()
        {
            var result = _gameRepository.GetNextState();
            if (result == null)
                return CustomResult.Fail(Messages.INVALID_NEXT_STATE);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public CustomResult RemoveById(int id)
        {
            if (id < 1)
                return CustomResult.Fail($"{Messages.INVALID_BOARD_ID} {id}");

            var result = _gameRepository.RemoveById(id);
            if (result == null)
                return CustomResult.Fail($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public CustomResult Simulate(int iterations)
        {
            if (iterations < 1)
                return CustomResult.Fail(Messages.INVALID_ITERATIONS);

            var result = _gameRepository.Simulate(iterations);
            if (result == null)
                return CustomResult.Fail($"{Messages.CURRRENT_BOARD_STATE_NOT_FOUND}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<GridResponse>(result));
        }

        public CustomResult Upload(Grid grid)
        {
            if (grid == null || !grid.Validate().IsValid)
                return CustomResult.Fail(Messages.INVALID_BOARD_GRID);

            var result = _gameRepository.Upload(grid);
            return CustomResult.Ok(result);
        }
    }
}
